<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminNotificaciones.aspx.cs" Inherits="ProWeb.AdminNotificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminNotificaciones.css" />
    <div >
        <p>Seleccione un usuario para enviar notificación: </p>
        <asp:DropDownList runat="server" ID="ListaUsuarios"  AutoPostBack="true" CssClass="ListaDesplegable" OnSelectedIndexChanged="ListaUsuarios_SelectedIndexChanged">
            <asp:ListItem Text="Opción 1" Value="1" />
            <asp:ListItem Text="Opción 2" Value="2" />
        </asp:DropDownList>
        <asp:DropDownList runat="server" ID="ListaRoles" CssClass="ListaDesplegable" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ListaRoles_SelectedIndexChanged" />

    </div>
    <hr />
    <div id="contenedor1">
<asp:TextBox ID="TextoNotificacion" runat="server" CssClass="InputNotif" 
             TextMode="MultiLine" Rows="5" placeholder="Escriba la notificación aquí..."></asp:TextBox>

<br /><br />
<asp:Button runat="server" CssClass="BotonAdminNotificaciones" 
            Text="Enviar" OnClick="EnviarNotificacion_Click" />
        <asp:Label ID="LblErrorEnviar" runat="server" CssClass="MensajeError" ForeColor="Red"></asp:Label>

            
    <hr />

        <p>Escriba el id de la notificación a gestionar: </p>
        <asp:TextBox runat="server" ID="IdGestion"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BotonBorrarNotificaciones" runat="server" Text="Eliminar Notificación" OnClick="BtnEliminarNotificacion_Click" CssClass="BotonBorrarNotificaciones" />
        <asp:Button ID="BotonLeerNotificacion" runat="server" Text="Leer Notificación" OnClick="BtnLeerNotificacion_Click" CssClass="BotonAdminNotificaciones" />

        <asp:Label ID="LblErrorEliminar" runat="server" CssClass="MensajeError" ForeColor="Red"></asp:Label>

    </div>
</asp:Content>
