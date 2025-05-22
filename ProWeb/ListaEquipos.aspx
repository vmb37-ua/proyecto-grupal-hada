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

        <div class="contenedor-dropdown">
            <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="dropdown-categorias" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" ></asp:DropDownList>
        </div>
            
        </div>

        <!-- Contenedor de equipos -->
        <div class="grid-equipos">
    <asp:Repeater ID="rptEquipos" runat="server" OnItemCommand="rptEquipos_ItemCommand">
        <ItemTemplate>
            <div class="card-equipo">
                <div class="escudo-fondo" 
                    style='background-image: url("<%# Eval("Escudo") %>")'>
                </div>

                <div class="contenido-equipo">
                    <h3><%# Eval("Nombre") %></h3>
                    <asp:LinkButton ID="btnFavorito" runat="server" CommandName="ToggleFavorito" CommandArgument='<%# Eval("Id_equipo") %>' CssClass="btn-favorito">
                        <i class='<%# ((List<int>)ViewState["favoritosIdEquipos"]).Contains((int)Eval("Id_equipo")) ? "fas fa-star" : "far fa-star" %>'></i>
                    </asp:LinkButton>

                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

    </div>
</asp:Content>
