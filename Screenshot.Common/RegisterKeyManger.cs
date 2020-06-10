using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Screenshot.Common
{
    public class RegisterKeyManger
    {
        public static void RegisterProgram()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.SetValue("Screenshot Teamviewer", System.Windows.Forms.Application.ExecutablePath.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Register Key: " + ex.Message);
            }
        }
    }
}
