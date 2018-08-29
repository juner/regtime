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
        static extern uint FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, [Out] StringBuilder lpBuffer,
   uint nSize, IntPtr Arguments);
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
    internal enum FormatMessageFlags :uint
    {
        AllocateBuffer = 0x00000100,
        IgnoreInserts = 0x00000200,
        FromSystem = 0x00001000,
        ArgumentArray = 0x00002000,
        FromHmodule = 0x00000800,
        FromString = 0x00000400,
    }
    struct Options
    {
        public bool AsCUI;
        public bool IsCUI;
        public bool ShowHelp;
        public bool DispKey;
        public string Machine;
        public bool DispAsGMT;
        public bool DispAsUnicode;
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
        internal static Func<TextWriter, string> my_puts;
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
        void my_putsG(TextWriter h, string s)
        {
            System.Windows.Forms.MessageBoxIcon n = h == Regtime.Error && h != null
                ? System.Windows.Forms.MessageBoxIcon.Error
                : System.Windows.Forms.MessageBoxIcon.Information;
            System.Windows.Forms.MessageBox.Show(s, "regtime", System.Windows.Forms.MessageBoxButtons.OK, n
                , System.Windows.Forms.MessageBoxDefaultButton.Button1
                , System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

        }

        // Console, ANSI
        void my_putsCA(TextWriter h, string s)
        {
            h.WriteLine(s);
        }

        // Console, Wide
        void my_putsCW(TextWriter h, string s)
        {
            h.WriteLine(s);
        }

        void my_printf(TextWriter h, params string[] lpszFormat)
        {
            va_list ap;
            LPTSTR buf;
            DWORD r;

            va_start(ap, lpszFormat);
            r = NativeMethods.FormatMessage(
                    FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem,
                    (LPCVOID)lpszFormat,
                    0, 0, (LPTSTR) & buf, 0,
                    &ap);
            if (r) my_puts(h, buf);

            va_end(ap);
            LocalFree(buf);
        }

        void ShowError(uint dwErrorCode, string s)
        {
            LPVOID p1 = NULL, p2 = NULL;
            DWORD r;
            TCHAR buf[1024];
            DWORD_PTR pArgs[2];

            __try{
                r = NativeMethods.FormatMessage(
                    FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem,
                    IntPtr.Zero, dwErrorCode, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
                    (LPTSTR) & p1, 0, NULL);
                if (r)
                {
                    if (s)
                    {
                        IntPtr formatPtr = Marshal.StringToHGlobalAnsi("%1!s!:%2!s!");
                        pArgs[0] = (DWORD_PTR)s;
                        pArgs[1] = (DWORD_PTR)p1;
                        r = NativeMethods.FormatMessage(
                            FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromString | FormatMessageFlags.ArgumentArray,
                            fomratPtr,
                            0,
                            0,
                            (LPTSTR) & p2,
                            0,
                            (va_list*)pArgs);
                        if (r)
                        {
                            my_puts(Error, (LPCTSTR)p2);
                        }
                        else
                        {
                            my_puts(hStderr, (LPCTSTR)p1);
                        }
                    }
                    else
                    {
                        my_puts(hStderr, (LPCTSTR)p1);
                    }
                }
                else
                {
                    StringCchPrintf(buf, _countof(buf), s ? _T("Error:%lu\n%s\n") : _T("Error:%lu\n"), dwErrorCode, s);
                    my_puts(hStderr, buf);
                }
            }
            __finally{
                if (p1) LocalFree(p1);
                if (p2) LocalFree(p2);
            }
        }

        void ShowHelp()
        {
            var s =
                "\nregtime - show timestamp of registry key.\n\n"
                + "Usage:  regtime [-c|-w] [-k] [\\\\machine] name-of-RegistryKey\n\n"
                + "        -c : run as console program.\n"
                + "        -w : run as GUI program.\n"
                + "        -k : display registry key name prior to  timestamp.\n"
                + "        -g : display timestamp as GMT.\n"
                + "        -u : display with Unicode.\n";
            my_puts(Out, s);

            return;
        }

        bool StringToHive(string lpszRegKey, out HandleKey phkHive, out string lplpszKey)
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

        bool GetRegTimestamp(string lpszMachine, string lpszRegKey, out System.Runtime.InteropServices.ComTypes.FILETIME lpTime)
        {
         
            if (!StringToHive(lpszRegKey,out var hHive, out var lpszKey))
            {
                throw new ArgumentException();
            }

            __try{
                r = RegConnectRegistry(lpszMachine, hHive, &hReg);
                if (r != ERROR_SUCCESS)
                {
                    ShowError(r, lpszMachine ? lpszMachine : _T("RegConnectRegistry"));
                    __leave;
                }

                r = RegOpenKeyEx(hReg, lpszKey, 0, KEY_READ, &hSubKey);
                if (r != ERROR_SUCCESS)
                {
                    ShowError(r, lpszKey);
                    __leave;
                }

                r = RegQueryInfoKey(hSubKey, NULL, NULL, NULL,
                        NULL, NULL, NULL, NULL, NULL, NULL, NULL, lpTime);
                if (r != ERROR_SUCCESS)
                {
                    ShowError(r, lpszKey);
                    __leave;
                }

                Result = TRUE;
            }
            __finally{
                if (hSubKey != NULL) RegCloseKey(hSubKey);
                if (hReg != NULL) RegCloseKey(hReg);
            }
            return Result;
        }

        bool PutTimestamp(string lpszKey, System.Runtime.InteropServices.ComTypes.FILETIME Time, Options lpOpt )
        {
            System.Runtime.InteropServices.ComTypes.FILETIME ftLocal;
            DateTime stTime;

            long _Time = (((long)Time.dwHighDateTime) << 32) | Time.dwLowDateTime;

            Time = lpOpt.DispAsGMT ? DateTime.FromFileTimeUtc(_Time)
                : DateTime.FromFileTime(_Time);

	        my_printf(hStdout,
                    lpOpt->DispKey?
                    _T("%8!s! - %1!4.4d!-%2!2.2d!-%3!2.2d! %4!2.2d!:%5!2.2d!:%6!2.2d!.%7!3.3d!\n") :

                    _T("%1!4.4d!-%2!2.2d!-%3!2.2d! %4!2.2d!:%5!2.2d!:%6!2.2d!.%7!3.3d!\n"),
			        stTime.wYear,
			        stTime.wMonth,
			        stTime.wDay,
			        stTime.wHour,
			        stTime.wMinute,
			        stTime.wSecond,
			        stTime.wMilliseconds,
			        lpszKey
			        );
	        return TRUE;

        }
        static void Main(string[] args)
        {
            //int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow )
            int nArgs;
            string lplpszArgs;
            int i, n = 0;
            string p;
            System.Runtime.InteropServices.ComTypes.FILETIME ft;
            Options opt;
    
            opt.Machine = null;

            opt.IsCUI = opt.AsCUI = IsCommandLine();
            lplpszArgs = CommandLineToArgvW(GetCommandLineW(), &nArgs);
            if (lplpszArgs)
            {
                for (i = 1; i < nArgs; i++)
                {
                    p = lplpszArgs[i];
                    if (*p == _T('-') || *p == _T('/'))
                    {
                        *p = '\0';
                        p++;
                        while (*p)
                        {
                            switch (*p)
                            {
                                case _T('h'):
                                case _T('H'):
                                case _T('?'):
                                    opt.ShowHelp = TRUE;
                                    break;
                                case _T('c'):
                                case _T('C'):
                                    opt.AsCUI = TRUE;
                                    break;
                                case _T('w'):
                                case _T('W'):
                                    opt.AsCUI = FALSE;
                                    break;
                                case _T('k'):
                                case _T('K'):
                                    opt.DispKey = TRUE;
                                    break;
                                case _T('g'):
                                case _T('G'):
                                    opt.DispAsGMT = TRUE;
                                    break;
                                case _T('u'):
                                case _T('U'):
                                    opt.DispAsUnicode = TRUE;
                                    break;
                            }
                            p++;
                        }
                    }
                    else if (*p == '\\' && *(p + 1) == '\\')
                    {
                        opt.Machine = lplpszArgs[i];
                        lplpszArgs[i] = NULL;
                    }
                    else
                    {
                        n++;
                    }
                }
            }
            else
            {
                return -1;
            }
            if (n == 0) opt.ShowHelp = TRUE;

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
                hStdout = GetStdHandle(STD_OUTPUT_HANDLE);
                hStderr = GetStdHandle(STD_ERROR_HANDLE);
                if (opt.DispAsUnicode) my_puts = my_putsCW;
                else my_puts = my_putsCA;
            }
            else
            {
                hStderr = GetStdHandle(STD_ERROR_HANDLE);
                hStdout = GetStdHandle(STD_OUTPUT_HANDLE);
                my_puts = my_putsG;
            }

            if (opt.ShowHelp)
            {
                ShowHelp();
                return 0;
            }
            if (!opt.IsCUI)
                ShowWindow(GetConsoleWindow(), SW_HIDE);

            for (i = 1; i < nArgs; i++)
            {
                p = lplpszArgs[i];
                if (!p || !*p) continue;
                if (GetRegTimestamp(opt.Machine, p, &ft))
                {
                    PutTimestamp(p, &ft, &opt);
                }
            }
            if (lplpszArgs) LocalFree(lplpszArgs);

            return 0;
}
    }
    static class KeyValuePairExtentsions
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> self, out T1 Key, out T2 Value)
            => (Key, Value) = (self.Key, self.Value);
    }
}

