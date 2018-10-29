using AppDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administrador_CompetenciasEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCompetencias();
        }
    }

    // referencia de strings de conexão:https://www.connectionstrings.com

    static string banco = HttpContext.Current.Server.MapPath("~/app_data/BancoCompetencias.accdb");

    string conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + banco + ";Persist Security Info=False;";

    // CRIA UMA instancia da classe de transação com DB
    AppDatabase.OleDBTransaction ole = new OleDBTransaction();


    // CARREGA OS COMPETENCIAS NO GRID
    protected void LoadCompetencias()
    {
        ole.ConnectionString = conexao;
        DataTable tb = (DataTable)ole.Query("SELECT * FROM Competencias WHERE Nome LIKE '%" + BuscarNome.Text + "%' ORDER BY Nome;");
        Grid_Competencias.DataSource = tb;
        Grid_Competencias.DataBind();
    }

    // GRAVA A COMPETENCIA
    protected void Gravar_Click(object sender, EventArgs e)
    {
        if (Nome.Text.Trim() == "")
        {
            MsgErro.Text = "O Nome deve ser digitado";
        }
        else
        {
            if (CompetenciaId.Text.Trim() == "")
            {
                ole.ConnectionString = conexao;
                string comando = "INSERT INTO Competencias(Nome) VALUES('" + Nome.Text + "');";
                ole.Query(comando);
                limpar();
            }
            else
            {
                string sql = "UPDATE Competencias SET Nome='" + Nome.Text + "' WHERE CompetenciaId=" + CompetenciaId.Text + ";";

                ole.ConnectionString = conexao;
                if ((int)ole.Query(sql) == 1)
                {
                    // grava as competencias do candidato
                    //GravaCompetencias();
                }
                else
                {
                    MsgErro.Text = "Ocorreu uma falha no cadastro, tente novamente.";
                }
                limpar();
            }
        }
    }

    private void limpar()
    {
        CompetenciaId.Text = "";
        Nome.Text = "";
        // Desativa os botões
        // Gravar.Enabled = false;
        Excluir.Enabled = false;

        LoadCompetencias();
    }

    private void GravaCompetencias()
    {
        // GRAVA AS COMPETENCIAS DO CANDIDATO 

        ole.ConnectionString = conexao;
        if (CompetenciaId.Text.Trim() != "")
        {
            //ole.Query("DELETE FROM Competencias WHERE CompetenciaId=" + CompetenciaId + ";");
            string comando = "UPDATE Competencias SET Nome = '" + Nome.Text + "' WHERE CompetenciaId= " + CompetenciaId.Text + ";";
            ole.Query(comando);
        }
        
        LoadCompetencias();
    }

    protected void Buscar_Click(object sender, EventArgs e)
    {
        LimparBusca.Visible = true;
        LoadCompetencias();
    }

    protected void LimparBusca_Click(object sender, EventArgs e)
    {
        BuscarNome.Text = "";
        LimparBusca.Visible = false;
        LoadCompetencias();
    }


    protected void Competencias_SelectedIndexChanged(object sender, EventArgs e)
    {
        ole.ConnectionString = conexao;
        DataTable tb = (DataTable)ole.Query("SELECT * FROM Competencias WHERE CompetenciaId=" + Grid_Competencias.SelectedRow.Cells[1].Text.ToString());
        if (tb.Rows.Count == 1)
        {
            CompetenciaId.Text = tb.Rows[0]["CompetenciaId"].ToString();
            Nome.Text = tb.Rows[0]["Nome"].ToString();
            
            // Ativa os botões
            Gravar.Enabled = true;
            Excluir.Enabled = true;
        }
    }

    protected void Excluir_Click(object sender, EventArgs e)
    {
        ole.ConnectionString = conexao;
        // DELETA AS COMPETENCIAS DO CANDIDATO
        ole.Query("DELETE FROM CandidatosCompetencias WHERE CompetenciaId=" + CompetenciaId.Text + ";");
        // DELETA O CANDIDATO
        ole.Query("DELETE FROM Competencias WHERE CompetenciaId=" + CompetenciaId.Text + ";");
        limpar();
    }

}