using System.Runtime.InteropServices;
using System;
using System.Text;
namespace WinAPI
{
    //WARNING: TOO MUCH is currently included, but that is intended to allow me to continue to build without
    //having to research APIs every two seconds.
    public class API
    {

        #region Send Messages
        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Auto)]
        public static extern long SendMessageStr(IntPtr hwnd, int wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref IntPtr lParam);

        #region "SendMessage(IntPtr hWnd, int Msg, ref int wParam, ref int lParam)"

        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessageTimeoutIntPtr(IntPtr hWnd, int Msg, ref int wParam, ref int lParam, int fuFlags, int uTimeout, out IntPtr lpdwResult);

        public static IntPtr SendMessage(IntPtr hWnd, int Msg, ref int wParam, ref int lParam)
        {
            IntPtr x;
            //return SendMessageTimeout(hWnd, Msg, wParam, lParam, SMTO_ABORTIFHUNG, 10000, out x);
            if (SendMessageTimeoutIntPtr(hWnd, Msg, ref wParam, ref lParam, SMTO_ABORTIFHUNG, 1000, out x) == false)
            {
                throw new Exception("Unable to communicate with the Windows Object.");
            }
            return x;
        }

        #endregion

        #region "SendMessageTimeoutInt(IntPtr hWnd, int Msg, ref int wParam, ref int lParam)"

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessageTimeout(IntPtr hWnd, int Msg, ref int wParam, ref int lParam, int fuFlags, int uTimeout, out int lpdwResult);

        public static int SendMessageTimeoutInt(IntPtr hWnd, int Msg, ref int wParam, ref int lParam)
        {
            int returnval;
            if (SendMessageTimeout(hWnd, Msg, ref wParam, ref lParam, SMTO_ABORTIFHUNG, 1000, out returnval) == false)
            {
                throw new Exception("Unable to communicate with the Windows Object.");
            }
            return returnval;
        }
        #endregion

        #region "SendMessageTimeout(IntPtr hWnd, int Msg, ref int wParam, ref System.Text.StringBuilder lParam)"

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, System.Text.StringBuilder lParam, int fuFlags, int uTimeout, out int lpdwResult);

        public static int SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, System.Text.StringBuilder lParam)
        {
            int x;
            if (SendMessageTimeout(hWnd, Msg, wParam, lParam, SMTO_ABORTIFHUNG, 10000, out x) == false)
            {
                throw new Exception("Unable to communicate with the Windows Object.");
            }

            return x;
        }
        #endregion

        /////////////////////// SendMessageTimeout

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, string lParam, int fuFlags, int uTimeout, out int lpdwResult);

        public static bool SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, string lParam)
        {
            int x;
            return SendMessageTimeout(hWnd, Msg, wParam, lParam, SMTO_ABORTIFHUNG, 10000, out x);
        }

        public static bool SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, string lParam, out int returnval)
        {
            return SendMessageTimeout(hWnd, Msg, wParam, lParam, SMTO_ABORTIFHUNG, 10000, out returnval);
        }


        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        //public static extern IntPtr SendMessageByRef(IntPtr hWnd, int Msg, ref int wParam, ref int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref IntPtr lParam);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        //public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessageStr(HandleRef hWnd, int Msg, IntPtr wParam, String lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, int wparam, System.Text.StringBuilder lparam);

        #endregion

        // '''''''''''''''''''''''''''''''''''''''''''''''''''
        // Constant
        // '''''''''''''''''''''''''''''''''''''''''''''''''''
        public const int FLASHW_STOP = 0;

        // Stop flashing. The system restores the window to its original state.
        public const int FLASHW_CAPTION = 1;

        // Flash the window caption.
        public const int FLASHW_TRAY = 2;

        // Flash the taskbar button.
        //public const int FLASHW_ALL = (FLASHW_CAPTION || FLASHW_TRAY);

        public const int FLASHW_TIMER = 4;

        // Flash continuously, until the FLASHW_STOP flag is set.
        public const int FLASHW_TIMERNOFG = 12;

        public const int HORZRES = 8;

        public const int VERTRES = 10;

        public const int SRCCOPY = 13369376;

        public const int SRCINVERT = 6684742;

        public const int USE_SCREEN_WIDTH = -1;

        public const int USE_SCREEN_HEIGHT = -1;

        public const int BM_SETSTATE = 243;

        public const int BM_CLICK = 245;

        public const int BM_SETCHECK = 241;//&HF1;

        public const int BM_GETCHECK = 240;//&HF0;

        public const int BN_CLICKED = 245;

        //used for check boxes
        public const int BST_UNCHECKED = 0;
        //used for check boxes
        public const int BST_CHECKED = 1;
        //used for check boxes
        public const int BST_INDETERMINATE = 2;

        // keyboard
        public const int KEYEVENTF_EXTENDEDKEY = 1;

        public const int KEYEVENTF_KEYUP = 2;

        // gw As Integer = Get Window function
        public const int GW_CHILD = 5;

        public const int GW_HWNDFIRST = 0;

        public const int GW_HWNDNEXT = 2;

        public const int GW_HWNDLAST = 1;

        public const int GA_ROOT = 2;
        
        public const int LB_FINDSTRING = 399;

        public const int LB_GETCURSEL = 392;

        public const int LB_SETCURSEL = 390;

        public const int LB_GETITEMDATA = 409;

        public const int LB_SETITEMDATA = 410;

        public const int LB_FINDSTRINGEXACT = 418;

        public const int LB_ITEMFROMPOINT = 425;

        public const int LB_GETCOUNT = 395;

        public const int LB_GETTEXT = 393;

        public const int LB_GETSELCOUNT = 400;

        public const int LB_GETSELITEMS = 401;

        public const int LB_ADDSTRING = 384;

        public const int LB_GETHORIZONTALEXTENT = 403;

        public const int LB_SETHORIZONTALEXTENT = 404;

        // hScrollbar on listbox
        public const int LB_SETSEL = 389;

        // use true or false to select all items - lParam -1 to select all
        public const int LB_GETTEXTLEN = 394;

        public const int LB_ERR = -1;

        //Combo box
        //         CB_GETEDITSEL = 0x0140,
        //         CB_LIMITTEXT = 0x0141,
        //         CB_SETEDITSEL = 0x0142,
        //         CB_ADDSTRING = 0x0143,
        //         CB_DELETESTRING = 0x0144,
        //         CB_DIR = 0x0145,
        public const int CB_GETCOUNT = 0x0146;
        public const int CB_ERR = -1;
        public const int CB_GETCURSEL = 0x0147;
        public const int CB_GETLBTEXT = 0x0148;
        public const int CB_GETLBTEXTLEN = 0x0149;
        //         CB_INSERTSTRING = 0x014A,
        //         CB_RESETCONTENT = 0x014B,
        //         CB_FINDSTRING = 0x014C,
        //         CB_SELECTSTRING = 0x014D,
        public const int CB_SETCURSEL = 0x014E;
        //         CB_SHOWDROPDOWN = 0x014F,
        //         CB_GETITEMDATA = 0x0150,
        //         CB_SETITEMDATA = 0x0151,
        //         CB_GETDROPPEDCONTROLRECT = 0x0152,
        //         CB_SETITEMHEIGHT = 0x0153,
        //         CB_GETITEMHEIGHT = 0x0154,
        //         CB_SETEXTENDEDUI = 0x0155,
        //         CB_GETEXTENDEDUI = 0x0156,
        //         CB_GETDROPPEDSTATE = 0x0157,
        //         CB_FINDSTRINGEXACT = 0x0158,
        //         CB_SETLOCALE = 0x0159,
        //         CB_GETLOCALE = 0x015A,
        //         CB_GETTOPINDEX = 0x015B,
        //         CB_SETTOPINDEX = 0x015C,
        //         CB_GETHORIZONTALEXTENT = 0x015d,
        //         CB_SETHORIZONTALEXTENT = 0x015e,
        //         CB_GETDROPPEDWIDTH = 0x015f,
        //         CB_SETDROPPEDWIDTH = 0x0160,
        //         CB_INITSTORAGE = 0x0161,
        //         CB_MULTIPLEADDSTRING = 0x0163,
        //         CB_GETCOMBOBOXINFO = 0x0164,
        //         CB_MSGMAX_501 = 0x0165,
        //         CB_MSGMAX_WCE400 = 0x0163,
        //         CB_MSGMAX_400 = 0x0162,
        //         CB_MSGMAX_PRE400 = 0x015B,

        //public const int GWL_STYLE = -16;

        //public const int GWL_EXSTYLE = (-20);

        //public const int GWL_ID = -12;

        //public const int GWL_WNDPROC = -4;

        //public const int GWL_ID = -12;

        public const int WM_CHAR = 258;

        public const int WM_COPY = 769;

        public const int WM_CUT = 768;

        public const int WM_PASTE = 770;

        public const int WM_CONTEXTMENU = 123;

        public const int WM_SETHOTKEY = 50;

        public const int WM_SETTEXT = 12;

        public const int WM_SHOWWINDOW = 24;

        public const int WM_UNDO = 772;

        public const int WM_NCLBUTTONDOWN = 161;

        public const int WM_USER = 1024;

        public const int WM_QUIT = 18;

        public const int WM_LBUTTONUP = 514;

        public const int WM_LBUTTONDOWN = 513;

        public const int WM_GETTEXT = 13;

        public const long WM_GETTEXTLENGTH = 14;

        // 
        // hk As Integer = hot key?
        public const int HK_SHIFTA = 321;

        // Shift + A
        public const int HK_SHIFTB = 322;

        // Shift + B
        public const int HK_CONTROLA = 577;

        // Control + A
        public const int HK_ALTZ = 1114;

        public const int HTCAPTION = 2;

        public const int EM_UNDO = 199;

        public const int EM_SCROLLCARET = 183;

        public const int EM_GETSEL = 176;

        public const int EM_SETSEL = 177;

        public const int EM_GETLINE = 196;

        public const int EM_GETLINECOUNT = 186;

        public const int EM_LINEFROMCHAR = 201;

        public const int EM_LINEINDEX = 187;

        public const int EM_LINELENGTH = 193;

        public const int EM_LINESCROLL = 182;

        public const int EM_SETREADONLY = 207;

        public const int EM_REPLACESEL = 194;

        // 
        // cb As Integer = combobox
        public const int CB_FINDSTRING = 1036;

        public const int CB_SHOWDROPDOWN = (WM_USER + 15);

        public const int CB_SETDROPPEDWIDTH = 352;

        //.////Part of SendMessageTimeout
        public const int HWND_BROADCAST = 0xffff;

        public const int WM_SETTINGCHANGE = 0x001A;

        public const int SMTO_NORMAL = 0x0000;

        public const int SMTO_BLOCK = 0x0001;

        public const int SMTO_ABORTIFHUNG = 0x0002;

        public const int SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;

        

        ///////

        // '''''''''''''''''''''''''''''''''''''''''''''
        // Methods
        // '''''''''''''''''''''''''''''''''''''''''''''
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MapVirtualKey(int nVirtKey, int nMapType);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetGUIThreadInfo", CharSet=CharSet.Auto)]
        public static extern bool GetGUIThreadInfo(int tId, out GUITHREADINFO threadInfo);

        public static GUITHREADINFO GetThreadInfo(int tid)
        {
            GUITHREADINFO tinfo = new GUITHREADINFO();
            tinfo.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(tinfo);
            GetGUIThreadInfo(tid, out tinfo);
            return tinfo;
        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int AttachThreadInput(int idAttach, int idAttachTo, int fAttach);

        ///////////////////////     
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowThreadProcessId(int hWnd, ref int ProcessId);
        
        public static int GetWindowThreadProcessIdWrapper(IntPtr hWnd, out int ProcessId)
        {
            int retval =  GetWindowThreadProcessId(hWnd, out ProcessId);
            return retval;

        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern int GetWindowThreadProcess(IntPtr hWnd, ref int ProcessId);

        [DllImportAttribute("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);
        
        //[DllImport("user32.dll", EntryPoint = "GetAncestor", CharSet = CharSet.Auto)]
        //public static extern int GetAncestor(System.Int32 hwnd, System.Int32 gaFlags);

        //[DllImport("kernel32.dll", EntryPoint = "lstrcpyA", CharSet = CharSet.Auto)]
        //public static extern int lstrcpy(string lpString1, string lpString2);

        //[DllImport("user32.dll", EntryPoint = "GetPropA", CharSet = CharSet.Auto)]
        //public static extern int GetProp(int hwnd, string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        //[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        //public static extern long GetWindowLongPtr(IntPtr hwnd, int Index);

        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (Is64Bit())
                return GetWindowLongPtr64(hWnd, nIndex);
            return new IntPtr(GetWindowLong32(hWnd, nIndex));
        }

        public static Int64 GetWindowLongAsInt64(IntPtr hwnd, int index)
        {
            return GetWindowLongPtr(hwnd, index).ToInt64();
        }

        public static bool Is64Bit()
        {
            return (IntPtr.Size == 8);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd,
           ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
           [In] ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Wrapper around the Winapi POINT type.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// The X Coordinate.
            /// </summary>
            public int X;

            /// <summary>
            /// The Y Coordinate.
            /// </summary>
            public int Y;

            /// <summary>
            /// Creates a new POINT.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            /// <summary>
            /// Implicit cast.
            /// </summary>
            /// <returns></returns>
            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            /// <summary>
            /// Implicit cast.
            /// </summary>
            /// <returns></returns>
            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        //[DllImport("kernel32.dll", EntryPoint = "lstrlenA", CharSet = CharSet.Auto)]
        //public static extern int lstrlen(string lpString);

        [DllImport("kernel32.dll")]
        public static extern int GlobalLock(int hMem);

        [DllImport("kernel32.dll")]
        public static extern int GlobalUnlock(int hMem);

        [DllImport("user32.dll", EntryPoint = "RegisterWindowMessageA")]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "CloseWindow")]
        public static extern int CloseWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        public static extern int DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "GetActiveWindow", CharSet = CharSet.Auto)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumWindows(long lpEnumFunc, long lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool FlashWindowEx(FLASHWINFO pfwi);

        [DllImport("user32.dll", EntryPoint = "GetWindow", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int wCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetDesktopWindow();

        //Unsupported by windows 7
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        public static IntPtr WindowFromPoint(int xPoint, int yPoint)
        {
            return WindowFromPoint(new POINT(xPoint, yPoint));
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, int hdc);

        [DllImport("user32.dll", EntryPoint = "SetFocus", CharSet = CharSet.Auto)]
        public static extern int SetFocus(IntPtr hwnd);

        //[DllImport("user32.dll")]
        //public static extern int GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll", EntryPoint = "GetParent", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hDC);

        [DllImport("GDI32.dll")]
        public static extern int DeleteDC(int hDC);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hDC, int hObject);

        [DllImport("GDI32.dll")]
        public static extern int DeleteObject(int hObj);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto)]
        public static extern int GetForegroundWindow();

        //[DllImport("user32.dll", EntryPoint = "IsWindow", CharSet = CharSet.Auto)]
        //public static extern bool IsWindow(int hwnd);

        [DllImport("user32.dll", EntryPoint = "IsWindow", CharSet = CharSet.Auto)]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthA", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos", CharSet = CharSet.Auto)]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "SetWindowTextA", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hwnd, string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetCaretPos(ref POINTAPI lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetCursorPos(ref POINTAPI lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ScreenToClient(IntPtr hwnd, POINTAPI lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(IntPtr handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetDlgCtrlID(IntPtr hwnd);//*** LOOK INTO!!!

        public static Int32 GetLowWord(Int32 Word)
        {
            return Word % 65536;
        }

        public static Int32 GetHighWord(Int32 Word)
        {
            return ((Word / 65536) % 65536);
        }

        // /////////////////////////////////////////////

        public static int MakeWParam(int loWord, int hiWord)
        {
            return loWord + hiWord * 65536;
        }

        public static IntPtr MakeWParamPtr(int loWord, int hiWord)
        {
            return new IntPtr(loWord + hiWord * 65536);
        }

        public const uint PROCESS_ALL_ACCESS = (uint)(0x000F0000L | 0x00100000L | 0xFFF);
        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;
        public const uint PAGE_READWRITE = 0x04;

        [DllImport("kernel32")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle,
          int dwProcessId);

        [DllImport("kernel32")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
          int dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize,
          uint dwFreeType);

        [DllImport("kernel32")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
          ref LV_ITEM buffer, int dwSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
          IntPtr lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProcDelegate lpEnumFunc, int lParam);

        [DllImport("user32.dll", EntryPoint = "GetClassNameA")]//, CharSet = CharSet.Auto
        public static extern int GetClassName(IntPtr hwnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "GetClassInfoA", CharSet = CharSet.Auto)]
        public static extern int GetClassInfo(IntPtr hInstance, string lpClassName, WNDCLASS lpWndClass);

        [DllImport("user32.dll", EntryPoint = "GetClassInfoExA", CharSet = CharSet.Auto)]
        public static extern bool GetClassInfoEx(IntPtr hInstance, string lpClassName, out WndClassEX lpWndClass);

        [DllImport("user32.dll", EntryPoint = "FindWindowA", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("user32.dll", EntryPoint = "FindWindowExA", CharSet = CharSet.Auto)]
        //public static extern int FindWindowExInt(IntPtr hWnd1, int hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll", EntryPoint = "FindWindowExA", CharSet = CharSet.Auto)]
        public static extern int FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll", EntryPoint = "MoveWindow", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextA", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hwnd, System.Text.StringBuilder lpString, int cch);

        [DllImport("gdi32.dll", EntryPoint = "CreateDCA", CharSet = CharSet.Auto)]
        public static extern int CreateDC(string lpDriverName, string lpDeviceName, string lpOutput, string lpInitData);

        [DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps", CharSet = CharSet.Auto)]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hDC, int nWidth, int nHeight);

        [DllImport("user32.dll", EntryPoint = "SetParent", CharSet = CharSet.Auto)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

#region WindowsControls

#region ToolBar Stuff

        #region ToolBar Styles
        public enum ToolBarStyles
        {
            TBSTYLE_BUTTON = 0x0000,
            TBSTYLE_SEP = 0x0001,
            TBSTYLE_CHECK = 0x0002,
            TBSTYLE_GROUP = 0x0004,
            TBSTYLE_CHECKGROUP = (TBSTYLE_GROUP | TBSTYLE_CHECK),
            TBSTYLE_DROPDOWN = 0x0008,
            TBSTYLE_AUTOSIZE = 0x0010,
            TBSTYLE_NOPREFIX = 0x0020,
            TBSTYLE_TOOLTIPS = 0x0100,
            TBSTYLE_WRAPABLE = 0x0200,
            TBSTYLE_ALTDRAG = 0x0400,
            TBSTYLE_FLAT = 0x0800,
            TBSTYLE_LIST = 0x1000,
            TBSTYLE_CUSTOMERASE = 0x2000,
            TBSTYLE_REGISTERDROP = 0x4000,
            TBSTYLE_TRANSPARENT = 0x8000,
            TBSTYLE_EX_DRAWDDARROWS = 0x00000001
        }
    #endregion
        #region ToolBar Ex Styles
        public enum ToolBarExStyles
        {
            TBSTYLE_EX_DRAWDDARROWS = 0x1,
            TBSTYLE_EX_HIDECLIPPEDBUTTONS = 0x10,
            TBSTYLE_EX_DOUBLEBUFFER = 0x80
        }
    #endregion
        #region ToolBar Messages
        public enum ToolBarMessages
        {
            WM_USER = 0x0400,
            TB_ENABLEBUTTON = (WM_USER + 1),
            TB_CHECKBUTTON = (WM_USER + 2),
            TB_PRESSBUTTON = (WM_USER + 3),
            TB_HIDEBUTTON = (WM_USER + 4),
            TB_INDETERMINATE = (WM_USER + 5),
            TB_MARKBUTTON = (WM_USER + 6),
            TB_ISBUTTONENABLED = (WM_USER + 9),
            TB_ISBUTTONCHECKED = (WM_USER + 10),
            TB_ISBUTTONPRESSED = (WM_USER + 11),
            TB_ISBUTTONHIDDEN = (WM_USER + 12),
            TB_ISBUTTONINDETERMINATE = (WM_USER + 13),
            TB_ISBUTTONHIGHLIGHTED = (WM_USER + 14),
            TB_SETSTATE = (WM_USER + 17),
            TB_GETSTATE = (WM_USER + 18),
            TB_ADDBITMAP = (WM_USER + 19),
            TB_ADDBUTTONSA = (WM_USER + 20),
            TB_INSERTBUTTONA = (WM_USER + 21),
            TB_ADDBUTTONS = (WM_USER + 20),
            TB_INSERTBUTTON = (WM_USER + 21),
            TB_DELETEBUTTON = (WM_USER + 22),
            TB_GETBUTTON = (WM_USER + 23),
            TB_BUTTONCOUNT = (WM_USER + 24),
            TB_COMMANDTOINDEX = (WM_USER + 25),
            TB_SAVERESTOREA = (WM_USER + 26),
            TB_CUSTOMIZE = (WM_USER + 27),
            TB_ADDSTRINGA = (WM_USER + 28),
            TB_GETITEMRECT = (WM_USER + 29),
            TB_BUTTONSTRUCTSIZE = (WM_USER + 30),
            TB_SETBUTTONSIZE = (WM_USER + 31),
            TB_SETBITMAPSIZE = (WM_USER + 32),
            TB_AUTOSIZE = (WM_USER + 33),
            TB_GETTOOLTIPS = (WM_USER + 35),
            TB_SETTOOLTIPS = (WM_USER + 36),
            TB_SETPARENT = (WM_USER + 37),
            TB_SETROWS = (WM_USER + 39),
            TB_GETROWS = (WM_USER + 40),
            TB_GETBITMAPFLAGS = (WM_USER + 41),
            TB_SETCMDID = (WM_USER + 42),
            TB_CHANGEBITMAP = (WM_USER + 43),
            TB_GETBITMAP = (WM_USER + 44),
            TB_GETBUTTONTEXTA = (WM_USER + 45),
            TB_GETBUTTONTEXTW = (WM_USER + 75),
            TB_REPLACEBITMAP = (WM_USER + 46),
            TB_SETINDENT = (WM_USER + 47),
            TB_SETIMAGELIST = (WM_USER + 48),
            TB_GETIMAGELIST = (WM_USER + 49),
            TB_LOADIMAGES = (WM_USER + 50),
            TB_GETRECT = (WM_USER + 51),
            TB_SETHOTIMAGELIST = (WM_USER + 52),
            TB_GETHOTIMAGELIST = (WM_USER + 53),
            TB_SETDISABLEDIMAGELIST = (WM_USER + 54),
            TB_GETDISABLEDIMAGELIST = (WM_USER + 55),
            TB_SETSTYLE = (WM_USER + 56),
            TB_GETSTYLE = (WM_USER + 57),
            TB_GETBUTTONSIZE = (WM_USER + 58),
            TB_SETBUTTONWIDTH = (WM_USER + 59),
            TB_SETMAXTEXTROWS = (WM_USER + 60),
            TB_GETTEXTROWS = (WM_USER + 61),
            TB_GETOBJECT = (WM_USER + 62),
            TB_GETBUTTONINFOW = (WM_USER + 63),
            TB_SETBUTTONINFOW = (WM_USER + 64),
            TB_GETBUTTONINFOA = (WM_USER + 65),
            TB_SETBUTTONINFOA = (WM_USER + 66),
            TB_INSERTBUTTONW = (WM_USER + 67),
            TB_ADDBUTTONSW = (WM_USER + 68),
            TB_HITTEST = (WM_USER + 69),
            TB_SETDRAWTEXTFLAGS = (WM_USER + 70),
            TB_GETHOTITEM = (WM_USER + 71),
            TB_SETHOTITEM = (WM_USER + 72),
            TB_SETANCHORHIGHLIGHT = (WM_USER + 73),
            TB_GETANCHORHIGHLIGHT = (WM_USER + 74),
            TB_SAVERESTOREW = (WM_USER + 76),
            TB_ADDSTRINGW = (WM_USER + 77),
            TB_MAPACCELERATORA = (WM_USER + 78),
            TB_GETINSERTMARK = (WM_USER + 79),
            TB_SETINSERTMARK = (WM_USER + 80),
            TB_INSERTMARKHITTEST = (WM_USER + 81),
            TB_MOVEBUTTON = (WM_USER + 82),
            TB_GETMAXSIZE = (WM_USER + 83),
            TB_SETEXTENDEDSTYLE = (WM_USER + 84),
            TB_GETEXTENDEDSTYLE = (WM_USER + 85),
            TB_GETPADDING = (WM_USER + 86),
            TB_SETPADDING = (WM_USER + 87),
            TB_SETINSERTMARKCOLOR = (WM_USER + 88),
            TB_GETINSERTMARKCOLOR = (WM_USER + 89)
        }
    #endregion
        #region ToolBar Notifications
        public enum ToolBarNotifications
        {
            TTN_NEEDTEXTA = ((0 - 520) - 0),
            TTN_NEEDTEXTW = ((0 - 520) - 10),
            TBN_QUERYINSERT = ((0 - 700) - 6),
            TBN_DROPDOWN = ((0 - 700) - 10),
            TBN_HOTITEMCHANGE = ((0 - 700) - 13)
        }
    #endregion
        #region Toolbar button info flags
        public enum ToolBarButtonInfoFlags
        {
            TBIF_IMAGE = 0x00000001,
            TBIF_TEXT = 0x00000002,
            TBIF_STATE = 0x00000004,
            TBIF_STYLE = 0x00000008,
            TBIF_LPARAM = 0x00000010,
            TBIF_COMMAND = 0x00000020,
            TBIF_SIZE = 0x00000040,
            I_IMAGECALLBACK = -1,
            I_IMAGENONE = -2
        }
                #endregion
        #region Toolbar button styles
        public enum ToolBarButtonStyles
        {
            TBSTYLE_BUTTON = 0x0000,
            TBSTYLE_SEP = 0x0001,
            TBSTYLE_CHECK = 0x0002,
            TBSTYLE_GROUP = 0x0004,
            TBSTYLE_CHECKGROUP = (TBSTYLE_GROUP | TBSTYLE_CHECK),
            TBSTYLE_DROPDOWN = 0x0008,
            TBSTYLE_AUTOSIZE = 0x0010,
            TBSTYLE_NOPREFIX = 0x0020,
            TBSTYLE_TOOLTIPS = 0x0100,
            TBSTYLE_WRAPABLE = 0x0200,
            TBSTYLE_ALTDRAG = 0x0400,
            TBSTYLE_FLAT = 0x0800,
            TBSTYLE_LIST = 0x1000,
            TBSTYLE_CUSTOMERASE = 0x2000,
            TBSTYLE_REGISTERDROP = 0x4000,
            TBSTYLE_TRANSPARENT = 0x8000,
            TBSTYLE_EX_DRAWDDARROWS = 0x00000001
        }
        #endregion
        #region Toolbar button state
        public enum ToolBarButtonStates
        {
            TBSTATE_CHECKED = 0x01,
            TBSTATE_PRESSED = 0x02,
            TBSTATE_ENABLED = 0x04,
            TBSTATE_HIDDEN = 0x08,
            TBSTATE_INDETERMINATE = 0x10,
            TBSTATE_WRAP = 0x20,
            TBSTATE_ELLIPSES = 0x40,
            TBSTATE_MARKED = 0x80
        }
        #endregion
#endregion
#region ListView Stuff
        [StructLayout(LayoutKind.Sequential)]
        public struct LV_ITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
        }

        #region ListView Messages
        public enum ListViewMessages
        {
            LVM_FIRST = 0x1000,
            LVM_GETHEADER = (LVM_FIRST + 31),
            LVM_GETSELECTEDCOUNT = (LVM_FIRST + 50),
            LVM_GETSUBITEMRECT = (LVM_FIRST + 56),
            LVM_GETCOLUMNWIDTH = (LVM_FIRST + 29),
            LVM_GETITEMSTATE = (LVM_FIRST + 44),
            LVM_GETITEMTEXTW = (LVM_FIRST + 115),
            LVM_GETITEM = (LVM_FIRST + 5),
            LVM_INSERTITEMA = (LVM_FIRST + 7),
            LVM_INSERTITEMW = (LVM_FIRST + 77),
            LVM_INSERTCOLUMNA = (LVM_FIRST + 27),
            LVM_INSERTCOLUMNW = (LVM_FIRST + 97),
            LVM_DELETECOLUMN = (LVM_FIRST + 28),
            LVM_GETCOLUMNA = (LVM_FIRST + 25),
            LVM_GETCOLUMNW = (LVM_FIRST + 95),
            LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54),
            LVM_SETITEMA = (LVM_FIRST + 6),
            LVM_SETITEMW = (LVM_FIRST + 76),
            LVM_EDITLABELA = (LVM_FIRST + 23),
            LVM_EDITLABELW = (LVM_FIRST + 118),
            LVM_DELETEITEM = (LVM_FIRST + 8),
            LVM_SETBKCOLOR = (LVM_FIRST + 1),
            LVM_GETBKCOLOR = (LVM_FIRST + 0),
            LVM_GETTEXTBKCOLOR = (LVM_FIRST + 37),
            LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38),
            LVM_DELETEALLITEMS = (LVM_FIRST + 9),
            LVM_GETNEXTITEM = (LVM_FIRST + 12),
            LVM_SETITEMCOUNT = (LVM_FIRST + 47),
            LVM_GETITEMCOUNT = (LVM_FIRST + 4),
            LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30),
            LVM_GETITEMRECT = (LVM_FIRST + 14),
            LVM_SETITEMSTATE = (LVM_FIRST + 43),
            LVM_EDITLABEL = (LVM_FIRST + 23)
        }
       #endregion
        #region List View sub item portion
        public enum SubItemPortion
        {
            LVIR_BOUNDS = 0,
            LVIR_ICON = 1,
            LVIR_LABEL = 2
        }
        #endregion
        #region ListViewItem flags
        public enum ListViewItemFlags
        {
            LVIF_TEXT = 0x0001,
            LVIF_IMAGE = 0x0002,
            LVIF_PARAM = 0x0004,
            LVIF_STATE = 0x0008,
            LVIF_INDENT = 0x0010,
            LVIF_NORECOMPUTE = 0x0800
        }
                #endregion
        #region List View Notifications
        public enum ListViewNotifications
        {
            LVN_FIRST = (0 - 100),
            LVN_GETDISPINFOW = (LVN_FIRST - 77),
            LVN_SETDISPINFOA = (LVN_FIRST - 51)
        }
                #endregion
        #region ListViewItemState
        public enum ListViewItemState
        {
            LVIS_FOCUSED = 0x0001,
            LVIS_SELECTED = 0x0002,
            LVIS_CUT = 0x0004,
            LVIS_DROPHILITED = 0x0008,
            LVIS_ACTIVATING = 0x0020,
            LVIS_OVERLAYMASK = 0x0F00,
            LVIS_CHECKED = 0x2000,	// Microsoft did not think that documenting this value was necessary
            LVIS_STATEIMAGEMASK = 0xF000,


        }
        #endregion
        public enum ListViewColumnFlags : int
        {

            LVCF_FMT = 0x0001,
            LVCF_WIDTH = 0x0002,
            LVCF_TEXT = 0x0004,
            LVCF_SUBITEM = 0x0008,
            LVCF_IMAGE = 0x0010,
            LVCF_ORDER = 0x0020,
            LVCFMT_LEFT = 0x0000,
            LVCFMT_RIGHT = 0x0001,
            LVCFMT_CENTER = 0x0002,
            LVCFMT_JUSTIFYMASK = 0x0003,
            LVCFMT_IMAGE = 0x0800,
            LVCFMT_BITMAP_ON_RIGHT = 0x1000,
            LVCFMT_COL_HAS_IMAGES = 0x8000
        }
        public enum ListViewStyles: int
        {
            //LVS_ICON = 0x0000,
            LVS_REPORT = 0x0001,
            LVS_SMALLICON = 0x0002,
            //LVS_LIST = 0x0003,
            LVS_TYPEMASK = 0x0003,
            LVS_SINGLESEL = 0x0004,
            LVS_SHOWSELALWAYS = 0x0008,
            LVS_SORTASCENDING = 0x0010,
            LVS_SORTDESCENDING = 0x0020,
            LVS_SHAREIMAGELISTS = 0x0040,
            LVS_NOLABELWRAP = 0x0080,
            LVS_AUTOARRANGE = 0x0100,
            LVS_EDITLABELS = 0x0200,
            LVS_OWNERDATA = 0x1000,
            LVS_NOSCROLL = 0x2000,
            LVS_TYPESTYLEMASK = 0xfc00,
            LVS_ALIGNTOP = 0x0000,
            LVS_ALIGNLEFT = 0x0800,
            LVS_ALIGNMASK = 0x0c00,
            LVS_OWNERDRAWFIXED = 0x0400,
            LVS_NOCOLUMNHEADER = 0x4000,
            LVS_NOSORTHEADER = 0x8000
        }
        public enum ListViewExtendedFlags : int
        {
            LVS_EX_GRIDLINES = 0x00000001,
            LVS_EX_SUBITEMIMAGES = 0x00000002,
            LVS_EX_CHECKBOXES = 0x00000004,
            LVS_EX_TRACKSELECT = 0x00000008,
            LVS_EX_HEADERDRAGDROP = 0x00000010,
            LVS_EX_FULLROWSELECT = 0x00000020,
            LVS_EX_ONECLICKACTIVATE = 0x00000040,
            LVS_EX_TWOCLICKACTIVATE = 0x00000080,
            LVS_EX_FLATSB = 0x00000100,
            LVS_EX_REGIONAL = 0x00000200,
            LVS_EX_INFOTIP = 0x00000400,
            LVS_EX_UNDERLINEHOT = 0x00000800,
            LVS_EX_UNDERLINECOLD = 0x00001000,
            LVS_EX_MULTIWORKAREAS = 0x00002000
        }
        public enum ListViewNotifyItem : int
        {
            LVNI_ALL = 0x0000,
            LVNI_FOCUSED = 0x0001,
            LVNI_SELECTED = 0x0002,
            LVNI_CUT = 0x0004,
            LVNI_DROPHILITED = 0x0008,
            LVNI_ABOVE = 0x0100,
            LVNI_BELOW = 0x0200,
            LVNI_TOLEFT = 0x0400,
            LVNI_TORIGHT = 0x0800

        }
        public enum ListViewNotifyMsg : int
        {

            LVN_FIRST = (0 - 100),
            LVN_LAST = (0 - 199),
            LVN_ITEMCHANGING = (LVN_FIRST - 0),
            LVN_ITEMCHANGED = (LVN_FIRST - 1),
            LVN_INSERTITEM = (LVN_FIRST - 2),
            LVN_DELETEITEM = (LVN_FIRST - 3),
            LVN_DELETEALLITEMS = (LVN_FIRST - 4),
            LVN_BEGINLABELEDITA = (LVN_FIRST - 5),
            LVN_BEGINLABELEDITW = (LVN_FIRST - 75),
            LVN_ENDLABELEDITA = (LVN_FIRST - 6),
            LVN_ENDLABELEDITW = (LVN_FIRST - 76),
            LVN_COLUMNCLICK = (LVN_FIRST - 8),
            LVN_BEGINDRAG = (LVN_FIRST - 9),
            LVN_BEGINRDRAG = (LVN_FIRST - 11),
            LVN_ODCACHEHINT = (LVN_FIRST - 13),
            LVN_ODFINDITEMA = (LVN_FIRST - 52),
            LVN_ODFINDITEMW = (LVN_FIRST - 79),
            LVN_ITEMACTIVATE = (LVN_FIRST - 14),
            LVN_ODSTATECHANGED = (LVN_FIRST - 15)
        }
        public enum ListViewColumnWithMsg : int
        {
            LVSCW_AUTOSIZE = -1,
            LVSCW_AUTOSIZE_USEHEADER = -2
        }
#endregion
#region ToolTip Stuff
        #region ToolTip Flags
        public enum ToolTipFlags
        {
            TTF_IDISHWND = 0x0001,
            TTF_CENTERTIP = 0x0002,
            TTF_RTLREADING = 0x0004,
            TTF_SUBCLASS = 0x0010,
            TTF_TRACK = 0x0020,
            TTF_ABSOLUTE = 0x0080,
            TTF_TRANSPARENT = 0x0100,
            TTF_DI_SETITEM = 0x8000
        }
        #endregion
        public enum ToolTipNotifications : int
        {
            TTN_FIRST = 0 - 520,
            TTN_GETDISPINFO = (TTN_FIRST - 10),
            TTN_SHOW = (TTN_FIRST - 1),
            TTN_POP = (TTN_FIRST - 2)
        }
        #region ToolTipControl Messages
        internal enum ToolTipControlMessages
        {
            TTM_ACTIVATE = (WM.USER + 1),
            TTM_SETDELAYTIME = (WM.USER + 3),
            TTM_SETMAXTIPWIDTH = (WM.USER + 24),
            TTM_ADDTOOL = (WM.USER + 4),
            TTM_DELTOOL = (WM.USER + 5),
            TTM_UPDATETIPTEXT = (WM.USER + 12)

        }
        #endregion
        #region ToolTipControl Styles
        internal enum ToolTipControlStyles
        {
            TTS_ALWAYSTIP = 0x01,
            TTS_NOPREFIX = 0x02
        }
        #endregion
        #region ToolTipControlDelay Flags
        internal enum ToolTipControlDelayFlags
        {
            TTDT_AUTOMATIC = 0,
            TTDT_RESHOW = 1,
            TTDT_AUTOPOP = 2,
            TTDT_INITIAL = 3
        }
        #endregion
#endregion
#region ComboBox Stuff
        #region ComboBox styles
        public enum ComboBoxStyles : uint
        {
            CBS_SIMPLE = 0x0001,
            CBS_DROPDOWN = 0x0002,
            CBS_DROPDOWNLIST = 0x0003,
            CBS_OWNERDRAWFIXED = 0x0010,
            CBS_OWNERDRAWVARIABLE = 0x0020,
            CBS_AUTOHSCROLL = 0x0040,
            CBS_OEMCONVERT = 0x0080,
            CBS_SORT = 0x0100,
            CBS_HASSTRINGS = 0x0200,
            CBS_NOINTEGRALHEIGHT = 0x0400,
            CBS_DISABLENOSCROLL = 0x0800,
            CBS_UPPERCASE = 0x2000,
            CBS_LOWERCASE = 0x4000
        }
        #endregion
        #region ComboBox messages
        public enum ComboBoxMessages
        {
            CB_GETEDITSEL = 0x140,
            CB_LIMITTEXT = 0x141,
            CB_SETEDITSEL = 0x142,
            CB_ADDSTRING = 0x143,
            CB_DELETESTRING = 0x144,
            CB_DIR = 0x145,
            CB_GETCOUNT = 0x146,
            CB_GETCURSEL = 0x147,
            CB_GETLBTEXT = 0x148,
            CB_GETLBTEXTLEN = 0x149,
            CB_INSERTSTRING = 0x14A,
            CB_RESETCONTENT = 0x14B,
            CB_FINDSTRING = 0x14C,
            CB_SELECTSTRING = 0x14D,
            CB_SETCURSEL = 0x14E,
            CB_SHOWDROPDOWN = 0x14F,
            CB_GETITEMDATA = 0x150,
            CB_SETITEMDATA = 0x151,
            CB_GETDROPPEDCONTROLRECT = 0x152,
            CB_SETITEMHEIGHT = 0x153,
            CB_GETITEMHEIGHT = 0x154,
            CB_SETEXTENDEDUI = 0x155,
            CB_GETEXTENDEDUI = 0x156,
            CB_GETDROPPEDSTATE = 0x157,
            CB_FINDSTRINGEXACT = 0x158,
            CB_SETLOCALE = 0x159,
            CB_GETLOCALE = 0x15A,
            CB_GETTOPINDEX = 0x15b,
            CB_SETTOPINDEX = 0x15c,
            CB_GETHORIZONTALEXTENT = 0x15d,
            CB_SETHORIZONTALEXTENT = 0x15e,
            CB_GETDROPPEDWIDTH = 0x15f,
            CB_SETDROPPEDWIDTH = 0x160,
            CB_INITSTORAGE = 0x161,
            CB_MSGMAX = 0x162,
            CB_MSGMAX_351 = 0x15B
        }
        #endregion
        internal enum ComboBoxNotification : int
        {
            CBN_ERRSPACE = -1,
            CBN_SELCHANGE = 1,
            CBN_DBLCLK = 2,
            CBN_SETFOCUS = 3,
            CBN_KILLFOCUS = 4,
            CBN_EDITCHANGE = 5,
            CBN_EDITUPDATE = 6,
            CBN_DROPDOWN = 7,
            CBN_CLOSEUP = 8,
            CBN_SELENDOK = 9,
            CBN_SELENDCANCEL = 10
        }

#endregion
#region ListBox Stuff
        public enum ListBoxMessages
        {
            LB_ADDSTRING = 0x0180,
            LB_INSERTSTRING = 0x0181,
            LB_DELETESTRING = 0x0182,
            LB_SELITEMRANGEEX = 0x0183,
            LB_RESETCONTENT = 0x0184,
            LB_SETSEL = 0x0185,
            LB_SETCURSEL = 0x0186,
            LB_GETSEL = 0x0187,
            LB_GETCURSEL = 0x0188,
            LB_GETTEXT = 0x0189,
            LB_GETTEXTLEN = 0x018A,
            LB_GETCOUNT = 0x018B,
            LB_SELECTSTRING = 0x018C,
            LB_DIR = 0x018D,
            LB_GETTOPINDEX = 0x018E,
            LB_FINDSTRING = 0x018F,
            LB_GETSELCOUNT = 0x0190,
            LB_GETSELITEMS = 0x0191,
            LB_SETTABSTOPS = 0x0192,
            LB_GETHORIZONTALEXTENT = 0x0193,
            LB_SETHORIZONTALEXTENT = 0x0194,
            LB_SETCOLUMNWIDTH = 0x0195,
            LB_ADDFILE = 0x0196,
            LB_SETTOPINDEX = 0x0197,
            LB_GETITEMRECT = 0x0198,
            LB_GETITEMDATA = 0x0199,
            LB_SETITEMDATA = 0x019A,
            LB_SELITEMRANGE = 0x019B,
            LB_SETANCHORINDEX = 0x019C,
            LB_GETANCHORINDEX = 0x019D,
            LB_SETCARETINDEX = 0x019E,
            LB_GETCARETINDEX = 0x019F,
            LB_SETITEMHEIGHT = 0x01A0,
            LB_GETITEMHEIGHT = 0x01A1,
            LB_FINDSTRINGEXACT = 0x01A2,
            LB_SETLOCALE = 0x01A5,
            LB_GETLOCALE = 0x01A6,
            LB_SETCOUNT = 0x01A7,
            LB_INITSTORAGE = 0x01A8,
            LB_ITEMFROMPOINT = 0x01A9,
            LB_MSGMAX = 0x01B0,
            LB_MSGMAX_351 = 0x01A8
        }
        public enum ListBoxStyles : long
        {
            LBS_NOTIFY = 0x0001,
            LBS_SORT = 0x0002,
            LBS_NOREDRAW = 0x0004,
            LBS_MULTIPLESEL = 0x0008,
            LBS_OWNERDRAWFIXED = 0x0010,
            LBS_OWNERDRAWVARIABLE = 0x0020,
            LBS_HASSTRINGS = 0x0040,
            LBS_USETABSTOPS = 0x0080,
            LBS_NOINTEGRALHEIGHT = 0x0100,
            LBS_MULTICOLUMN = 0x0200,
            LBS_WANTKEYBOARDINPUT = 0x0400,
            LBS_EXTENDEDSEL = 0x0800,
            LBS_DISABLENOSCROLL = 0x1000,
            LBS_NODATA = 0x2000,
            LBS_NOSEL = 0x4000,
            LBS_STANDARD = 0x03 | WS.WS_VSCROLL | WS.WS_BORDER
        }
        internal enum ListBoxNotifications : int
        {
            LB_OKAY = 0,
            LB_ERR = -1,
            LBN_ERRSPACE = -2,
            LBN_SELCHANGE = 1,
            LBN_DBLCLK = 2,
            LBN_SELCANCEL = 3,
            LBN_SETFOCUS = 4,
            LBN_KILLFOCUS = 5
        }
#endregion
#region TreeView Stuff
        #region TreeView Messages
        public enum TreeViewMessages : int
        {
            TV_FIRST = 0x1100,
            TVM_INSERTITEMA = (TV_FIRST + 0),
            TVM_DELETEITEM = (TV_FIRST + 1),
            TVM_EXPAND = (TV_FIRST + 2),
            TVM_GETITEMRECT = (TV_FIRST + 4),
            TVM_GETCOUNT = (TV_FIRST + 5),
            TVM_GETINDENT = (TV_FIRST + 6),
            TVM_SETINDENT = (TV_FIRST + 7),
            TVM_GETIMAGELIST = (TV_FIRST + 8),
            TVM_SETIMAGELIST = (TV_FIRST + 9),
            TVM_GETNEXTITEM = (TV_FIRST + 10),
            TVM_SELECTITEM = (TV_FIRST + 11),
            TVM_GETITEMA = (TV_FIRST + 12),
            TVM_SETITEMA = (TV_FIRST + 13),
            TVM_EDITLABELA = (TV_FIRST + 14),
            TVM_GETEDITCONTROL = (TV_FIRST + 15),
            TVM_GETVISIBLECOUNT = (TV_FIRST + 16),
            TVM_HITTEST = (TV_FIRST + 17),
            TVM_CREATEDRAGIMAGE = (TV_FIRST + 18),
            TVM_SORTCHILDREN = (TV_FIRST + 19),
            TVM_ENSUREVISIBLE = (TV_FIRST + 20),
            TVM_SORTCHILDRENCB = (TV_FIRST + 21),
            TVM_SETITEMHEIGHT = (TV_FIRST + 27),
            TVM_GETITEMHEIGHT = (TV_FIRST + 28),
            TVM_SETBKCOLOR = (TV_FIRST + 29),
            TVM_SETTEXTCOLOR = (TV_FIRST + 30),
            TVM_GETITEMW = (TV_FIRST + 62),
            TVM_SETITEMW = (TV_FIRST + 63),
            TVM_INSERTITEMW = (TV_FIRST + 50)
        }
#endregion
        #region TreeViewImageListFlags
        public enum TreeViewImageListFlags
        {
            TVSIL_NORMAL = 0,
            TVSIL_STATE = 2
        }
        #endregion
        #region TreeViewItem Flags
        [Flags]
        public enum TreeViewItemFlags
        {
            TVIF_NONE = 0x0000,
            TVIF_TEXT = 0x0001,
            TVIF_IMAGE = 0x0002,
            TVIF_PARAM = 0x0004,
            TVIF_STATE = 0x0008,
            TVIF_HANDLE = 0x0010,
            TVIF_SELECTEDIMAGE = 0x0020,
            TVIF_CHILDREN = 0x0040,
            TVIF_INTEGRAL = 0x0080,
            I_CHILDRENCALLBACK = -1,
            LPSTR_TEXTCALLBACK = -1,
            I_IMAGECALLBACK = -1,
            I_IMAGENONE = -2
        }
        #endregion
        public enum TreeViewItemSelFlags : int
        {
            TVGN_ROOT = 0x0000,
            TVGN_NEXT = 0x0001,
            TVGN_PREVIOUS = 0x0002,
            TVGN_PARENT = 0x0003,
            TVGN_CHILD = 0x0004,
            TVGN_FIRSTVISIBLE = 0x0005,
            TVGN_NEXTVISIBLE = 0x0006,
            TVGN_PREVIOUSVISIBLE = 0x0007,
            TVGN_DROPHILITE = 0x0008,
            TVGN_CARET = 0x0009,
            TVGN_LASTVISIBLE = 0x000A
        }
        #region TreeViewItemInsertPosition
        public enum TreeViewItemInsertPosition : uint
        {
            TVI_ROOT = 0xFFFF0000,
            TVI_FIRST = 0xFFFF0001,
            TVI_LAST = 0xFFFF0002,
            TVI_SORT = 0xFFFF0003
        }
        #endregion
        #region TreeViewNotifications
        public enum TreeViewNotifications : int
        {
            TVN_FIRST = -400,
            TVN_SELCHANGINGA = (TVN_FIRST - 1),
            TVN_SELCHANGINGW = (TVN_FIRST - 50),
            TVN_SELCHANGEDA = (TVN_FIRST - 2),
            TVN_SELCHANGEDW = (TVN_FIRST - 51),
            TVN_GETDISPINFOA = (TVN_FIRST - 3),
            TVN_GETDISPINFOW = (TVN_FIRST - 52),
            TVN_SETDISPINFOA = (TVN_FIRST - 4),
            TVN_SETDISPINFOW = (TVN_FIRST - 53),
            TVN_ITEMEXPANDINGA = (TVN_FIRST - 5),
            TVN_ITEMEXPANDINGW = (TVN_FIRST - 54),
            TVN_ITEMEXPANDEDA = (TVN_FIRST - 6),
            TVN_ITEMEXPANDEDW = (TVN_FIRST - 55),
            TVN_BEGINDRAGA = (TVN_FIRST - 7),
            TVN_BEGINDRAGW = (TVN_FIRST - 56),
            TVN_BEGINRDRAGA = (TVN_FIRST - 8),
            TVN_BEGINRDRAGW = (TVN_FIRST - 57),
            TVN_DELETEITEMA = (TVN_FIRST - 9),
            TVN_DELETEITEMW = (TVN_FIRST - 58),
            TVN_BEGINLABELEDITA = (TVN_FIRST - 10),
            TVN_BEGINLABELEDITW = (TVN_FIRST - 59),
            TVN_ENDLABELEDITA = (TVN_FIRST - 11),
            TVN_ENDLABELEDITW = (TVN_FIRST - 60),
            TVN_KEYDOWN = (TVN_FIRST - 12),
            TVN_GETINFOTIPA = (TVN_FIRST - 13),
            TVN_GETINFOTIPW = (TVN_FIRST - 14),
            TVN_SINGLEEXPAND = (TVN_FIRST - 15)
        }
        #endregion
        #region TreeViewItemExpansion
        public enum TreeViewItemExpansion
        {
            TVE_COLLAPSE = 0x0001,
            TVE_EXPAND = 0x0002,
            TVE_TOGGLE = 0x0003,
            TVE_EXPANDPARTIAL = 0x4000,
            TVE_COLLAPSERESET = 0x8000
        }
        #endregion
        #region TreeViewHitTest
        public enum TreeViewHitTestFlags
        {
            TVHT_NOWHERE = 0x0001,
            TVHT_ONITEMICON = 0x0002,
            TVHT_ONITEMLABEL = 0x0004,
            TVHT_ONITEM = (TVHT_ONITEMICON | TVHT_ONITEMLABEL | TVHT_ONITEMSTATEICON),
            TVHT_ONITEMINDENT = 0x0008,
            TVHT_ONITEMBUTTON = 0x0010,
            TVHT_ONITEMRIGHT = 0x0020,
            TVHT_ONITEMSTATEICON = 0x0040,
            TVHT_ABOVE = 0x0100,
            TVHT_BELOW = 0x0200,
            TVHT_TORIGHT = 0x0400,
            TVHT_TOLEFT = 0x0800
        }
        #endregion
        #region TreeViewItemState
        public enum TreeViewItemState
        {
            TVIS_SELECTED = 0x0002,
            TVIS_CUT = 0x0004,
            TVIS_DROPHILITED = 0x0008,
            TVIS_BOLD = 0x0010,
            TVIS_EXPANDED = 0x0020,
            TVIS_EXPANDEDONCE = 0x0040,
            TVIS_EXPANDPARTIAL = 0x0080,
            TVIS_OVERLAYMASK = 0x0F00,
            TVIS_STATEIMAGEMASK = 0xF000,
            TVIS_USERMASK = 0xF000
        }
        #endregion
        public enum TreeViewStyles : int
        {
            TVS_HASBUTTONS = 0x0001,
            TVS_HASLINES = 0x0002,
            TVS_LINESATROOT = 0x0004,
            TVS_EDITLABELS = 0x0008,
            TVS_DISABLEDRAGDROP = 0x0010,
            TVS_SHOWSELALWAYS = 0x0020,
            TVS_RTLREADING = 0x0040,
            TVS_NOTOOLTIPS = 0x0080,
            TVS_CHECKBOXES = 0x0100,
            TVS_TRACKSELECT = 0x0200,
            TVS_SINGLEEXPAND = 0x0400,
            TVS_INFOTIP = 0x0800,
            TVS_FULLROWSELECT = 0x1000,
            TVS_NOSCROLL = 0x2000,
            TVS_NONEVENHEIGHT = 0x4000
        }
#endregion
#region ScrollBar Stuff
        #region ScrollBarFlags
        public enum ScrollBarFlags
        {
            SBS_HORZ = 0x0000,
            SBS_VERT = 0x0001,
            SBS_TOPALIGN = 0x0002,
            SBS_LEFTALIGN = 0x0002,
            SBS_BOTTOMALIGN = 0x0004,
            SBS_RIGHTALIGN = 0x0004,
            SBS_SIZEBOXTOPLEFTALIGN = 0x0002,
            SBS_SIZEBOXBOTTOMRIGHTALIGN = 0x0004,
            SBS_SIZEBOX = 0x0008,
            SBS_SIZEGRIP = 0x0010
        }
        #endregion
        #region ScrollBarMessages
        public enum ScrollBarMessages
        {
            SBM_SETPOS = 0x00E0,
            SBM_GETPOS = 0x00E1,
            SBM_SETRANGE = 0x00E2,
            SBM_GETRANGE = 0x00E3
        }
        #endregion
        #region ScrollBarTypes
        public enum ScrollBarTypes
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }
        #endregion
        #region SrollBarInfoFlags
        public enum ScrollBarInfoFlags
        {
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_DISABLENOSCROLL = 0x0008,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
        }
        #endregion
        #region Enable ScrollBar flags
        public enum EnableScrollBarFlags
        {
            ESB_ENABLE_BOTH = 0x0000,
            ESB_DISABLE_BOTH = 0x0003,
            ESB_DISABLE_LEFT = 0x0001,
            ESB_DISABLE_RIGHT = 0x0002,
            ESB_DISABLE_UP = 0x0001,
            ESB_DISABLE_DOWN = 0x0002,
            ESB_DISABLE_LTUP = ESB_DISABLE_LEFT,
            ESB_DISABLE_RTDN = ESB_DISABLE_RIGHT
        }
        #endregion
        #region Scroll Requests
        public enum ScrollBarRequests
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }
        #endregion
        #region SrollWindowEx flags
        public enum ScrollWindowExFlags
        {
            SW_SCROLLCHILDREN = 0x0001,
            SW_INVALIDATE = 0x0002,
            SW_ERASE = 0x0004,
            SW_SMOOTHSCROLL = 0x0010
        }
        #endregion
#endregion
#region Button Stuff
        public enum BS : int//ButtonStyles
        {
            PUSHBUTTON = 0,
            DEFPUSHBUTTON = 1,
            CHECKBOX = 2,
            AUTOCHECKBOX = 3,
            RADIOBUTTON = 4,
            n3STATE = 5,
            AUTO3STATE = 6,
            GROUPBOX = 7,
            USERBUTTON = 8,
            AUTORADIOBUTTON = 9,
            OWNERDRAW = 11,
            LEFTTEXT = 0x20,
            TEXT = 0,
            ICON = 0x40,
            BITMAP = 0x80,
            LEFT = 0x100,
            RIGHT = 0x200,
            CENTER = 0x300,
            TOP = 0x400,
            BOTTOM = 0x800,
            VCENTER = 0xC00,
            PUSHLIKE = 0x1000,
            MULTILINE = 0x2000,
            NOTIFY = 0x4000,
            FLAT = 0x8000,
            RIGHTBUTTON = 0x20
        }
        internal enum ButtonNotification : uint
        {
            BN_CLICKED = 0,
            BN_PAINT = 1,
            BN_HILITE = 2,
            BN_UNHILITE = 3,
            BN_DISABLE = 4,
            BN_DOUBLECLICKED = 5,
            BN_PUSHED = 2,
            BN_UNPUSHED = 3,
            BN_DBLCLK = 5,
            BN_SETFOCUS = 6,
            BN_KILLFOCUS = 7
        }
        public enum BM : int//ButtonMessages
        {
            BM_GETCHECK = 0x00f0,
            BM_SETCHECK = 0x00f1,
            BM_GETSTATE = 0x00f2,
            BM_SETSTATE = 0x00f3,
            BM_SETSTYLE = 0x00f4,
            BM_CLICK = 0x00f5,
            BM_GETIMAGE = 0x00f6,
            BM_SETIMAGE = 0x00f7
        }
        internal enum NativeButtonState : int
        {
            BST_UNCHECKED = 0x0000,
            BST_CHECKED = 0x0001,
            BST_INDETERMINATE = 0x0002,
            BST_PUSHED = 0x0004,
            BST_FOCUS = 0x0008
        }
#endregion
#region CommonControls
        #region Common Controls Styles
        private enum CommonControlStyles
        {
            CCS_TOP = 0x00000001,
            CCS_NOMOVEY = 0x00000002,
            CCS_BOTTOM = 0x00000003,
            CCS_NORESIZE = 0x00000004,
            CCS_NOPARENTALIGN = 0x00000008,
            CCS_ADJUSTABLE = 0x00000020,
            CCS_NODIVIDER = 0x00000040,
            CCS_VERT = 0x00000080,
            CCS_LEFT = (CCS_VERT | CCS_TOP),
            CCS_RIGHT = (CCS_VERT | CCS_BOTTOM),
            CCS_NOMOVEX = (CCS_VERT | CCS_NOMOVEY)
        }
#endregion
        #region CommonControlMessages
        private enum CommonControlMessages
        {
            CCM_FIRST = 0x2000,			// Common control shared messages
            CCM_SETBKCOLOR = (CCM_FIRST + 1),	// lParam is bkColor
            CCM_SETCOLORSCHEME = (CCM_FIRST + 2),	// lParam is color scheme
            CCM_GETCOLORSCHEME = (CCM_FIRST + 3),	// fills in COLORSCHEME pointed to by lParam
            CCM_GETDROPTARGET = (CCM_FIRST + 4),
            CCM_SETUNICODEFORMAT = (CCM_FIRST + 5),
            CCM_GETUNICODEFORMAT = (CCM_FIRST + 6),
            CCM_SETVERSION = (CCM_FIRST + 7)
        }
                #endregion

#endregion
#region ImageList Stuff
        #region ImageListFlags
        public enum ImageListFlags
        {
            ILC_MASK = 0x0001,
            ILC_COLOR = 0x0000,
            ILC_COLORDDB = 0x00FE,
            ILC_COLOR4 = 0x0004,
            ILC_COLOR8 = 0x0008,
            ILC_COLOR16 = 0x0010,
            ILC_COLOR24 = 0x0018,
            ILC_COLOR32 = 0x0020,
            ILC_PALETTE = 0x0800
        }
        #endregion
        #region ImageListDrawFlags
        public enum ImageListDrawFlags
        {
            ILD_NORMAL = 0x0000,
            ILD_TRANSPARENT = 0x0001,
            ILD_MASK = 0x0010,
            ILD_IMAGE = 0x0020,
            ILD_ROP = 0x0040,
            ILD_BLEND25 = 0x0002,
            ILD_BLEND50 = 0x0004,
            ILD_OVERLAYMASK = 0x0F00
        }
        #endregion
#endregion
#region StatusBar Stuff
        #region StatusBar Control Styles
        public enum StatusbarControlStyles
        {
            SBARS_SIZEGRIP = 0x0100,
            SBT_TOOLTIPS = 0x0800
        }
        #endregion
        #region StatusBar notifications
        internal enum StatusbarNotifications : int
        {
            SBN_FIRST = (0 - 880),
            SBN_LAST = (0 - 899),
            SBN_SIMPLEMODECHANGE = (SBN_FIRST - 0)
        }
        #endregion
        #region Statusbar Control Messages
        internal enum StatusbarMessages
        {
            SB_SETTEXT = (WM.USER + 1),
            SB_GETTEXT = (WM.USER + 2),
            SB_GETTEXTLENGTH = (WM.USER + 3),
            SB_SETPARTS = (WM.USER + 4),
            SB_GETPARTS = (WM.USER + 6),
            SB_GETBORDERS = (WM.USER + 7),
            SB_SETMINHEIGHT = (WM.USER + 8),
            SB_SIMPLE = (WM.USER + 9),
            SB_GETRECT = (WM.USER + 10),
            SB_ISSIMPLE = (WM.USER + 14),
            SB_SETICON = (WM.USER + 15),
            SB_SETTIPTEXT = (WM.USER + 16),
            SB_GETTIPTEXT = (WM.USER + 18),
            SB_GETICON = (WM.USER + 20),
            SB_SETBKCOLOR = CommonControlMessages.CCM_SETBKCOLOR
        }
        #endregion
        #region Statusbar Drawing Operations Types
        internal enum StatusbarDrawType
        {
            SBT_OWNERDRAW = 0x1000,
            SBT_NOBORDERS = 0x0100,
            SBT_POPOUT = 0x0200,
            SBT_RTLREADING = 0x0400
        }
        #endregion
#endregion
#region Trackbar Stuff
        #region Trackbar Control Messages
        internal enum TrackbarMessages
        {
            TBM_GETPOS = (WM.USER),
            TBM_GETRANGEMIN = (WM.USER + 1),
            TBM_GETRANGEMAX = (WM.USER + 2),
            TBM_GETTIC = (WM.USER + 3),
            TBM_SETTIC = (WM.USER + 4),
            TBM_SETPOS = (WM.USER + 5),
            TBM_SETRANGE = (WM.USER + 6),
            TBM_SETRANGEMIN = (WM.USER + 7),
            TBM_SETRANGEMAX = (WM.USER + 8),
            TBM_CLEARTICS = (WM.USER + 9),
            TBM_SETSEL = (WM.USER + 10),
            TBM_SETSELSTART = (WM.USER + 11),
            TBM_SETSELEND = (WM.USER + 12),
            TBM_GETPTICS = (WM.USER + 14),
            TBM_GETTICPOS = (WM.USER + 15),
            TBM_GETNUMTICS = (WM.USER + 16),
            TBM_GETSELSTART = (WM.USER + 17),
            TBM_GETSELEND = (WM.USER + 18),
            TBM_CLEARSEL = (WM.USER + 19),
            TBM_SETTICFREQ = (WM.USER + 20),
            TBM_SETPAGESIZE = (WM.USER + 21),
            TBM_GETPAGESIZE = (WM.USER + 22),
            TBM_SETLINESIZE = (WM.USER + 23),
            TBM_GETLINESIZE = (WM.USER + 24),
            TBM_GETTHUMBRECT = (WM.USER + 25),
            TBM_GETCHANNELRECT = (WM.USER + 26),
            TBM_SETTHUMBLENGTH = (WM.USER + 27),
            TBM_GETTHUMBLENGTH = (WM.USER + 28),
            TBM_SETTOOLTIPS = (WM.USER + 29),
            TBM_GETTOOLTIPS = (WM.USER + 30),
            TBM_SETTIPSIDE = (WM.USER + 31)
        }
        #endregion
        #region Trackbar Control Styles
        public enum TrackbarControlStyles
        {
            TBS_AUTOTICKS = 0x0001,
            TBS_VERT = 0x0002,
            TBS_HORZ = 0x0000,
            TBS_TOP = 0x0004,
            TBS_BOTTOM = 0x0000,
            TBS_LEFT = 0x0004,
            TBS_RIGHT = 0x0000,
            TBS_BOTH = 0x0008,
            TBS_NOTICKS = 0x0010,
            TBS_ENABLESELRANGE = 0x0020,
            TBS_FIXEDLENGTH = 0x0040,
            TBS_NOTHUMB = 0x0080,
            TBS_TOOLTIPS = 0x0100
        }
        #endregion
        internal enum TrackbarNotifications : int
        {
            TB_LINEUP = 0,
            TB_LINEDOWN = 1,
            TB_PAGEUP = 2,
            TB_PAGEDOWN = 3,
            TB_THUMBPOSITION = 4,
            TB_THUMBTRACK = 5,
            TB_TOP = 6,
            TB_BOTTOM = 7,
            TB_ENDTRACK = 8
        }
#endregion
#region DateTimePicker Stuff
        #region DateTimePicker Control Styles
        public enum DateTimePickerControlStyles
        {
            DTS_UPDOWN = 0x0001,
            DTS_SHOWNONE = 0x0002,
            DTS_SHORTDATEFORMAT = 0x0000,
            DTS_LONGDATEFORMAT = 0x0004,
            DTS_TIMEFORMAT = 0x0009,
            DTS_APPCANPARSE = 0x0010,
            DTS_RIGHTALIGN = 0x0020
        }
             #endregion
        #region DateTimePicker Control Messages
        internal enum DateTimePickerMessages
        {
            DTM_FIRST = 0x1000,
            DTM_GETSYSTEMTIME = (DTM_FIRST + 1),
            DTM_SETSYSTEMTIME = (DTM_FIRST + 2),
            DTM_GETRANGE = (DTM_FIRST + 3),
            DTM_SETRANGE = (DTM_FIRST + 4),
            DTM_SETFORMATA = (DTM_FIRST + 5),
            DTM_SETMCCOLOR = (DTM_FIRST + 6),
            DTM_GETMCCOLOR = (DTM_FIRST + 7),
            DTM_GETMONTHCAL = (DTM_FIRST + 8),
            DTM_SETMCFONT = (DTM_FIRST + 9),
            DTM_GETMCFONT = (DTM_FIRST + 10),
            DTM_SETFORMATW = (DTM_FIRST + 50)
        }
        #endregion
        #region DateTimePicker Control Flags
        internal enum DateTimePickerFlags
        {
            GDT_ERROR = -1,
            GDT_VALID = 0,
            GDT_NONE = 1,
            GDTR_MIN = 0x0001,
            GDTR_MAX = 0x0002
        }
        #endregion
        #region DateTimePicker Notifications
        internal enum DateTimePickerNotifications
        {
            DTN_FIRST = (0 - 760),
            DTN_DATETIMECHANGE = (DTN_FIRST + 1),
            DTN_USERSTRINGA = (DTN_FIRST + 2),
            DTN_WMKEYDOWNA = (DTN_FIRST + 3),
            DTN_FORMATA = (DTN_FIRST + 4),
            DTN_FORMATQUERYA = (DTN_FIRST + 5),
            DTN_DROPDOWN = (DTN_FIRST + 6),
            DTN_CLOSEUP = (DTN_FIRST + 7),
            DTN_USERSTRINGW = (DTN_FIRST + 15),
            DTN_WMKEYDOWNW = (DTN_FIRST + 16),
            DTN_FORMATW = (DTN_FIRST + 17),
            DTN_FORMATQUERYW = (DTN_FIRST + 18)
        }
        #endregion
        #region MonthCal Colors
        internal enum MonthCalColors
        {
            MCSC_BACKGROUND = 0,
            MCSC_TEXT = 1,
            MCSC_TITLEBK = 2,
            MCSC_TITLETEXT = 3,
            MCSC_MONTHBK = 4,
            MCSC_TRAILINGTEXT = 5
        }
                #endregion
#endregion
#region TabControl Stuff
        public enum TabControlMessages : int
        {
            TCM_FIRST = 0x1300,
            TCM_GETITEMCOUNT = (TCM_FIRST + 4),
            TCM_SETIMAGELIST = (TCM_FIRST + 3),
            TCM_GETITEM = (TCM_FIRST + 60),
            TCM_SETITEM = (TCM_FIRST + 6),
            TCM_INSERTITEM = (TCM_FIRST + 7),
            TCM_DELETEITEM = (TCM_FIRST + 8),
            TCM_DELETEALLITEMS = (TCM_FIRST + 9),
            TCM_GETITEMRECT = (TCM_FIRST + 10),
            TCM_GETCURSEL = (TCM_FIRST + 11),
            TCM_SETCURSEL = (TCM_FIRST + 12),
            TCM_HITTEST = (TCM_FIRST + 13),
            TCM_ADJUSTRECT = (TCM_FIRST + 40),
            TCM_SETITEMSIZE = (TCM_FIRST + 41),
            TCM_REMOVEIMAGE = (TCM_FIRST + 42),
            TCM_SETPADDING = (TCM_FIRST + 43),
            TCM_GETROWCOUNT = (TCM_FIRST + 44),
            TCM_GETTOOLTIPS = (TCM_FIRST + 45),
            TCM_SETTOOLTIPS = (TCM_FIRST + 46),
            TCM_GETCURFOCUS = (TCM_FIRST + 47),
            TCM_SETCURFOCUS = (TCM_FIRST + 48),
            TCM_SETMINTABWIDTH = (TCM_FIRST + 49),
            TCM_DESELECTALL = (TCM_FIRST + 50),
            TCM_HIGHLIGHTITEM = (TCM_FIRST + 51)
        }
        public enum TabControlStyles : int
        {
            TCS_SCROLLOPPOSITE = 0x0001,
            TCS_BOTTOM = 0x0002,
            //TCS_RIGHT = 0x0002,
            TCS_MULTISELECT = 0x0004,
            TCS_FLATBUTTONS = 0x0008,
            TCS_FORCEICONLEFT = 0x0010,
            TCS_FORCELABELLEFT = 0x0020,
            TCS_HOTTRACK = 0x0040,
            TCS_VERTICAL = 0x0080,
            //TCS_TABS = 0x0000,
            TCS_BUTTONS = 0x0100,
            TCS_SINGLELINE = 0x0000,
            TCS_MULTILINE = 0x0200,
            //TCS_RIGHTJUSTIFY = 0x0000,
            TCS_FIXEDWIDTH = 0x0400,
            TCS_RAGGEDRIGHT = 0x0800,
            TCS_FOCUSONBUTTONDOWN = 0x1000,
            TCS_OWNERDRAWFIXED = 0x2000,
            TCS_TOOLTIPS = 0x4000,
            TCS_FOCUSNEVER = 0x8000
        }
        public enum TabControlNotifications : int
        {
            TCN_FIRST = 0 - 550,
            TCN_SELCHANGE = (TCN_FIRST - 1),
            TCN_SELCHANGING = (TCN_FIRST - 2)
        }
        public enum TabControlItemFlags : int
        {
            TCIF_TEXT = 0x0001,
            TCIF_IMAGE = 0x0002,
            TCIF_RTLREADING = 0x0004,
            TCIF_PARAM = 0x0008,
            TCIF_STATE = 0x0010
        }
#endregion
#region Edit Stuff
        public enum ES : int // EditControlStyles
        {
            LEFT = 0x0000,
            CENTER = 0x0001,
            RIGHT = 0x0002,
            MULTILINE = 0x0004,
            UPPERCASE = 0x0008,
            LOWERCASE = 0x0010,
            PASSWORD = 0x0020,
            AUTOVSCROLL = 0x0040,
            AUTOHSCROLL = 0x0080,
            NOHIDESEL = 0x0100,
            OEMCONVERT = 0x0400,
            READONLY = 0x0800,
            WANTRETURN = 0x1000
        }
        internal enum EditControlMessages : int
        {
            EM_GETSEL = 0x00B0,
            EM_SETSEL = 0x00B1,
            EM_GETRECT = 0x00B2,
            EM_SETRECT = 0x00B3,
            EM_SETRECTNP = 0x00B4,
            EM_SCROLL = 0x00B5,
            EM_LINESCROLL = 0x00B6,
            EM_SCROLLCARET = 0x00B7,
            EM_GETMODIFY = 0x00B8,
            EM_SETMODIFY = 0x00B9,
            EM_GETLINECOUNT = 0x00BA,
            EM_LINEINDEX = 0x00BB,
            EM_SETHANDLE = 0x00BC,
            EM_GETHANDLE = 0x00BD,
            EM_GETTHUMB = 0x00BE,
            EM_LINELENGTH = 0x00C1,
            EM_REPLACESEL = 0x00C2,
            EM_GETLINE = 0x00C4,
            EM_LIMITTEXT = 0x00C5,
            EM_CANUNDO = 0x00C6,
            EM_UNDO = 0x00C7,
            EM_FMTLINES = 0x00C8,
            EM_LINEFROMCHAR = 0x00C9,
            EM_SETTABSTOPS = 0x00CB,
            EM_SETPASSWORDCHAR = 0x00CC,
            EM_EMPTYUNDOBUFFER = 0x00CD,
            EM_GETFIRSTVISIBLELINE = 0x00CE,
            EM_SETREADONLY = 0x00CF,
            EM_SETWORDBREAKPROC = 0x00D0,
            EM_GETWORDBREAKPROC = 0x00D1,
            EM_GETPASSWORDCHAR = 0x00D2,
            EM_SETMARGINS = 0x00D3,
            EM_GETMARGINS = 0x00D4,
            EM_SETLIMITTEXT = EM_LIMITTEXT,
            EM_GETLIMITTEXT = 0x00D5,
            EM_POSFROMCHAR = 0x00D6,
            EM_CHARFROMPOS = 0x00D7
        }
        internal enum EditControlNotifications : int
        {
            EN_CHANGE = 0x0300,
            EN_UPDATE = 0x0400,
            EN_ERRSPACE = 0x0500,
            EN_MAXTEXT = 0x0501,
            EN_HSCROLL = 0x0601,
            EN_VSCROLL = 0x0602
        }
        public enum RichTextEditControlMessages : uint
        {
            WM_USER = 0x0400,
            EM_CANPASTE = (WM_USER + 50),
            EM_DISPLAYBAND = (WM_USER + 51),
            EM_EXGETSEL = (WM_USER + 52),
            EM_EXLIMITTEXT = (WM_USER + 53),
            EM_EXLINEFROMCHAR = (WM_USER + 54),
            EM_EXSETSEL = (WM_USER + 55),
            EM_FINDTEXT = (WM_USER + 56),
            EM_FORMATRANGE = (WM_USER + 57),
            EM_GETCHARFORMAT = (WM_USER + 58),
            EM_GETEVENTMASK = (WM_USER + 59),
            EM_GETOLEINTERFACE = (WM_USER + 60),
            EM_GETPARAFORMAT = (WM_USER + 61),
            EM_GETSELTEXT = (WM_USER + 62),
            EM_HIDESELECTION = (WM_USER + 63),
            EM_PASTESPECIAL = (WM_USER + 64),
            EM_REQUESTRESIZE = (WM_USER + 65),
            EM_SELECTIONTYPE = (WM_USER + 66),
            EM_SETBKGNDCOLOR = (WM_USER + 67),
            EM_SETCHARFORMAT = (WM_USER + 68),
            EM_SETEVENTMASK = (WM_USER + 69),
            EM_SETOLECALLBACK = (WM_USER + 70),
            EM_SETPARAFORMAT = (WM_USER + 71),
            EM_SETTARGETDEVICE = (WM_USER + 72),
            EM_STREAMIN = (WM_USER + 73),
            EM_STREAMOUT = (WM_USER + 74),
            EM_GETTEXTRANGE = (WM_USER + 75),
            EM_FINDWORDBREAK = (WM_USER + 76),
            EM_SETOPTIONS = (WM_USER + 77),
            EM_GETOPTIONS = (WM_USER + 78),
            EM_FINDTEXTEX = (WM_USER + 79),
            EM_GETWORDBREAKPROCEX = (WM_USER + 80),
            EM_SETWORDBREAKPROCEX = (WM_USER + 81),
            EM_SETUNDOLIMIT = (WM_USER + 82),
            EM_REDO = (WM_USER + 84),
            EM_CANREDO = (WM_USER + 85),
            EM_GETUNDONAME = (WM_USER + 86),
            EM_GETREDONAME = (WM_USER + 87),
            EM_STOPGROUPTYPING = (WM_USER + 88),
            EM_SETTEXTMODE = (WM_USER + 89),
            EM_GETTEXTMODE = (WM_USER + 90),
            EM_AUTOURLDETECT = (WM_USER + 91),
            EM_GETAUTOURLDETECT = (WM_USER + 92),
            EM_SETPALETTE = (WM_USER + 93),
            EM_GETTEXTEX = (WM_USER + 94),
            EM_GETTEXTLENGTHEX = (WM_USER + 95),
            EM_SETPUNCTUATION = (WM_USER + 100),
            EM_GETPUNCTUATION = (WM_USER + 101),
            EM_SETWORDWRAPMODE = (WM_USER + 102),
            EM_GETWORDWRAPMODE = (WM_USER + 103),
            EM_SETIMECOLOR = (WM_USER + 104),
            EM_GETIMECOLOR = (WM_USER + 105),
            EM_SETIMEOPTIONS = (WM_USER + 106),
            EM_GETIMEOPTIONS = (WM_USER + 107),
            EM_CONVPOSITION = (WM_USER + 108),
            EM_SETLANGOPTIONS = (WM_USER + 120),
            EM_GETLANGOPTIONS = (WM_USER + 121),
            EM_GETIMECOMPMODE = (WM_USER + 122),
            EM_FINDTEXTW = (WM_USER + 123),
            EM_FINDTEXTEXW = (WM_USER + 124),
            EM_SETBIDIOPTIONS = (WM_USER + 200),
            EM_GETBIDIOPTIONS = (WM_USER + 201)
        }
#endregion
#region UpDown Stuff
        internal enum UpDownControlStyles : int
        {
            UDS_WRAP = 0x0001,
            UDS_SETBUDDYINT = 0x0002,
            UDS_ALIGNRIGHT = 0x0004,
            UDS_ALIGNLEFT = 0x0008,
            UDS_AUTOBUDDY = 0x0010,
            UDS_ARROWKEYS = 0x0020,
            UDS_HORZ = 0x0040,
            UDS_NOTHOUSANDS = 0x0080,
            UDS_HOTTRACK = 0x0100
        }
        internal enum UpDownControlMessages : int
        {
            WM_USER = 0x0400,
            UDN_FIRST = (0 - 721),
            UDN_LAST = (0 - 740),
            UDN_DELTAPOS = (UDN_FIRST - 1),
            UDM_SETRANGE = (WM_USER + 101),
            UDM_GETRANGE = (WM_USER + 102),
            UDM_SETPOS = (WM_USER + 103),
            UDM_GETPOS = (WM_USER + 104),
            UDM_SETBUDDY = (WM_USER + 105),
            UDM_GETBUDDY = (WM_USER + 106),
            UDM_SETACCEL = (WM_USER + 107),
            UDM_GETACCEL = (WM_USER + 108),
            UDM_SETBASE = (WM_USER + 109),
            UDM_GETBASE = (WM_USER + 110),
            UDM_SETRANGE32 = (WM_USER + 111),
            UDM_GETRANGE32 = (WM_USER + 112),
            //UDM_SETUNICODEFORMAT    =CCM_SETUNICODEFORMAT,
            //UDM_GETUNICODEFORMAT    =CCM_GETUNICODEFORMAT,
            UDM_SETPOS32 = (WM_USER + 113),
            UDM_GETPOS32 = (WM_USER + 114)
        }
#endregion
#region ProgressBar Stuff
        internal enum ProgressBarMessages : int
        {
            WM_USER = 0x0400,
            PBM_SETRANGE = WM_USER + 1,
            PBM_SETPOS = WM_USER + 2,
            PBM_DELTAPOS = WM_USER + 3,
            PBM_SETSTEP = WM_USER + 4,
            PBM_STEPIT = WM_USER + 5,
            PBM_SETRANGE32 = WM_USER + 6,
            PBM_GETRANGE = WM_USER + 7,
            PBM_GETPOS = WM_USER + 8,
            PBM_SETBARCOLOR = WM_USER + 9,
            PBM_SETBKCOLOR = CommonControlMessages.CCM_SETBKCOLOR
        }
#endregion
#region Header stuff
        #region Header Control Messages
        public enum HeaderMessages : int
        {
            HDM_FIRST = 0x1200,// Header messages
            HDM_GETITEMCOUNT = (HDM_FIRST + 0),
            HDM_INSERTITEMA = (HDM_FIRST + 1),
            HDM_INSERTITEMW = (HDM_FIRST + 10),
            HDM_DELETEITEM = (HDM_FIRST + 2),
            HDM_GETITEMA = (HDM_FIRST + 3),
            HDM_GETITEMW = (HDM_FIRST + 11),
            HDM_SETITEMA = (HDM_FIRST + 4),
            HDM_SETITEMW = (HDM_FIRST + 12),
            HDM_LAYOUT = (HDM_FIRST + 5),
            HDM_HITTEST = (HDM_FIRST + 6),
            HDM_GETITEMRECT = (HDM_FIRST + 7),
            HDM_SETIMAGELIST = (HDM_FIRST + 8),
            HDM_GETIMAGELIST = (HDM_FIRST + 9),
            HDM_ORDERTOINDEX = (HDM_FIRST + 15),
            HDM_CREATEDRAGIMAGE = (HDM_FIRST + 16),
            HDM_GETORDERARRAY = (HDM_FIRST + 17),
            HDM_SETORDERARRAY = (HDM_FIRST + 18),
            HDM_SETHOTDIVIDER = (HDM_FIRST + 19),
            HDM_SETBITMAPMARGIN = (HDM_FIRST + 20),
            HDM_GETBITMAPMARGIN = (HDM_FIRST + 21),
            HDM_SETFILTERCHANGETIMEOUT = (HDM_FIRST + 22),
            HDM_EDITFILTER = (HDM_FIRST + 23),
            HDM_CLEARFILTER = (HDM_FIRST + 24)
        }

        #endregion
        #region Header Control Notifications
        public enum HeaderControlNotifications
        {
            HDN_FIRST = (0 - 300),
            HDN_ITEMCLICKW = (HDN_FIRST - 22),
            HDN_ITEMDBLCLICKW = (HDN_FIRST - 23),
            HDN_DIVIDERDBLCLICKW = (HDN_FIRST - 25),
            HDN_BEGINTRACKW = (HDN_FIRST - 26),
            HDN_ENDTRACKW = (HDN_FIRST - 27)
        }
        #endregion
        #region Header Control HitTest Flags
        public enum HeaderControlHitTestFlags
        {
            HHT_NOWHERE = 0x0001,
            HHT_ONHEADER = 0x0002,
            HHT_ONDIVIDER = 0x0004,
            HHT_ONDIVOPEN = 0x0008,
            HHT_ABOVE = 0x0100,
            HHT_BELOW = 0x0200,
            HHT_TORIGHT = 0x0400,
            HHT_TOLEFT = 0x0800
        }
        #endregion

#endregion
#region Menu Stuff
        ///////////////
        // MenuItemInfo Mask constants
        public const int MIIM_STATE = 1;
        public const int MIIM_ID = 2;
        public const int MIIM_SUBMENU = 4;
        public const int MIIM_CHECKMARKS = 8;
        public const int MIIM_TYPE = 16;
        public const int MIIM_DATA = 32;
        public const int MIIM_STRING = 64;
        public const int MIIM_FTYPE = 256;
        // Menu flag constants:
        public const int MF_BYPOSITION = 1024;
        public const int MF_BYCOMMAND = 0;
        public const int MF_ENABLED = 0;
        public const int MF_GRAYED = 1;
        public const int MF_DISABLED = 2;
        public const int MF_SEPARATOR = 2048;
        public const int MF_STRING = 0;
        public const int MF_OWNERDRAW = 256;
        public const int MF_CHECKED = 8;
        public const int MF_UNCHECKED = 0;
        public const int MF_HILITE = 128;
        public const int MF_APPEND = 256;
        public const int MF_BITMAP = 4;
        public const int MF_CALLBACKS = 134217728;
        public const int MF_CHANGE = 128;
        public const int MF_CONV = 1073741824;
        public const int MF_DELETE = 512;
        public const int MF_END = 128;
        public const int MF_ERRORS = 268435456;
        public const int MF_HELP = 16384;
        public const int MF_HSZ_INFO = 16777216;
        public const int MF_INSERT = 0;
        public const int MF_LINKS = 536870912;
        public const int MF_MASK = -16777216;
        public const int MF_MENUBARBREAK = 32;
        public const int MF_MENUBREAK = 64;
        public const int MF_MOUSESELECT = 32768;
        public const int MF_POPUP = 16;
        public const int MF_POSTMSGS = 67108864;
        public const int MF_REMOVE = 4096;
        public const int MF_SENDMSGS = 33554432;
        public const int MF_SYSMENU = 8192;
        public const int MF_UNHILITE = 0;
        public const int MF_USECHECKBITMAPS = 512;
        public const int MF_DEFAULT = 4096;
        public const int MFT_BITMAP = MF_BITMAP;
        public const int MFT_MENUBARBREAK = MF_MENUBARBREAK;
        public const int MFT_MENUBREAK = MF_MENUBREAK;
        public const int MFT_OWNERDRAW = MF_OWNERDRAW;
        public const int MFT_RADIOCHECK = 512;
        public const int MFT_SEPARATOR = MF_SEPARATOR;
        public const int MFT_RIGHTORDER = 8192;
        // New versions of the names...
        public const int MFS_GRAYED = 3;
        public const int MFS_DISABLED = MFS_GRAYED;
        public const int MFS_CHECKED = MF_CHECKED;
        public const int MFS_HILITE = MF_HILITE;
        public const int MFS_ENABLED = MF_ENABLED;
        public const int MFS_UNCHECKED = MF_UNCHECKED;
        public const int MFS_UNHILITE = MF_UNHILITE;
        public const int MFS_DEFAULT = MF_DEFAULT;

        [DllImport("user32.dll", EntryPoint = "IsMenu", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int IsMenu(IntPtr hMenu);

        [StructLayout(LayoutKind.Sequential)]
        public struct MENUITEMINFO
        {
            public int cbSize;
            public int fMask;
            public int fType;
            public int fState;
            public int wID;
            public int hSubMenu;
            public int hbmpChecked;
            public int hbmpUnchecked;
            public int dwItemData;
            public string dwTypeData;
            public int cch;
            public int hbmpItem;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetMenu(IntPtr hwnd);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetMenu(int hwnd);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetMenuItemCount(int hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        //[DllImport("user32.dll")]
        //public static extern int GetMenuItemRect(int hWnd, int hMenu, int uItem, [MarshalAs(UnmanagedType.Struct)] ref  RECT lprcItem);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetMenuItemInfo(int hMenu, int un, int b, [MarshalAs(UnmanagedType.Struct)] ref  MENUITEMINFO lpMenuItemInfo);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetSubMenu(int hMenu, int nPos);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetMenuString(int hMenu, int wIDItem, StringBuilder lpString, int nMaxCount, int wFlag);

        //[DllImport("user32")]
        //public static extern int GetMenuState(int hMenu, int wID, int wFlags);

        //[DllImport("user32")]
        //public static extern int HiliteMenuItem(int hwnd, int hMenu, int wIDHiliteItem, int wHilite);

        //[DllImport("user32")]
        //public static extern int GetMenuItemID(int hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetMenuItemInfo(IntPtr hMenu, int un, int b, [MarshalAs(UnmanagedType.Struct)] ref  MENUITEMINFO lpMenuItemInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetMenuString(IntPtr hMenu, int wIDItem, StringBuilder lpString, int nMaxCount, int wFlag);

        [DllImport("user32")]
        public static extern int GetMenuState(IntPtr hMenu, int wID, int wFlags);

        [DllImport("user32")]
        public static extern int HiliteMenuItem(IntPtr hwnd, IntPtr hMenu, int wIDHiliteItem, int wHilite);

        [DllImport("user32")]
        public static extern int GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("user32")]
        public static extern int GetMenuItemRect(IntPtr hwnd, IntPtr hMenu, int uItem, ref RECT lprcItem);

        //[DllImport("user32")]
        //public static extern int GetMenuItemRect(int hwnd, int hMenu, int uItem, ref RECT lprcItem);

#endregion

#endregion




        // useful list of all the WMs from what I can tell...  not always
        // correct naming convention though.
        public enum WM
        {
            ACTIVATE = 6,
            ACTIVATEAPP = 28,
            AFXFIRST = 864,
            AFXLAST = 895,
            APP = 32768,
            ASKCBFORMATNAME = 780,
            CANCELJOURNAL = 75,
            CANCELMODE = 31,
            CAPTURECHANGED = 533,
            CHANGECBCHAIN = 781,
            CHAR = 258,
            CHARTOITEM = 47,
            CHILDACTIVATE = 34,
            CLEAR = 771,
            CLOSE = 16,
            COMMAND = 273,
            COMPACTING = 65,
            COMPAREITEM = 57,
            CONTEXTMENU = 123,
            COPY = 769,
            COPYDATA = 74,
            CREATE = 1,
            CTLCOLORBTN = 309,
            CTLCOLORDLG = 310,
            CTLCOLOREDIT = 307,
            CTLCOLORLISTBOX = 308,
            CTLCOLORMSGBOX = 306,
            CTLCOLORSCROLLBAR = 311,
            CTLCOLORSTATIC = 312,
            CUT = 768,
            DEADCHAR = 259,
            DELETEITEM = 45,
            DESTROY = 2,
            DESTROYCLIPBOARD = 775,
            DEVICECHANGE = 537,
            DEVMODECHANGE = 27,
            DISPLAYCHANGE = 126,
            DRAWCLIPBOARD = 776,
            DRAWITEM = 43,
            DROPFILES = 563,
            ENABLE = 10,
            ENDSESSION = 22,
            ENTERIDLE = 289,
            ENTERMENULOOP = 529,
            ENTERSIZEMOVE = 561,
            ERASEBKGND = 20,
            EXITMENULOOP = 530,
            EXITSIZEMOVE = 562,
            FONTCHANGE = 29,
            GETDLGCODE = 135,
            GETFONT = 49,
            GETHOTKEY = 51,
            GETICON = 127,
            GETMINMAXINFO = 36,
            GETOBJECT = 61,
            GETTEXT = 13,
            GETTEXTLENGTH = 14,
            HANDHELDFIRST = 856,
            HANDHELDLAST = 863,
            HELP = 83,
            HOTKEY = 786,
            HSCROLL = 276,
            HSCROLLCLIPBOARD = 782,
            ICONERASEBKGND = 39,
            IME_CHAR = 646,
            IME_COMPOSITION = 271,
            IME_COMPOSITIONFULL = 644,
            IME_CONTROL = 643,
            IME_ENDCOMPOSITION = 270,
            IME_KEYDOWN = 656,
            IME_KEYLAST = 271,
            IME_KEYUP = 657,
            IME_NOTIFY = 642,
            IME_REQUEST = 648,
            IME_SELECT = 645,
            IME_SETCONTEXT = 641,
            IME_STARTCOMPOSITION = 269,
            INITDIALOG = 272,
            INITMENU = 278,
            INITMENUPOPUP = 279,
            INPUTLANGCHANGE = 81,
            INPUTLANGCHANGEREQUEST = 80,
            KEYDOWN = 256,
            KEYFIRST = 256,
            KEYLAST = 264,
            KEYUP = 257,
            KILLFOCUS = 8,
            LBUTTONDBLCLK = 515,
            LBUTTONDOWN = 513,
            LBUTTONUP = 514,
            MBUTTONDBLCLK = 521,
            MBUTTONDOWN = 519,
            MBUTTONUP = 520,
            MDIACTIVATE = 546,
            MDICASCADE = 551,
            MDICREATE = 544,
            MDIDESTROY = 545,
            MDIGETACTIVE = 553,
            MDIICONARRANGE = 552,
            MDIMAXIMIZE = 549,
            MDINEXT = 548,
            MDIREFRESHMENU = 564,
            MDIRESTORE = 547,
            MDISETMENU = 560,
            MDITILE = 550,
            MEASUREITEM = 44,
            MENUCHAR = 288,
            MENUCOMMAND = 294,
            MENUDRAG = 291,
            MENUGETOBJECT = 292,
            MENURBUTTONUP = 290,
            MENUSELECT = 287,
            MOUSEACTIVATE = 33,
            MOUSEFIRST = 512,
            MOUSEHOVER = 673,
            MOUSELAST = 522,
            MOUSELEAVE = 675,
            MOUSEMOVE = 512,
            MOUSEWHEEL = 522,
            MOVE = 3,
            MOVING = 534,
            NCACTIVATE = 134,
            NCCALCSIZE = 131,
            NCCREATE = 129,
            NCDESTROY = 130,
            NCHITTEST = 132,
            NCLBUTTONDBLCLK = 163,
            NCLBUTTONDOWN = 161,
            NCLBUTTONUP = 162,
            NCMBUTTONDBLCLK = 169,
            NCMBUTTONDOWN = 167,
            NCMBUTTONUP = 168,
            NCMOUSEMOVE = 160,
            NCPAINT = 133,
            NCRBUTTONDBLCLK = 166,
            NCRBUTTONDOWN = 164,
            NCRBUTTONUP = 165,
            NEXTDLGCTL = 40,
            NEXTMENU = 531,
            NOTIFY = 78,
            NOTIFYFORMAT = 85,
            NULL = 0,
            PAINT = 15,
            PAINTCLIPBOARD = 777,
            PAINTICON = 38,
            PALETTECHANGED = 785,
            PALETTEISCHANGING = 784,
            PARENTNOTIFY = 528,
            PASTE = 770,
            PENWINFIRST = 896,
            PENWINLAST = 911,
            POWER = 72,
            PRINT = 791,
            PRINTCLIENT = 792,
            QUERYDRAGICON = 55,
            QUERYENDSESSION = 17,
            QUERYNEWPALETTE = 783,
            QUERYOPEN = 19,
            QUEUESYNC = 35,
            QUIT = 18,
            RBUTTONDBLCLK = 518,
            RBUTTONDOWN = 516,
            RBUTTONUP = 517,
            RENDERALLFORMATS = 774,
            RENDERFORMAT = 773,
            SETCURSOR = 32,
            SETFOCUS = 7,
            SETFONT = 48,
            SETHOTKEY = 50,
            SETICON = 128,
            SETREDRAW = 11,
            SETTEXT = 12,
            SETTINGCHANGE = 26,
            SHOWWINDOW = 24,
            SIZE = 5,
            SIZECLIPBOARD = 779,
            SIZING = 532,
            SPOOLERSTATUS = 42,
            STYLECHANGED = 125,
            STYLECHANGING = 124,
            SYNCPAINT = 136,
            SYSCHAR = 262,
            SYSCOLORCHANGE = 21,
            SYSCOMMAND = 274,
            SYSDEADCHAR = 263,
            SYSKEYDOWN = 260,
            SYSKEYUP = 261,
            TCARD = 82,
            TIMECHANGE = 30,
            TIMER = 275,
            UNDO = 772,
            UNINITMENUPOPUP = 293,
            USER = 1024,
            USERCHANGED = 84,
            VKEYTOITEM = 46,
            VSCROLL = 277,
            VSCROLLCLIPBOARD = 778,
            WINDOWPOSCHANGED = 71,
            WINDOWPOSCHANGING = 70,
            WININICHANGE = 26,
        }
        public enum WS_EX : uint
        {
            WS_EX_ACCEPTFILES = 16,
            WS_EX_APPWINDOW = 262144,
            WS_EX_CLIENTEDGE = 512,
            WS_EX_COMPOSITED = 0x2000000,
            WS_EX_CONTEXTHELP = 1024,
            WS_EX_CONTROLPARENT = 65536,
            WS_EX_DLGMODALFRAME = 1,
            WS_EX_LAYERED = 524288,
            WS_EX_LAYOUTRTL = 4194304,
            WS_EX_LEFT = 0,
            WS_EX_LEFTSCROLLBAR = 16384,
            WS_EX_LTRREADING = 0,
            WS_EX_MDICHILD = 64,
            WS_EX_NOACTIVATE = 134217728,
            WS_EX_NOINHERITLAYOUT = 1048576,
            WS_EX_NOPARENTNOTIFY = 4,
            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | (WS_EX_TOOLWINDOW | WS_EX_TOPMOST)),
            WS_EX_RIGHT = 4096,
            WS_EX_RIGHTSCROLLBAR = 0,
            WS_EX_RTLREADING = 8192,
            WS_EX_STATICEDGE = 131072,
            WS_EX_TOOLWINDOW = 128,
            WS_EX_TOPMOST = 8,
            WS_EX_TRANSPARENT = 32,
            WS_EX_WINDOWEDGE = 256,
        }
        public enum WS : uint
        {
            WS_ACTIVECAPTION = 1,
            WS_BORDER = 8388608,
            WS_CAPTION = 12582912,
            WS_CHILD = 1073741824,
            WS_CHILDWINDOW = WS_CHILD,
            WS_CLIPCHILDREN = 33554432,
            WS_CLIPSIBLINGS = 67108864,
            WS_DISABLED = 134217728,
            WS_DLGFRAME = 4194304,
            WS_GROUP = 131072,
            WS_HSCROLL = 1048576,
            WS_ICONIC = WS_MINIMIZE,
            WS_MAXIMIZE = 16777216,
            WS_MAXIMIZEBOX = 65536,
            WS_MINIMIZE = 536870912,
            WS_MINIMIZEBOX = 131072,
            WS_OVERLAPPED = 0,
            WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED
                        | (WS_CAPTION
                        | (WS_SYSMENU
                        | (WS_THICKFRAME
                        | (WS_MINIMIZEBOX | WS_MAXIMIZEBOX))))),
            // POPUP = &H80000000 'shouldn't this work?
            WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
            WS_POPUP = 2147483648,
            //0x80000000
            WS_SIZEBOX = WS_THICKFRAME,
            WS_SYSMENU = 524288,
            WS_TABSTOP = 65536,
            WS_THICKFRAME = 262144,
            WS_TILED = WS_OVERLAPPED,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
            WS_VISIBLE = 268435456,
            WS_VSCROLL = 2097152,
        }
        public enum SW
        {
            HIDE = 0,
            SHOWNORMAL = 1,
            SHOWMINIMIZED = 2,
            SHOWMAXIMIZED = 3,
            NORMALNOACTIVATE = 4,
            SHOW = 5,
            MINIMIZED = 6,
            SHOWMINIMIZEDNOACTIVATE = 7,
            SHOWNA = 8,
            RESTORE = 9,
        }

        // '''''''''''''''''''''''''''''''''''''''''''''
        // Structures
        // '''''''''''''''''''''''''''''''''''''''''''''
        // 32 bit integer 
        public struct POINTAPI
        {
            public int X;
            public int Y;
        }

        public struct WNDCLASS
        {
            public int style;
            public int lpfnwndproc;
            public int cbClsextra;
            public int cbWndExtra2;
            public int hInstance;
            public int hIcon;
            public int hCursor;
            public int hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
        }

        public struct WndClassEX
        {
            public int cbSize;
            public int style;
            public int lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public int hInstance;
            public int hIcon;
            public int hCursor;
            public int hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public int hIconSm;
        }

        public struct FLASHWINFO
        {
            public int cbSize;
            public Int32 hwnd;
            public int dwFlags;
            public int uCount;
            public int dwTimeout;
        }

        public enum GWL : int
        {
            WNDPROC = (-4),
            HINSTANCE = (-6),
            HWNDPARENT = (-8),
            ID = (-12),
            STYLE = (-16),
            EXSTYLE = (-20),
            USERDATA = (-21)
        }

        //flags (the thread state)
        //hwndActive (Handle to the active window within the thread)
        //hwndFocus (Handle to the window that has the keyboard focus)
        //hwndCapture (Handle to the window that has captured the mouse)
        //hwndMenuOwner (Handle to the window that owns any active menus)
        //hwndMoveSize (Handle to the window in a move or size loop)
        //hwndCaret (Handle to the window that is displaying the caret), and 
        //rcCaret (A RECT structure that describes the caret's bounding rectangle, in client coordinates, relative to the window specified by the hwndCaret member).

        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCapred;
            public RECT rcCaret;
        };

        public struct RECT
        {
            public uint left;
            public uint top;
            public uint right;
            public uint bottom;
        }

        #region SetWindowPos Z Order
        public enum SetWindowPosZOrder
        {
            HWND_TOP = 0,
            HWND_BOTTOM = 1,
            HWND_TOPMOST = -1,
            HWND_NOTOPMOST = -2
            //		public const int HWND_MESSAGE = -3;
        }
        #endregion

        #region SetWindowPosFlags
        [Flags]
        public enum SetWindowPosFlags
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOREDRAW = 0x0008,
            SWP_NOACTIVATE = 0x0010,
            SWP_FRAMECHANGED = 0x0020,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_DRAWFRAME = SWP_FRAMECHANGED,
            SWP_NOREPOSITION = SWP_NOOWNERZORDER,
            SWP_DEFERERASE = 0x2000,
            SWP_ASYNCWINDOWPOS = 0x4000
        }
        #endregion


        public delegate bool EnumChildProcDelegate(IntPtr hWnd, int lParam);
    }
}