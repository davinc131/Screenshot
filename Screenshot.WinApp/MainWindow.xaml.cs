using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using Screenshot.Common;
using Screenshot.Model;

namespace Screenshot.WinApp
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RegisterKeyManger.RegisterProgram();
            Run();
        }

        private void Run()
        {
            try
            {
                if (SerializeConfiguration.ExistArchice())
                {
                    StartProgram startProgram = new StartProgram();
                    ThreadManager.ThreadManagerApp(20000);
                    ScreenshotImage.ScreenShot(this);
                    LoadConfiguration();
                    SendEmail.Send();
                }
                else
                {
                    MessageBox.Show(string.Format("Insira dados de configuração para poder usar o aplicativo." + "\n O arquivo no caminho ({0}) não foi localizado.", SerializeConfiguration.pathArchive));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadConfiguration()
        {
            Configuration configuration = SerializeConfiguration.ReaderXml();

            txtEmail.Text = configuration.Email;
            txtPassword.Password = configuration.Password;
            txtPath.Text = configuration.PathProgram;
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Password) && !string.IsNullOrEmpty(txtPath.Text))
                {
                    Configuration configuration = new Configuration() { Email = txtEmail.Text, Password = txtPassword.Password, PathProgram = txtPath.Text };
                    if (SerializeConfiguration.CreateXml(configuration))
                        MessageBox.Show("Configurãções salvas com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
