using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//    BIBLIOTECAS PARA BANCO DE DADOS
using System.Data;
using AppDatabase;
using System.Security.Cryptography;
using System.Text;

public partial class Cadastro : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         if (Request.QueryString["c"] != null)
         {
            LoadCandidato(Request.QueryString["c"].ToString());
         }


         LoadCompetencias();
      }
   }

    //CLASSE QUE VAI GERAR A CRIPTOGRAFIA DA SENHA
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

    // referencia de strings de conexão:https://www.connectionstrings.com

    static string banco = HttpContext.Current.Server.MapPath("~/app_data/BancoCompetencias.accdb");

   string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + banco + ";Persist Security Info=False;";

   // CRIA UMA instancia da classe de transação com DB
   AppDatabase.OleDBTransaction ole = new OleDBTransaction();

   // CARREGA AS COMPETENCIAS NO CHECKBOXLIST
   protected void LoadCompetencias()
   {
      ole.ConnectionString = conexao;
      DataTable tb = (DataTable)ole.Query("SELECT * FROM Competencias ORDER BY Nome;");

      for (int i = 0; i <= tb.Rows.Count - 1; i++)
      {
         Competencias.Items.Add(new ListItem(tb.Rows[i]["Nome"].ToString(), tb.Rows[i]["CompetenciaId"].ToString()));
      }
   }

   protected void Gravar_Click(object sender, EventArgs e)
   {
      if (Nome.Text.Trim() == "")
      {
         MsgErro.Text = "O Nome deve ser digitado";
         MsgErro.Visible = true;
      }
      else if (Email.Text.Trim() == "")
      {
         MsgErro.Text = "O email deve ser digitado";
         MsgErro.Visible = true;
      }
      else
      {

            //GRAVA A FOTO DO CANDIDATO

            if (FotoUpload.HasFile) {
                //Define o caminho de gravação
                CaminhoFoto.Value = "~/Candidatos_Fotos/" + FotoUpload.FileName;
                string caminhoFisico = Server.MapPath(CaminhoFoto.Value);
                //SALVA O ARQUIVO
                FotoUpload.SaveAs(caminhoFisico);
            }

         string sql = "";

         if (CandidatoId.Value != "")
         {

            sql = "UPDATE Candidatos SET Nome='" + Nome.Text + "',Email='" + Email.Text + "',Telefone='" + Telefone.Text + "',Resumo='" + Resumo.Text + "',Nascimento='" + Nascimento.Text + "',Sexo=" + Sexo.SelectedValue + ",Cep='" + Cep.Text + "',GrauInstrucao=" + GrauInstrucao.SelectedValue + ",CursoFatec=" + CursoFatec.SelectedValue + ",AnoConclusao='" + AnoConclusao.Text + "', Foto='"+CaminhoFoto.Value+"'  WHERE CandidatoId=" + CandidatoId.Value + ";";

            ole.ConnectionString = conexao;
            if ((int)ole.Query(sql) == 1)
            {

            }
         }
         else
         {

            sql = "INSERT INTO Candidatos(Nome,Email,Telefone,Resumo,Nascimento,Sexo,Cep,GrauInstrucao,CursoFatec,AnoConclusao,Foto) VALUES('" + Nome.Text + "','" + Email.Text + "','" + Telefone.Text + "','" + Resumo.Text + "','" + Nascimento.Text + "'," + Sexo.SelectedValue + ",'" + Cep.Text + "'," + GrauInstrucao.SelectedValue + "," + CursoFatec.SelectedValue + ",'" + AnoConclusao.Text + "','" + CaminhoFoto.Value + "');";

            ole.ConnectionString = conexao;
            if ((int)ole.Query(sql) == 1)
            {
               //OBTEM O ID DO CANDIDATO QUE FOI INSERIDO
               DataTable t = (DataTable)ole.Query("SELECT MAX(CandidatoId) AS ID FROM Candidatos");
               string candidatoId = t.Rows[0]["ID"].ToString();

               // grava as competencias do candidato
               GravaCompetencias(candidatoId);

               // INSERE O CANDIDATO NA TABELA DE USUÁRIOS
               sql = "INSERT INTO Usuarios(CandidatoId,Status,NomeLogin,Senha) VALUES(" + candidatoId + ",0,'" + NomeLogin.Text + "','" + criptografar(Senha.Text) + "');";
               ole.Query(sql);

               Entrada.Visible = false;
               MensagemFinal .Visible = true;
               limpar();
            }
            else
            {
               MsgErro.Text = "Houve uma falha no cadastro, tente novamente.";
               MsgErro.Visible = true;
            }

         }
      }
   }

   private void limpar()
   {
      Nome.Text         = "";
      Email.Text        = "";
      Telefone.Text     = "";
      Resumo.Text       = "";
      Nascimento.Text   = "";
      Cep.Text          = "";
      AnoConclusao.Text = "";
      NomeLogin.Text    = "";
      Senha.Text        = "";
      CaminhoFoto.Value = "";
   }

   private void GravaCompetencias(string candidatoId)
   {
      // GRAVA AS COMPETENCIAS DO CANDIDATO 

      ole.ConnectionString = conexao;

      for (int i = 0; i <= Competencias.Items.Count - 1; i++)
      {
         if (Competencias.Items[i].Selected)
         {
            string comando = "INSERT INTO CandidatosCompetencias(CompetenciaId,CandidatoId) VALUES(" + Competencias.Items[i].Value.ToString() + "," + candidatoId + ");";

            ole.Query(comando);
         }
      }
   }


   protected void LoadCandidato(string candidatoId)
   {
      ole.ConnectionString = conexao;
      DataTable tb = (DataTable)ole.Query("SELECT * FROM Candidatos WHERE CandidatoId=" + candidatoId);
      if (tb.Rows.Count == 1)
      {
         CandidatoId.Value  = candidatoId;
         Nome.Text          = tb.Rows[0]["Nome"].ToString();
         Email.Text         = tb.Rows[0]["Email"].ToString();
         Telefone.Text      = tb.Rows[0]["Telefone"].ToString();
         Resumo.Text        = tb.Rows[0]["Resumo"].ToString();


            if (tb.Rows[0]["Foto"].ToString() != "")
            {
                Foto.ImageUrl   = ResolveUrl(tb.Rows[0]["Foto"].ToString());
                Foto.Visible    = true;
            }
            else
            {
                Foto.Visible = false;
            }


         // SELECIONA AS COMPETENCIAS DESTE CANDIDATO
         DefineCompetencias(candidatoId);

         // Ativa os botões
         Gravar.Enabled = true;
      }
   }

   protected void DefineCompetencias(string candidatoId)
   {
      ole.ConnectionString = conexao;
      DataTable tb = new DataTable();

      foreach (ListItem it in Competencias.Items)
      {
         tb = (DataTable)ole.Query("SELECT * FROM CandidatosCompetencias WHERE CandidatoId=" + candidatoId + " AND CompetenciaId=" + it.Value + ";");
         if (tb.Rows.Count == 1)
         {
            it.Selected = true;
         }
         else
         {
            it.Selected = false;
         }
         tb.Dispose();
      }
   }


}