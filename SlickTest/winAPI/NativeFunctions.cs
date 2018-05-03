using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WinAPI
{
    public class NativeFunctions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetDriveType(string rootDir);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr handle, API.SW nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumWindows(NativeCallbackHandler ewp, IntPtr ByVallParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, NativeCallbackHandler ByVallpEnumProc, IntPtr parameter);

        // using same types as the Message class
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int message, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll", CharSet=CharSet.Auto)]
        //public static void SendMessage(IntPtr handle, int message, IntPtr wParam, void <, void MarshalAs, void UnmanagedType.LPWStr) {
        //  ((Text.StringBuilder)(lParam));
        //  Integer;
        //}

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        //[DllImport("wininet.dll")]
        //public static bool InternetGetConnectedState(ref int description, int reservedValue) {
        //}

        public delegate bool NativeCallbackHandler(IntPtr handle, int parameter);
    }
}
