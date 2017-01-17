using System;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ScreenShot
{

    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool minimize = false;
            foreach (string s in args)
            {
                if (s == "/minimize")
                    minimize = true;
                else if (s == "/SetAutoSetUp")
                {
                    if (!RegistryHandle.SetRegistryKey(true))
                        MessageBox.Show("Need permission to set auto start\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (s == "/ClearAutoSetUp")
                {
                    if (!RegistryHandle.SetRegistryKey(false))
                        MessageBox.Show("Need permission to set auto start\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Process instance = RunningInstance();
            if (instance == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(minimize));
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop   through   the   running   processes   in   with   the   same   name
            foreach (Process process in processes)
            {
                //Ignore   the   current   process
                if (process.Id != current.Id)
                {
                    //Make   sure   that   the   process   is   running   from   the   exe   file.
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                        current.MainModule.FileName)
                    {
                        //Return   the   other   process   instance.
                        return process;
                    }
                }
            }

            //No   other   instance   was   found,   return   null.
            return null;
        }
        public static void HandleRunningInstance(Process instance)
        {
            //Make   sure   the   window   is   not   minimized   or   maximized
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);

            //Set   the   real   intance   to   foreground   window
            SetForegroundWindow(instance.MainWindowHandle);
        }
    }
}