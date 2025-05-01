<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminEquipos.aspx.cs" Inherits="ProWeb.AdminEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminEquipos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>Nombre del equipo:</p>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="InputCampo"></asp:TextBox>

        <p>Ciudad:</p>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="InputCampo"></asp:TextBox>

        <p>Estadio asignado:</p>
        <asp:DropDownList ID="ddlEstadios" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <br /><br />
        <asp:Button ID="btnCrear" runat="server" CssClass="BotonAdmin" Text="Crear equipo" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar equipo" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar equipo" />
    </div>
</asp:Content>
