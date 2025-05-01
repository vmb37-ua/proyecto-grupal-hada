<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DBPatrocinadores.aspx.cs" Inherits="ProWeb.DBPatrocinadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/DBPatrocinadores.css" />

        <div >
        <p>Seleccione un patrocinador: </p>
        <asp:DropDownList runat="server" ID="ListaPatrocinadores" CssClass="ListaDesplegable" OnSelectedIndexChanged="ListaPatrocinadores_SelectedIndexChanged">
            <asp:ListItem Text="Nuevo patrocinador" Value="1" />
        </asp:DropDownList>
    </div>
    <hr/>
    <div id="Editor_Patrocinadores">
            ID de Patrocinador&nbsp;&nbsp;
            <asp:TextBox ID="TBid" runat="server"></asp:TextBox>
            <br />
            <br />
            Pago acordado (€)&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBdinero" runat="server"></asp:TextBox> 
            <br />
            <br />
            Observaciones&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBtexto" runat="server" Width="300px" Height="100px"></asp:TextBox>
            <br />
            <br />
            Imagen (.png)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBimagen" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="BTNagregar_pat" runat="server" CssClass="BotonEditarPatrocinador" Text="Agregar/Editar patrocinador" />
            <asp:Button ID="BTNeliminar_pat" runat="server" CssClass="BotonBorrarPatrocinador" Text="Eliminar patrocinador" />
            
    </div>



</asp:Content>
