using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Linq;
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
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Module32First(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Module32Next(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
            [DllImport(Kernel32, SetLastError = true)]
            public static extern bool Heap32First(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Module32Next(Toolhelp32Snapshot hSnapshot, ref Moduleentry32 lpme);
        }
        public IEnumerable<Processentry32> GetProcess32()
        {
            var entry = new Processentry32()
                 .Set(Size: (uint)Marshal.SizeOf<Processentry32>());
            if (!NativeMethods.Process32First(this, ref entry))
                return Enumerable.Empty<Processentry32>();
            IEnumerable<Processentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (NativeMethods.Process32Next(this, ref entry));
            }
            return Generator();
        }
        public IEnumerable<Moduleentry32> GetModule32()
        {
            var entry = new Moduleentry32()
                .Set(Size: (uint)Marshal.SizeOf<Moduleentry32>());
            if (!NativeMethods.Module32First(this, ref entry))
                return Enumerable.Empty<Moduleentry32>();
            IEnumerable<Moduleentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (NativeMethods.Module32Next(this, ref entry));
            }
            return Generator();

        }
        protected override bool ReleaseHandle() => NativeMethods.CloseHandle(handle);
    }
}
