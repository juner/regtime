using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    [System.Security.SecurityCritical]
    public sealed class Toolhelp32Snapshot : SafeHandleZeroOrMinusOneIsInvalid
    {
        public Toolhelp32Snapshot() : base(true) { }
        public Toolhelp32Snapshot(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle) => SetHandle(preexistingHandle);
        public Toolhelp32Snapshot(SnapshotFlags Flags, uint ProcessID = 0) : this(NativeMethods.CreateToolhelp32Snapshot(Flags, ProcessID), true) { }
        public override string ToString()
            => $"{nameof(Toolhelp32Snapshot)}{{{nameof(IsInvalid)}:{IsInvalid}, {nameof(IsClosed)}:{IsClosed}}}";

        private static class NativeMethods
        {
            private const string Kernel32 = "kernel32.dll";
            
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);

            [DllImport(Kernel32, SetLastError = true)]
            internal static extern bool CloseHandle(IntPtr handle);
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Process32First(Toolhelp32Snapshot hSnapshot, ref Processentry32 lppe);
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Process32Next(Toolhelp32Snapshot hSnapshot, ref Processentry32 lppe);
        }
        public IEnumerable<Processentry32> GetProcess32()
        {
            var entry = new Processentry32()
                 .Set(Size: (uint)Marshal.SizeOf<Processentry32>());
            if (!NativeMethods.Process32First(this, ref entry))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            IEnumerable<Processentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (NativeMethods.Process32Next(this, ref entry));
            }
            return Generator();
        }
        protected override bool ReleaseHandle() => NativeMethods.CloseHandle(handle);

    }
}
