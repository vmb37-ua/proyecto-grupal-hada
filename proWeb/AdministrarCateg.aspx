<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarCateg.aspx.cs" Inherits="ProWeb.AdministrarCateg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Source/Styles/EstiloAdministrar.css" />

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Panel ID="PanelCreacion" runat="server" CssClass="Panel">
        <asp:Label ID="LabelPanelOperacion" runat="server"></asp:Label>
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
        <h1 class="Titulo">Editar Categorias</h1>
        <div style="display: inline-block; text-align: center;">
            <span class="Etiqueta">Nombre</span>
            
            
            <asp:TextBox ID="TBNombreCat" runat="server" placeholder="Introduce un nombre" width="140px" CssClass="TextBox" AutoPostBack="true"></asp:TextBox>
        
            <ajaxToolkit:AutoCompleteExtender
                ID="AutoCompleteExtender1"
                runat="server"
                TargetControlID="TBNombreCat"
                ServiceMethod="ObtenerCategorias"
                MinimumPrefixLength="0"
                CompletionInterval="100"
                EnableCaching="true"
                CompletionSetCount="50"
                FirstRowSelected="false"
                CompletionListCssClass="autoCompleteList"
                CompletionListItemCssClass="autoCompleteListItem"
                CompletionListHighlightedItemCssClass="autoCompleteHighlightedListItem"
            />

            <asp:RequiredFieldValidator 
                ID="RFVNombre" 
                runat="server" 
                ControlToValidate="TBNombreCat" 
                ErrorMessage="Campo obligatorio" 
                ForeColor="Red" 
                Display="Dynamic"
                ValidationGroup="VGNombre" />

            <br />
            <br />
            <div style="height: 20px">
                <asp:Label ID="LabelNombre" runat="server" ClientIDMode="Static" />
            </div>
        </div>
        <br />
        <br />
        <asp:Button id="BotonCrearCat" Text="Crear" runat="server" CssClass="Boton" OnClick="Create" ValidationGroup="VGNombre"/>
        <asp:Button id="BotonEliminarCat" Text="Eliminar" runat="server" CssClass="Boton" OnClick="Delete" ValidationGroup="VGNombre"/>
        
        <ajaxToolkit:ConfirmButtonExtender 
            ID="ConfirmEliminarCat" 
            runat="server" 
            ConfirmText="¿Seguro que quieres eliminar la Categoria? No se podra revertir la accion." 
            TargetControlID="BotonEliminarCat" />
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
