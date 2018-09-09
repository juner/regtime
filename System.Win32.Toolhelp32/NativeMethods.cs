using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    internal static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateToolhelp32Snapshot(ToolHelp32CreateSnapshot dwFlags, uint th32ProcessID);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern uint GetCurrentProcessId();
        [DllImport(Kernel32, SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Process32First(Toolhelp32Snapshot hSnapshot, ref Processentry32 lppe);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Process32Next(Toolhelp32Snapshot hSnapshot, ref Processentry32 lppe);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Module32First(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Module32Next(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32ListFirst(Toolhelp32Snapshot hSnapshot, ref Heaplist32_32 lpme);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32ListNext(Toolhelp32Snapshot hSnapshot, ref Heaplist32_32 lpme);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32ListFirst(Toolhelp32Snapshot hSnapshot, ref Heaplist32_64 lpme);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32ListNext(Toolhelp32Snapshot hSnapshot, ref Heaplist32_64 lpme);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32First(ref Heapentry32_32 lphe, uint ProcessID, uint HeapID);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Heap32Next(ref Heapentry32_32 lphe);
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool Heap32First(ref Heapentry32_64 lphe, uint ProcessID, ulong HeapID);
        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool Heap32Next(ref Heapentry32_64 lphe);
    }
}
