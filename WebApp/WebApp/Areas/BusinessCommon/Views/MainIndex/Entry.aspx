<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Base.Master" Inherits="System.Web.Mvc.ViewPage<BusinessCommon.Models.MainIndex.EntryModel>" %>

<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.AppBeginForm(Model.PageId, Model.FormId)%>
    <div class="ui-widget">
        <div class="ui-widget-header ui-corner-all" style="margin: 0px; padding: 0px;">
            <p style="padding-left: 3px; margin: 4px">
                <%--<span class="ui-icon ui-icon-star" style="float: left; margin-right: .3em;"></span>--%>
                <strong>申请发起</strong>
            </p>
        </div>
    </div>
    <fieldset style="padding-bottom: 5px">
        <div class="editor-field">
            <div class="MainIndexColumn1">
                <%:Html.AppNormalButton(Model.PageId, "btnPurchaseApply", AppMember.AppText["BtnPurchaseApply"])%>
            </div>
            <div class="MainIndexColumn2">
                <%:Html.AppNormalButton(Model.PageId, "btnTransferApply", AppMember.AppText["BtnTransferApply"])%>
            </div>
            <div class="MainIndexColumn3">
                <%:Html.AppNormalButton(Model.PageId, "btnScrapApply", AppMember.AppText["BtnScrapApply"])%>
            </div>
        </div>
    </fieldset>
    <%:Html.AppHiddenFor(m => m.PageId, Model.PageId)%>
    <%:Html.AppHiddenFor(m => m.EntryGridId, Model.PageId)%>
    <%:Html.AppHiddenFor(m => m.FormId, Model.PageId)%>
    <%:Html.AppEndForm() %>
    <%--<div class="ui-widget">
        <div class="ui-state-default ui-corner-all" style="margin-top: 10px; padding: 0em;">
            <p style="color:Black">
                <span class="ui-icon ui-icon-star" style="float: left; margin-right: .3em;"></span>
                <strong>待审核单据</strong>
            </p>
        </div>
    </div>--%>
    <br />
    <%:Html.AppEntryGridFor(this.Url, Model.PageId, Model.EntryGridId, Url.Action("EntryGridData"), Model.EntryGridLayout, 400, false, 0, true, false, "btnSave", "MainIndex")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //#region 公共变量
            var pageId = '<%=Model.PageId %>';
            var gridId = '<%=Model.EntryGridId %>' + pageId;
            //#endregion 公共变量

            //#region 画面操作
            $('#btnPurchaseApply' + pageId).click(function () {
                var spageId = pageId + "91";
                var urlStr = '<%=Url.Action("Entry", "AssetsPurchase", new { Area = "AssetsBusiness"}) %>';
                var maintab = jQuery('#tabs', '#RightPane');
                var st = "#t" + spageId;
                if ($(st).html() != null) {
                    maintab.tabs('select', st);
                } else {
                    maintab.tabs('add', st, "采购申请");
                    //$(st,"#tabs").load(treedata.url);
                    $.ajax({
                        url: urlStr,
                        type: "GET",
                        dataType: "html",
                        data: { pageId: spageId, formMode: "new2", viewTitle: "采购申请" },
                        complete: function (req, err) {
                            $(st, "#tabs").append(req.responseText);
                        }
                    });
                }
            });

            $('#btnTransferApply' + pageId).click(function () {
                var spageId = pageId + "92";
                var urlStr = '<%=Url.Action("Entry", "AssetsTransfer", new { Area = "AssetsBusiness"}) %>';
                var maintab = jQuery('#tabs', '#RightPane');
                var st = "#t" + spageId;
                if ($(st).html() != null) {
                    maintab.tabs('select', st);
                } else {
                    maintab.tabs('add', st, "调拨申请");
                    //$(st,"#tabs").load(treedata.url);
                    $.ajax({
                        url: urlStr,
                        type: "GET",
                        dataType: "html",
                        data: { pageId: spageId, formMode: "new2", viewTitle: "调拨申请" },
                        complete: function (req, err) {
                            $(st, "#tabs").append(req.responseText);
                        }
                    });
                }
            });

            $('#btnScrapApply' + pageId).click(function () {
                var spageId = pageId + "93";
                var urlStr = '<%=Url.Action("Entry", "AssetsScrap", new { Area = "AssetsBusiness"}) %>';
                var maintab = jQuery('#tabs', '#RightPane');
                var st = "#t" + spageId;
                if ($(st).html() != null) {
                    maintab.tabs('select', st);
                } else {
                    maintab.tabs('add', st, "报废申请");
                    //$(st,"#tabs").load(treedata.url);
                    $.ajax({
                        url: urlStr,
                        type: "GET",
                        dataType: "html",
                        data: { pageId: spageId, formMode: "new2", viewTitle: "报废申请" },
                        complete: function (req, err) {
                            $(st, "#tabs").append(req.responseText);
                        }
                    });
                }
            });

            var hadInitBtn = 'false';
            var btnProcess = 'btnProcess' + gridId;
            var btnRefresh = 'btnRefresh' + gridId;
            loadGridCompleteMainIndex = function () {
                if (hadInitBtn == 'false') {

                    $('#t_' + gridId).append("<input id=" + btnProcess + " type='button' value='处理' style='height:20px;font-size:-1;line-height: 0.8;'/>");
                    $('#' + btnProcess, '#t_' + gridId).button({
                        text: true
                    });
                }

                $('#' + btnProcess, '#t_' + gridId).click(function () {
                    var id = jQuery('#' + gridId).jqGrid('getGridParam', 'selrow');
                    if (!id) {
                        AppMessage(pageId, '<%=AppMember.AppText["MessageTitle"]%>', '请选择一行！', 'warning', function () { });
                        return;
                    }
                    else {
                        var applyType = jQuery('#' + gridId).getCell(id, 'ApplyType');
                        var pk = jQuery('#' + gridId).getCell(id, 'RefId');
                        if (applyType == "采购申请") {
                            var spageId = "3010" + id;
                            var urlStr = '<%=Url.Action("Entry", "AssetsPurchase", new { Area = "AssetsBusiness"}) %>';
                            var maintab = jQuery('#tabs', '#RightPane');
                            var st = "#t" + spageId;
                            if ($(st).html() != null) {
                                maintab.tabs('select', st);
                            } else {
                                maintab.tabs('add', st, "采购审批");
                                //$(st,"#tabs").load(treedata.url);
                                $.ajax({
                                    url: urlStr,
                                    type: "GET",
                                    dataType: "html",
                                    data: { pageId: spageId, formMode: "approve", viewTitle: "采购审批", primaryKey: pk, isDirectCall: "true" },
                                    complete: function (req, err) {
                                        $(st, "#tabs").append(req.responseText);
                                    }
                                });
                            }
                        }
                        else if (applyType == "调拨申请") {
                            var spageId = "3015" + id;
                            var urlStr = '<%=Url.Action("Entry", "AssetsTransfer", new { Area = "AssetsBusiness"}) %>';
                            var maintab = jQuery('#tabs', '#RightPane');
                            var st = "#t" + spageId;
                            if ($(st).html() != null) {
                                maintab.tabs('select', st);
                            } else {
                                maintab.tabs('add', st, "调拨审批");
                                //$(st,"#tabs").load(treedata.url);
                                $.ajax({
                                    url: urlStr,
                                    type: "GET",
                                    dataType: "html",
                                    data: { pageId: spageId, formMode: "approve", viewTitle: "调拨审批", primaryKey: pk, isDirectCall: "true" },
                                    complete: function (req, err) {
                                        $(st, "#tabs").append(req.responseText);
                                    }
                                });
                            }
                        }
                        else if (applyType == "报废申请") {
                            var spageId = "3065" + id;
                            var urlStr = '<%=Url.Action("Entry", "AssetsScrap", new { Area = "AssetsBusiness"}) %>';
                            var maintab = jQuery('#tabs', '#RightPane');
                            var st = "#t" + spageId;
                            if ($(st).html() != null) {
                                maintab.tabs('select', st);
                            } else {
                                maintab.tabs('add', st, "报废审批");
                                //$(st,"#tabs").load(treedata.url);
                                $.ajax({
                                    url: urlStr,
                                    type: "GET",
                                    dataType: "html",
                                    data: { pageId: spageId, formMode: "approve", viewTitle: "报废审批", primaryKey: pk, isDirectCall: "true" },
                                    complete: function (req, err) {
                                        $(st, "#tabs").append(req.responseText);
                                    }
                                });
                            }
                        }
                    }
                });

                if (hadInitBtn == 'false') {

                    $('#t_' + gridId).append("<input id=" + btnRefresh + " type='button' value='刷新' style='height:20px;font-size:-1;line-height: 0.8;'/>");
                    $('#' + btnRefresh, '#t_' + gridId).button({
                        text: true
                    });
                }

                $('#' + btnRefresh, '#t_' + gridId).click(function () {
                    hadInitBtn = 'true';
                    $('#' + gridId).trigger('reloadGrid');
                });
            }

            //#endregion grid操作

            $(document).keydown(function (e) {
                //shift+p,购买申请快捷键
                if (e.shiftKey && e.which == 80) {
                    $('#btnPurchaseApply' + pageId).click();
                }
                //shift+s,报废申请快捷键
                else if (e.shiftKey && e.which == 83) {
                    $('#btnScrapApply' + pageId).click();
                }
                //shift+T,调拨申请快捷键
                else if (e.shiftKey && e.which == 84) {
                    $('#btnTransferApply' + pageId).click();
                }
            });


        });
    </script>
</asp:Content>
