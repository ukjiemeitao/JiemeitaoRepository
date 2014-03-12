<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProductUploader.master" CodeBehind="UploadSetting.aspx.cs" Inherits="ProductUploader.UploadSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css" media="all">
         .auto-style1 {
             width: 202px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
     <div id="header">
            <h1>UK姐妹淘 Shop Style 商品上传配置</h1>
        </div>
        <div id="upperrow">
            <h4>初始化本地淘宝数据</h4>
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style1"> <asp:Button ID="btnInitTBItemCat" runat="server" Text="初始化淘宝分类" OnClick="btnInitTBItemCat_Click" /></td>
                    <td><asp:Button ID="btnInitTBItemProp" runat="server" Text="初始化淘宝分类属性" OnClick="btnInitTBItemProp_Click" /></td>
                    <td><asp:Button ID="btnInitTBItemPropValue" runat="server" Text="初始化淘宝分类属性值" OnClick="btnInitTBItemPropValue_Click" /></td>
                </tr>
               
            </table>

        </div>
       
              
        <div id="filterrow">
            <table style="width:100%;height:100%">
                <tr>
                    <td>下载日期</td>
                    <td>
                        <asp:TextBox ID="txtDownloadDate" runat="server"  OnTextChanged="txtDownloadDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDownloadDate_CalendarExtender" runat="server" TargetControlID="txtDownloadDate" Format="dd-MM-yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>产品组</td>
                    <td>
                        <asp:DropDownList ID="ddlProductSet" runat="server" Width="155px" OnSelectedIndexChanged="ddlProductSet_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        
                    </td>
                </tr>
               <tr>
                    <td>商品状态</td>
                    <td>
                        <asp:DropDownList ID="ddlProdctState" runat="server" Width="155px" AutoPostBack="True" OnSelectedIndexChanged="ddlProdctState_SelectedIndexChanged">
                            <asp:ListItem Value="NA">--全部--</asp:ListItem>
                            <asp:ListItem Value="True">已翻译</asp:ListItem>
                            <asp:ListItem Value="False">未翻译</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="更新商品列表" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="上传商品" OnClick="btnUpload_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="centrecolumn">
            <asp:GridView ID="gridProducts" runat="server" Width="800px" OnPageIndexChanging="gridProducts_PageIndexChanging" AutoGenerateSelectButton="True" OnSelectedIndexChanging="gridProducts_SelectedIndexChanged" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="gridProducts_RowDeleting" OnRowDataBound="gridProducts_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="名称" />
                    <asp:BoundField DataField="Brand" HeaderText="品牌" />
                    <asp:BoundField DataField="Retailer" HeaderText="经销商" />
                    <asp:BoundField DataField="Price" HeaderText="价格" />
                    <asp:BoundField DataField="Sale" HeaderText="折扣价" />
                    <asp:BoundField DataField="ProductSet" HeaderText="组名称" />
                    <asp:CheckBoxField DataField="IsTranslated" HeaderText="已翻译" />
                    <asp:ImageField DataImageUrlField="ProductImage" HeaderText="缩略图">
                    </asp:ImageField>
                    <asp:CommandField CancelText="取消" DeleteText="删除" EditText="编辑" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" BorderStyle="None" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
</asp:Content>


