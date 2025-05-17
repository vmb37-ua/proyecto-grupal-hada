<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarRoles.aspx.cs" Inherits="ProWeb.AdministrarRoles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/EstiloAdministrar.css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Panel ID="PanelCreacion" runat="server" CssClass="Panel">
        <div>Operacion completada</div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender 
        ID="MPECreacion" 
        runat="server" 
        TargetControlID="BotonLanzadorPopup"
        PopupControlID="PanelCreacion"
        DropShadow="true"
    />

    <asp:Button ID="BotonLanzadorPopup" runat="server" style="display:none;" />
    <div class="Holder">
        <h1 class="Titulo">Editar Roles</h1>
        <div style="display: inline-block; text-align: left;">
            <span class="Etiqueta">Id</span>
            
            <asp:TextBox ID="TBIdRol" runat="server" placeholder="Introduce un id" width="140px" CssClass="TextBox" OnTextChanged="IdEntrante" AutoPostBack="true"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="RequiredFieldValidatorId" 
            runat="server" 
            ControlToValidate="TBIdRol" 
            ErrorMessage="Campo obligatorio" 
            ForeColor="Red" 
            Display="Dynamic"
            ValidationGroup="VGId" />
            <asp:Label ID="LabelId" runat="server" ClientIDMode="Static" />
            <asp:Button id="BotonDe" Text=">" Visible="false" runat="server" CssClass="BotonDe" OnClick="ReadNext" Style="float: right;"/> 
            <asp:Button id="BotonBuscarRol" Text="Buscar" Visible="false" runat="server" CssClass="BotonBuscar" OnClick="Read" Style="float: right; margin-left: 5px; margin-right: 5px;"/>
            <asp:Button id="BotonIz" Text="<" Visible="false" runat="server" CssClass="BotonIz" OnClick="ReadPrev" Style="float: right;"/>
            <br />
            <br />
            <span class="Etiqueta">Nombre</span>
            <asp:TextBox ID="TBNombreRol" runat="server" placeholder="Introduce un nombre" width=140px CssClass="TextBox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator 
            ID="RequiredFieldValidatorNombre" 
            runat="server" 
            ControlToValidate="TBNombreRol" 
            ErrorMessage="Campo obligatorio" 
            ForeColor="Red" 
            Display="Dynamic" 
            ValidationGroup="VGNombre" />
            <asp:Label ID="LabelNombre" runat="server" ClientIDMode="Static" />
            <br />
            <br />
            <span class="Etiqueta">Descripción</span>
            <asp:TextBox ID="TBDescipcionRol" TextMode="MultiLine"  Columns="72" Rows="4" runat="server" placeholder="Introduce una breve descripcion del rol" CssClass="TextBox"></asp:TextBox>
        </div>
        <br />
        <br />
        <asp:Button id="BotonCrearRol" Text="Crear" runat="server" CssClass="Boton" OnClick="Create" ValidationGroup="VGNombre"/>
        <asp:Button id="BotonActualizarRol" Text="Actualizar" runat="server" CssClass="Boton" OnClick="Update" ValidationGroup="VGNombre"/>
        <asp:Button id="BotonEliminarRol" Text="Eliminar" runat="server" CssClass="Boton" OnClick="Delete" ValidationGroup="VGId"/>
        <hr />
        <script type="text/javascript">
            function cerrarPopup() {
                setTimeout(function () {
                    $find('<%= MPECreacion.ClientID %>').hide();
                }, 2000);
            }
        </script>
    </div>
</asp:Content>
