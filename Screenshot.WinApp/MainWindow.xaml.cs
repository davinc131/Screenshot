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

            LoadTexts();
            RegisterKeyManger.RegisterProgram();
            Run();
        }

        private void LoadTexts()
        {
            try
            {
                string text1 = @"O Screenshot Teamviewer é um simples utilitário para facilitar o processo de captura de tela do Teamviewer. Com essa ferramenta você pode receber por email o print de tela do Teamviewer para acessar novamente após a reinicialização do sistema operacional. Coloque apenas as suas credenciais (email e senha) e o caminho do .exe do aplicativo. Para o perfeito funcionamento do programa é importante que você possa configurar o seu email do Google para aceitar acesso de aplicativos menos seguros. Faça isso acessando este link: 
                            https://myaccount.google.com/lesssecureapps
                             Acessando este link em um navegador logado em sua conta, a aparência do dessa configuração deve ser semelhante a esta da imagem abaixo.";
                string text2 = @"Depois disso vá na aba de configurações e inclua os dados de acesso a sua conta e o caminho onde está o executável do Teamviewer (sem aspas), como mostrado na imagem abaixo.";
                string text3 = @"Após este procedimento o aplicativo irá iniciar automaticamente quando o computador for reiniciado e irá iniciar o Teamviewer e enviar uma imagem para o email configurado no programa./nCaso ocorra algum erro no momento da execução do programa, vá a pasta de instalação do aplicativo e dê permissões para todos os usuários, como mostrado na imagem abaixo.";

                txtInstructions.Text = text1;
                txtInstructions1.Text = text2;
                txtInstructions2.Text = text3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
