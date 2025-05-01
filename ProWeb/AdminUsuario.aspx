<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminUsuario.aspx.cs" Inherits="ProWeb.AdminUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminUsuario.css"/>
    <div class="Contenedor1">
        <asp:Label runat="server" CssClass="Titulo">Editar perfil</asp:Label>

        <div class="Contenedor2">
            <asp:Label runat="server" CssClass="Etiqueta">Nombre:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>

            <asp:Label runat="server" CssClass="Etiqueta">Num. tarjeta:</asp:Label>
                <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>

            <asp:Label runat="server" CssClass="Etiqueta">CVV:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>

            <asp:Label runat="server" CssClass="Etiqueta">Fecha cad.:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>

            <asp:Label runat="server" CssClass="Etiqueta">Dirección:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>

            <asp:Label runat="server" CssClass="Etiqueta">Teléfono:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto"></asp:TextBox>
        </div>

        <asp:Button runat="server" CssClass="Boton" Text="Editar el perfil"/>
    </div>
</asp:Content>
