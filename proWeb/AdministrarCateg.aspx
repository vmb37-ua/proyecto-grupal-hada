<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarCateg.aspx.cs" Inherits="ProWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/EstiloAdministrar.css" />
    <div id="Holder">
        <h1 id="Titulo">Editar Categorias</h1>
        <span id="Etiqueta">Nombre</span>
        <asp:TextBox ID="TBNombreRol" runat="server" placeholder="Introduce un nombre" width=140px CssClass="TextBox"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" />
        <br />
        <br />
        <asp:Button id="BotonCrearRol" TExt="Crear" runat="server" CssClass="Boton"/>
        <asp:Button id="BotonActualizarRol" TExt="Actualizar" runat="server" CssClass="Boton"/>
        <asp:Button id="BotonEliminarRol" TExt="Eliminar" runat="server" CssClass="Boton"/>
        <hr />
    </div>
</asp:Content>
