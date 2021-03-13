<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addition.aspx.cs" Inherits="AspWithNUnit.Addition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1">
                <tr>
                    <td>Num1</td>
                    <td>
                        <asp:TextBox ID="txtnum1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Num2</td>
                    <td><asp:TextBox ID="txtNum2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /></td>
                    
                </tr>
                <tr>
                    <td>Addition</td>
                    <td>
                        <asp:Label ID="lblAdd" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
