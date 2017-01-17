using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    class RegistryHandle
    {
        private const string RUN_NAME = "ScreenShot";
        private const string REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static bool SetRegistryKey(bool autoStart)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY, true);
                if (autoStart)
                    key.SetValue(RUN_NAME, Application.ExecutablePath + " /minimize");
                else
                    key.DeleteValue(RUN_NAME, false);
                key.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public enum AutoStartStat
        {
            NO_PERMISSION,
            YES,
            NO
        }
        public static AutoStartStat GetAutoStart()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY, RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
                if (key == null)
                    return AutoStartStat.NO_PERMISSION;
                string str = key.GetValue(RUN_NAME) as string;
                if (str != null && str.ToLower() == (Application.ExecutablePath + " /minimize").ToLower())
                {
                    return AutoStartStat.YES;
                }
                return AutoStartStat.NO;
            }
            catch(Exception)
            {
                return AutoStartStat.NO_PERMISSION;
            }

        }
        public static bool SetAutoStart(bool autoStart)
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                return SetRegistryKey(autoStart);
            }
            else
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Arguments = (autoStart ? "/SetAutoSetUp" : "/ClearAutoSetUp");

                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
    }
}
