<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Login.css" />
    <div id="Contenedor1">
        <p id="titulo">INICIAR SESIÓN</p>
        <asp:Label runat="server" CssClass="MensajeError" ID="ErrMsg"></asp:Label>
        <asp:TextBox ID="Emaillogin" runat="server" CssClass="Inputlogin"></asp:TextBox>
        
        <asp:TextBox ID="Passlogin" runat="server" CssClass="Inputlogin" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button runat="server" Text="Entrar" CssClass="botonlogin" OnClick="EventoMainPage"/>
        <asp:Button runat="server" Text="Registrarse" CssClass="botonlogin" OnClick="EventoRegistrar"/>
    </div>
</asp:Content>