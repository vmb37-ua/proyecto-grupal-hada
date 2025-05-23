<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DBUsuario.aspx.cs" Inherits="ProWeb.DBUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/DBUsuario.css" />

    <div>
        <p>Seleccione un usuario: </p>
        <asp:DropDownList runat="server" ID="ListaDBUsuarios" CssClass="ListaDesplegable" 
            AutoPostBack="true" OnSelectedIndexChanged="ListaDBUsuarios_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <hr/>
    <div id="Editor_Usuarios">
            ID de Usuario&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBid" runat="server" ReadOnly="true"></asp:TextBox>
            <br />
            <br />
            Nombre&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBnombre" runat="server"></asp:TextBox>
            <br />
            <br />
            Dirección&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBdireccion" runat="server"></asp:TextBox>
            <br />
            <br />
            Teléfono&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBtelefono" runat="server" TextMode="Phone"></asp:TextBox>
            <br />
            <br />
            Rol&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBrol" runat="server" Visible="false"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="BTNagregar_pat" runat="server" CssClass="BotonEditarUsuario" 
                Text="Editar usuario" OnClick="BTNagregar_pat_Click" />
            <asp:Button ID="BTNeliminar_pat" runat="server" CssClass="BotonBorrarUsuario" 
                Text="Eliminar usuario" OnClick="BTNeliminar_pat_Click" />
    </div>


</asp:Content>
