<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cartera.aspx.cs" Inherits="ProWeb.Cartera" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mi Cartera</title>
    <link rel="stylesheet" href="Source/Styles/Cartera.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="ContenedorCartera">
            <h2>Mi Cartera</h2>
            <p>Saldo actual: 
                <asp:Label ID="DineroDisponible" runat="server" />
            </p>
        </div>
    </form>
</body>
</html>
