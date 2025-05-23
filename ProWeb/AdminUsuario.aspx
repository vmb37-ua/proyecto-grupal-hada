<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminUsuario.aspx.cs" Inherits="ProWeb.AdminUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/AdminUsuario.css"/>
    <div class="Contenedor1">
        <asp:Label runat="server" CssClass="Titulo">Editar perfil</asp:Label>

        <div class="Contenedor2">
            <asp:Label runat="server" CssClass="Etiqueta">Nombre:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" id="CajaNombre"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rfvNombre" 
            runat="server" 
            ControlToValidate="CajaNombre" 
            ErrorMessage="El nombre es obligatorio." 
            ForeColor="Red" 
            Display="Dynamic"/>

            <asp:Label runat="server" CssClass="Etiqueta">Num. tarjeta:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" id="CajaNumTar"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rvfNumTar" 
            runat="server" 
            ControlToValidate="CajaNumTar" 
            ErrorMessage="El número de la tarjeta es obligatorio." 
            ForeColor="Red" 
            Display="Dynamic"/>
            <asp:RegularExpressionValidator 
            ID="rvfTar" 
            runat="server" 
            ControlToValidate="CajaNumTar"
            ValidationExpression="^\d{16}$"
            ErrorMessage="El num. de tarjeta debe tener exactamente 16 números"
            ForeColor="Red"
            Display="Dynamic" />

            <asp:Label runat="server" CssClass="Etiqueta">CVV:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" ID="CajaCvv"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rvfCvv" 
            runat="server" 
            ControlToValidate="CajaCvv" 
            ErrorMessage="El cvv es obligatorio." 
            ForeColor="Red" 
            Display="Dynamic"/>
            <asp:RegularExpressionValidator 
            ID="revCvv" 
            runat="server" 
            ControlToValidate="CajaCvv"
            ValidationExpression="^\d{3}$"
            ErrorMessage="Cvv debe tener exactamente 3 números"
            ForeColor="Red"
            Display="Dynamic" />

            <asp:Label runat="server" CssClass="Etiqueta">Fecha cad.:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" ID="CajaCad"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rvfCad" 
            runat="server" 
            ControlToValidate="CajaCad" 
            ErrorMessage="La caducidad de la tarjeta es obligatoria." 
            ForeColor="Red" 
            Display="Dynamic"/>
            <asp:Label runat="server" CssClass="MensajeError" ID="Errorval"></asp:Label>

            <asp:Label runat="server" CssClass="Etiqueta">Dirección:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" id="CajaDir"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rvfDir" 
            runat="server" 
            ControlToValidate="CajaDir" 
            ErrorMessage="La direccion es obligatoria." 
            ForeColor="Red" 
            Display="Dynamic"/>

            <asp:Label runat="server" CssClass="Etiqueta">Teléfono:</asp:Label>
            <asp:TextBox runat="server" CssClass="CajaDeTexto" id="CajaTelef"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="rvfTelef" 
            runat="server" 
            ControlToValidate="CajaTelef" 
            ErrorMessage="El teléfono es obligatorio." 
            ForeColor="Red" 
            Display="Dynamic"/>
            <asp:RegularExpressionValidator 
            ID="RegularExpressionValidator1" 
            runat="server" 
            ControlToValidate="CajaTelef"
            ValidationExpression="^\d{9}$"
            ErrorMessage="El teléfono debe ser de exactamente 9 números"
            ForeColor="Red"
            Display="Dynamic" />

            <div id="cajaFoto">
                <div id="selecFoto">
                    <asp:FileUpload runat="server" CssClass="fileup" ID="selecFoto"/>
                </div>
                <asp:Button ID="BotonFoto" Text="Cambiar foto de perfil" runat="server" CssClass="Boton" OnClick="EventoCambioFoto"></asp:Button>
                <asp:Label runat="server" ID="MensajeFoto" CssClass="MensajeError"></asp:Label>
            </div>  
        </div>
        <asp:Button runat="server" CssClass="Boton" Text="Editar el perfil" OnClick="EventoCambiar"/>
    </div>
</asp:Content>
