using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Moduleentry32
    {
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Module32First function, set this member to sizeof(<see cref="Moduleentry32"/>). If you do not initialize dwSize, Module32First fails.
        /// </summary>
        public uint Size;
        /// <summary>
        /// This member is no longer used, and is always set to one.
        /// </summary>
        public uint ModuleID;
        /// <summary>
        /// The identifier of the process whose modules are to be examined.
        /// </summary>
        public uint ProcessID;
        /// <summary>
        /// The load count of the module, which is not generally meaningful, and usually equal to 0xFFFF.
        /// </summary>
        public uint GlblcntUsage;
        /// <summary>
        /// The load count of the module (same as GlblcntUsage), which is not generally meaningful, and usually equal to 0xFFFF.
        /// </summary>
        public uint ProccntUsage;
        /// <summary>
        /// The base address of the module in the context of the owning process.
        /// </summary>
        public IntPtr BaseAddr;
        /// <summary>
        /// The size of the module, in bytes.
        /// </summary>
        public uint BaseSize;
        /// <summary>
        /// A handle to the module in the context of the owning process.
        /// </summary>
        public IntPtr Module;
        /// <summary>
        /// The module name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ModuleName;
        /// <summary>
        /// The module path.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string ExePath;
        public Moduleentry32(uint Size, uint ModuleID, uint ProcessID, uint GlblcntUsage, uint ProccntUsage, IntPtr BaseAddr, uint BaseSize, IntPtr Module, string ModuleName, string ExePath)
            => (this.Size, this.ModuleID, this.ProcessID, this.GlblcntUsage, this.ProccntUsage, this.BaseAddr, this.BaseSize, this.Module, this.ModuleName, this.ExePath)
            = (Size, ModuleID, ProcessID, GlblcntUsage, ProccntUsage, BaseAddr, BaseSize, Module, ModuleName, ExePath);
        public Moduleentry32 Set(uint? Size = null, uint? ModuleID = null, uint? ProcessID = null, uint? GlblcntUsage = null, uint? ProccntUsage = null, IntPtr? BaseAddr = null, uint? BaseSize = null, IntPtr? Module = null, string ModuleName = null, string ExePath = null)
            => Size == null && ModuleID == null && ProcessID == null && GlblcntUsage == null && ProccntUsage == null && BaseAddr == null && BaseSize == null && Module == null && ModuleName == null && ExePath == null ? this
            : new Moduleentry32(Size ?? this.Size, ModuleID ?? this.ModuleID, ProcessID ?? this.ProcessID, GlblcntUsage ?? this.GlblcntUsage, ProccntUsage ?? this.ProccntUsage, BaseAddr ?? this.BaseAddr, BaseSize ?? this.BaseSize, Module ?? this.Module, ModuleName ?? this.ModuleName, ExePath ?? this.ExePath);
        public override string ToString()
        {
            return $"{nameof(Moduleentry32)}{{"
                + $"{nameof(Size)}:{Size}"
                + $", {nameof(ModuleID)}:{ModuleID}"
                + $", {nameof(ProcessID)}:{ProcessID}"
                + $", {nameof(GlblcntUsage)}:{GlblcntUsage}"
                + $", {nameof(ProccntUsage)}:{ProccntUsage}"
                + $", {nameof(BaseAddr)}:{BaseAddr}"
                + $", {nameof(BaseSize)}:{BaseSize}"
                + $", {nameof(Module)}:{Module}"
                + $", {nameof(ModuleName)}:{ModuleName}"
                + $", {nameof(ExePath)}:{ExePath}"
                + $"}}";
        }
    }
}
