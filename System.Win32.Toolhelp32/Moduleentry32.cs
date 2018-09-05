using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Win32.Toolhelp32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct Moduleentry32 : IEquatable<Moduleentry32>
    {
        /// <summary>
        /// The size of the structure, in bytes. Before calling the Module32First function, set this member to sizeof(<see cref="Moduleentry32"/>). If you do not initialize dwSize, Module32First fails.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// This member is no longer used, and is always set to one.
        /// </summary>
        public readonly uint ModuleID;
        /// <summary>
        /// The identifier of the process whose modules are to be examined.
        /// </summary>
        public readonly uint ProcessID;
        /// <summary>
        /// The load count of the module, which is not generally meaningful, and usually equal to 0xFFFF.
        /// </summary>
        public readonly uint GlblcntUsage;
        /// <summary>
        /// The load count of the module (same as GlblcntUsage), which is not generally meaningful, and usually equal to 0xFFFF.
        /// </summary>
        public readonly uint ProccntUsage;
        /// <summary>
        /// The base address of the module in the context of the owning process.
        /// </summary>
        public readonly IntPtr BaseAddr;
        /// <summary>
        /// The size of the module, in bytes.
        /// </summary>
        public readonly uint BaseSize;
        /// <summary>
        /// A handle to the module in the context of the owning process.
        /// </summary>
        public readonly IntPtr Module;
        /// <summary>
        /// The module name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public readonly string ModuleName;
        /// <summary>
        /// The module path.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public readonly string ExePath;
        public Moduleentry32(uint Size, uint ModuleID, uint ProcessID, uint GlblcntUsage, uint ProccntUsage, IntPtr BaseAddr, uint BaseSize, IntPtr Module, string ModuleName, string ExePath)
            => (this.Size, this.ModuleID, this.ProcessID, this.GlblcntUsage, this.ProccntUsage, this.BaseAddr, this.BaseSize, this.Module, this.ModuleName, this.ExePath)
            = (Size, ModuleID, ProcessID, GlblcntUsage, ProccntUsage, BaseAddr, BaseSize, Module, ModuleName, ExePath);

        public override bool Equals(object obj) => obj is Moduleentry32 entry32 && Equals(entry32);

        public bool Equals(Moduleentry32 other)
            => Size == other.Size &&
                   ModuleID == other.ModuleID &&
                   ProcessID == other.ProcessID &&
                   GlblcntUsage == other.GlblcntUsage &&
                   ProccntUsage == other.ProccntUsage &&
                   EqualityComparer<IntPtr>.Default.Equals(BaseAddr, other.BaseAddr) &&
                   BaseSize == other.BaseSize &&
                   EqualityComparer<IntPtr>.Default.Equals(Module, other.Module) &&
                   ModuleName == other.ModuleName &&
                   ExePath == other.ExePath;

        public override int GetHashCode()
        {
            var hashCode = 1085485287;
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            hashCode = hashCode * -1521134295 + ModuleID.GetHashCode();
            hashCode = hashCode * -1521134295 + ProcessID.GetHashCode();
            hashCode = hashCode * -1521134295 + GlblcntUsage.GetHashCode();
            hashCode = hashCode * -1521134295 + ProccntUsage.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(BaseAddr);
            hashCode = hashCode * -1521134295 + BaseSize.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(Module);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ModuleName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ExePath);
            return hashCode;
        }

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
        public static bool operator ==(Moduleentry32 moduleentry1, Moduleentry32 moduleentry2) => moduleentry1.Equals(moduleentry2);
        public static bool operator !=(Moduleentry32 moduleentry1, Moduleentry32 moduleentry2) => !(moduleentry1 == moduleentry2);
    }
}
