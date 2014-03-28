<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProductUploader.master" CodeBehind="DownloadSetting.aspx.cs" Inherits="ProductUploader.DownloadSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" media="all">
        .auto-style6 { width: 99px; height: 37px; }
        .auto-style7 { width: 150px; height: 37px; }
        .auto-style8 { height: 37px; }
        .auto-style9 { width: 99px; height: 42px; }
        .auto-style10 { width: 150px; height: 42px; }
        .auto-style11 { height: 42px; }
        .auto-style12 { width: 99px; height: 39px; }
        .auto-style13 { width: 150px; height: 39px; }
        .auto-style14 { height: 39px; }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div id="header">
        <h1>UK姐妹淘 Shop Style 商品下载配置</h1>
    </div>
    <div id="rightcolumn">
        <table style="width: 100%; height: 150px;">
            <tr>
                <td class="auto-style9">价格范围</td>
                <td class="auto-style10">
                    <asp:ListBox ID="lsbprice" runat="server" SelectionMode="Multiple" Width="150px"></asp:ListBox>
                </td>
                <td class="auto-style11"></td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style12">&nbsp;</td>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">打折范围</td>
                <td class="auto-style13">
                    <asp:ListBox ID="lsbdiscount" runat="server" SelectionMode="Multiple" Width="150px"></asp:ListBox>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12"></td>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12"></td>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">尺寸</td>
                <td class="auto-style13">
                    <asp:ListBox ID="lsbsize" runat="server" SelectionMode="Multiple" Width="150px"></asp:ListBox>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">商品组名称</td>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12" colspan="2">
                    <asp:TextBox ID="txtProSetName" runat="server" Width="241px"></asp:TextBox>
                </td>

                <td class="auto-style14">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <asp:Button ID="btndownload" runat="server" Text="开始下载" OnClick="btndownload_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="leftcolumn">
        <table style="width: 100%; height: 150px;">
            <tr>
                <td class="auto-style9">关键词</td>
                <td class="auto-style10">
                    <asp:TextBox ID="txtfts" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style11"></td>
            </tr>
            <tr>
                <td class="auto-style6">品牌</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="ddlBrands" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style12">分类</td>
                <td class="auto-style13">
                    <asp:DropDownList ID="ddlCategories" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">经销商</td>
                <td class="auto-style13">
                    <asp:DropDownList ID="ddlRetailers" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">颜色</td>
                <td class="auto-style13">
                    <asp:DropDownList ID="ddlColors" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12">打折时间</td>
                <td class="auto-style13">
                    <asp:TextBox ID="txtpdd" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="txtpdd_CalendarExtender" runat="server" TargetControlID="txtpdd" Format="dd-MM-yyyy">
                    </asp:CalendarExtender>
                </td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12"></td>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style14"></td>
            </tr>
            <tr>
                <td class="auto-style12"></td>
                <td class="auto-style13">
                    <asp:Button ID="btngetpriceanddiscount" runat="server" Text="获取价格和折扣范围" OnClick="btngetpriceanddiscount_Click" />
                </td>
                <td class="auto-style14"></td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Repeater ID="re_goods" runat="server">
            <HeaderTemplate>
                <ol>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href="JavaScript:void(0)">
                    <%# DataBinder.Eval(Container.DataItem,"key") %></a>
                    <a href="JavaScript:void(0)">
                        <%# DataBinder.Eval(Container.DataItem,"Value.Key") %></a>
                    <a href="JavaScript:void(0)">
                        <%# DataBinder.Eval(Container.DataItem,"Value.Value") %></a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ol></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>



