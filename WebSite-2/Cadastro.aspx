<%@ Page Title="Cadastro" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cadastro.aspx.cs" Inherits="Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="container">
      <div class="row">
         <div class="col-1 header">
            <h1>TRABALHE CONOSCO</h1>
            <h4>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua</h4>
         </div>
      </div>

      <!-- Form de entrada de dados -->
      <div id="Entrada" runat="server" visible="true">

         <div class="row">
            <div class="col-2">
               <div class="box">
                  <asp:Label ID="MsgErro" runat="server" Visible="false" CssClass="label-error" />

                   
                   <div style="padding:20px">

                       <asp:Image       ID="Foto" style=" max-width:300px; max-height:300px;" runat="server" />
                       <asp:FileUpload  ID="FotoUpload"   runat="server" />
                       <asp:HiddenField ID="CaminhoFoto"  runat="server" />

                   </div>


                  <label>Nome</label>
                  <asp:HiddenField ID="CandidatoId" runat="server" />
                  <asp:TextBox ID="Nome" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Email</label>
                  <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Telefone</label>
                  <asp:TextBox ID="Telefone" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Data Nascimento</label>
                  <asp:TextBox ID="Nascimento" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Sexo</label>
                  <asp:RadioButtonList ID="Sexo" CssClass="form-control" runat="server">
                     <asp:ListItem Text="Masculino" Selected="true" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Feminino" Value="0"></asp:ListItem>
                  </asp:RadioButtonList>

                  <label>CEP</label>
                  <asp:TextBox ID="Cep" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Grau de Instrução</label>
                  <asp:DropDownList ID="GrauInstrucao" CssClass="form-control" runat="server">
                     <asp:ListItem Text="Técnico" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Superior" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Especializaçao" Value="3"></asp:ListItem>
                     <asp:ListItem Text="Mestrado" Value="4"></asp:ListItem>
                     <asp:ListItem Text="Doutorado" Value="5"></asp:ListItem>
                  </asp:DropDownList>

                  <label>Curso</label>
                  <asp:DropDownList ID="CursoFatec" CssClass="form-control" runat="server">
                     <asp:ListItem Text="Analise e Desenvolvimento de Sistemas" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Segurança da Informação" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Jogos Digitais" Value="3"></asp:ListItem>
                  </asp:DropDownList>

                  <label>Ano de Conclusão</label>
                  <asp:TextBox ID="AnoConclusao" CssClass="form-control" runat="server"></asp:TextBox>

                  <label>Resumo do Curriculo/Experiências</label>
                  <asp:TextBox ID="Resumo" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="6"></asp:TextBox>

                  <label>Nome de Login</label>
                  <asp:TextBox ID="NomeLogin" CssClass="form-control" runat="server"></asp:TextBox>
                  <label>Senha</label>
                  <asp:TextBox ID="Senha" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>

               </div>
            </div>

            <!-- Seleção das competencias-->
            <div class="col-2">
               <asp:Panel ID="PanelCompetencias" runat="server" ScrollBars="Auto" Height="1000px" CssClass="box ">
                  <h3>COMPETÊNCIAS</h3>
                  <h5>Marque as competências </h5>
                  <asp:CheckBoxList ID="Competencias" Font-Size="18px" RepeatDirection="Vertical" runat="server"></asp:CheckBoxList>
               </asp:Panel>
            </div>
         </div>

         <div class="row">
            <div class="col-2">
               <div class="box">
                  <asp:Button ID="Gravar" OnClick="Gravar_Click" Font-Size="16px" Style="padding: 10px;" Text="Enviar" runat="server" />
               </div>
            </div>
         </div>

      </div>

      <div class="row">
         <div class="box box-border" id="MensagemFinal" runat="server" visible="false">
            <h1>Seus dados foram enviado com sucesso</h1>
            <h4>Obrigado pela participação.</h4>
         </div>
      </div>

      <br />
      <br />
      <br />
      <br />

   </div>
</asp:Content>

