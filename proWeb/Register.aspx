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
    <asp:DropDownList ID="ddlPais" runat="server" CssClass="Inputregister">
        <asp:ListItem Text="País" Value="" Selected="True" />
        <asp:ListItem Text="España" Value="España" />
        <asp:ListItem Text="Francia" Value="Francia" />
        <asp:ListItem Text="Italia" Value="Italia" />
    </asp:DropDownList>

    <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="Inputregister">
        <asp:ListItem Text="Provincia" Value="" Selected="True" />
        <asp:ListItem Text="Alicante" Value="Alicante" />
        <asp:ListItem Text="Madrid" Value="Madrid" />
        <asp:ListItem Text="Valencia" Value="Valencia" />
    </asp:DropDownList>

    <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="Inputregister">
        <asp:ListItem Text="Municipio" Value="" Selected="True" />
        <asp:ListItem Text="Elche" Value="Elche" />
        <asp:ListItem Text="San Vicente" Value="San Vicente" />
        <asp:ListItem Text="Alicante" Value="Alicante" />
    </asp:DropDownList>
</div>



        <br />

         <asp:Label ID="Labelerror" runat="server" CssClass="mensajeError" ForeColor="Red" Visible="false" ></asp:Label>

        <br />

        <asp:Button runat="server" Text="Crear cuenta" CssClass="botonregister" OnClick="EventoPaginaPrincipal"/>
        <asp:Button runat="server" Text="Iniciar Sesión" CssClass="botonregister" OnClick="EventoInicioSesion"/>

    </div>
</asp:Content>
