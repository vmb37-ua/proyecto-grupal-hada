<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="ProWeb.PanelAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/PanelAdmin.css" />
    <div>
        <p id="titulo">OPCIONES DE ADMINISTRACIÓN</p>

<div class="contenedor-botones">
    <button class="BotonAdmin">Roles</button>
    <button class="BotonAdmin">Usuarios</button>
    <button class="BotonAdmin">Ubicaciones</button>
    <button class="BotonAdmin">Estadios</button>
    <button class="BotonAdmin">Equipos</button>
    <button class="BotonAdmin">Notificaciones</button>
    <button class="BotonAdmin">Apuestas</button>
    <button class="BotonAdmin">Patrocinadores</button>
<button class="BotonAdmin centrar-ultimo">Categorías</button>

</div>

    </div>
</asp:Content>
