<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ApuestaUsuario.aspx.cs" Inherits="ProWeb.ApuestaUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/ApuestaUsuario.css" />
    
    <div id="ContenedorApuestaUsuario">
      <div id="Contenedor1">
        <!-- Cabecera de apuesta -->
        <div class="ResumenApuesta">
            <h1>CONFIRMAR APUESTA</h1>
            <div class="DetallePartido">
                <asp:Label ID="lblEquipoLocal" runat="server" Text="FC Barcelona" CssClass="Equipo"></asp:Label>
                <span class="VS">VS</span>
                <asp:Label ID="lblEquipoVisitante" runat="server" Text="Real Madrid" CssClass="Equipo"></asp:Label>
            </div>
        </div>

        <!-- Selector de apuesta -->    
        <div class="SelectorApuesta">
            <h2>SELECCIONA TU PREDICCIÓN</h2>
            <div class="OpcionesApuesta">
                <asp:RadioButtonList ID="rblOpcionesApuesta" runat="server" CssClass="RadioButtonList" AutoPostBack="true" OnSelectedIndexChanged="rblOpcionesApuesta_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="Equipo1" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="X" Text="Empate"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Equipo2"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            
            <!-- Sección Cuota dinámica -->
            <div class="CuotaContainer">
                <span class="CuotaLabel">CUOTA:</span>
                <asp:Label ID="lblCuotaActual" runat="server" Text="2.10" CssClass="Cuota"></asp:Label>
            </div>
        </div>
      </div>

      <div id="Contenedor2">
        <!-- Sección de pago -->
        <div class="SeccionPago">
            <div class="CampoFormulario">
                <label for="txtCantidad">CANTIDAD (€):</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="InputApuesta" placeholder="Ej: 20.00" TextMode="Number" step="1" AutoPostBack="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad" ErrorMessage="*Campo obligatorio" CssClass="Validador"></asp:RequiredFieldValidator>
            </div>

            <div class="InfoFinanciera">
                <div class="GananciaPotencial">
                    <span>GANANCIA POTENCIAL:</span>
                    <asp:Label ID="lblGananciaPotencial" runat="server" Text="0.00 €" CssClass="Destacado"></asp:Label>
                </div>
                <div class="SaldoDisponible">
                    <span>SALDO DISPONIBLE:</span>
                    <asp:Label ID="lblSaldo" runat="server" Text="100.00 €" CssClass="Saldo"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Sección Botones -->
        <div class="BotonesAccion">
            <asp:Button ID="btnApostar" runat="server" Text="APOSTAR AHORA" CssClass="BotonApostar" OnClick="btnApostar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" CssClass="BotonCancelar" OnClick="btnCancelar_Click" />
        </div>

        <!-- Sección Confirmación -->
        <asp:Panel ID="pnlConfirmacion" runat="server" CssClass="PanelConfirmacion" Visible="false">
            <asp:Label ID="lblMensajeExito" runat="server" Text="¡Apuesta realizada con éxito!" CssClass="MensajeExito"></asp:Label>
        </asp:Panel>
      </div>
    </div>
</asp:Content>