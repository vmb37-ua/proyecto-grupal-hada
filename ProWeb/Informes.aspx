<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="ProWeb.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Informes.css" />
    
    <div id="ContenedorEstadisticas">
        <!-- Columna 1: Estadísticas de Partidos -->
        <div class="ColumnaEstadistica" id="ColumnaPartidos">
            <h3>⚽ TOP PARTIDOS</h3>
            <div class="EstadisticaItem">
                <h4>Top 5 más apostados</h4>
                <asp:GridView ID="gvTopPartidos" runat="server" CssClass="TablaEstadisticas" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Posicion" HeaderText="#" />
                        <asp:BoundField DataField="Partido" HeaderText="Partido" />
                        <asp:BoundField DataField="Apuestas" HeaderText="Apuestas" />
                    </Columns>
                </asp:GridView>
            </div>
            
            <div class="EstadisticaItem">
                <h4>Partido con mayor cuota</h4>
                <asp:Label ID="lblMayorCuota" runat="server" CssClass="DatoDestacado">Barcelona vs PSG - Cuota: 9.5</asp:Label>
            </div>
        </div>

        <!-- Columna 2: Estadísticas de Usuarios -->
        <div class="ColumnaEstadistica" id="ColumnaUsuarios">
            <h3>👥 TOP USUARIOS</h3>
            <div class="EstadisticaItem">
                <h4>Mayores ganadores</h4>
                <asp:BulletedList ID="blTopGanadores" runat="server" CssClass="ListaEstadisticas">
                    <asp:ListItem>Usuario123 - 2,450€</asp:ListItem>
                    <asp:ListItem>BetMaster - 1,890€</asp:ListItem>
                </asp:BulletedList>
            </div>
            
            <div class="EstadisticaItem">
                <h4>Usuarios más activos</h4>
                <asp:Label ID="lblUsuarioActivo" runat="server" CssClass="DatoDestacado">Apostador1 - X apuestas</asp:Label>
                <asp:Label ID="lblUsuarioActivo2" runat="server" CssClass="DatoDestacado">Apostador2 - X apuestas</asp:Label>
            </div>
        </div>

        <!-- Columna 3: Estadísticas Generales -->
        <div class="ColumnaEstadistica" id="ColumnaGlobal">
            <h3>📈 GLOBAL</h3>
            <div class="EstadisticaItem">
                <h4>Apuestas hoy</h4>
                <asp:Label ID="lblApuestasHoy" runat="server" CssClass="DatoGrande">1,248</asp:Label>
            </div>
            
            <div class="EstadisticaItem">
                <h4>Ganancias totales</h4>
                <asp:Label ID="lblGanancias" runat="server" CssClass="DatoGrande">24,580€</asp:Label>
            </div>
            
            <div class="EstadisticaItem">
                <h4>Próximo evento</h4>
                <asp:Label ID="lblProximoEvento" runat="server" CssClass="DatoDestacado">Champions: Bayern vs Real Madrid</asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
