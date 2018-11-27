using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace BackgroundScripts
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            while (true)
            {
                ThumbdriveBackup.Backup();
                Thread.Sleep(600000);
            }
        }
    }
}
