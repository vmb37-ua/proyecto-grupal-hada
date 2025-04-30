<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminUsuario.aspx.cs" Inherits="ProWeb.AdminUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminUsuario.css" />
    <div >
        <p>Seleccione un usuario: </p>
        <asp:DropDownList runat="server" ID="ListaUsuarios" CssClass="ListaDesplegable" OnSelectedIndexChanged="ListaUsuarios_SelectedIndexChanged">
            <asp:ListItem Text="Opción 1" Value="1" />
            <asp:ListItem Text="Opción 2" Value="2" />
        </asp:DropDownList>
    </div>
    <hr />
    <div id="contenedor1">
        <p>Seleccione un rol: </p>
        <asp:DropDownList runat="server" ID="ListaRoles" CssClass="ListaDesplegable">
            <asp:ListItem Text="Opción 1" Value="1" />
            <asp:ListItem Text="Opción 2" Value="2" />

        </asp:DropDownList>
        <asp:Button runat="server" CssClass="BotonEditarUsuario" Text="Agregar rol"/>
        <asp:Button runat="server" CssClass="BotonEditarUsuario" Text="Eliminar rol"/>

        <br /><br />
        <asp:Button runat="server" CssClass="BotonBorrarUsuario" Text="Eliminar usuario"/>
    </div>
</asp:Content>
