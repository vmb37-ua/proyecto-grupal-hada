<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Juegos.aspx.cs" Inherits="ProWeb.Juegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link rel="stylesheet" href="Source/Styles/Juegos.css" />


        <div class="juegos-container">
        <h1>Próximos partidos:</h1>
        <asp:Repeater ID="rptJuegos" runat="server">
            <ItemTemplate>
                    
                <div class="juego">

                        <div class="titulo-partido">
                                <img src='<%# "Source/Images/" + ((ProWeb.Juegos.Juego)Container.DataItem).EquipoLocal.Replace(" ", "").ToLower() + ".png" %>' alt="Imagen izquierda" class="imagen_lateral" />
                                <span class="titulo-texto">
                                    <h2><asp:Label ID="lblEquipoLocal" runat="server" Text='<%# ((ProWeb.Juegos.Juego)Container.DataItem).EquipoLocal %>'></asp:Label> vs 
                                    <asp:Label ID="lblEquipoVisitante" runat="server" Text='<%# ((ProWeb.Juegos.Juego)Container.DataItem).EquipoVisitante %>'></asp:Label></h2>
                                </span>

                                <img src='<%# "Source/Images/" + ((ProWeb.Juegos.Juego)Container.DataItem).EquipoVisitante.Replace(" ", "").ToLower() + ".png" %>' alt="Imagen derecha" class="imagen_lateral" />
                        </div>


                    <p>Estadio: <asp:Label ID="lblEstadio" runat="server" Text='<%# ((ProWeb.Juegos.Juego)Container.DataItem).Estadio %>'></asp:Label></p>
                    <p>Fecha: <asp:Label ID="lblFecha" runat="server" Text='<%# ((ProWeb.Juegos.Juego)Container.DataItem).Fecha.ToString("dd/MM/yyyy") %>'></asp:Label></p>
                    <p>Hora: <asp:Label ID="lblHora" runat="server" Text='<%# ((ProWeb.Juegos.Juego)Container.DataItem).Hora.ToString(@"hh\:mm") %>'></asp:Label></p>
                    

                    <asp:Button ID="botonJuego" runat="server" Text="Apostar" CssClass="juego_boton" OnClick="EventoJuegoClick"/>

           

                </div>
                
            </ItemTemplate>
        </asp:Repeater>
    </div>


</asp:Content>
