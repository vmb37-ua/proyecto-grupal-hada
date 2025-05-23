<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminEstadios.aspx.cs" Inherits="ProWeb.AdminEstadios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminEstadios.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>Nombre del estadio:</p>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="InputCampo"></asp:TextBox>

        <p>Capacidad:</p>
        <asp:TextBox ID="txtCapacidad" runat="server" CssClass="InputCampo" TextMode="Number"></asp:TextBox>

        <p>Descripción:</p>
        <asp:TextBox ID="txtTexto" runat="server" CssClass="InputCampo" TextMode="MultiLine"></asp:TextBox>

        <p>Nombre del Municipio:</p>
        <asp:DropDownList ID="ddlMunicipios" runat="server" CssClass="ListaDesplegable">
        <asp:ListItem Text="-- Selecciona un municipio --" Value="" />
        </asp:DropDownList>

        <p>Estadios Disponibles:</p>
        <asp:DropDownList ID="ddlEstadios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadios_SelectedIndexChanged" />


        <br /><br />
        <asp:Button ID="btnCrear" runat="server" CssClass="BotonAdmin" Text="Crear estadio" OnClick="btnCrear_Click" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar estadio" OnClick="btnActualizar_Click" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar estadio" OnClick="btnEliminar_Click" />
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" /> 
    
    </div>
</asp:Content>
