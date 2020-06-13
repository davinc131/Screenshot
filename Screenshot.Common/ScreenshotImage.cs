using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Screenshot.Common
{
    public class ScreenshotImage
    {
        private static string nameArchive = "Screenshot.png";
        public static void ScreenShot(Window win)
        {
            try
            {
                
                Bitmap bmpScreenshot;

                // Esconde o form para ele não aparecer no screenshot
                // Hide the form so that it does not appear in the screenshot
                win.Hide();

                // Espera um tempo para garantir que o form escondeu
                // Wait a second
                ThreadManager.ThreadManagerApp(5000);

                // Seta o obejto bitmap com o tamanho da tela
                // Set the bitmap object to the size of the screen
                bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                using(Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot))
                {
                    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }

                var c = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
                var path = Path.Combine(c, nameArchive);

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        bmpScreenshot.Save(memory, ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                win.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Screenshot Image: {0} \n Detalhes da Exceção: {1}", ex.Message, ex.InnerException));
            }
        }

        public static void ScreeshotEspecificProgram(Window win)
        {
            try
            {
                win.Hide();

                ThreadManager.ThreadManagerApp(5000);

                var proc = Process.GetProcessesByName("TeamViewer")[0];

                var modulos = proc.Modules;

                while(proc == null)
                {
                    ThreadManager.ThreadManagerApp(5000);
                    proc = Process.GetProcessesByName("TeamViewer")[0];
                }

                var rect = new User32.Rect();
                User32.GetWindowRect(proc.MainWindowHandle, ref rect);

                int width = rect.right - rect.left;
                int height = rect.bottom - rect.top;

                var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
                }

                var c = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
                var path = Path.Combine(c, nameArchive);

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        bmp.Save(memory, ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                win.Show();
            }
            catch (Exception ex)
            {
                throw new Exception("Screeshot: " + ex.Message + "\n Detalhes da Exeção: " + ex.InnerException);
            }
        }
    }
}
