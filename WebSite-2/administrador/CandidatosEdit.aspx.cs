using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using AppDatabase;

public partial class administrador_CandidatosEdit : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         LoadCompetencias();
         LoadCandidatos();
      }
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

      Competencias.Items.Clear();
      for (int i = 0; i <= tb.Rows.Count - 1; i++)
      {
         Competencias.Items.Add(new ListItem(tb.Rows[i]["Nome"].ToString(), tb.Rows[i]["CompetenciaId"].ToString()));
      }
   }

   // CARREGA OS CANDIODATOS NO GRID
   protected void LoadCandidatos()
   {
      ole.ConnectionString = conexao;
      DataTable tb = (DataTable)ole.Query("SELECT * FROM Candidatos WHERE Nome LIKE '%" + BuscarNome.Text + "%' ORDER BY Nome;");

        if (tb.Rows[0]["Foto"].ToString() != "")
        {
            Foto.ImageUrl = ResolveUrl(tb.Rows[0]["Foto"].ToString());
            CaminhoFoto.Value = FotoUpload.ResolveUrl(tb.Rows[0]["Foto"].ToString());
            //FotoUpload = ;
            Foto.Visible = true;
        }
        else
        {
            Foto.Visible = false;
        }

        Candidatos.DataSource = tb;
      Candidatos.DataBind();
   }


   // GRAVA A EDIÇÃO
   protected void Gravar_Click(object sender, EventArgs e)
   {
      if (Nome.Text.Trim() == "")
      {
         MsgErro.Text = "O Nome deve ser digitado";
      }
      else if (Email.Text.Trim() == "")
      {
         MsgErro.Text = "O email deve ser digitado";
      }
      else
      {

            //GRAVA A FOTO DO CANDIDATO

            if (FotoUpload.HasFile)
            {
                //Define o caminho de gravação
                CaminhoFoto.Value = "~/Candidatos_Fotos/" + FotoUpload.FileName;
                string caminhoFisico = Server.MapPath(CaminhoFoto.Value);
                //SALVA O ARQUIVO
                FotoUpload.SaveAs(caminhoFisico);
            }

            string sql = "UPDATE Candidatos SET Nome='" + Nome.Text + "',Email='" + Email.Text + "',Telefone='" + Telefone.Text + "',Resumo='" + Resumo.Text + "', Foto='" + CaminhoFoto.Value + "' WHERE CandidatoId=" + CandidatoId.Text + ";";

         ole.ConnectionString = conexao;
         if ((int)ole.Query(sql) == 1)
         {
            // grava as competencias do candidato
            GravaCompetencias(CandidatoId.Text);

            limpar();
         }
         else
         {
            MsgErro.Text = "Ocorreu uma falha no cadastro, tente novamente.";
         }
      }
   }

   private void limpar()
   {
      CandidatoId.Text = "";
      Nome.Text = "";
      Email.Text = "";
      Telefone.Text = "";
      Resumo.Text = "";
      CaminhoFoto.Value = "";
      // Desativa os botões
      Gravar.Enabled = false;
      Excluir.Enabled = false;
        

      LoadCompetencias();
      LoadCandidatos();
   }

   private void GravaCompetencias(string candidatoId)
   {
      // GRAVA AS COMPETENCIAS DO CANDIDATO 

      ole.ConnectionString = conexao;

      ole.Query("DELETE FROM CandidatosCompetencias WHERE CandidatoId=" + candidatoId + ";");

      for (int i = 0; i <= Competencias.Items.Count - 1; i++)
      {
         if (Competencias.Items[i].Selected)
         {
            string comando = "INSERT INTO CandidatosCompetencias(CompetenciaId,CandidatoId) VALUES(" + Competencias.Items[i].Value.ToString() + "," + candidatoId + ");";

            ole.Query(comando);
         }
      }
      LoadCandidatos();
   }

   protected void Buscar_Click(object sender, EventArgs e)
   {
      LimparBusca.Visible = true;
      LoadCandidatos();
   }

   protected void LimparBusca_Click(object sender, EventArgs e)
   {
      BuscarNome.Text = "";
      LimparBusca.Visible = false;
      LoadCandidatos();
   }

   protected void Candidatos_SelectedIndexChanged(object sender, EventArgs e)
   {
      ole.ConnectionString = conexao;
      DataTable tb = (DataTable)ole.Query("SELECT * FROM Candidatos WHERE CandidatoId=" + Candidatos.SelectedRow.Cells[1].Text.ToString());
      if (tb.Rows.Count == 1)
      {
         CandidatoId.Text = tb.Rows[0]["CandidatoId"].ToString();
         Nome.Text = tb.Rows[0]["Nome"].ToString();
         Email.Text = tb.Rows[0]["Email"].ToString();
         Telefone.Text = tb.Rows[0]["Telefone"].ToString();
         Resumo.Text = tb.Rows[0]["Resumo"].ToString();

            Foto.ImageUrl = ResolveUrl(tb.Rows[0]["Foto"].ToString());
            CaminhoFoto.Value = FotoUpload.ResolveUrl(tb.Rows[0]["Foto"].ToString());
            Foto.Visible = true;
        

         // SELECIONA AS COMPETENCIAS DESTE CANDIDATO
         DefineCompetencias(CandidatoId.Text);

         // Ativa os botões
         Gravar.Enabled = true;
         Excluir.Enabled = true;
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


   protected void Excluir_Click(object sender, EventArgs e)
   {
      ole.ConnectionString = conexao;
      // DELETA AS COMPETENCIAS DO CANDIDATO
      ole.Query("DELETE FROM CandidatosCompetencias WHERE CandidatoId=" + CandidatoId.Text + ";");
      // DELETA O CANDIDATO
      ole.Query("DELETE FROM Candidatos WHERE CandidatoId=" + CandidatoId.Text + ";");
      limpar();
   }
}