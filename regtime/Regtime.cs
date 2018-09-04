using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using regtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace regtime
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentProcessId();
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);
        [DllImport("kernel32.dll")]
        public static extern bool Process32First(SafeFileHandle hSnapshot, ref Processentry32 lppe);
        [DllImport("kernel32.dll")]
        public static extern bool Process32Next(SafeFileHandle hSnapshot, ref Processentry32 lppe);
        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer, uint nSize, IntPtr Arguments);
        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer, uint nSize, string[] Arguments);
        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(FormatMessageFlags dwFlags, string[] lpSource, int dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer, uint nSize, string[] Arguments);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegConnectRegistry(string lpmachineName, HandleKey hKey, out UIntPtr phKResult);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx( UIntPtr hKey, string subKey, uint ulOptions, KeyOptions samDesired, out UIntPtr hkResult);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey( UIntPtr hKey);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int RegQueryInfoKey(UIntPtr hKey, [Out()]StringBuilder lpClass, ref uint lpcchClass,
           IntPtr lpReserved, out uint lpcSubkey, out uint lpcchMaxSubkeyLen,
           out uint lpcchMaxClassLen, out uint lpcValues, out uint lpcchMaxValueNameLen,
           out uint lpcbMaxValueLen, IntPtr lpSecurityDescriptor, IntPtr lpftLastWriteTime);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int RegQueryInfoKey(UIntPtr hKey, IntPtr lpClass, IntPtr lpcchClass,
           IntPtr lpReserved, IntPtr lpcSubkey, IntPtr lpcchMaxSubkeyLen,
           IntPtr lpcchMaxClassLen, IntPtr lpcValues, IntPtr lpcchMaxValueNameLen,
           IntPtr lpcbMaxValueLen, IntPtr lpSecurityDescriptor, IntPtr lpftLastWriteTime);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCommandLine();
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr CommandLineToArgvW(
           [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
           out int pNumArgs);
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr CommandLineToArgv(
           IntPtr lpCmdLine,
           out int pNumArgs);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalFree(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum nCmdShow);
    }
    public enum ShowWindowEnum : int
    {
        /// <summary>
        /// Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        /// <remarks>SW_FORCEMINIMIZE</remarks>
        Forceminimize = 11,
        /// <summary>
        /// Hides the window and activates another window. 
        /// </summary>
        /// <remarks>SW_HIDE</remarks>
        Hide = 0,
        /// <summary>
        /// Maximizes the specified window. 
        /// </summary>
        /// <remarks>SW_MAXIMIZE</remarks>
        Maximize = 3,
        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the Z order. 
        /// </summary>
        /// <remarks>SW_MINIMIZE</remarks>
        Minimize = 6,
        /// <summary>
        /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window. 
        /// </summary>
        /// <remarks>SW_RESTORE</remarks>
        Restore = 9,
        /// <summary>
        /// Activates the window and displays it in its current size and position. 
        /// </summary>
        /// <remarks>SW_SHOW</remarks>
        Show = 5,
        /// <summary>
        /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application. 
        /// </summary>
        /// <remarks>SW_SHOWDEFAULT</remarks>
        Showdefault = 10,
        /// <summary>
        /// Activates the window and displays it as a maximized window. 
        /// </summary>
        /// <remarks>SW_SHOWMAXIMIZED</remarks>
        Showmaximized = 3,
        /// <summary>
        /// Activates the window and displays it as a minimized window. 
        /// </summary>
        /// <remarks>SW_SHOWMINIMIZED</remarks>
        Showminimized = 2,
        /// <summary>
        /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated. 
        /// </summary>
        /// <remarks>SW_SHOWMINNOACTIVE</remarks>
        Showminnoactive = 7,
        /// <summary>
        /// Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated. 
        /// </summary>
        /// <remarks>SW_SHOWNA</remarks>
        Showna = 8,
        /// <summary>
        /// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated. 
        /// </summary>
        /// <remarks>SW_SHOWNOACTIVATE</remarks>
        Shownoactivate = 4,
        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time. 
        /// </summary>
        /// <remarks>SW_SHOWNORMAL</remarks>
        Shownormal = 1,
    }
    [Flags]
    public enum SnapshotFlags : uint
    {
        HeapList = 0x00000001,
        Process = 0x00000002,
        Thread = 0x00000004,
        Module = 0x00000008,
        Module32 = 0x00000010,
        Inherit = 0x80000000,
        All = 0x0000001F,
        NoHeaps = 0x40000000
    }
    public enum HandleKey : uint
    {
        ClassesRoot = 0x80000000,
        CurrentUser = 0x80000001,
        LocalMachine = 0x80000002,
        Users = 0x80000003,
        PerformanceData = 0x80000004,
        CurrentConfig = 0x80000005,
        DynData = 0x80000006,
    }
    [Flags]
    public enum KeyOptions : uint
    {
        /// <summary>
        /// KEY_QUERY_VALUE
        /// </summary>
        QueryValue = 1 << 0,
        /// <summary>
        /// KEY_SET_VALUE
        /// </summary>
        SetValue = 1 << 1,
        /// <summary>
        /// KEY_CREATE_SUB_KEY
        /// </summary>
        CreateSubKey = 1 << 2,
        /// <summary>
        /// KEY_ENUMERATE_SUB_KEYS
        /// </summary>
        EnumerateSubKeys = 1 << 3,
        /// <summary>
        /// KEY_NOTIFY
        /// </summary>
        Notify = 1 << 4,
        /// <summary>
        /// KEY_CREATE_LINK
        /// </summary>
        CreateLink = 1 << 5,
        /// <summary>
        /// KEY_WOW64_64KEY
        /// </summary>
        Wow64_64Key = 1 << 8,
        /// <summary>
        /// KEY_WOW64_32KEY
        /// </summary>
        Wow64_32Key = 1 << 9,
        /// <summary>
        /// KEY_WRITE
        /// </summary>
        Write = 0x20006,
        /// <summary>
        /// KEY_EXECUTE
        /// </summary>
        Execute = 0x20019,
        /// <summary>
        /// KEY_READ
        /// </summary>
        Read = 0x20019,
        /// <summary>
        /// KEY_ALL_ACCESS
        /// </summary>
        AllAccess = 0xf003f,
    }
    public enum RegWow64Options : uint
    {
        None = 0,
        /// <summary>
        /// KEY_WOW64_64KEY
        /// </summary>
        KEY_WOW64_64KEY = 0x0100,
        /// <summary>
        /// KEY_WOW64_32KEY
        /// </summary>
        KEY_WOW64_32KEY = 0x0200
    }
    [Flags]
    internal enum FormatMessageFlags :uint
    {
        AllocateBuffer = 0x00000100,
        IgnoreInserts = 0x00000200,
        FromSystem = 0x00001000,
        ArgumentArray = 0x00002000,
        FromHmodule = 0x00000800,
        FromString = 0x00000400,
    }
    readonly struct Options
    {
        public readonly bool AsCUI;
        public bool IsCUI => AsCUI;
        public readonly bool ShowHelp;
        public readonly bool DispKey;
        public readonly string Machine;
        public readonly bool DispAsGMT;
        public readonly bool DispAsUnicode;
        public Options(bool AsCUI, bool ShowHelp, bool DispKey, string Machine, bool DispAsGMT, bool DispAsUnicode)
            => (this.AsCUI, this.ShowHelp, this.DispKey, this.Machine, this.DispAsGMT, this.DispAsUnicode)
            = (AsCUI, ShowHelp, DispKey, Machine, DispAsGMT, DispAsUnicode);
        public Options Set(bool? AsCUI = null, bool? ShowHelp = null, bool? DispKey = null, string Machine = null, bool? DispAsGMT = null, bool? DispAsUnicode = null)
            => AsCUI == null && ShowHelp == null && DispKey == null && Machine == null && DispAsGMT == null && DispAsUnicode == null ? this
            : new Options(AsCUI ?? this.AsCUI, ShowHelp ?? this.ShowHelp, DispKey ?? this.DispKey, Machine ?? this.Machine, DispAsGMT ?? this.DispAsGMT, DispAsUnicode ?? this.DispAsUnicode);
        public void Deconstruct(out bool AsCUI, out bool ShowHelp, out bool DispKey, out string Machine, out bool DispAsGMT, out bool DispAsUnicode)
            => (AsCUI, ShowHelp, DispKey, Machine, DispAsGMT, DispAsUnicode)
            = (this.AsCUI, this.ShowHelp, this.DispKey, this.Machine, this.DispAsGMT, this.DispAsUnicode);
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Processentry32
    {
        public uint Size;
        public uint Usage;
        public uint ProcessID;
        public IntPtr DefaultHeapID;
        public uint ModuleID;
        public uint ThreadsCount;
        public uint ParentProcessID;
        public int PriClassBase;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    };
    class Regtime
    {
        internal static Action<TextWriter, string> my_puts;
        internal static TextWriter Out = null;
        internal static TextWriter Error = null;

        public static int MakeLangId(int primary, int sub) => (((ushort)sub) << 10) | ((ushort)primary);
        public static int PrimaryLangId(int lcid) => ((ushort)lcid) & 0x3ff;
        public static int SubLangId(int lcid) => ((ushort)lcid) >> 10;
        static uint GetParentProcessName(out string ParentName)
        {
            ParentName = null;
            Processentry32 pe32 = default;
            var ppid = 0u;
            var r = 0u;

            var pid = NativeMethods.GetCurrentProcessId();

            using (var hSnapshot = NativeMethods.CreateToolhelp32Snapshot(SnapshotFlags.Process, 0))
            {
                if (hSnapshot.IsInvalid)
                    return r;
                pe32.Size = (uint)Marshal.SizeOf<Processentry32>();
                if (!NativeMethods.Process32First(hSnapshot, ref pe32))
                    return r;

                // find my process and get parent's pid
                do
                {
                    if (pe32.ParentProcessID == pid)
                    {
                        pid = pe32.ParentProcessID;
                        break;
                    }
                } while (NativeMethods.Process32Next(hSnapshot, ref pe32));

                if (ppid == 0)
                    return r; // not found

                // rewind
                pe32 = default;
                pe32.Size = (uint)Marshal.SizeOf<Processentry32>();
                if (!NativeMethods.Process32First(hSnapshot, ref pe32))
                    return r;
                // find parrent process and get process name
                do
                {
                    if (pe32.ProcessID == ppid)
                    {
                        ParentName = pe32.szExeFile;
                        r = (uint)ParentName.Length;
                        break;
                    }
                } while (NativeMethods.Process32Next(hSnapshot, ref pe32));
            }
            return r;


        }
        /* return TRUE if parent process is CMD.EXE */
        static bool IsCommandLine()
        {
            uint r = GetParentProcessName(out var ParentName);
            if (!string.IsNullOrEmpty(ParentName))
            {
                var SearchSymbol = new[] { '\\', '/' };
                var index = 0;
                do
                {
                    if (string.Compare( ParentName, "cmd.exe", true) == 0)
                    {
                        return true;
                    }
                    index = ParentName.IndexOfAny(SearchSymbol);
                    if (index < 0)
                        break;
                    ParentName = ParentName.Substring(index + 1);
                } while (index >= 0);
            }
            return false;
        }
        static void my_putsG(TextWriter h, string s)
        {
            System.Windows.Forms.MessageBoxIcon n = h == Regtime.Error && h != null
                ? System.Windows.Forms.MessageBoxIcon.Error
                : System.Windows.Forms.MessageBoxIcon.Information;
            System.Windows.Forms.MessageBox.Show(s, "regtime", System.Windows.Forms.MessageBoxButtons.OK, n
                , System.Windows.Forms.MessageBoxDefaultButton.Button1
                , System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

        }

        // Console, ANSI
        static void my_putsCA(TextWriter h, string s)
        {
            h.WriteLine(s);
        }

        // Console, Wide
        static void my_putsCW(TextWriter h, string s)
        {
            h.WriteLine(s);
        }

        static void my_printf(TextWriter h, params string[] lpszFormat)
        {
            var buf = new StringBuilder(256);
            var r = NativeMethods.FormatMessage(
                    FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem,
                    lpszFormat,
                    0, 0, buf, 0,
                    lpszFormat);
            if (0 < r) my_puts(h, buf.ToString());
        }

        static void ShowError(int dwErrorCode, string s = null)
        {
            uint r;
            const int LangNeutral = 0x00;
            const int SubLangDefault = 0x01;
            var lang = (uint)MakeLangId(LangNeutral, SubLangDefault);
            var message = new StringBuilder(255);
            r = NativeMethods.FormatMessage(
                FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem,
                IntPtr.Zero, dwErrorCode, lang,
                message, (uint)message.Capacity, IntPtr.Zero);
            if (0 < r)
            {
                if (s != null)
                {
                    IntPtr formatPtr = Marshal.StringToHGlobalAnsi("%1!s!:%2!s!");
                    var _message = new StringBuilder(255);
                    var pArgs = new []{ s, message.ToString() };
                    r = NativeMethods.FormatMessage(
                        FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromString | FormatMessageFlags.ArgumentArray,
                        formatPtr,
                        0,
                        0,
                        _message,
                        (uint)_message.Capacity,
                        pArgs);
                    if (0 < r)
                    {
                        my_puts(Error, _message.ToString());
                    }
                    else
                    {
                        my_puts(Error, message.ToString());
                    }
                }
                else
                {
                    my_puts(Error, message.ToString());
                }
            }
            else
            {
                my_puts(Error, s != null ? $"Error:{dwErrorCode}\n{s}" : $"Error:{dwErrorCode}");
            }
        }

        static void ShowHelp()
        {
            my_puts(Out, "\nregtime - show timestamp of registry key.\n\n"
                + "Usage:  regtime [-c|-w] [-k] [\\\\machine] name-of-RegistryKey\n\n"
                + "        -c : run as console program.\n"
                + "        -w : run as GUI program.\n"
                + "        -k : display registry key name prior to  timestamp.\n"
                + "        -g : display timestamp as GMT.\n"
                + "        -u : display with Unicode.\n");

            return;
        }

        static bool StringToHive(string lpszRegKey, out HandleKey phkHive, out string lplpszKey)
        {
            var sh = new Dictionary<string, HandleKey>{
                { "HKEY_CLASSES_ROOT\\", HandleKey.ClassesRoot },
                { "HKEY_CURRENT_USER\\", HandleKey.CurrentUser },
                { "HKEY_LOCAL_MACHINE\\", HandleKey.LocalMachine },
                { "HKEY_USERS\\", HandleKey.Users },
                { "HKEY_PERFORMANCE_DATA\\", HandleKey.PerformanceData },
                { "HKEY_CURRENT_CONFIG\\", HandleKey.CurrentConfig },
                { "HKCR\\", HandleKey.CurrentConfig },
                { "HKCU\\", HandleKey.CurrentUser },
                { "HKLM\\", HandleKey.LocalMachine },
                { "HKU\\", HandleKey.Users },
            };
            foreach(var (key, regKey) in sh)
            {
                var index = lpszRegKey.IndexOf(key);
                if (index == 0)
                {
                    phkHive = regKey;
                    lplpszKey = key;
                    return true;
                }
            }
            phkHive = default;
            lplpszKey = default;
            return false;

        }

        static bool GetRegTimestamp(string lpszMachine, string lpszRegKey, out System.Runtime.InteropServices.ComTypes.FILETIME Time)
        {
         
            if (!StringToHive(lpszRegKey,out var hHive, out var lpszKey))
            {
                throw new ArgumentException();
            }
            const int ERROR_SUCCESS = 0;
            var r = NativeMethods.RegConnectRegistry(lpszMachine, hHive, out var hReg);
            if (r != ERROR_SUCCESS)
            {
                ShowError(r, lpszMachine ?? "RegConnectRegistry");
                Time = default;
                return false;
            }
            using (Disposable.Create(() => NativeMethods.RegCloseKey(hReg)))
            {
                r = NativeMethods.RegOpenKeyEx(hReg, lpszKey, 0, KeyOptions.Read, out var hSubKey);
                if (r != ERROR_SUCCESS)
                {
                    ShowError(r, lpszKey);
                    Time = default;
                    return false;
                }
                using (Disposable.Create(() => NativeMethods.RegCloseKey(hSubKey)))
                {
                    var Size = Marshal.SizeOf<System.Runtime.InteropServices.ComTypes.FILETIME>();
                    var TimePtr = Marshal.AllocCoTaskMem(Size);
                    using (Disposable.Create(() => Marshal.FreeCoTaskMem(TimePtr)))
                    {
                        r = NativeMethods.RegQueryInfoKey(hSubKey, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, TimePtr);
                        Time = (System.Runtime.InteropServices.ComTypes.FILETIME)Marshal.PtrToStructure(TimePtr, typeof(System.Runtime.InteropServices.ComTypes.FILETIME));

                        if (r != ERROR_SUCCESS)
                        {
                            ShowError(r, lpszKey);
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        static bool PutTimestamp(string lpszKey, System.Runtime.InteropServices.ComTypes.FILETIME Time, Options lpOpt )
        {
            var _Time = unchecked((long)(((ulong)Time.dwHighDateTime) << 32) | (uint)Time.dwLowDateTime);
            var __Time = lpOpt.DispAsGMT ? DateTime.FromFileTimeUtc(_Time)
                : DateTime.FromFileTime(_Time);
            my_printf(Out, $"{__Time:yyyy/MM/dd}");
            return true;
        }
        static string GetCommandLine() => NativeMethods.GetCommandLine() is IntPtr Line ? Marshal.PtrToStringAuto(Line) : null;
        static IEnumerable<string> CommandLineToArgv(IntPtr CommandLine) {
            var Ptr = NativeMethods.CommandLineToArgv(CommandLine, out var NumArgs);
            if (Ptr == IntPtr.Zero)
                return Enumerable.Empty<string>();
            IEnumerable<string> ReadStrings()
            {
                using (Disposable.Create(() => NativeMethods.LocalFree(Ptr)))
                    foreach (var i in Enumerable.Range(0, NumArgs))
                        yield return Marshal.PtrToStringAuto(Marshal.ReadIntPtr(Ptr, i * IntPtr.Size));
            }
            return ReadStrings();
        }
        static int Main(string[] args)
        {
            //int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow )
            Options opt = default;
    
            opt = opt.Set(AsCUI: IsCommandLine());
            var CommandLine = NativeMethods.GetCommandLine();
            var path = new List<string>();
            if (CommandLine != IntPtr.Zero)
            {
                var lplpszArgs = CommandLineToArgv(CommandLine).ToArray();
                var ShowHelp = false;
                var AsCUI = false;
                var DispKey = false;
                var DispAsGMT = false;
                var DispAsUnicode = false;
                string Machine = null;
                foreach (var arg in lplpszArgs)
                {
                    if (arg[0] == '-' || arg[0] == '/')
                    {
                        foreach (var p in arg)
                        {
                            switch (p)
                            {
                                case 'h':
                                case 'H':
                                case '?':
                                    ShowHelp = true;
                                    break;
                                case 'c':
                                case 'C':
                                    AsCUI = true;
                                    break;
                                case 'w':
                                case 'W':
                                    AsCUI = false;
                                    break;
                                case 'k':
                                case 'K':
                                    DispKey = true;
                                    break;
                                case 'g':
                                case 'G':
                                    DispAsGMT = true;
                                    break;
                                case 'u':
                                case 'U':
                                    DispAsUnicode = true;
                                    break;
                            }
                        }
                    }
                    else if (arg[0] == '\\' && arg[1] == '\\')
                        Machine = arg;
                    else
                        path.Add(arg);
                }
                opt = opt.Set(AsCUI: AsCUI, ShowHelp: ShowHelp, DispKey: DispKey, Machine: Machine, DispAsGMT: DispAsGMT, DispAsUnicode: DispAsUnicode);
            }
            else
            {
                return -1;
            }
            if (!path.Any()) opt = opt.Set(ShowHelp: true);

            if (opt.AsCUI)
            {
                /*
		        if( !AttachConsole( ATTACH_PARENT_PROCESS ) ){
			        MessageBox( 0, _T("cannot attach to console."), _T("regtime"), MB_OK | MB_ICONSTOP );
			        return -1;
		        }
		        hStdout = CreateFile( _T("CONOUT$"), GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, 0);
		        hStderr = CreateFile( _T("CONOUT$"), GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, 0);
		        my_puts = my_putsC;
		        */
                Out = Console.Out;
                Error = Console.Error;
                if (opt.DispAsUnicode) my_puts = my_putsCW;
                else my_puts = my_putsCA;
            }
            else
            {
                Error = Console.Error;
                Out = Console.Out;
                my_puts = my_putsG;
            }

            if (opt.ShowHelp)
            {
                ShowHelp();
                return 0;
            }
            if (!opt.IsCUI)
                NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), ShowWindowEnum.Hide);

            foreach(var p in path)
            {
                if (string.IsNullOrEmpty(p))
                    continue;
                if (GetRegTimestamp(opt.Machine, p, out var ft))
                {
                    PutTimestamp(p, ft, opt);
                }
            }
            return 0;
        }
        static RegistryKey PointerToRegistryKey(IntPtr hKey, bool Writable, bool ownsHandle)
        {
            // Create a SafeHandles.SafeRegistryHandle from this pointer - this is a private class
            var privateConstructors = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
            var safeRegistryHandleType = typeof(Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid).Assembly.GetType("Microsoft.Win32.SafeHandles.SafeRegistryHandle");
            var safeRegistryHandleConstructorTypes = new[] { typeof(IntPtr), typeof(bool) };
            var safeRegistryHandleConstructor = safeRegistryHandleType.GetConstructor(privateConstructors, null, safeRegistryHandleConstructorTypes, null);
            var safeHandle = safeRegistryHandleConstructor.Invoke(new object[] { hKey, ownsHandle });
            // Create a new Registry key using the private constructor using the safeHandle - this should then behave like a .NET natively opened handle and disposed of correctly
            var registryKeyType = typeof(RegistryKey);
            var registryKeyConstructorTypes = new[] { safeRegistryHandleType, typeof(bool) };
            var registryKeyConstructor = registryKeyType.GetConstructor(privateConstructors, null, registryKeyConstructorTypes, null);
            return (RegistryKey)registryKeyConstructor.Invoke(new object[] { safeHandle, Writable });
        }
    }
    static class KeyValuePairExtentsions
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> self, out T1 Key, out T2 Value)
            => (Key, Value) = (self.Key, self.Value);
    }
    internal class Disposable : IDisposable
    {
        Action Action;
        public static Disposable Create(Action Action) => new Disposable(Action);
        Disposable(Action Action)
            => this.Action = Action ?? throw new ArgumentNullException(nameof(Action));
        public void Dispose()
        {
            try
            {
                Action?.Invoke();
            }
            catch {
                // noop
            }
            finally {
                Action = null;
            }
        }
    }
}

