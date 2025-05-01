<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminEstadios.aspx.cs" Inherits="ProWeb.AdminEstadios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminEstadios.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>Nombre del estadio:</p>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="InputCampo"></asp:TextBox>

        <p>Ciudad:</p>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="InputCampo"></asp:TextBox>

        <p>Dirección:</p>
        <asp:TextBox ID="txtDireccion" runat="server" CssClass="InputCampo"></asp:TextBox>

        <br /><br />
        <asp:Button ID="btnCrear" runat="server" CssClass="BotonAdmin" Text="Crear estadio" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar estadio" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar estadio" />
    </div>
</asp:Content>