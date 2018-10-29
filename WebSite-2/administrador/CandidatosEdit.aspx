<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CandidatosEdit.aspx.cs" Inherits="administrador_CandidatosEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <!-- BUSCAR -->
   <div style="padding: 20px;">
      <asp:TextBox ID="BuscarNome" Width="180px" CssClass="form-control" runat="server"></asp:TextBox>
      <asp:Button ID="Buscar" Width="80px" OnClick="Buscar_Click" runat="server" Text="Buscar" />
      <asp:Button ID="LimparBusca" Visible="false" Width="50px" OnClick="LimparBusca_Click" runat="server" Text="X" />
   </div>
   <!-- GRID -->
   <div style="padding: 20px;">
      <asp:Label ID="Label2" runat="server" ForeColor="#ff6a00" Font-Size="30px" Text="Lista de Candidatos" />
      <br />
      <asp:Panel ID="Panel1" Height="300px" ScrollBars="Vertical" runat="server">
         <asp:GridView ID="Candidatos" Width="100%" AutoGenerateColumns="true" AutoGenerateSelectButton="true" OnSelectedIndexChanged="Candidatos_SelectedIndexChanged" CellPadding="8" HeaderStyle-BackColor="#dfdfdf" BorderColor="#cccccc" runat="server">
         </asp:GridView>
      </asp:Panel>
   </div>
   <!-- CONTROLES DE EDIÇÃO -->
   <div style="padding: 20px;">
      <!-- FORMULÁRIO DE CADASTRO -->
      <div style="float: left; width: 40%;">
         <asp:Label ID="MsgErro" runat="server" />
         <br />

         <asp:Label ID="CandidatoId" CssClass="form-control" runat="server"></asp:Label>

          <asp:Image       ID="Foto" style=" max-width:300px; max-height:300px;" runat="server" Visible="false"/>
          <asp:FileUpload  ID="FotoUpload"   runat="server" />
          <asp:HiddenField ID="CaminhoFoto"  runat="server" />

         <label>Nome</label>
         <asp:TextBox ID="Nome" CssClass="form-control" runat="server"></asp:TextBox>
         <label>Email</label>
         <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
         <label>Telefone</label>
         <asp:TextBox ID="Telefone" CssClass="form-control" runat="server"></asp:TextBox>
         <label>Resumo</label>
         <asp:TextBox ID="Resumo" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="6"></asp:TextBox>
         <br />
         <br />
         <asp:Button ID="Gravar" Enabled="false" OnClick="Gravar_Click" runat="server" Text="Gravar" />
         <asp:Button ID="Excluir" Enabled="false" OnClick="Excluir_Click" runat="server" Text="Excluir" />
      </div>
      <!-- FORMULÁRIO DAS COMPETENCIAS -->
      <div style="float: left; margin-left: 30px; width: 35%;">
         <h3>SUAS COMPETÊNCIAS</h3>
         <hr />
         <asp:CheckBoxList ID="Competencias" Font-Size="14px" RepeatDirection="Vertical" runat="server"></asp:CheckBoxList>
      </div>
   </div>
    <div style="clear:both;"></div>
</asp:Content>

