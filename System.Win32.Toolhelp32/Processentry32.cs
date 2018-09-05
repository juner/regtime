using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    /// <summary>
    /// Describes an entry from a list of the processes residing in the system address space when a snapshot was taken.
    /// </summary>
    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Unicode)]
    public readonly struct Processentry32 : IEquatable<Processentry32>
    {
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Process32First function, set this member to sizeof(<see cref="Processentry32"/>). If you do not initialize dwSize, Process32First fails.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        public readonly uint Usage;
        /// <summary>
        /// The process identifier.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        public readonly IntPtr DefaultHeapID;
        /// <summary>
        /// This member is no longer used and is always set to zero.
        /// </summary>
        public readonly uint ModuleID;
        /// <summary>
        /// The number of execution threads started by the process.
        /// </summary>
        public readonly uint ThreadsCount;
        /// <summary>
        /// The identifier of the process that created this process (its parent process).
        /// </summary>
        public readonly uint ParentProcessID;
        /// <summary>
        /// The base priority of any threads created by this process.
        /// </summary>
        public readonly int PriClassBase;
        /// <summary>
        /// This member is no longer used, and is always set to zero.
        /// </summary>
        public readonly uint Flags;
        /// <summary>
        /// The name of the executable file for the process. To retrieve the full path to the executable file, call the Module32First function and check the szExePath member of the MODULEENTRY32 structure that is returned. However, if the calling process is a 32-bit process, you must call the QueryFullProcessImageName function to retrieve the full path of the executable file for a 64-bit process.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public readonly string ExeFile;
        public Processentry32(uint Size, uint Usage, uint ProcessID, IntPtr DefaultHeapID, uint ModuleID, uint ThreadsCount, uint ParentProcessID, int PriClassBase, uint Flags, string ExeFile)
            => (this.Size, this.Usage, this.ProcessID, this.DefaultHeapID, this.ModuleID, this.ThreadsCount, this.ParentProcessID, this.PriClassBase, this.Flags, this.ExeFile)
            = (Size, Usage, ProcessID, DefaultHeapID, ModuleID, ThreadsCount, ParentProcessID, PriClassBase, Flags, ExeFile);

        public override bool Equals(object obj) => obj is Processentry32 entry32 && Equals(entry32);

        public bool Equals(Processentry32 other)
            => Size == other.Size &&
                   Usage == other.Usage &&
                   ProcessID == other.ProcessID &&
                   EqualityComparer<IntPtr>.Default.Equals(DefaultHeapID, other.DefaultHeapID) &&
                   ModuleID == other.ModuleID &&
                   ThreadsCount == other.ThreadsCount &&
                   ParentProcessID == other.ParentProcessID &&
                   PriClassBase == other.PriClassBase &&
                   Flags == other.Flags &&
                   ExeFile == other.ExeFile;

        public override int GetHashCode()
        {
            var hashCode = 1244525484;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + Usage.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(DefaultHeapID);
            hashCode = hashCode * -1521134295 + ModuleID.GetHashCode();
            hashCode = hashCode * -1521134295 + ThreadsCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ParentProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + PriClassBase.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ExeFile);
            return hashCode;
        }

        public Processentry32 Set(uint? Size = null, uint? Usage = null, uint? ProcessID = null, IntPtr? DefaultHeapID = null, uint? ModuleID = null, uint? ThreadsCount = null, uint? ParentProcessID = null, int? PriClassBase = null, uint? Flags = null, string ExeFile = null)
            => Size == null && Usage == null && ProcessID == null && DefaultHeapID == null && ModuleID == null && ThreadsCount == null && ParentProcessID == null && PriClassBase == null && Flags == null && ExeFile == null ? this
            : new Processentry32(Size ?? this.Size, Usage ?? this.Usage, ProcessID ?? this.ProcessID, DefaultHeapID ?? this.DefaultHeapID, ModuleID ?? this.ModuleID, ThreadsCount ?? this.ThreadsCount, ParentProcessID ?? this.ParentProcessID, PriClassBase ?? this.PriClassBase, Flags ?? this.Flags, ExeFile ?? this.ExeFile);
        public override string ToString()
            => $"{nameof(Processentry32)}{{"
            + $"{nameof(Size)}:{Size}"
            + $", {nameof(Usage)}:{Usage}"
            + $", {nameof(ProcessID)}:{ProcessID}"
            + $", {nameof(DefaultHeapID)}:{DefaultHeapID}"
            + $", {nameof(ModuleID)}:{ModuleID}"
            + $", {nameof(ThreadsCount)}:{ThreadsCount}"
            + $", {nameof(ParentProcessID)}:{ParentProcessID}"
            + $", {nameof(PriClassBase)}:{PriClassBase}"
            + $", {nameof(Flags)}:{Flags}"
            + $", {nameof(ExeFile)}:{ExeFile}"
            +"}}";

        public static bool operator ==(Processentry32 processentry1, Processentry32 processentry2) => processentry1.Equals(processentry2);

        public static bool operator !=(Processentry32 processentry1, Processentry32 processentry2) => !(processentry1 == processentry2);
    };
}
