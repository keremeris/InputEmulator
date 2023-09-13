using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace InputEmulator.Test
{
    public class Window
    {
        public static Rect GetWindowRect(string processName, string windowTitle)
        {
            IntPtr hWnd = IntPtr.Zero;

            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                if (process.MainWindowTitle == windowTitle)
                {
                    hWnd = process.MainWindowHandle;
                    break;
                }
            }

            if (hWnd == IntPtr.Zero)
            {
                throw new Exception($"Window not found: {processName} {windowTitle}");
            }

            RECT rect;
            GetWindowRect(hWnd, out rect);
            return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }


        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
