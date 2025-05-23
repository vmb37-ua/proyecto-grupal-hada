<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ApuestasUsuario.aspx.cs" Inherits="ProWeb.ApuestasUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/ApuestasUsuario.css" />

    <p>Apuestas realizadas: </p>
     <div id="Tabla_apuestas">
         <div class="grid-container">
        <asp:GridView ID="GridViewApuestasUsuario" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewJuegos_RowDataBound" CssClass="table">
        
        <Columns>

            <asp:BoundField DataField="IdApuesta" HeaderText="Id de la apuesta" />
            <asp:BoundField DataField="EquipoLocal" HeaderText="Local" />
            <asp:BoundField DataField="EquipoVisitante" HeaderText="Visitante" />
            <asp:BoundField DataField="Resultado_apuesta" HeaderText="Resultado de la apuesta" />
            <asp:BoundField DataField="Cotizacion" HeaderText="Cotización" />
            <asp:BoundField DataField="Estadio" HeaderText="Estadio" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
        </asp:GridView>
             </div>
     </div>

</asp:Content>
