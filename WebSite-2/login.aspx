<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel ID="Panel1" DefaultButton="btn_entrar" runat="server">
    
        <div style="width:240px;">

            <asp:Label ID="lbl_mensagem" Visible="false" CssClass="label-error" runat="server" />
            <label>Nome</label>
            <asp:TextBox ID="txt_nome" CssClass="form-control" runat="server"></asp:TextBox>


            <label>Senha</label>
            <asp:TextBox ID="txt_senha" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btn_Entrar" CssClass="form-control" OnClick="btn_Entrar_Click" runat="server" Text="Entrar" />

        </div>

    </asp:Panel>
</asp:Content>

