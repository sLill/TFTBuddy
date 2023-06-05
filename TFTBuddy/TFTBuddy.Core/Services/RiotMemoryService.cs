using System.Diagnostics;
using System.Globalization;
using TFTBuddy.Common;
using TFTBuddy.Data;
using TFTBuddy.Logging;

namespace TFTBuddy.Core
{
    public class RiotMemoryService : IRiotMemoryService
    {
        #region Fields..
        private const string PROCESS_NAME = "LeagueOfLegends.exe";

        private readonly IApplicationLogger _applicationLogger;

        private Process _targetProcess;
        
        private CancellationTokenSource _cancellationTokenSource;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotMemoryService(IApplicationLogger applicationLogger)
        {
            _applicationLogger = applicationLogger;        
        }
        #endregion Constructors..

        #region Methods..
        /// <inheritdoc/>
        public void StartService(int interval)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            // Fire and forget
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    GetAndPublishData();
                    await Task.Delay(interval);
                }
            });
        }

        public void StopService()
        {
            _cancellationTokenSource?.Cancel();
        }

        private List<byte[]> GetBytePatterns()
        {
            var bytePatternStrings = EnumHelper.GetValues<BytePatterns>().ToList().Select(x => (string)x.GetCustomAttribute<ValueAttribute>().Value)?.ToList();
            var bytePatterns = bytePatternStrings?.Select(x => 
            {
                string[] byteStrings = x.Split(" ");
                byte[] result = new byte[byteStrings.Length];

                for (int i = 0; i < byteStrings.Length; i++) 
                {
                    if (byteStrings[i] == "??")
                        result[i] = 0x00;
                    else
                        result[i] = byte.Parse(byteStrings[i], NumberStyles.HexNumber);
                }

                return result;
            }).ToList();

            return bytePatterns;
        }

        public void GetAndPublishData()
        {
            bool processFound = CheckSetTargetProcess();
            if (processFound)
            {
                // Loop through and find pointers for each byte pattern
                var bytePatterns = GetBytePatterns();
                if (bytePatterns.Any())
                {
                    bytePatterns.ForEach(x =>
                    {
                        var patternPointer = SearchForPointerFromPattern(x);
                        
                        // TODO
                    });
                }
            }
        }

        private bool CheckSetTargetProcess()
        {
            if (_targetProcess == null || _targetProcess.HasExited)
            {
                var processMatches = Process.GetProcessesByName(PROCESS_NAME);
                if (processMatches.Any())
                    _targetProcess = processMatches.First();
                else 
                    return false;
            }

            return true;
        }

        public IntPtr SearchForPointerFromPattern(byte[] bytePattern)
        {
            var baseAddress = _targetProcess.MainModule.BaseAddress;
            var memorySize = _targetProcess.MainModule.ModuleMemorySize;
            var buffer = new byte[memorySize];

            try
            {
                if (!WindowsApi.ReadProcessMemory(_targetProcess.Handle, baseAddress, buffer, memorySize, out _))
                    throw new InvalidOperationException("Failed to read process memory");

                for (int i = 0; i < memorySize - bytePattern.Length + 1; i++)
                {
                    bool patternMatched = true;
                    for (int j = 0; j < bytePattern.Length; j++)
                    {
                        if (bytePattern[j] != 0x00 && bytePattern[j] != buffer[i + j])
                        {
                            patternMatched = false;
                            break;
                        }
                    }

                    if (patternMatched)
                        return baseAddress + i;
                }
            }
            catch (Exception ex)
            {
                _applicationLogger.LogException(ex);
            }

            return IntPtr.Zero;
        }

        public T ReadData<T>(IntPtr pointerAddress) where T : struct
        {
            T data = default(T);

            try
            {
                var size = System.Runtime.InteropServices.Marshal.SizeOf<T>();
                var buffer = new byte[size];

                if (!WindowsApi.ReadProcessMemory(_targetProcess.Handle, pointerAddress, buffer, size, out _))
                    throw new InvalidOperationException("Failed to read process memory at address: 0x" + pointerAddress.ToString("X"));

                var handle = System.Runtime.InteropServices.GCHandle.Alloc(buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
                data = (T)System.Runtime.InteropServices.Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                handle.Free();
            }
            catch (Exception ex)
            {
                _applicationLogger.LogException(ex);
            }

            return data;
        }
        #endregion Methods..
    }
}
