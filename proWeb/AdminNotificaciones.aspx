<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminNotificaciones.aspx.cs" Inherits="ProWeb.AdminNotificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminNotificaciones.css" />
    <div >
        <p>Seleccione un usuario para enviar notificación: </p>
        <asp:DropDownList runat="server" ID="ListaUsuarios" CssClass="ListaDesplegable" OnSelectedIndexChanged="ListaUsuarios_SelectedIndexChanged">
            <asp:ListItem Text="Opción 1" Value="1" />
            <asp:ListItem Text="Opción 2" Value="2" />
        </asp:DropDownList>
    </div>
    <hr />
    <div id="contenedor1">
    <p>Escriba a continuación la notificación a enviar: </p>
    <textarea class="InputNotif" placeholder="Escriba la notificación aquí..."></textarea>

         

    <br /><br />
    <asp:Button runat="server" CssClass="BotonAdminNotificaciones" Text="Enviar a usuario"/>
    <asp:Button runat="server" CssClass="BotonAdminNotificaciones" Text="Enviar a todos"/>
            
    <hr />

        <p>Escriba el id de la notificación a eliminar: </p>
        <asp:TextBox runat="server" ID="IdEliminar"></asp:TextBox>
        <br />
        <br />
        <asp:Button runat="server" CssClass="BotonBorrarNotificaciones" Text="Eliminar notificación" />

    </div>
</asp:Content>
