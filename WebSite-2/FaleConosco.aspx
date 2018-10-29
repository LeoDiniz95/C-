<%@ Page Title="Fale Conosco" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FaleConosco.aspx.cs" Inherits="FaleConosco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="margin:0px;">

        <div>
            <h1>FALE CONOSCO</h1>

        </div>

        <div style="width: 50%; border: 1px solid #c0c0c0; float: left; height: 500px; background-color: #eaeaea ">

            <div style="margin: 20px;">

                <asp:Label ID="lbl_falha" runat="server"></asp:Label><br />

                <asp:Label ID="lbl_nome" runat="server" Text="Nome"></asp:Label><br />
                <asp:TextBox ID="txt_nome" CssClass="form-control" runat="server"></asp:TextBox><br />

                <asp:Label ID="lbl_email" runat="server" Text="E-mail"></asp:Label><br />
                <asp:TextBox ID="txt_email" CssClass="form-control" runat="server"></asp:TextBox><br />

                <asp:Label ID="lbl_telefone" runat="server" Text="Telefone"></asp:Label><br />
                <asp:TextBox ID="txt_telefone" CssClass="form-control" runat="server"></asp:TextBox><br />
        
                <asp:Label ID="lbl_mensagem" runat="server" Text="Mensagem"></asp:Label><br />
                <asp:TextBox ID="txt_mensagem" CssClass="form-control" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox><br />
                <br />
                <br />
                <asp:Button ID="btn_enviar" OnClick="btn_enviar_Click" runat="server" Text="Enviar" />

            </div>

         </div>

        <div style="width:49%; float: right; height:500px;border: 1px solid #c0c0c0;">

            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3679.6893715244146!2d-47.3525225849547!3d-22.73978423765029!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x94c89bea5cdb94f9%3A0xffb368bd91ea12ae!2sFatec+Americana!5e0!3m2!1spt-BR!2sbr!4v1510683860625" width="100%" height="100%" frameborder="0" style="border:0" allowfullscreen></iframe>

        </div>

    </div>

</asp:Content>

