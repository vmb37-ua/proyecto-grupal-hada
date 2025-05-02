<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="ProWeb.PanelAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/PanelAdmin.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p id="titulo">OPCIONES DE ADMINISTRACIÓN</p>

        <div class="contenedor-botones">
            <asp:Button ID="BtnRoles" runat="server" CssClass="BotonAdmin" Text="Roles" OnClick="EventoAdminRoles" />
            <asp:Button ID="BtnUsuarios" runat="server" CssClass="BotonAdmin" Text="Usuarios" OnClick="EventoAdminUsuarios" />
            <asp:Button ID="BtnUbicaciones" runat="server" CssClass="BotonAdmin" Text="Ubicaciones" OnClick="EventoAdminUbicaciones" />
            <asp:Button ID="BtnEstadios" runat="server" CssClass="BotonAdmin" Text="Estadios" OnClick="EventoAdminEstadios" />
            <asp:Button ID="BtnEquipos" runat="server" CssClass="BotonAdmin" Text="Equipos" OnClick="EventoAdminEquipos" />
            <asp:Button ID="BtnNotificaciones" runat="server" CssClass="BotonAdmin" Text="Notificaciones" OnClick="EventoAdminNotificaciones" />
            <asp:Button ID="BtnApuestas" runat="server" CssClass="BotonAdmin" Text="Apuestas" OnClick="EventoAdminApuestas" />
            <asp:Button ID="BtnPatrocinadores" runat="server" CssClass="BotonAdmin" Text="Patrocinadores" OnClick="EventoAdminPatrocinadores" />
            <asp:Button ID="BtnCategorias" runat="server" CssClass="BotonAdmin centrar-ultimo" Text="Categorías" OnClick="EventoAdminCategorias" />
        </div>
    </div>
</asp:Content>
