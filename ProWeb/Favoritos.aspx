<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="ProWeb.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Source/Styles/Favoritos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ContenedorFavoritos">
        <h2>Mis Equipos Favoritos</h2>

        <asp:GridView ID="TablaFavoritos" runat="server" CssClass="tablaFavoritos" AutoGenerateColumns="false" EmptyDataText="No tienes equipos favoritos guardados.">
            <Columns>
                <asp:ImageField DataImageUrlField="Escudo" HeaderText="Escudo">
                    <ControlStyle Height="40px" Width="40px" />
                </asp:ImageField>
                <asp:BoundField DataField="NombreEquipo" HeaderText="Equipo" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="VolverPerfil" runat="server" Text="Volver al Perfil" OnClick="IrPerfil_Click" CssClass="botonVolver" />
    </div>
</asp:Content>