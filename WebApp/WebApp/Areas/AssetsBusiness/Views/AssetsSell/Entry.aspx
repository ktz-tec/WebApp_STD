﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ApproveEntry.Master"
    Inherits="System.Web.Mvc.ViewPage<BusinessLogic.AssetsBusiness.Models.AssetsSell.EntryModel>" %>

<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
    <div class="editor-field">
        <%:Html.AppLabelFor(m => m.AssetsSellNo, Model.PageId, "AssetsSellEntry")%>
        <%:Html.AppTextBoxFor(m => m.AssetsSellNo, Model.PageId, "NoAssetsSellEntry")%>
          <%:Html.AppNormalButton(Model.PageId, "btnAutoNo", "ui-icon-pencil", "autoNoButton")%>
        <%:Html.AppRequiredFlag()%>
        <%:Html.ValidationMessageFor(m => m.AssetsSellNo)%>
    </div>
    <div class="editor-field">
        <%:Html.AppLabelFor(m => m.AssetsSellName, Model.PageId, "AssetsSellEntry")%>
        <%:Html.AppTextBoxFor(m => m.AssetsSellName, Model.PageId, "AssetsSellEntry")%>
        <%:Html.AppRequiredFlag()%>
        <%:Html.ValidationMessageFor(m => m.AssetsSellName)%>
    </div>
    <div class="editor-field">
        <%:Html.AppLabelFor(m => m.SellDate, Model.PageId, "AssetsSellEntry")%>
        <%:Html.AppDatePickerFor(m => m.SellDate, Model.PageId, "AssetsSellEntry")%>
        <%:Html.ValidationMessageFor(m => m.SellDate)%>
    </div>
    <%:Html.AppHiddenFor(m => m.AssetsSellId, Model.PageId)%>
    <%:Html.AppHiddenFor(m=>m.EntryGridId,Model.PageId)%>
    </fieldset>
    <%:Html.AppEntryGridFor(this.Url, Model.PageId, Model.EntryGridId,  Url.Action("EntryGridData", new { formMode = Model.FormMode, primaryKey = Model.AssetsSellId }), Model.EntryGridLayout, 300, 0, "btnSave", "AssetsSell")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //#region 公共变量
            var pageId = '<%=Model.PageId %>';
            var gridId = '<%=Model.EntryGridId %>' + pageId;
            //#endregion 公共变量

            $('#btnAutoNo' + pageId).click(function () {
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("GetAutoNo") %>',
                    datatype: "text",
                    success: function (data) {
                        $('#AssetsSellNo' + pageId).val(data);
                    }
                });
                return false;
            });

            //#region grid操作
            onCellEditAssetsSell = function (id) {
                $('#' + gridId + ' :input').live("change", function () {
                    $('#' + 'btnSave' + pageId).attr('disabled', false);
                    $('#' + 'btnApproveReturn' + pageId).attr('disabled', false);
                })
            }
            $('#btnDelete' + pageId).click(function () {
                var id = jQuery('#' + gridId).jqGrid('getGridParam', 'selrow');
                $('#' + gridId).jqGrid('delRowData', id);
            });
            $('#btnAdd' + pageId).click(function () {
                var spageId = pageId + "s";
                $.ajax({
                    type: "POST",
                    url: '<%:Model.SelectUrl %>',
                    data: { pageId: spageId, selectMode: "AssetsSelect" },
                    datatype: "html",
                    success: function (data) {
                        $("#SelectDialog" + pageId).html(data).dialog({
                            title: '<%=AppMember.AppText["AssetsSelect"]%>',
                            height: 470,
                            width: 800,
                            modal: true,
                            resizable: true,
                            buttons: {
                                '<%=AppMember.AppText["BtnQuery"]%>': function () {
                                    var obj = $('#SelectForm' + spageId).serializeObject();
                                    var formvar = JSON.stringify(obj);
                                    jQuery('#list' + spageId).jqGrid('setGridParam', { postData: { formVar: formvar} });
                                    jQuery('#list' + spageId).trigger('reloadGrid');
                                },
                                '<%=AppMember.AppText["BtnConfirm"]%>': function () {
                                    var arrRow = jQuery('#list' + spageId).jqGrid('getGridParam', 'selarrrow');
                                    var curGridData = $('#' + gridId).getDataIDs();
                                    if (arrRow.length > 0) {
                                        var rowdata;
                                        for (var i = 0; i < arrRow.length; i++) {
                                            rowdata = jQuery('#list' + spageId).getRowData(arrRow[i]);
                                            var pk = rowdata["assetsId"];
                                            var hasPk = false;
                                           
                                            for (j = 0; j < curGridData.length; j++) {
                                                if (pk == curGridData[j])
                                                    hasPk = true;
                                            }
                                            if (hasPk == false && pk) {
                                                var dataRow = { AssetsId: rowdata["assetsId"], AssetsNo: rowdata["assetsNo"], AssetsName: rowdata["assetsName"], AssetsTypeId: rowdata["assetsTypeId"], AssetsClassId: rowdata["assetsClassId"], ScrapTypeId: "", ScrapTypeName: "", Remark: "" };
                                                $('#' + gridId).jqGrid('addRowData', pk, dataRow);
                                                $('#' + gridId).jqGrid('editRow', pk, true);
                                            }
                                        }
                                    }
                                    $(this).dialog("close");
                                },
                                '<%=AppMember.AppText["BtnCancel"]%>': function () {
                                    $(this).dialog("close");
                                }
                            },
                            close: function () {
                                //allFields.val("").removeClass("ui-state-error");
                            }
                        });
                    }
                });
            });
            //#endregion grid操作
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TabsLiContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabsDivContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ButtonContent" runat="server">
    <% if (Model.FormMode != "approve" && !Model.FormMode.Contains("view"))
       { %>
    <%:Html.AppNormalButton(Model.PageId, "btnAdd", AppMember.AppText["BtnAdd"])%>
    <%:Html.AppNormalButton(Model.PageId, "btnDelete", AppMember.AppText["BtnDelete"])%>
    <% } %>
</asp:Content>
