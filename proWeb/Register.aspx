<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ProWeb.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Register.css" />
    <div id="Contenedor1">
        <p id="titulo">REGISTRARSE</p>

        <asp:TextBox ID="Nameregister" runat="server" CssClass="Inputregister"></asp:TextBox>

        <asp:TextBox ID="Numberregister" runat="server" CssClass="Inputregister"></asp:TextBox>

        <asp:TextBox ID="Emailregister" runat="server" CssClass="Inputregister"></asp:TextBox>
        
        <asp:TextBox ID="Adressresgister" runat="server" CssClass="Inputregister"></asp:TextBox>

        <asp:TextBox ID="Passregister" runat="server" CssClass="Inputregister" TextMode="Password"></asp:TextBox>


    <asp:TextBox ID="Passrepregister" runat="server" CssClass="Inputregister" TextMode="Password"></asp:TextBox>
    <div class="dropdown-row">
    <asp:DropDownList ID="Paisregister" runat="server" CssClass="Inputregister" AutoPostBack="true" OnSelectedIndexChanged="Paisregister_SelectedIndexChanged" />
    <asp:DropDownList ID="Provinciaregister" runat="server" CssClass="Inputregister" AutoPostBack="true" OnSelectedIndexChanged="Provinciaregister_SelectedIndexChanged" />

    <asp:DropDownList ID="Municipioregister" runat="server" CssClass="Inputregister"/>


</div>



        <br />

         <asp:Label ID="Labelerror" runat="server" CssClass="mensajeError" ForeColor="Red" Visible="false" ></asp:Label>

        <br />

        <asp:Button runat="server" Text="Crear cuenta" CssClass="botonregister" OnClick="EventoPaginaPrincipal"/>
        <asp:Button runat="server" Text="Iniciar Sesión" CssClass="botonregister" OnClick="EventoInicioSesion"/>
        <asp:Label ID="LabelDebug" runat="server" Visible="false" ForeColor="Blue"></asp:Label>

    </div>
</asp:Content>
