using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    public interface Heapentry32 : IEquatable<Heapentry32>
    {
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32First function, set this member to sizeof(HEAPENTRY32). If you do not initialize dwSize, Heap32First fails.
        /// </summary>
        ulong Size { get; }
        /// <summary>
        /// A handle to the heap block.
        /// </summary>
        IntPtr Handle { get; }
        /// <summary>
        /// The linear address of the start of the block.
        /// </summary>
        ulong Address { get; }
        /// <summary>
        /// The size of the heap block, in bytes.
        /// </summary>
        ulong BlockSize { get; }
        /// <summary>
        /// This member can be one of the following values. <see cref="ListFlags"/>
        /// </summary>
        LocationFlags Flags { get; }
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        uint LockCount { get; }
        /// <summary>
        /// Reserved; do not use or alter.
        /// </summary>
        uint Reserved { get; }
        /// <summary>
        /// The identifier of the process that uses the heap.
        /// </summary>
        uint ProcessID { get; }
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        ulong HeapID { get; }
        Heapentry32 Set(ulong? Size = null, IntPtr? Handle = null, ulong? Address = null, ulong? BlockSize = null, LocationFlags? Flags = null, uint? LockCount = null, uint? Reserved = null, uint? ProcessID = null, ulong? HeapID = null);
    }
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heapentry32_32 : Heapentry32, IEquatable<Heapentry32_32>
    {
        ulong Heapentry32.Size => Size;
        IntPtr Heapentry32.Handle => Handle;
        ulong Heapentry32.Address => Address;
        ulong Heapentry32.BlockSize => BlockSize;
        LocationFlags Heapentry32.Flags => Flags;
        uint Heapentry32.LockCount => LockCount;
        uint Heapentry32.Reserved => Reserved;
        uint Heapentry32.ProcessID => ProcessID;
        ulong Heapentry32.HeapID => HeapID;

        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32First function, set this member to sizeof(HEAPENTRY32). If you do not initialize dwSize, Heap32First fails.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// A handle to the heap block.
        /// </summary>
        public readonly IntPtr Handle;
        /// <summary>
        /// The linear address of the start of the block.
        /// </summary>
        public readonly uint Address;
        /// <summary>
        /// The size of the heap block, in bytes.
        /// </summary>
        public readonly uint BlockSize;
        /// <summary>
        /// This member can be one of the following values. <see cref="ListFlags"/>
        /// </summary>
        public readonly LocationFlags Flags;
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        public readonly uint LockCount;
        /// <summary>
        /// Reserved; do not use or alter.
        /// </summary>
        public readonly uint Reserved;
        /// <summary>
        /// The identifier of the process that uses the heap.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        public readonly uint HeapID;
        public Heapentry32_32(uint Size, IntPtr Handle, uint Address, uint BlockSize, LocationFlags Flags, uint LockCount, uint Reserved, uint ProcessID, uint HeapID)
            => (this.Size, this.Handle, this.Address, this.BlockSize, this.Flags, this.LockCount, this.Reserved, this.ProcessID, this.HeapID)
            = (Size, Handle, Address, BlockSize, Flags, LockCount, Reserved, ProcessID, HeapID);

        public override bool Equals(object obj)
            => obj is Heapentry32_32 entry32 ? Equals(entry32)
            : obj is Heapentry32 entry32_2 ? Equals(entry32_2)
            : false;

        public bool Equals(Heapentry32_32 other)
            => Size == other.Size &&
                   EqualityComparer<IntPtr>.Default.Equals(Handle, other.Handle) &&
                   Address == other.Address &&
                   BlockSize == other.BlockSize &&
                   Flags == other.Flags &&
                   LockCount == other.LockCount &&
                   Reserved == other.Reserved &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID;
        public bool Equals(Heapentry32 other)
            => Size == other.Size &&
                   EqualityComparer<IntPtr>.Default.Equals(Handle, other.Handle) &&
                   Address == other.Address &&
                   BlockSize == other.BlockSize &&
                   Flags == other.Flags &&
                   LockCount == other.LockCount &&
                   Reserved == other.Reserved &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID;

        public override int GetHashCode()
        {
            var hashCode = 1206797176;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(Handle);
            hashCode = hashCode * -1521134295 + Address.GetHashCode();
            hashCode = hashCode * -1521134295 + BlockSize.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + LockCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Reserved.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + HeapID.GetHashCode();
            return hashCode;
        }

        public Heapentry32_32 Set(uint? Size = null, IntPtr? Handle = null, uint? Address = null, uint? BlockSize = null, LocationFlags? Flags = null, uint? LockCount = null, uint? Reserved = null, uint? ProcessID = null, uint? HeapID = null)
            => Size == null && Handle == null && Address == null && BlockSize == null && Flags == null && LockCount == null && Reserved == null && ProcessID == null && HeapID == null ? this
            : new Heapentry32_32(Size ?? this.Size, Handle ?? this.Handle, Address ?? this.Address, BlockSize ?? this.BlockSize, Flags ?? this.Flags, LockCount ?? this.LockCount, Reserved ?? this.Reserved, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID);
        Heapentry32 Heapentry32.Set(ulong? Size = null, IntPtr? Handle = null, ulong? Address = null, ulong? BlockSize = null, LocationFlags? Flags = null, uint? LockCount = null, uint? Reserved = null, uint? ProcessID = null, ulong? HeapID = null)
            => Set((uint?)Size, Handle, (uint?)Address, (uint?)BlockSize, Flags, LockCount, Reserved, ProcessID, (uint?)HeapID);
        public override string ToString()
            => $"{nameof(Heapentry32_32)}{{"
            + $"{nameof(Size)}:{Size}"
            + $", {nameof(Handle)}:{Handle}"
            + $", {nameof(Address)}:{Address}"
            + $", {nameof(BlockSize)}:{BlockSize}"
            + $", {nameof(Flags)}:{Flags}"
            + $", {nameof(LockCount)}:{LockCount}"
            + $", {nameof(Reserved)}:{Reserved}"
            + $", {nameof(ProcessID)}:{ProcessID}"
            + $", {nameof(HeapID)}:{HeapID}"
            + $"}}";

        public static bool operator ==(Heapentry32_32 heapentry1, Heapentry32_32 heapentry2) => heapentry1.Equals(heapentry2);

        public static bool operator !=(Heapentry32_32 heapentry1, Heapentry32_32 heapentry2) => !(heapentry1 == heapentry2);
    }
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heapentry32_64 : Heapentry32, IEquatable<Heapentry32_64>
    {
        ulong Heapentry32.Size => Size;
        IntPtr Heapentry32.Handle => Handle;
        ulong Heapentry32.Address => Address;
        ulong Heapentry32.BlockSize => BlockSize;
        LocationFlags Heapentry32.Flags => Flags;
        uint Heapentry32.LockCount => LockCount;
        uint Heapentry32.Reserved => Reserved;
        uint Heapentry32.ProcessID => ProcessID;
        ulong Heapentry32.HeapID => HeapID;
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Heap32First function, set this member to sizeof(HEAPENTRY32). If you do not initialize dwSize, Heap32First fails.
        /// </summary>
        public readonly ulong Size;
        /// <summary>
        /// A handle to the heap block.
        /// </summary>
        public readonly IntPtr Handle;
        /// <summary>
        /// The linear address of the start of the block.
        /// </summary>
        public readonly ulong Address;
        /// <summary>
        /// The size of the heap block, in bytes.
        /// </summary>
        public readonly ulong BlockSize;
        /// <summary>
        /// This member can be one of the following values. <see cref="ListFlags"/>
        /// </summary>
        public readonly LocationFlags Flags;
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        public readonly uint LockCount;
        /// <summary>
        /// Reserved; do not use or alter.
        /// </summary>
        public readonly uint Reserved;
        /// <summary>
        /// The identifier of the process that uses the heap.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
        /// </summary>
        public readonly ulong HeapID;
        public Heapentry32_64(ulong Size, IntPtr Handle, ulong Address, ulong BlockSize, LocationFlags Flags, uint LockCount, uint Reserved, uint ProcessID, ulong HeapID)
            => (this.Size, this.Handle, this.Address, this.BlockSize, this.Flags, this.LockCount, this.Reserved, this.ProcessID, this.HeapID)
            = (Size, Handle, Address, BlockSize, Flags, LockCount, Reserved, ProcessID, HeapID);

        public override bool Equals(object obj)
            => obj is Heapentry32_64 entry32 ? Equals(entry32)
            : obj is Heapentry32 entry32_2 ? Equals(entry32_2)
            : false;

        public bool Equals(Heapentry32_64 other)
            => Size == other.Size &&
                   EqualityComparer<IntPtr>.Default.Equals(Handle, other.Handle) &&
                   Address == other.Address &&
                   BlockSize == other.BlockSize &&
                   Flags == other.Flags &&
                   LockCount == other.LockCount &&
                   Reserved == other.Reserved &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID;
        public bool Equals(Heapentry32 other)
            => Size == other.Size &&
                   EqualityComparer<IntPtr>.Default.Equals(Handle, other.Handle) &&
                   Address == other.Address &&
                   BlockSize == other.BlockSize &&
                   Flags == other.Flags &&
                   LockCount == other.LockCount &&
                   Reserved == other.Reserved &&
                   ProcessID == other.ProcessID &&
                   HeapID == other.HeapID;
        public override int GetHashCode()
        {
            var hashCode = 1206797176;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(Handle);
            hashCode = hashCode * -1521134295 + Address.GetHashCode();
            hashCode = hashCode * -1521134295 + BlockSize.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + LockCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Reserved.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + HeapID.GetHashCode();
            return hashCode;
        }

        public Heapentry32_64 Set(ulong? Size = null, IntPtr? Handle = null, ulong? Address = null, ulong? BlockSize = null, LocationFlags? Flags = null, uint? LockCount = null, uint? Reserved = null, uint? ProcessID = null, ulong? HeapID = null)
            => Size == null && Handle == null && Address == null && BlockSize == null && Flags == null && LockCount == null && Reserved == null && ProcessID == null && HeapID == null ? this
            : new Heapentry32_64(Size ?? this.Size, Handle ?? this.Handle, Address ?? this.Address, BlockSize ?? this.BlockSize, Flags ?? this.Flags, LockCount ?? this.LockCount, Reserved ?? this.Reserved, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID);
        Heapentry32 Heapentry32.Set(ulong? Size, IntPtr? Handle, ulong? Address, ulong? BlockSize, LocationFlags? Flags, uint? LockCount, uint? Reserved, uint? ProcessID, ulong? HeapID)
            => Set(Size, Handle, Address, BlockSize, Flags, LockCount, Reserved, ProcessID, HeapID);
        public override string ToString()
            => $"{nameof(Heapentry32_64)}{{"
            + $"{nameof(Size)}:{Size}"
            + $", {nameof(Handle)}:{Handle}"
            + $", {nameof(Address)}:{Address}"
            + $", {nameof(BlockSize)}:{BlockSize}"
            + $", {nameof(Flags)}:{Flags}"
            + $", {nameof(LockCount)}:{LockCount}"
            + $", {nameof(Reserved)}:{Reserved}"
            + $", {nameof(ProcessID)}:{ProcessID}"
            + $", {nameof(HeapID)}:{HeapID}"
            + $"}}";

        public static bool operator ==(Heapentry32_64 heapentry1, Heapentry32_64 heapentry2) => heapentry1.Equals(heapentry2);

        public static bool operator !=(Heapentry32_64 heapentry1, Heapentry32_64 heapentry2) => !(heapentry1 == heapentry2);
    }
}
