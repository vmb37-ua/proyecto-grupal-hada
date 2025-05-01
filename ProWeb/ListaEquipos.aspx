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
            <i class="fas fa-search"></i> <!-- Icono de busqueda -->
            <asp:TextBox ID="txtBusqueda" runat="server" placeholder="Buscar equipo..." 
                CssClass="input-busqueda" AutoPostBack="true"></asp:TextBox>
        </div>

        <!-- Contenedor de equipos (datos estáticos) -->
        <div class="grid-equipos">

            <!-- Equipo 1 de ejemplo -->
            <div class="card-equipo">
                <div class="escudo-fondo" style="background-image: url('Source/Images/madrid.png')"></div>
                
                
                <div class="contenido-equipo">
                    <h3>Real Madrid</h3>
                    
                    <!-- Estrella de favoritos  -->
                    <a class="btn-favorito" onclick="toggleFavorito(this)">
                        <i class="far fa-star"></i> <!-- Icono hueco -->
                    </a>
                </div>
            </div>

            <!-- Equipo 2 de ejemplo -->
            <div class="card-equipo">
                
                <div class="escudo-fondo" style="background-image: url('Source/Images/barcelona.png')"></div>
    
                
                <div class="contenido-equipo">
                    <h3>Barcelona</h3>
        
                    
                    <a class="btn-favorito" onclick="toggleFavorito(this)">
                        <i class="far fa-star"></i>
                    </a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>