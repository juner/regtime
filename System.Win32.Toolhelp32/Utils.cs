using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    internal static class Utils
    {
        public static readonly int NOT_MORE = unchecked((int)0x80070012);
        public static void ThrowIfHasException()
        {
            var error = Marshal.GetHRForLastWin32Error();
            if (NOT_MORE != error)
            {
                System.Diagnostics.Trace.WriteLine($"0x{error:X8}");
                Marshal.ThrowExceptionForHR(error);
            }
            return;
        }
    }
}
