<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DBPatrocinadores.aspx.cs" Inherits="ProWeb.DBPatrocinadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/DBPatrocinadores.css" />

        <div >
        <p>Seleccione un patrocinador: </p>
        <asp:DropDownList runat="server" ID="ListaPatrocinadores" CssClass="ListaDesplegable" AutoPostBack="true" OnSelectedIndexChanged="ListaPatrocinadores_SelectedIndexChanged">
            <asp:ListItem Text="Nuevo patrocinador" Value="1" />
        </asp:DropDownList>
    </div>
    <hr/>
    <div id="Editor_Patrocinadores">
            ID de Patrocinador&nbsp;&nbsp;
            <asp:TextBox ID="TBid" runat="server" ReadOnly="true"></asp:TextBox>
            <br />
            <br />
            Nombre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBtexto" runat="server"></asp:TextBox>
            <br />
            <br />
            Pago acordado (€)&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TBdinero" runat="server"></asp:TextBox> 
            <br />
            <br />
            <asp:Button ID="BTNagregar_pat" runat="server" CssClass="BotonEditarPatrocinador" Text="Agregar/Editar patrocinador"  OnClick="BTNagregar_pat_Click" />
            <asp:Button ID="BTNeliminar_pat" runat="server" CssClass="BotonBorrarPatrocinador" Text="Eliminar patrocinador" OnClick="BTNeliminar_pat_Click"/>
            
    </div>



</asp:Content>
