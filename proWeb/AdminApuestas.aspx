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

        <p>Cuota Equipo 1:</p>
        <asp:TextBox ID="txtCot1" runat="server" CssClass="InputCampo" />

        <p>Cuota Empate (X):</p>
        <asp:TextBox ID="txtCotX" runat="server" CssClass="InputCampo" />

        <p>Cuota Equipo 2:</p>
        <asp:TextBox ID="txtCot2" runat="server" CssClass="InputCampo" />


        <p>Estadio:</p>
        <asp:DropDownList ID="ddlEstadios" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <p>Equipo 1:</p>
        <asp:DropDownList ID="ddlEquipo1" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <p>Equipo 2:</p>
        <asp:DropDownList ID="ddlEquipo2" runat="server" CssClass="ListaDesplegable"></asp:DropDownList>

        <br /><br />
        <asp:Button ID="btnCrear" runat="server" Text="Crear apuesta" CssClass="BotonAdmin" OnClick="btnCrear_Click" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar apuesta" OnClick="btnActualizar_Click" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar apuesta" OnClick="btnEliminar_Click" />

         
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" />
    </div>
</asp:Content>