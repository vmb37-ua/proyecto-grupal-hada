<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ProWeb.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>

    <style>
        .input-error {
            border: 2px solid red !important;
            background-color: #ffe6e6;
        }
    </style>

    <script type="text/javascript">
        function validateAndHighlight() {
            var isValid = Page_ClientValidate();

            var controlsToValidate = [
    '<%= Nameregister.ClientID %>',
    '<%= Numberregister.ClientID %>',
    '<%= Emailregister.ClientID %>',
    '<%= Adressresgister.ClientID %>',
    '<%= Numerotarregister.ClientID %>',   
    '<%= CaducidadTarregister.ClientID %>', 
    '<%= Cvvregister.ClientID %>',          
    '<%= Passregister.ClientID %>',
    '<%= Passrepregister.ClientID %>',
    '<%= Paisregister.ClientID %>',
    '<%= Provinciaregister.ClientID %>',
                '<%= Municipioregister.ClientID %>'
            ];


            for (var i = 0; i < controlsToValidate.length; i++) {
                var ctrl = document.getElementById(controlsToValidate[i]);
                if (!ctrl) continue;

                var isCtrlValid = true;
                var validators = Page_Validators;

                for (var v = 0; v < validators.length; v++) {
                    var validator = validators[v];
                    if (validator.controltovalidate == controlsToValidate[i]) {
                        if (!validator.isvalid) {
                            isCtrlValid = false;
                            break;
                        }
                    }
                }

                if (!isCtrlValid) {
                    ctrl.classList.add("input-error");
                } else {
                    ctrl.classList.remove("input-error");
                }
            }

            return isValid; 
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/Register.css" />
    <div id="Contenedor1">
        <p id="titulo">REGISTRARSE</p>

        <asp:TextBox ID="Nameregister" runat="server" CssClass="Inputregister" placeholder="Nombre Completo" />
        <asp:RequiredFieldValidator ControlToValidate="Nameregister" ErrorMessage="El nombre es obligatorio." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="Numberregister" runat="server" CssClass="Inputregister" placeholder="Número de teléfono" />
        <asp:RequiredFieldValidator ControlToValidate="Numberregister" ErrorMessage="El número es obligatorio." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="Emailregister" runat="server" CssClass="Inputregister" placeholder="Correo electrónico" />
        <asp:RequiredFieldValidator ControlToValidate="Emailregister" ErrorMessage="El correo es obligatorio." ForeColor="Red" Display="Dynamic" runat="server" />
        <asp:RegularExpressionValidator ControlToValidate="Emailregister" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,}$" ErrorMessage="Correo no válido." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="Adressresgister" runat="server" CssClass="Inputregister" placeholder="Dirección" />
        <asp:RequiredFieldValidator ControlToValidate="Adressresgister" ErrorMessage="La dirección es obligatoria." ForeColor="Red" Display="Dynamic" runat="server" />

                    <asp:DropDownList ID="Paisregister" runat="server" CssClass="Inputregister" AutoPostBack="true" OnSelectedIndexChanged="Paisregister_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ControlToValidate="Paisregister" InitialValue="" ErrorMessage="Selecciona un país." ForeColor="Red" Display="Dynamic" runat="server" />

            <asp:DropDownList ID="Provinciaregister" runat="server" CssClass="Inputregister" AutoPostBack="true" OnSelectedIndexChanged="Provinciaregister_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ControlToValidate="Provinciaregister" InitialValue="" ErrorMessage="Selecciona una provincia." ForeColor="Red" Display="Dynamic" runat="server" />

            <asp:DropDownList ID="Municipioregister" runat="server" CssClass="Inputregister" />
            <asp:RequiredFieldValidator ControlToValidate="Municipioregister" InitialValue="" ErrorMessage="Selecciona un municipio." ForeColor="Red" Display="Dynamic" runat="server" />



<asp:TextBox ID="Passregister" runat="server" CssClass="Inputregister" TextMode="Password" placeholder="Contraseña" />
<asp:RequiredFieldValidator ControlToValidate="Passregister" ErrorMessage="La contraseña es obligatoria." ForeColor="Red" Display="Dynamic" runat="server" />
<asp:RegularExpressionValidator 
    ControlToValidate="Passregister"
    ValidationExpression="^.{7,}$"
    ErrorMessage="La contraseña debe tener al menos 7 caracteres."
    ForeColor="Red"
    Display="Dynamic"
    runat="server" />

        <asp:TextBox ID="Passrepregister" runat="server" CssClass="Inputregister" TextMode="Password" placeholder="Confirmar Contraseña" />
        <asp:RequiredFieldValidator ControlToValidate="Passrepregister" ErrorMessage="La confirmación es obligatoria." ForeColor="Red" Display="Dynamic" runat="server" />
        <asp:CompareValidator ControlToCompare="Passregister" ControlToValidate="Passrepregister" ErrorMessage="Las contraseñas no coinciden." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="Numerotarregister" runat="server" CssClass="Inputregister" placeholder="Número de tarjeta" MaxLength="50" />
        <asp:RequiredFieldValidator ControlToValidate="Numerotarregister" ErrorMessage="El número de tarjeta es obligatorio." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="CaducidadTarregister" runat="server" CssClass="Inputregister" placeholder="Caducidad Tarjeta (DD/MM/AAAA)" />
        <asp:RequiredFieldValidator ControlToValidate="CaducidadTarregister" ErrorMessage="La caducidad es obligatoria." ForeColor="Red" Display="Dynamic" runat="server" />
        <asp:RegularExpressionValidator ControlToValidate="CaducidadTarregister" ValidationExpression="^\d{2}/\d{2}/\d{4}$" ErrorMessage="Formato de fecha no válido (DD/MM/AAAA)." ForeColor="Red" Display="Dynamic" runat="server" />

        <asp:TextBox ID="Cvvregister" runat="server" CssClass="Inputregister" placeholder="CVV" MaxLength="3" />
        <asp:RequiredFieldValidator ControlToValidate="Cvvregister" ErrorMessage="El CVV es obligatorio." ForeColor="Red" Display="Dynamic" runat="server" />
        <asp:RegularExpressionValidator ControlToValidate="Cvvregister" ValidationExpression="^\d{3}$" ErrorMessage="El CVV debe tener 3 dígitos." ForeColor="Red" Display="Dynamic" runat="server" />




        <br />
        <asp:Label ID="Labelerror" runat="server" CssClass="mensajeError" ForeColor="Red" Visible="false" />
        <br />
        <asp:Button runat="server" Text="Crear cuenta" CssClass="botonregister" OnClick="EventoPaginaPrincipal" OnClientClick="return validateAndHighlight();" />
        <asp:Button runat="server" Text="Iniciar Sesión" CssClass="botonregister" OnClick="EventoInicioSesion" CausesValidation="false" />
    </div>
</asp:Content>
