using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Win32.Toolhelp32.Utils;

namespace System.Win32.Toolhelp32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heaplist32 {
        public readonly uint Size;
        public readonly uint ProcessID;
        public readonly UIntPtr HeapID;
        public readonly HeapFlags Flags;
        private static class NativeMethods
        {
            private const string Kernel32 = "kernel32.dll";
            [DllImport(Kernel32, SetLastError = true)]
            public static extern bool Heap32First(ref Heapentry32 lphe, uint ProcessID, UIntPtr HeapID);
            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool Heap32Next(ref Heapentry32 lphe);
        }
        public IEnumerable<Heapentry32> GetHeap32()
        {
            var entry = new Heapentry32()
                 .Set(Size: (uint)Marshal.SizeOf<Heapentry32>());
            if (!NativeMethods.Heap32First(ref entry, ProcessID, HeapID))
            {
                var error = Marshal.GetHRForLastWin32Error();
                ThrowIfHasException();
                return Enumerable.Empty<Heapentry32>();
            }
            IEnumerable<Heapentry32> Generator()
            {
                do
                {
                    yield return entry;
                } while (NativeMethods.Heap32Next(ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        public Heaplist32(uint Size, uint ProcessID, UIntPtr HeapID, HeapFlags Flags)
            => (this.Size, this.ProcessID, this.HeapID, this.Flags) = (Size, ProcessID, HeapID, Flags);
        public Heaplist32 Set(uint? Size = null, uint? ProcessID = null, UIntPtr? HeapID = null, HeapFlags? Flags = null)
            => Size == null && ProcessID == null && HeapID == null && Flags == null ? this
            : new Heaplist32(Size ?? this.Size, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID, Flags ?? this.Flags);
        public override string ToString()
            => $"{nameof(Heaplist32)}{{{nameof(Size)}:{Size}, {nameof(ProcessID)}:{ProcessID}, {nameof(HeapID)}:0x{HeapID:X8}, {nameof(Flags)}:{Flags}}}";
    }
}
