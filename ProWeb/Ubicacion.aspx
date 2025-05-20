<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Ubicacion.aspx.cs" Inherits="ProWeb.Ubicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Ubicacion.css" />
    <div id="Contenedor">
        <!-- Creación de País -->
        <div class="Columna" id="ColumnaIzquierda">
            <h3>PAÍS</h3>
            <label for="txtNuevoPais">Nombre del país</label>
            <asp:TextBox ID="txtNuevoPais" runat="server" CssClass="InputUbicacion" placeholder="Ej: España"></asp:TextBox>
            <asp:Button ID="btnCrearPais" runat="server" Text="Crear País" CssClass="BotonCrear" OnClick="btnCrearPais_Click" />
            <label for="ddlEliminarPais">Eliminar país existente</label>
            <asp:DropDownList ID="ddlEliminarPais" runat="server" CssClass="InputUbicacion"></asp:DropDownList>
            <asp:Button ID="btnEliminarPais" runat="server" Text="Eliminar País" CssClass="BotonEliminar" OnClick="btnEliminarPais_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje-estilo" Visible="false"></asp:Label>
        </div>

        <!-- Creación de Provincia -->
        <div class="Columna" id="ColumnaCentro">
            <h3>PROVINCIA</h3>
            <label for="txtNuevoProvincia">Nombre de la provincia</label>
            <asp:TextBox ID="txtNuevoProvincia" runat="server" CssClass="InputUbicacion" placeholder="Ej: Madrid"></asp:TextBox>
            <label for="ddlPaisProvincia">País asociado</label>
            <asp:DropDownList ID="ddlPaisProvincia" runat="server" AutoPostBack ="True" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged" CssClass="InputUbicacion"></asp:DropDownList>
            <asp:Button ID="btnCrearProvincia" runat="server" Text="Crear Provincia" CssClass="BotonCrear" OnClick="btnCrearProvincia_Click" />
            <label for="ddlEliminarProvincia">Eliminar provincia existente</label>
            <asp:DropDownList ID="ddlEliminarProvincia" runat="server" CssClass="InputUbicacion"></asp:DropDownList>
            <asp:Button ID="btnEliminarProvincia" runat="server" Text="Eliminar Provincia" CssClass="BotonEliminar" OnClick="btnEliminarProvincia_Click" />
            <asp:Label ID="lblMensaje2" runat="server" CssClass="mensaje-estilo" Visible="false"></asp:Label>
        </div>

        <!-- Creación de Municipio -->
        <div class="Columna" id="ColumnaDerecha">
            <h3>MUNICIPIO</h3>
            <label for="txtNuevoMunicipio">Nombre del municipio</label>
            <asp:TextBox ID="txtNuevoMunicipio" runat="server" CssClass="InputUbicacion" placeholder="Ej: Alcalá de Henares"></asp:TextBox>
            <label for="ddlProvinciaMunicipio">Provincia asociada</label>
            <asp:DropDownList ID="ddlProvinciaMunicipio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincias_SelectedIndexChanged" CssClass="InputUbicacion"></asp:DropDownList>
            <asp:Button ID="btnCrearMunicipio" runat="server" Text="Crear Municipio" CssClass="BotonCrear" OnClick="btnCrearMunicipio_Click" />
            <label for="ddlEliminarMunicipio">Eliminar municipio existente</label>
            <asp:DropDownList ID="ddlEliminarMunicipio" runat="server" CssClass="InputUbicacion"></asp:DropDownList>
            <asp:Button ID="btnEliminarMunicipio" runat="server" Text="Eliminar Municipio" CssClass="BotonEliminar" OnClick="btnEliminarMunicipio_Click" />
        </div>
    </div>
</asp:Content>