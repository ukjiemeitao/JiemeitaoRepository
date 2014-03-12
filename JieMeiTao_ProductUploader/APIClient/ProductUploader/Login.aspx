<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProductUploader.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">@import "htdocs/css/master.css";
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="centre">
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">用户名：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">密码：</td>
                <td>
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="登陆" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
