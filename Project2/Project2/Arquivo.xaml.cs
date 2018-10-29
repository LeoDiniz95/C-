using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;




/************************ Namespaces para trabalhar com arquivos ************************/
using Microsoft.Win32;      // Contem as janelas de dialogo Abrir/Gravar
using System.IO;            // Contem as classes para ler e gravar o arquivo




/************************** NAMESPACE DA BIBLIOTECA APPMANAGER ********************************************/
using AppManager;


namespace Project2
{
    /// <summary>
    /// Interaction logic for Arquivo.xaml
    /// </summary>
    public partial class Arquivo : Window
    {

        // VARIAVEIS PUBLICAS
        private string caminho = "";


        //Janelas de dialogo Abrir/Salvar Arquivo
        private OpenFileDialog DialogoAbrir     = null;
        private SaveFileDialog DialogoSalvar    = null;


        public Arquivo()
        {
            InitializeComponent();

            DialogoAbrir                = new OpenFileDialog();
            DialogoAbrir.Filter         = "txt|*.txt";
            DialogoAbrir.AddExtension   = true;
            // Define o método que será executado quando
            // Pressionado OK na janela Abrir Arquivo
            DialogoAbrir.FileOk         += AbrirArquivoOk;

            
            //JANELA SALVAR
            DialogoSalvar               = new SaveFileDialog();
            DialogoSalvar.Filter        = "txt|*.txt";
            DialogoSalvar.AddExtension  = true;
            DialogoSalvar.FileOk        += GravarArquivoOk;
        }

        //ABRIR O ARQUIVO
        private void Abrir_Click(object sender, RoutedEventArgs e)
        {
            DialogoAbrir.ShowDialog();
        }
        
        //Le o arquivo texto e coloca no textbox "Conteudo"
        private void AbrirArquivoOk(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextReader reader       = null;

            try
            {
                //throw new System.ArgumentException("Error open archive", "Open File Error");

                caminho                 = DialogoAbrir.FileName;
                FileInfo info           = new FileInfo(caminho);
                NomeArquivo.Text        = info.Name;
                Conteudo.Text           = "";
                // abre o arquivo para operação de leitura
                reader                  = info.OpenText();
                // lê a primeira linha do arquivo
                string line             = reader.ReadLine();
                // repete enquanto houver linha para ler
                while(line != null)
                {
                    // coloca a linha no conteudo
                    Conteudo.Text       += line + "\n";
                    // lê a proxima linha
                    line                = reader.ReadLine();
                }
                Gravar.IsEnabled        = false;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                
                //GRAVAR OS DADOS DA EXCEÇÃO NO ARQUIVO EXCEPTION.TXT
                AppExceptions appex = new AppExceptions();
                appex.SaveException(ex);

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            
        }

        private void Gravar_Click(object sender, RoutedEventArgs e)
        {
            Salvar();
        }

        private void GravarComo_Click(object sender, RoutedEventArgs e)
        {
            DialogoSalvar.ShowDialog();
        }

        private void GravarArquivoOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
                caminho = DialogoSalvar.FileName;
                Salvar();
        }

        // SALVA OS DADOS NO ARQUIVO
        private void Salvar()
        {

            try
            {

                //throw new System.ArgumentException("Error on save archive", "Save error");

                if (caminho.Trim() == "")
                {
                    DialogoSalvar.ShowDialog();
                }
                else
                {
                    // GRAVA O CONTEÚDO EDITADO NO CAMINHO DEFINIDO
                    File.WriteAllText(caminho, Conteudo.Text, Encoding.UTF8);
                    Gravar.IsEnabled        = false;
                    GravarComo.IsEnabled    = false;
                }
            }
            catch (Exception ex)
            {
                AppExceptions appex = new AppExceptions();
                appex.PathSaveExceptions = "C:\\Users\\0040481422033\\Downloads\\CSharp";

                appex.SaveException(ex);
            }
            
            
        }

        private void Fechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Conteudo_TextChanged(object sender, TextChangedEventArgs e)
        {
            Gravar.IsEnabled = true;
            GravarComo.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Gravar.IsEnabled == true)
            {
                MessageBoxResult resposta = MessageBox.Show("Deseja salvar o arquivo?", "Salvar", MessageBoxButton.YesNoCancel);

                if (resposta == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                } else if( resposta == MessageBoxResult.Yes)
                {

                    if (DialogoSalvar.ShowDialog() == true)
                    {
                        //SALVA E SAI NORMALMENTE
                        Salvar();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
