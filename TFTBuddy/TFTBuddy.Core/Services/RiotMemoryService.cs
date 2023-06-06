using Newtonsoft.Json;
using System.Diagnostics;
using TFTBuddy.Common;
using TFTBuddy.Logging;

namespace TFTBuddy.Core
{
    public class RiotMemoryService : MemoryServiceBase, IRiotMemoryService
    {
        #region Fields..
        private const string PROCESS_NAME = "League Of Legends";

        private readonly IApplicationLogger _applicationLogger;

        private Process _targetProcess;
        
        private CancellationTokenSource _cancellationTokenSource;
        #endregion Fields..

        #region Properties..
        #endregion Properties..

        #region Constructors..
        public RiotMemoryService(IApplicationLogger applicationLogger)
            : base(applicationLogger)
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

        private List<BytePattern> GetBytePatterns()
        {
            var bytePatternString = ResourceHelper.GetResourceString("TFTBuddy.Resources", @"TFT_BytePatterns.json");
            var bytePatterns = JsonConvert.DeserializeObject<List<BytePattern>>(bytePatternString);
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
                        var patternAddress = FindPatternAddresses(_targetProcess, x, true);
                        //this.Read(_targetProcess, patternAddress);
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

        //public IntPtr SearchForPointerFromPattern(byte[] bytePattern)
        //{
        //    var baseAddress = _targetProcess.MainModule.BaseAddress;
        //    var memorySize = _targetProcess.MainModule.ModuleMemorySize;
        //    var buffer = new byte[memorySize];

        //    try
        //    {
        //        bool readProcessResult = WindowsApi.ReadProcessMemory(_targetProcess.Handle, baseAddress, buffer, memorySize, out _);
        //        if (!readProcessResult)
        //            throw new InvalidOperationException("Failed to read process memory");

        //        for (int i = 0; i < memorySize - bytePattern.Length + 1; i++)
        //        {
        //            bool patternMatched = true;
        //            for (int j = 0; j < bytePattern.Length; j++)
        //            {
        //                if (bytePattern[j] != 0x00 && bytePattern[j] != buffer[i + j])
        //                {
        //                    patternMatched = false;
        //                    break;
        //                }
        //            }

        //            if (patternMatched)
        //                return baseAddress + i;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _applicationLogger.LogException(ex);
        //    }

        //    return IntPtr.Zero;
        //}

        //public T ReadData<T>(IntPtr address) where T : struct
        //{
        //    T data = default(T);

        //    try
        //    {
        //        var size = System.Runtime.InteropServices.Marshal.SizeOf<T>();
        //        var buffer = new byte[size];

        //        if (!WindowsApi.ReadProcessMemory(_targetProcess.Handle, address, buffer, size, out _))
        //            throw new InvalidOperationException("Failed to read process memory at address: 0x" + address.ToString("X"));

        //        var handle = System.Runtime.InteropServices.GCHandle.Alloc(buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
        //        data = (T)System.Runtime.InteropServices.Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        //        handle.Free();
        //    }
        //    catch (Exception ex)
        //    {
        //        _applicationLogger.LogException(ex);
        //    }

        //    return data;
        //}
        #endregion Methods..
    }
}
