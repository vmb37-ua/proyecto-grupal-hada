<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" 
    CodeBehind="ListaEquipos.aspx.cs" Inherits="ProWeb.ListaEquipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <link rel="stylesheet" href="Source/Styles/ListaEquipos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contenedor-principal">
        <!-- Barra de búsqueda -->
        <div class="barra-busqueda">
            <i class="fas fa-search"></i>
            <asp:TextBox ID="txtBusqueda" runat="server" placeholder="Buscar equipo..." 
                CssClass="input-busqueda" AutoPostBack="true" 
                OnTextChanged="txtBusqueda_TextChanged"></asp:TextBox>
        </div>

        <!-- Contenedor de equipos -->
        <div class="grid-equipos">
            <asp:Repeater ID="rptEquipos" runat="server">
                <ItemTemplate>
                    <div class="card-equipo">
                        <div class="escudo-fondo" 
                             style='background-image: url("Source/Images/<%# Eval("Escudo") %>")'>
                        </div>

                        <div class="contenido-equipo">
                            <h3><%# Eval("Nombre") %></h3>
                            <a class="btn-favorito" onclick="toggleFavorito">
                                <i class="far fa-star"></i>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
