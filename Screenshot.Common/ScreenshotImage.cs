using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
                using (Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using(Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot))
                    {
                        //Bitmap bmpScreenshot;
                        //Graphics gfxScreenshot;

                        // Esconde o form para ele não aparecer no screenshot
                        // Hide the form so that it does not appear in the screenshot
                        win.Hide();

                        // Espera um tempo para garantir que o form escondeu
                        // Wait a second
                        ThreadManager.ThreadManagerApp(5000);

                        // Seta o obejto bitmap com o tamanho da tela
                        // Set the bitmap object to the size of the screen
                        //bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        // Cria um objeto graphics a partir do bitmap
                        // Create a graphics object from the bitmap
                        //gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                        // Tira o screenshot do canto superior esquerdo até o canto inferior direito
                        // Take the screenshot from the upper left corner to the right bottom corner
                        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                        // Salva o screenshot no local indicado e no formato esolhido
                        // Save the screenshot to the specified path
                        bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);

                        bmpScreenshot.Dispose();
                        gfxScreenshot.Dispose();

                        ThreadManager.ThreadManagerApp(5000);

                        // Exibe o form
                        win.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Screenshot Image: " + ex.Message);
            }    
        }

        //public static void Screenshot2(Window window)
        //{
        //    try
        //    {
        //        window.Hide();

        //        ThreadManager.ThreadManagerApp(5000);

        //        var proc = Process.GetProcessesByName("TeamViewer")[0];
        //        var rect = new User32.Rect();
        //        User32.GetWindowRect(proc.MainWindowHandle, ref rect);

        //        int width = rect.right - rect.left;
        //        int height = rect.bottom - rect.top;

        //        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        //        using (Graphics graphics = Graphics.FromImage(bmp))
        //        {
        //            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
        //        }

        //        bmp.Save("Screenshot.png", ImageFormat.Png);

        //        ThreadManager.ThreadManagerApp(5000);

        //        window.Show();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Screeshot: " + ex.Message + "\n Detalhes da Exeção: " + ex.InnerException);
        //    }
        //}
    }

    //public class User32
    //{
    //    [StructLayout(LayoutKind.Sequential)]
    //    public struct Rect
    //    {
    //        public int left;
    //        public int top;
    //        public int right;
    //        public int bottom;
    //    }

    //    [DllImport("user32.dll")]
    //    public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
    //}
}
