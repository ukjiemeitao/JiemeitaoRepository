<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProductUploader.master" CodeBehind="DownloadSetting.aspx.cs" Inherits="ProductUploader.DownloadSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        <style type="text/css" media="all">
        .auto-style6 {
            width: 99px;
            height: 37px;
        }

        .auto-style7 {
            width: 150px;
            height: 37px;
        }

        .auto-style8 {
            height: 37px;
        }

        .auto-style9 {
            width: 99px;
            height: 42px;
        }

        .auto-style10 {
            width: 150px;
            height: 42px;
        }

        .auto-style11 {
            height: 42px;
        }

        .auto-style12 {
            width: 99px;
            height: 39px;
        }

        .auto-style13 {
            width: 150px;
            height: 39px;
        }

        .auto-style14 {
            height: 39px;
        }

        .glist {
            float: left;
            width: 233px;
        }

            .glist a {
                text-decoration: none;
            }
    </style>
    </style>
    <link href="Content/css/modern.css" rel="stylesheet" />
    <link href="Content/css/modern-responsive.css" rel="stylesheet" />
    <link rel="stylesheet/less" type="text/css" href="Content/css/dialog.less">
    <script src="Content/js/jquery-1.9.0.min.js"></script>
    <script src="Content/js/dialog.js"></script>
    <style>
        .loading_box {
            position: absolute;
            top: 45%;
            left: 45%;
            z-index: 99999;
            padding: 10px 20px;
            color: #333;
            font-weight: bold;
            background: #feffc8;
            border: 1px solid #f1aa2d;
        }

        .loading_box span {
            background: url("Content/images/msg_loading.gif") no-repeat;
            padding-left: 22px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //处理提示框
            var handlebox = $('<div class="loading_box"><span>正在处理中..</span></div><div class="bg_overlay"></div>');
            $.fn.extend({
                handleboxopen: function () {
                    var $doc = $;
                    if (window.location != window.parent.location) {
                        //页面在iframe中
                        $doc = window.parent.$;
                    }
                    $doc("body").append(handlebox);
                    handlebox.show();
                },
                handleboxclose: function () {
                    handlebox.remove();
                }
            });
            /*下载商品*/
            $("#btnDownload").click(function () {
                var selectItems = new Array();
                $("input[name=goods]:checked").each(function () {
                    selectItems.push($(this).val());
                });
                $.fn.handleboxopen();
                $.ajax({
                    url: "DownloadSetting.aspx",
                    type: "POST",
                    data: {
                        Pid: selectItems.join(),
                        Catid: $("#MainContent_ddlCategories").val(),
                        ProSetName: $("#MainContent_txtProSetName").val(),
                        Fts: $("#MainContent_txtfts").val()
                    },
                    success: function (data) {
                        $.fn.handleboxclose();
                        var obj = eval("(" + data + ")");
                        if (obj.data == "0")
                            alert("未读取到缓存");
                        else if (obj.data == "-1")
                            alert(obj.msg);
                        else if (obj.data == "-2")
                            window.location.href = data.msg;
                        else
                            alert("下载成功");
                    }
                });
            });
        })
    </script>
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
                    <input id="btnDownload" type="button" name="name" value="下载选择商品" />
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
    <div id="goodsList">
        <asp:Repeater ID="re_goods" runat="server">
            <HeaderTemplate>
                <ol>
            </HeaderTemplate>
            <ItemTemplate>
                <li class="glist">
                    <a href="<%# DataBinder.Eval(Container.DataItem,"Item3") %>" target="_blank">
                        <img src="<%# DataBinder.Eval(Container.DataItem,"Item4") %>" title="<%# DataBinder.Eval(Container.DataItem,"Item2") %>" />
                    </a>
                    <input type="checkbox" name="goods" value=" <%# DataBinder.Eval(Container.DataItem,"Item1") %>" />
                </li>
            </ItemTemplate>
            <FooterTemplate></ol></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>



