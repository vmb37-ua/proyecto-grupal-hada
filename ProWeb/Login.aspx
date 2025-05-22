<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://www.google.com/recaptcha/api.js" async defer></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Login.css" />
    <div id="Contenedor1">
        <p id="titulo">INICIAR SESIÓN</p>
        <asp:Label runat="server" CssClass="MensajeError" ID="ErrMsg"></asp:Label>
        <asp:TextBox ID="Emaillogin" runat="server" CssClass="Inputlogin"></asp:TextBox>
        <asp:RegularExpressionValidator
            ID="revCorreo"
            runat="server"
            ControlToValidate="Emaillogin"
            ErrorMessage="Correo no válido"
            ForeColor="Red"
            Display="Dynamic"
            ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,}$"/>
        <asp:RequiredFieldValidator 
            ID="rfvCorreo" 
            runat="server" 
            ControlToValidate="Emaillogin" 
            ErrorMessage="El correo es obligatorio." 
            ForeColor="Red" 
            Display="Dynamic"/>
        
        <asp:TextBox ID="Passlogin" runat="server" CssClass="Inputlogin" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="rfvPass" 
            runat="server" 
            ControlToValidate="Passlogin" 
            ErrorMessage="La contraseña se debe rellenar" 
            ForeColor="Red" 
            Display="Dynamic"/>
         <div class="g-recaptcha" data-sitekey="6Lcq4S8rAAAAAKpLlujRZkn6yRUa04G6Ge4iffZH"></div>

        <br />
        <br />
        <asp:Button runat="server" Text="Entrar" CssClass="botonlogin" OnClick="EventoMainPage"/>
        <asp:Button runat="server" Text="Registrarse" CssClass="botonlogin" OnClick="EventoRegistrar" CausesValidation="false"/>
    </div>
</asp:Content>