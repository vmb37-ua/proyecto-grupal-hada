<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Login.css" />
    <div id="Contenedor1">
        <p id="titulo">INICIAR SESIÓN</p>
        
        <asp:TextBox ID="Emaillogin" runat="server" CssClass="Inputlogin"></asp:TextBox>
        
        <asp:TextBox ID="Passlogin" runat="server" CssClass="Inputlogin" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button runat="server" Text="Entrar" CssClass="botonlogin"/>
        <asp:Button runat="server" Text="Registrarse" CssClass="botonlogin"/>
    </div>
</asp:Content>
