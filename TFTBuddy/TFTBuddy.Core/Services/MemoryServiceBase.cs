using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using TFTBuddy.Common;
using TFTBuddy.Logging;

namespace TFTBuddy.Core
{
    public abstract class MemoryServiceBase
    {
        #region Fields..
        private readonly IApplicationLogger _applicationLogger;

        private List<PointerTrace> _uniquePointerTraces = new List<PointerTrace>();
        #endregion Fields..

        #region Properties..
        private WindowsApi.RegionPageProtection[] ProtectionExclusions
        {
            get
            {
                return new WindowsApi.RegionPageProtection[]
                {
                    WindowsApi.RegionPageProtection.PAGE_GUARD,
                    WindowsApi.RegionPageProtection.PAGE_NOACCESS
                };
            }
        }
        #endregion Properties..

        #region Constructors..
        public MemoryServiceBase(IApplicationLogger applicationLogger)
        {
            _applicationLogger = applicationLogger;
        }
        #endregion Constructors..

        #region Methods..
        private bool CheckProtection(uint flags)
        {
            var protectionFlags = (WindowsApi.RegionPageProtection)flags;
            foreach (var protectionExclusion in ProtectionExclusions)
            {
                if (protectionFlags.HasFlag(protectionExclusion))
                    return false;
            }

            return true;
        }

        public List<AddressRange> DivideAddressRange(AddressRange addressRange, int divisionCount)
        {
            List<AddressRange> addressRangeDivisions = new List<AddressRange>();

            ulong start = addressRange.Start;
            ulong end = addressRange.End;

            ulong range = end - start;
            float rangePerDivision = range / (float)divisionCount;

            end = start + (ulong)Math.Ceiling(rangePerDivision);

            for (int index = 0; index < divisionCount; ++index)
            {
                ulong startAddress = start;
                ulong endAddress = end;

                if (index + 1 == divisionCount)
                    endAddress = addressRange.End;

                addressRangeDivisions.Add(new AddressRange(startAddress, endAddress - startAddress));

                start = end;
                end += (ulong)Math.Floor(rangePerDivision);
            }

            return addressRangeDivisions;
        }

        /// <summary>
        /// KMP algorithm modified to search bytes with a nullable/wildcard search
        /// </summary>
        public List<ulong> FindPatternAddresses(Process process, BytePattern pattern, bool stopAfterFirst)
        {
            List<ulong> matchAddresses = new List<ulong>();

            AddressRange addressRange = default;

            // Get address range
            // Use cached address if possible
            if (!string.IsNullOrEmpty(pattern.LastResultAddress))
            {
                long hexAddress = pattern.LastResultAddress.TryParseHex();
                addressRange = new AddressRange((ulong)hexAddress, (ulong)pattern.Bytes.Length);
            }
            else
                addressRange = new AddressRange((ulong)process.MainModule.BaseAddress.ToInt64(), (ulong)process.MainModule.ModuleMemorySize);

            _applicationLogger.Log($"Base: 0x{addressRange.Start.ToString("X")}, End: 0x{addressRange.End.ToString("X")}, Size: 0x{addressRange.Size.ToString("X")}");

            ulong currentAddress = addressRange.Start;
            while (currentAddress < addressRange.End && !process.HasExited)
            {
                WindowsApi.MEMORY_BASIC_INFORMATION64 memoryRegion;
                int virtualQueryResultCode = WindowsApi.VirtualQueryEx(process.Handle, (IntPtr)currentAddress, out memoryRegion, (uint)Marshal.SizeOf(typeof(WindowsApi.MEMORY_BASIC_INFORMATION64)));

                if (virtualQueryResultCode > 0
                    && memoryRegion.RegionSize > 0
                    && memoryRegion.State == (uint)WindowsApi.RegionPageState.MEM_COMMIT
                    && CheckProtection(memoryRegion.Protect))
                {
                    var regionStartAddress = memoryRegion.BaseAddress;
                    if (addressRange.Start > regionStartAddress)
                        regionStartAddress = addressRange.Start;

                    var regionEndAddress = memoryRegion.BaseAddress + memoryRegion.RegionSize;
                    if (addressRange.End < regionEndAddress)
                        regionEndAddress = addressRange.End;
                    if (regionEndAddress <= regionStartAddress)
                        regionEndAddress = regionStartAddress + addressRange.Size;

                    ulong regionBytesToRead = regionEndAddress - regionStartAddress;
                    byte[] regionBytes = new byte[regionBytesToRead];

                    if (process.HasExited)
                        break;

                    int lpNumberOfBytesRead = 0;
                    WindowsApi.ReadProcessMemory(process.Handle, (IntPtr)regionStartAddress, regionBytes, regionBytes.Length, out lpNumberOfBytesRead);

                    var matchIndices = FindPatternMatchIndices(regionBytes, pattern, stopAfterFirst);
                    foreach (var matchIndex in matchIndices)
                    {
                        var matchAddress = regionStartAddress + (ulong)matchIndex;
                        matchAddresses.Add(matchAddress);

                        pattern.LastResultAddress = matchAddress.ToString("X8");

                        // TODO: Rimetti
                        _applicationLogger.Log($"Found '{pattern.Name}' at address 0x{matchAddress.ToString("X8")}");
                    }

                    if (matchAddresses.Any() && stopAfterFirst)
                        break;
                }

                currentAddress = memoryRegion.BaseAddress + memoryRegion.RegionSize;
            }

            return matchAddresses;
        }

        private List<int> FindPatternMatchIndices(byte[] bytes, BytePattern pattern, bool stopAfterFirst)
        {
            List<int> matchedIndices = new List<int>();

            int textLength = bytes.Length;
            int patternLength = pattern.Bytes.Length;

            if (textLength == 0 || patternLength == 0 || textLength < patternLength)
                return matchedIndices;

            int[] longestPrefixSuffices = new int[patternLength];

            GetLongestPrefixSuffices(pattern, ref longestPrefixSuffices);

            int textIndex = 0;
            int patternIndex = 0;

            while (textIndex < textLength)
            {
                // Ignore compare if the pattern index is nullable - this treats it like a * wildcard
                if (!pattern.Bytes[patternIndex].HasValue
                    || bytes[textIndex] == pattern.Bytes[patternIndex])
                {
                    textIndex++;
                    patternIndex++;
                }

                if (patternIndex == patternLength)
                {
                    matchedIndices.Add(textIndex - patternIndex);
                    patternIndex = longestPrefixSuffices[patternIndex - 1];

                    if (stopAfterFirst)
                        break;
                }
                else if (textIndex < textLength
                         && (pattern.Bytes[patternIndex].HasValue // Only compare disparity if the pattern byte isn't a null wildcard
                         && bytes[textIndex] != pattern.Bytes[patternIndex]))
                {
                    if (patternIndex != 0)
                        patternIndex = longestPrefixSuffices[patternIndex - 1];
                    else
                        textIndex++;
                }
            }

            return matchedIndices;
        }

        private void GetLongestPrefixSuffices(BytePattern pattern, ref int[] longestPrefixSuffices)
        {
            int patternLength = pattern.Bytes.Length;
            int length = 0;
            int patternIndex = 1;

            longestPrefixSuffices[0] = 0;

            while (patternIndex < patternLength)
            {
                if (pattern.Bytes[patternIndex] == pattern.Bytes[length])
                {
                    length++;
                    longestPrefixSuffices[patternIndex] = length;
                    patternIndex++;
                }
                else
                {
                    if (length == 0)
                    {
                        longestPrefixSuffices[patternIndex] = 0;
                        patternIndex++;
                    }
                    else
                        length = longestPrefixSuffices[length - 1];
                }
            }
        }

        public ulong LoadEffectiveAddressRelative(Process process, ulong address)
        {
            const uint opcodeLength = 3;
            const uint paramLength = 4;
            const uint instructionLength = opcodeLength + paramLength;

            uint operand = Read<uint>(process, address + opcodeLength);
            ulong operand64 = operand;

            // 64 bit relative addressing 
            if (operand64 > Int32.MaxValue)
                operand64 = 0xffffffff00000000 | operand64;

            return address + operand64 + instructionLength;
        }

        public T Read<T>(Process process, ulong address) where T : struct
        {
            T result = default(T);

            byte[] bytes = new byte[Marshal.SizeOf(typeof(T))];
            int lpNumberOfBytesRead = 0;

            WindowsApi.ReadProcessMemory(process.Handle, (IntPtr)address, bytes, bytes.Length, out lpNumberOfBytesRead);

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        public ulong ReadMultiLevelPointer(bool traceUniquePointers, Process process, ulong address, params long[] offsets)
        {
            PointerTrace trace = new PointerTrace();

            ulong result = address;
            foreach (var offset in offsets)
            {
                var readResult = Read<ulong>(process, address);
                result = (ulong)((long)readResult + offset);

                trace.Levels.Add(new PointerTraceLevel(address, readResult, offset, result));

                address = result;
            }

            if (traceUniquePointers && !_uniquePointerTraces.Contains(trace))
                _uniquePointerTraces.Add(trace);

            return result;
        }

        public uint ReadStaticOffset(Process process, ulong address)
        {
            uint opcodeLength = 3;
            return Read<uint>(process, address + opcodeLength);
        }

        public string ReadString(Process process, ulong address, uint length)
        {
            string result = null;

            byte[] bytes = new byte[length];
            int lpNumberOfBytesRead = 0;

            WindowsApi.ReadProcessMemory(process.Handle, (IntPtr)address, bytes, bytes.Length, out lpNumberOfBytesRead);

            int nullTerminatorIndex = Array.FindIndex(bytes, (byte b) => b == 0);
            if (nullTerminatorIndex >= 0)
            {
                Array.Resize(ref bytes, nullTerminatorIndex);
                result = Encoding.UTF8.GetString(bytes);
            }

            return result;
        }
        #endregion Methods..
    }
}
