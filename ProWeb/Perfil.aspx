<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="ProWeb.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Perfil.css" />
    <div id="ContentPerfil">
        <div id="Contenedor1">
            <asp:Image ImageURL="~/Source/Images/DefaultPFP.jpg" CssClass="ImagenPerfil" ID="FotoPerfil" runat="server"/>
            <br />
            <asp:Button ID="BotonFoto" Text="Cambiar foto de perfil" runat="server"></asp:Button>
            <asp:Button ID="BotonPerfil" Text="Editar perfil" runat="server" OnClick="EventoEditarPerfil"></asp:Button>
            <asp:Button ID="BotonEliminar" Text="Eliminar cuenta" runat="server" CssClass="Botonborrar"></asp:Button>
        </div>
        <div id="Contenedor2">
            <p class="TituloPerfil">Nombre</p>
            <asp:Label ID="CampoNombre" CssClass="CampoPerfil" runat="server">Name Placeholder</asp:Label>
            <br />
            <p class="TituloPerfil">Dirección</p>
            <asp:Label ID="CampoDireccion" CssClass="CampoPerfil" runat="server">Name Placeholder</asp:Label>
            <br />
            <p class="TituloPerfil">Teléfono</p>
            <asp:Label ID="CampoTelefono" CssClass="CampoPerfil" runat="server">Name Placeholder</asp:Label>
            <br />
            <p class="TituloPerfil">Saldo</p>
            <asp:Label ID="CampoSaldo" CssClass="CampoPerfil" runat="server">Name Placeholder</asp:Label>
        </div>
    </div>
</asp:Content>