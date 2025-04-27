<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DBUsuario.aspx.cs" Inherits="ProWeb.DBUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/DBUsuario.css" />

        <div >
        <p>Seleccione un usuario: </p>
        <asp:DropDownList runat="server" ID="ListaDBUsuarios" CssClass="ListaDesplegable" OnSelectedIndexChanged="ListaDBUsuarios_SelectedIndexChanged">
            <asp:ListItem Text="Nuevo Usuario" Value="1" />
        </asp:DropDownList>
    </div>
    <hr/>
    <div id="Editor_Usuarios">
            ID de Usuario&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBid" runat="server"></asp:TextBox>
            <br />
            <br />
            Contraseña&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBcontrasena" runat="server"></asp:TextBox>
            <br />
            <br />
            Nombre&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBnombre" runat="server"></asp:TextBox>
            <br />
            <br />
            Saldo (€)&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBsaldo" runat="server"></asp:TextBox>
            <br />
            <br />
            Datos de pago&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBdatos_pago" runat="server"></asp:TextBox>
            <br />
            <br />
            Dirección&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBdireccion" runat="server"></asp:TextBox>
            <br />
            <br />
            Teléfono&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBtelefono" runat="server"></asp:TextBox>
            <br />
            <br />
            Imagen (.png)&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBimagen" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="BTNagregar_pat" runat="server" CssClass="BotonEditarUsuario" Text="Agregar/Editar usuario" />
            <asp:Button ID="BTNeliminar_pat" runat="server" CssClass="BotonBorrarUsuario" Text="Eliminar usuario" />
            
    </div>


</asp:Content>
