using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//bibliotecas para enviar email
using System.Net;
using System.Net.Mail;

public partial class FaleConosco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        /*
        // INSTANCIA A CLASSE QUE ENVIA EMAIL
        SmtpClient smtp     = new SmtpClient();
        // INSTANCIA A CLASSE PARA CRIAR O EMAIL
        MailMessage email   = new MailMessage();

        // MONTA O EMAIL
        email.Subject       = "Fale conosco";
        email.Body          = txt_nome.Text + "\n" + txt_email.Text + "\n" + txt_telefone.Text + "\n" + txt_mensagem.Text;

        email.IsBodyHtml    = false;// Caso de deseje um email com edição especial( com imagens, por exemplo) precisa colocar como true

        email.To.Add("contato@seudominio.com.br");
        MailAddress eFrom       = new MailAddress("contato@seudominio.com.br");
        email.From              = eFrom;

        // AUTENTICAÇÃO NO SERVER SMTP
        smtp.Host           ="smtp.seudominio.com.br";
        smtp.Port           = 587;
        smtp.Credentials    = new NetworkCredential("contato@seudominio.com.br","sua senha");

        //ENVIA O EMAIL
        smtp.Send(email);
        */

    }
}