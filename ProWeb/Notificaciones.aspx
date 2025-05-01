<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="ProWeb.Notificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/Notificaciones.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ContenedorNotificaciones">
        <h2>Mis Notificaciones</h2>
        <asp:BulletedList ID="ListaNotificaciones" runat="server" CssClass="listaNotificaciones"></asp:BulletedList>
    </div>
</asp:Content>
