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
        <asp:TextBox ID="CantidadIngresar" runat="server" CssClass="InputCartera"></asp:TextBox>
        <asp:Button ID="BotonIngresar" runat="server" Text="Ingresar" CssClass="BotonCartera" OnClick="AñadirSaldo" />

        <h3>Retirar dinero</h3>
        <asp:TextBox ID="CantidadRetirar" runat="server" CssClass="InputCartera"></asp:TextBox>
        <asp:Button ID="BotonRetirar" runat="server" Text="Retirar" CssClass="BotonCartera" OnClick="RetirarSaldo" />

        <br /><br />
        <asp:Label ID="MensajeOperacion" runat="server" ForeColor="Red" />
    </div>
</asp:Content>
