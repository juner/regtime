namespace System.Win32.Toolhelp32
{
    public readonly struct Heaplist32 {
        public readonly uint Size;
        public readonly uint ProcessID;
        public readonly UIntPtr HeapID;
        public readonly uint Flags;
    }
}
