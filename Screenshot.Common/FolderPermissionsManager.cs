using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot.Common
{
    public class FolderPermissionsManager
    {
        public static void PermissionsManager()
        {
            DirectoryEntry machine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            //var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
            //var path = @"D:\Imagens";

            if (machine.Children != null)
            {
                var results = machine.Children.Cast<DirectoryEntry>().Where(r => r.SchemaClassName == "Group").OrderBy(r => r.Name);

                foreach (DirectoryEntry child in results) 
                {
                    //Debug.WriteLine(child.Name);
                }
            }
        }
    }
}
