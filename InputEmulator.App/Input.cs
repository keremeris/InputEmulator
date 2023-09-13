using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace InputEmulator.App
{
    public static class Input
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void KeyDown(Key key)
        {
            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            uint keyCode = MapVirtualKey((uint)virtualKey, 0);

            keybd_event((byte)virtualKey, (byte)keyCode, 0, 0);
        }

        public static void KeyUp(Key key)
        {
            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            uint keyCode = MapVirtualKey((uint)virtualKey, 0);

            keybd_event((byte)virtualKey, (byte)keyCode, 0x0002, 0);
        }

        public static Key ResolveKey(char @char)
        {
            return KeyInterop.KeyFromVirtualKey(VkKeyScan(@char));
        }

        public static void MoveCursor(Point point)
        {
            int x = (int)(point.X * 65535.0 / (SystemParameters.PrimaryScreenWidth - 1)) + 1;
            int y = (int)(point.Y * 65535.0 / (SystemParameters.PrimaryScreenHeight - 1)) + 1;

            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
        }

        public static void MoveCursor(double X, double Y)
        {
            MoveCursor(new Point(X, Y));
        }

        public static void MouseLeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseLeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseRightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void MouseRightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);
    }
}
