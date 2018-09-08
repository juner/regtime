namespace System.Win32.Toolhelp32
{
    [Flags]
    public enum LocationFlags : uint
    {
        /// <summary>
        /// The memory block has a fixed (unmovable) location. 
        /// </summary>
        /// <remarks>LF32_FIXED</remarks>
        Fixed = 0x00000001,
        /// <summary>
        /// The memory block is not used. 
        /// </summary>
        /// <remarks>LF32_FREE</remarks>
        Free = 0x00000002,
        /// <summary>
        /// The memory block location can be moved. 
        /// </summary>
        /// <remarks>LF32_MOVEABLE</remarks>
        Moveable = 0x00000004,
    }

}
