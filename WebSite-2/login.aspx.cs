using AppDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // referencia de strings de conexão:https://www.connectionstrings.com

    static string banco = HttpContext.Current.Server.MapPath("~/app_data/BancoCompetencias.accdb");

    string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + banco + ";Persist Security Info=False;";

    // CRIA UMA instancia da classe de transação com DB
    AppDatabase.OleDBTransaction ole = new OleDBTransaction();

    protected void btn_Entrar_Click(object sender, EventArgs e)
    {
        ole.ConnectionString = conexao;

        DataTable tb = (DataTable)ole.Query("SELECT * FROM Usuarios WHERE NomeLogin='" + txt_nome.Text + "' AND Senha='" + criptografar(txt_senha.Text) + "';");
        if (tb.Rows.Count == 1)
        {
            // Armazena na dados do usuário nas variáveis de sessão 
            Session["UsuarioId"] = tb.Rows[0]["UsuarioId"].ToString();
            Session["CandidatoId"] = tb.Rows[0]["CandidatoId"].ToString();
            Session["Status"] = tb.Rows[0]["Status"].ToString();

            // Inicializa a classe de autenticação do usuário
            FormsAuthentication.Initialize();
            //Define os dados do ticket de autenticação 
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, txt_nome.Text, DateTime.Now, DateTime.Now.AddMinutes(30), false, Session["Status"].ToString(), FormsAuthentication.FormsCookiePath);
            // Grava o ticket no cookie de autenticação
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)));
            // Redireciona para a página do usuário
            Response.Redirect(FormsAuthentication.GetRedirectUrl(txt_nome.Text, false));
        }

    }

    //CRIPTOGRAFIA
    public static string criptografar(string originalPassword)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;

        // instancia o MD5CryptoServiceProvider, e transforma a password em byte[] para calcular o hash
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
        encodedBytes = md5.ComputeHash(originalBytes);

        // converte o hash em string
        return BitConverter.ToString(encodedBytes);
    }

}