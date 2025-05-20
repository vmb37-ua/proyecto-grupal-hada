<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cartera.aspx.cs" Inherits="ProWeb.Cartera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/Cartera.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ContenedorCartera">
        <h2>Mi Cartera</h2>

        <p>Saldo actual: 
            <asp:Label ID="DineroDisponible" runat="server" Font-Bold="True" />
        </p>

        <h3>Ingresar dinero</h3>
        <asp:TextBox ID="CantidadIngresar" runat="server" CssClass="InputCartera" />
        <asp:Button ID="BotonIngresar" runat="server" Text="Añadir saldo" CssClass="BotonCartera" OnClick="Ingresar_Click" />

        <h3>Retirar dinero</h3>
        <asp:TextBox ID="CantidadRetirar" runat="server" CssClass="InputCartera" />
        <asp:Button ID="BotonRetirar" runat="server" Text="Retirar saldo" CssClass="BotonCartera" OnClick="Retirar_Click" />

        <br /><br />
        <asp:Label ID="MensajeOperacion" runat="server" />

        <br /><br />
        <asp:Button ID="BotonVolverPerfil" runat="server" Text="Volver al perfil" CssClass="BotonCartera" OnClick="Volver_Click" />
    </div>
</asp:Content>