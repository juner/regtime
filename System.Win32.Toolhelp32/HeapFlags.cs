namespace System.Win32.Toolhelp32
{
    [Flags]
    public enum HeapFlags
    {
        /// <summary>
        /// process's default heap
        /// </summary>
        /// <remarks>HF32_DEFAULT</remarks>
        Default = 1,
        /// <summary>
        /// is shared heap
        /// </summary>
        /// <remarks>HF32_SHARED</remarks>
        Shared = 2,
    }
}
