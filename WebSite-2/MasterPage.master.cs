using AppDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] != null)
        {

            if (Session["Status"].ToString() == "1")
            {
                EditarCadastro.Visible = true;
                btn_Entrar.Visible = false;
                btn_Sair.Visible = true;
                EditarCompetencia.Visible = true;
             }
        }
        else
        {
            EditarCadastro.Visible = false;
            btn_Entrar.Visible = true;
            btn_Sair.Visible = false;
        }
    }


    protected void btn_Entrar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login.aspx");
    }


    protected void btn_Sair_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/logout.aspx");
    }

    
}
