namespace System.Win32.Toolhelp32
{
    /// <summary>
    /// The portions of the system to be included in the snapshot. This parameter can be one or more of the following values.
    /// </summary>
    [Flags]
    public enum SnapshotFlags : uint
    {
        /// <summary>
        /// Includes all heaps of the process specified in th32ProcessID in the snapshot. To enumerate the heaps, see Heap32ListFirst. 
        /// </summary>
        /// <remarks>TH32CS_SNAPHEAPLIST</remarks>
        HeapList = 0x00000001,
        /// <summary>
        /// Includes all processes in the system in the snapshot. To enumerate the processes, see Process32First. 
        /// </summary>
        /// <remarks>TH32CS_SNAPPROCESS</remarks>
        Process = 0x00000002,
        /// <summary>
        /// Includes all threads in the system in the snapshot. To enumerate the threads, see Thread32First. 
        /// To identify the threads that belong to a specific process, compare its process identifier to the th32OwnerProcessID member of the THREADENTRY32 structure when enumerating the threads.
        /// </summary>
        /// <remarks>TH32CS_SNAPTHREAD</remarks>
        Thread = 0x00000004,
        /// <summary>
        /// Includes all modules of the process specified in th32ProcessID in the snapshot. To enumerate the modules, see Module32First. If the function fails with ERROR_BAD_LENGTH, retry the function until it succeeds. 
        /// 64-bit Windows:  Using this flag in a 32-bit process includes the 32-bit modules of the process specified in th32ProcessID, while using it in a 64-bit process includes the 64-bit modules.To include the 32-bit modules of the process specified in th32ProcessID from a 64-bit process, use the TH32CS_SNAPMODULE32 flag.
        /// </summary>
        /// <remarks>TH32CS_SNAPMODULE</remarks>
        Module = 0x00000008,
        /// <summary>
        /// Includes all 32-bit modules of the process specified in th32ProcessID in the snapshot when called from a 64-bit process. This flag can be combined with TH32CS_SNAPMODULE or TH32CS_SNAPALL. If the function fails with ERROR_BAD_LENGTH, retry the function until it succeeds. 
        /// </summary>
        /// <remarks>TH32CS_SNAPMODULE32</remarks>
        Module32 = 0x00000010,
        /// <summary>
        /// Indicates that the snapshot handle is to be inheritable. 
        /// </summary>
        /// <remarks>TH32CS_INHERIT</remarks>
        Inherit = 0x80000000,
        /// <summary>
        /// Includes all processes and threads in the system, plus the heaps and modules of the process specified in th32ProcessID. Equivalent to specifying the <see cref="HeapList"/>, <see cref="Module"/>, <see cref="Process"/>, and <see cref="Thread"/> values combined using an OR operation ('|'). 
        /// </summary>
        /// <remarks>TH32CS_SNAPALL</remarks>
        All = HeapList | Module | Process | Thread,
        NoHeaps = 0x40000000
    }
}
