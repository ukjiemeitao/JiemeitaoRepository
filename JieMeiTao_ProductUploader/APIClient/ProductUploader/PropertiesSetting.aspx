<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProductUploader.master" CodeBehind="PropertiesSetting.aspx.cs" Inherits="ProductUploader.PropertiesSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1>UK姐妹淘 Shop Style-淘宝属性匹配配置</h1>
    </div>


    <div class="centrecolumn">

        <table style="width: 100%">
            <tr>
                <td style="vertical-align: top;width:auto">
                    <div>
                        <label style="font-size: small; font-weight: 700">ShopStyle-分类匹配</label>
                    </div>
                   
                    <div style="margin-top: 10px; margin-bottom: 10px">
                        <asp:DropDownList ID="ddlSSCats" runat="server" OnSelectedIndexChanged="ddlSSCats_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        <asp:Button ID="btnSaveCat" runat="server" Text="保存淘宝分类" OnClick="btnSaveCat_Click"/>
                    </div>                    
                    <asp:TreeView ID="tvCats" runat="server" ExpandDepth="0" SelectedNodeStyle-BackColor="#ffff66" OnSelectedNodeChanged="tvCats_SelectedNodeChanged">
                    </asp:TreeView>
                </td>
                <td style="vertical-align: top; width: auto">
                    <div>
                        <label style="font-size: small; font-weight: 700">ShopStyle-淘宝分类属性匹配</label>
                    </div>
                    <div id="PropsMapping">
                        <asp:PlaceHolder ID="phPropsMapping" runat="server"></asp:PlaceHolder>
                    </div>                                      
                </td>
            </tr>
        </table>

    </div>     
</asp:Content>



