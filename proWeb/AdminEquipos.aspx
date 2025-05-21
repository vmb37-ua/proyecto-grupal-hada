<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminEquipos.aspx.cs" Inherits="ProWeb.AdminEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminEquipos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <p>Nombre del equipo:</p>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="InputCampo" />

        <p>Escudo (URL o nombre de archivo):</p>
        <asp:FileUpload ID="fileEscudo" runat="server" CssClass="InputCampo" />


        <p>Categoría:</p>
        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="ListaDesplegable" />

        <br /><br />
        <asp:Button ID="btnCrear" runat="server" CssClass="BotonAdmin" Text="Crear equipo" OnClick="btnCrear_Click" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="BotonAdmin" Text="Actualizar equipo" OnClick="btnActualizar_Click" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="BotonEliminar" Text="Eliminar equipo" OnClick="btnEliminar_Click" />

        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" />
    </div>
</asp:Content>

