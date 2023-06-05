namespace TFTBuddy.Common
{
    public class WindowsApi
    {
        #region Properties..
        public const int PROCESS_VM_READ = 0x0010;
        #endregion Properties..

        #region Methods..
        #region kernel32..
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);  
        #endregion kernel32..
        #endregion Methods..
    }
}
