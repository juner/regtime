using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Win32.Toolhelp32.Utils;
using static System.Win32.Toolhelp32.NativeMethods;

namespace System.Win32.Toolhelp32
{
    public interface Heaplist32 : IEquatable<Heaplist32>
    {
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32ListFirst function, set this member to sizeof(<see cref="Heaplist32"/>). If you do not initialize dwSize, Heap32ListFirst will fail.
        /// </summary>
        ulong Size { get; }
        /// <summary>
        /// The identifier of the process to be examined.
        /// </summary>
        uint ProcessID { get; }
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        ulong HeapID { get; }
        /// <summary>
        /// This member can be one of the following values. <see cref="HeapFlags"/>
        /// </summary>
        HeapFlags Flags { get; }
        IEnumerable<Heapentry32> GetHeap32();
        Heaplist32 Set(ulong? Size = null, uint? ProcessID = null, ulong? HeapID = null, HeapFlags? Flags = null);
    }
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heaplist32_32 : Heaplist32, IEquatable<Heaplist32_32>
    {
        ulong Heaplist32.Size => Size;
        uint Heaplist32.ProcessID => ProcessID;
        ulong Heaplist32.HeapID => HeapID;
        HeapFlags Heaplist32.Flags => Flags;
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32ListFirst function, set this member to sizeof(<see cref="Heaplist32_32"/>). If you do not initialize dwSize, Heap32ListFirst will fail.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// The identifier of the process to be examined.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        public readonly uint HeapID;
        /// <summary>
        /// This member can be one of the following values. <see cref="HeapFlags"/>
        /// </summary>
        public readonly HeapFlags Flags;
        public IEnumerable<Heapentry32_32> GetHeap32()
        {
            var entry = new Heapentry32_32()
                 .Set(Size: (uint)Marshal.SizeOf<Heapentry32_32>());
            if (!Heap32First(ref entry, ProcessID, HeapID))
            {
                var error = Marshal.GetHRForLastWin32Error();
                ThrowIfHasException();
                return Enumerable.Empty<Heapentry32_32>();
            }
            IEnumerable<Heapentry32_32> Generator()
            {
                do
                {
                    yield return entry;
                } while (Heap32Next(ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        IEnumerable<Heapentry32> Heaplist32.GetHeap32() => GetHeap32().Cast<Heapentry32>();
        public Heaplist32_32(uint Size, uint ProcessID, uint HeapID, HeapFlags Flags)
            => (this.Size, this.ProcessID, this.HeapID, this.Flags) = (Size, ProcessID, HeapID, Flags);
        public Heaplist32_32 Set(uint? Size = null, uint? ProcessID = null, uint? HeapID = null, HeapFlags? Flags = null)
            => Size == null && ProcessID == null && HeapID == null && Flags == null ? this
            : new Heaplist32_32(Size ?? this.Size, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID, Flags ?? this.Flags);
        Heaplist32 Heaplist32.Set(ulong? Size, uint? ProcessID, ulong? HeapID, HeapFlags? Flags)
            => Set((uint?)Size, ProcessID, (uint?)HeapID, Flags);
        public override string ToString()
            => $"{nameof(Heaplist32_32)}{{{nameof(Size)}:{Size}, {nameof(ProcessID)}:{ProcessID}, {nameof(HeapID)}:0x{HeapID:X8}, {nameof(Flags)}:{Flags}}}";

        public override bool Equals(object obj)
            => obj is Heaplist32_32 list ? Equals(list)
            : obj is Heaplist32 list_2 ? Equals(list_2)
            : false;

        public bool Equals(Heaplist32_32 other)
            => Size == other.Size &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID &&
                   Flags == other.Flags;
        public bool Equals(Heaplist32 other)
            => Size == other.Size &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID &&
                   Flags == other.Flags;

        public override int GetHashCode()
        {
            var hashCode = 1091725553;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + HeapID.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Heaplist32_32 heaplist1, Heaplist32_32 heaplist2) => heaplist1.Equals(heaplist2);

        public static bool operator !=(Heaplist32_32 heaplist1, Heaplist32_32 heaplist2) => !(heaplist1 == heaplist2);
    }
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heaplist32_64 : Heaplist32, IEquatable<Heaplist32_64>
    {
        ulong Heaplist32.Size => Size;
        uint Heaplist32.ProcessID => ProcessID;
        ulong Heaplist32.HeapID => HeapID;
        HeapFlags Heaplist32.Flags => Flags;
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32ListFirst function, set this member to sizeof(<see cref="Heaplist32_64"/>). If you do not initialize dwSize, Heap32ListFirst will fail.
        /// </summary>
        public readonly ulong Size;
        /// <summary>
        /// The identifier of the process to be examined.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        public readonly ulong HeapID;
        /// <summary>
        /// This member can be one of the following values. <see cref="HeapFlags"/>
        /// </summary>
        public readonly HeapFlags Flags;
        public IEnumerable<Heapentry32_64> GetHeap32()
        {
            var entry = new Heapentry32_64()
                 .Set(Size: (uint)Marshal.SizeOf<Heapentry32_64>());
            if (!Heap32First(ref entry, ProcessID, HeapID))
            {
                var error = Marshal.GetHRForLastWin32Error();
                ThrowIfHasException();
                return Enumerable.Empty<Heapentry32_64>();
            }
            IEnumerable<Heapentry32_64> Generator()
            {
                do
                {
                    yield return entry;
                } while (Heap32Next(ref entry));
                ThrowIfHasException();
                yield break;
            }
            return Generator();
        }
        IEnumerable<Heapentry32> Heaplist32.GetHeap32() => GetHeap32().Cast<Heapentry32>();
        public Heaplist32_64(ulong Size, uint ProcessID, ulong HeapID, HeapFlags Flags)
            => (this.Size, this.ProcessID, this.HeapID, this.Flags) = (Size, ProcessID, HeapID, Flags);
        public Heaplist32_64 Set(ulong? Size = null, uint? ProcessID = null, ulong? HeapID = null, HeapFlags? Flags = null)
            => Size == null && ProcessID == null && HeapID == null && Flags == null ? this
            : new Heaplist32_64(Size ?? this.Size, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID, Flags ?? this.Flags);
        Heaplist32 Heaplist32.Set(ulong? Size, uint? ProcessID, ulong? HeapID, HeapFlags? Flags)
            => Set(Size, ProcessID, HeapID, Flags);
        public override string ToString()
            => $"{nameof(Heaplist32_64)}{{{nameof(Size)}:{Size}, {nameof(ProcessID)}:{ProcessID}, {nameof(HeapID)}:0x{HeapID:X8}, {nameof(Flags)}:{Flags}}}";
        public override bool Equals(object obj)
            => obj is Heaplist32_64 list ? Equals(list)
            : obj is Heaplist32 list_2 ? Equals(list_2)
            : false;
        public bool Equals(Heaplist32_64 other)
            => Size == other.Size &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID &&
                   Flags == other.Flags;
        public bool Equals(Heaplist32 other)
            => Size == other.Size &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID &&
                   Flags == other.Flags;
        public override int GetHashCode()
        {
            var hashCode = 1091725553;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + HeapID.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Heaplist32_64 heaplist1, Heaplist32_64 heaplist2) => heaplist1.Equals(heaplist2);

        public static bool operator !=(Heaplist32_64 heaplist1, Heaplist32_64 heaplist2) => !(heaplist1 == heaplist2);
    }
}
