<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TranslateProductDetail.aspx.cs" Inherits="ProductUploader.TranslateProductDetail" %>
<%@ PreviousPageType VirtualPath="~/UploadSetting.aspx" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<!DOCTYPE html>
<!--script>
    window.onunload = refreshParent;
    function refreshParent() {
        window.opener.location.reload();
    }
</!--script-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
        <style type="text/css" media="all">@import "htdocs/css/master.css";
        .auto-style9 {
            width: 99px;
            
        }
        .auto-style10 {
            width: 150px;
            height: 42px;
        }
        .auto-style14 {
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style9">产品ID：<asp:Label ID="lbID" runat="server"></asp:Label>
                </td>
                <td >
                    <asp:HyperLink ID="lbUrl" Target="_blank" runat="server">ShopStyle商品链接</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td with="200" class="auto-style9">产品标题:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbTitle" runat="server" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">产品描述</td>
                <td>选择商品标题短语：<asp:DropDownList ID="ddlTitlePrefixs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTitlePrefixs_SelectedIndexChanged">
                    </asp:DropDownList>
&nbsp;选择宝贝模板： 
                    <asp:DropDownList ID="ddlModels" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModels_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="auto-style10">
                    <asp:Label ID="lbDescription" runat="server"></asp:Label>
                </td>
                <td height="500">

                    <FCKeditorV2:FCKeditor Height="500" ID="FCKeditor1" runat="server">
                    </FCKeditorV2:FCKeditor>

                </td>
            </tr>
            <tr>
                <td colspan="2"  class="auto-style14">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="提交翻译" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
