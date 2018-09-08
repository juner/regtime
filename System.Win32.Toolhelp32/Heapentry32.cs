using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Heapentry32 : IEquatable<Heapentry32>
    {
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
        public readonly UIntPtr Address;
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
        public readonly UIntPtr HeapID;
        public Heapentry32(uint Size, IntPtr Handle, UIntPtr Address, uint BlockSize, LocationFlags Flags, uint LockCount, uint Reserved, uint ProcessID, UIntPtr HeapID)
            => (this.Size, this.Handle, this.Address, this.BlockSize, this.Flags, this.LockCount, this.Reserved, this.ProcessID, this.HeapID)
            = (Size, Handle, Address, BlockSize, Flags, LockCount, Reserved, ProcessID, HeapID);

        public override bool Equals(object obj) => obj is Heapentry32 entry32 && Equals(entry32);

        public bool Equals(Heapentry32 other)
            => Size == other.Size &&
                   EqualityComparer<IntPtr>.Default.Equals(Handle, other.Handle) &&
                   EqualityComparer<UIntPtr>.Default.Equals(Address, other.Address) &&
                   BlockSize == other.BlockSize &&
                   Flags == other.Flags &&
                   LockCount == other.LockCount &&
                   Reserved == other.Reserved &&
                   ProcessID == other.ProcessID &&
                   EqualityComparer<UIntPtr>.Default.Equals(HeapID, other.HeapID);

        public override int GetHashCode()
        {
            var hashCode = 1206797176;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(Handle);
            hashCode = hashCode * -1521134295 + EqualityComparer<UIntPtr>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + BlockSize.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + LockCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Reserved.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<UIntPtr>.Default.GetHashCode(HeapID);
            return hashCode;
        }

        public Heapentry32 Set(uint? Size = null, IntPtr? Handle = null, UIntPtr? Address = null, uint? BlockSize = null, LocationFlags? Flags = null, uint? LockCount = null, uint? Reserved = null, uint? ProcessID = null, UIntPtr? HeapID = null)
            => Size == null && Handle == null && Address == null && BlockSize == null && Flags == null && LockCount == null && Reserved == null && ProcessID == null && HeapID == null ? this
            : new Heapentry32(Size ?? this.Size, Handle ?? this.Handle, Address ?? this.Address, BlockSize ?? this.BlockSize, Flags ?? this.Flags, LockCount ?? this.LockCount, Reserved ?? this.Reserved, ProcessID ?? this.ProcessID, HeapID ?? this.HeapID);
        public override string ToString()
            => $"{nameof(Heapentry32)}{{"
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

        public static bool operator ==(Heapentry32 heapentry1, Heapentry32 heapentry2) => heapentry1.Equals(heapentry2);

        public static bool operator !=(Heapentry32 heapentry1, Heapentry32 heapentry2) => !(heapentry1 == heapentry2);
    }
}
