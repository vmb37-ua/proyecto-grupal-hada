<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InformacionEstadio.aspx.cs" Inherits="ProWeb.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/InformacionEstadio.css" />

    <div style="display: flex; align-items: flex-start; gap: 20px; justify-content: center;">
        <div class="Holder1">
            <asp:DropDownList ID="TBSelector" runat="server">
                <asp:ListItem Text="Introduce un estadio" Value="" Disabled="True" Selected="True" />
            </asp:DropDownList>




        </div>

        <span class="Holder2">
            <asp:Label ID="LabelNombre" runat="server" CssClass="Nombre" />
            <br />
            <br />
            <asp:Label ID="LabelCiudad" runat="server" CssClass="Ciudad" />
            &nbsp;&nbsp;
            <asp:HyperLink ID="LinkDireccion" runat="server" Target="_blank"></asp:HyperLink>
            <br />
            <br />
            <br />
            <asp:Literal ID="MapFrame" runat="server" />
        </span>
    </div>



</asp:Content>
