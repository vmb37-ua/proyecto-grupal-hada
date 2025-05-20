<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminApuestas.aspx.cs" Inherits="ProWeb.AdminApuestas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminApuestas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>Fecha de la apuesta:</p>
        <asp:TextBox ID="txtFecha" runat="server" CssClass="InputCampo" TextMode="Date"></asp:TextBox>
        
        <p>ID Apuesta:</p>
        <asp:TextBox ID="txtIdApuesta" runat="server" CssClass="InputCampo" />

        <p>Resultado:</p>
        <asp:TextBox ID="txtResultado" runat="server" CssClass="InputCampo" TextMode="Number" />

        <p>Estadio:</p>
        <asp:DropDownList ID="ddlEstadios" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <p>Equipo 1:</p>
        <asp:DropDownList ID="ddlEquipo1" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <p>Equipo 2:</p>
        <asp:DropDownList ID="ddlEquipo2" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <br /><br />
        <asp:Button ID="btnCrear" runat="server" CssClass="BotonAdmin" Text="Crear apuesta" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar apuesta" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar apuesta" />
         
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" />
    </div>
</asp:Content>