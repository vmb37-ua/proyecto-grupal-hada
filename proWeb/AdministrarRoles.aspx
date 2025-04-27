<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarRoles.aspx.cs" Inherits="ProWeb.AdministrarRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/EstiloAdministrar.css" />
    <div id="Holder">
        <h1 id="Titulo">Editar Roles</h1>
        <div style="display: inline-block; text-align: left;">
            <span id="Etiqueta">Nombre</span>
            <asp:TextBox ID="TBNombreRol" runat="server" placeholder="Introduce un nombre" width=140px CssClass="TextBox"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" />
            <br />
            <br />
            <span id="Etiqueta">Descripción</span>
            <asp:TextBox ID="TextBox1" TextMode="MultiLine"  Columns="72" Rows="4" runat="server" placeholder="Introduce una breve descripcion del rol" CssClass="TextBox"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" />
        </div>
        <br />
        <br />
        <asp:Button id="BotonCrearRol" TExt="Crear" runat="server" CssClass="Boton"/>
        <asp:Button id="BotonActualizarRol" TExt="Actualizar" runat="server" CssClass="Boton"/>
        <asp:Button id="BotonEliminarRol" TExt="Eliminar" runat="server" CssClass="Boton"/>
        <hr />
    </div>
</asp:Content>
