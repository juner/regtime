using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Win32.Toolhelp32.Utils;
using static System.Win32.Toolhelp32.NativeMethods;

namespace System.Win32.Toolhelp32
{
    [System.Security.SecurityCritical]
    public sealed class Toolhelp32Snapshot : SafeHandleZeroOrMinusOneIsInvalid
    {
        public Toolhelp32Snapshot() : base(true) { }
        public Toolhelp32Snapshot(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle) => SetHandle(preexistingHandle);
        public Toolhelp32Snapshot(ToolHelp32CreateSnapshot Flags, uint ProcessID = 0) : this(CreateToolhelp32Snapshot(Flags, ProcessID), true) { }
        public static Toolhelp32Snapshot GetCurrent(ToolHelp32CreateSnapshot Flags) => new Toolhelp32Snapshot(Flags, GetCurrentProcessId());
        public override string ToString()
            => $"{nameof(Toolhelp32Snapshot)}{{{nameof(IsInvalid)}:{IsInvalid}, {nameof(IsClosed)}:{IsClosed}}}";
        public IEnumerable<Processentry32> GetProcess32()
        {
            var entry = new Processentry32()
                 .Set(Size: (uint)Marshal.SizeOf<Processentry32>());
            if (!Process32First(this, ref entry))
            {
                var error = Marshal.GetHRForLastWin32Error();
                if (error != NOT_MORE)
                    Marshal.ThrowExceptionForHR(error);
                return Enumerable.Empty<Processentry32>();
            }
            IEnumerable<Processentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (Process32Next(this, ref entry));
                var error = Marshal.GetHRForLastWin32Error();
                if (error != NOT_MORE)
                    Marshal.ThrowExceptionForHR(error);
                yield break;
            }
            return Generator();
        }
        public IEnumerable<Moduleentry32> GetModule32()
        {
            var entry = new Moduleentry32()
                .Set(Size: (uint)Marshal.SizeOf<Moduleentry32>());
            if (!Module32First(this, ref entry))
            {
                ThrowIfHasException();
                return Enumerable.Empty<Moduleentry32>();
            }
            IEnumerable<Moduleentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (Module32Next(this, ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        public IEnumerable<Heaplist32> GetHeaplist32()
        {
            return Environment.Is64BitProcess
                ? GetHeaplist32_64().Cast<Heaplist32>()
                : GetHeaplist32_32().Cast<Heaplist32>();
        }
        public IEnumerable<Heaplist32_32> GetHeaplist32_32()
        {
            var entry = new Heaplist32_32()
                .Set(Size: (uint)Marshal.SizeOf<Heaplist32_32>());
            if (!Heap32ListFirst(this, ref entry))
            {
                ThrowIfHasException();
                return Enumerable.Empty<Heaplist32_32>();
            }
            IEnumerable<Heaplist32_32> Generator()
            {
                do
                {
                    yield return entry;
                } while (Heap32ListNext(this, ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        public IEnumerable<Heaplist32_64> GetHeaplist32_64()
        {
            var entry = new Heaplist32_64()
                .Set(Size: (uint)Marshal.SizeOf<Heaplist32_64>());
            if (!Heap32ListFirst(this, ref entry))
            {
                ThrowIfHasException();
                return Enumerable.Empty<Heaplist32_64>();
            }
            IEnumerable<Heaplist32_64> Generator()
            {
                do
                {
                    yield return entry;
                } while (Heap32ListNext(this, ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        protected override bool ReleaseHandle() => CloseHandle(handle);
    }
}
