<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ApproveList.Master"
    Inherits="System.Web.Mvc.ViewPage<BusinessLogic.AssetsBusiness.Models.AssetsTransfer.ListModel>" %>

<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="editor-field">
        <div class="AssetsTransferListColumn1">
            <%:Html.AppLabelFor(m => m.AssetsTransferNo, Model.PageId, "AssetsTransferList")%>
            <%:Html.AppTextBoxFor(m => m.AssetsTransferNo, Model.PageId, "AssetsTransferList")%>
        </div>
        <div class="AssetsTransferListColumn2">
            <%:Html.AppLabelFor(m => m.TransferDate1, Model.PageId, "AssetsTransferList")%>
            <%:Html.AppDatePickerFor(m => m.TransferDate1, Model.PageId, "AssetsTransferList")%>
        </div>
        <div class="AssetsTransferListColumn3">
            <%:Html.AppLabelFor(m => m.TransferDate2, Model.PageId, "AssetsTransferList")%>
            <%:Html.AppDatePickerFor(m => m.TransferDate2, Model.PageId, "AssetsTransferList")%>
        </div>
    </div>
    <div class="editor-field">
        <div class="AssetsTransferListColumn1">
            <%:Html.AppLabelFor(m => m.ApproveState, Model.PageId, "AssetsTransferList")%>
            <%:Html.AppDropDownListFor(m => m.ApproveState, Model.PageId, Url.Action("DropList", "CodeTable", new { Area = "", filter = "ApproveState!P|A" }), "AssetsTransferList")%>
        </div>
          <%if (Model.ListMode == "approve" || Model.ListMode == "reapply")
          { %>
        <div class="AssetsTransferListColumn2">
            <%:Html.AppLabelFor(m => m.QueryAllApproveRecord, Model.PageId, "AssetsTransferList")%>
            <%:Html.AppCheckBoxFor(m => m.QueryAllApproveRecord, Model.PageId, "AssetsTransferList")%>
        </div>
           <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //#region 公共变量
            var pageId = '<%=Model.PageId %>';
            var gridId = 'list' + pageId;
            //#endregion 公共变量

            var btnapprove = 'approve' + gridId;
            $('#QueryAllApproveRecord' + pageId).click(function () {
                if ($("#QueryAllApproveRecord" + pageId).attr("checked")) {
                    $('#' + btnapprove).hide();
                }
                else {
                    $('#' + btnapprove).show();
                }
            });

        });
    </script>
</asp:Content>
