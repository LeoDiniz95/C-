using System;
using System.Diagnostics;
using System.IO;

namespace AppManager
{

    /// <summary>
    /// Classe para tratamento das exceções
    /// </summary>
    public class AppExceptions
    {

        #region PROPERTIES

        private string m_PathSaveExceptions = Directory.GetCurrentDirectory();

        /// <summary>
        /// Obtem ou define o caminho onde será gravado o arquivo Exceptions.txt;
        /// </summary>
        public string PathSaveExceptions
        {
            get
            {
                return m_PathSaveExceptions;
            }
            set
            {
                m_PathSaveExceptions = value;
            }
        }



        #endregion



        /// <summary>
        /// Método para salvar os dados da exceção no arquivo Exceptions.txt
        /// </summary>
        /// <param name="ex">Classe Exception com os dados da última exceção ocorrida</param>
        public void SaveException(Exception ex)
        {
            string message = DateTime.Now.ToString() + "\r\n";
            message += ex.Message + "\r\n";
            message += ex.Source + "\r\n";
            message += ex.GetType().FullName + "\r\n";

            // obtem o numero da linha onde ocorreu a exceção
            // using System.Diagnostics;
            StackFrame stackframe = new StackFrame(1, true);

            message += stackframe.GetMethod().ToString() + "\r\n";
            message += stackframe.GetFileLineNumber().ToString() + "\r\n";
            message += "----------------------------------------------------\r\n";

            // GRAVA NO ARQUVIO EXCEPTIONS.TXT

            string caminho = m_PathSaveExceptions + "\\Exceptions.txt";

            File.AppendAllText(caminho, message);
        }


    }

}
