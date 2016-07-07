<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="WebApplication1.Customers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Oracle Transactions example</h3>
        <br /><br />
        <asp:Label ID="Label1" runat="server" Text="First Name" CssClass="etqs"></asp:Label>
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="edits"></asp:TextBox>
        <br /><br />
        <asp:Label ID="Label2" runat="server" Text="Last Name" CssClass="etqs"></asp:Label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="edits"></asp:TextBox>
        <br /><br />
        <asp:CheckBoxList ID="clbGenres" runat="server">
        </asp:CheckBoxList>
    </div>
    <br />
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Insert" 
            onclick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
