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
                var path = Path.Combine(c, "Screenshot.png");

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
    }
}
