<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InformacionEstadio.aspx.cs" Inherits="ProWeb.InformacionEstadio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/InformacionEstadio.css" />

    
    <div class="Holder">
        <h1 class="Titulo">Estadios</h1>
    </div>
    <div class="contenedor-principal">
        <asp:Repeater ID="RepeaterEstadios" runat="server">
            <ItemTemplate>
                <div class="card-estadio">
                    <div class="contenido-estadio">
                        <h3><%# Eval("Nombre") %></h3>
                        <p><%# TruncarTexto(Eval("Texto")) %></p>
                        <p>Capacidad: <%# Eval("Capacidad") %></p>
                        <p>Municipio: <%# Eval("NombreMunicipio") %></p>
                        <div style="width:600px; height:200px; margin: auto;">
                            <iframe width="600px" height="200" frameborder="0" style="border:0"
                                src='<%# GetGoogleMapsEmbedUrl(Eval("Nombre"), Eval("NombreMunicipio")) %>'
                                allowfullscreen loading="lazy">
                            </iframe>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Label ID="LabelEstadios" runat="server"/>
    </div>

</asp:Content>