<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InformacionEstadio.aspx.cs" Inherits="ProWeb.InformacionEstadio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/InformacionEstadio.css" />

    <div class="contenedor-principal">
        <h1>Estadios disponibles</h1>
        <div class="grid-estadios" id="gridEstadios" runat="server">
        </div>
    </div>



</asp:Content>
