<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ApproveList.Master"
    Inherits="System.Web.Mvc.ViewPage<BusinessLogic.AssetsBusiness.Models.AssetsScrap.ListModel>" %>

<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="editor-field">
        <div class="AssetsScrapListColumn1">
            <%:Html.AppLabelFor(m => m.AssetsScrapNo, Model.PageId, "AssetsScrapList")%>
            <%:Html.AppTextBoxFor(m => m.AssetsScrapNo, Model.PageId, "AssetsScrapList")%>
        </div>
        <div class="AssetsScrapListColumn2">
            <%:Html.AppLabelFor(m => m.ScrapDate1, Model.PageId, "AssetsScrapList")%>
            <%:Html.AppDatePickerFor(m => m.ScrapDate1, Model.PageId, "AssetsScrapList")%>
        </div>
        <div class="AssetsScrapListColumn3">
            <%:Html.AppLabelFor(m => m.ScrapDate2, Model.PageId, "AssetsScrapList")%>
            <%:Html.AppDatePickerFor(m => m.ScrapDate2, Model.PageId, "AssetsScrapList")%>
        </div>
    </div>
     <div class="editor-field">
        <div class="AssetsScrapListColumn1">
            <%:Html.AppLabelFor(m => m.ApproveState, Model.PageId, "AssetsScrapList")%>
            <%:Html.AppDropDownListFor(m => m.ApproveState, Model.PageId, Url.Action("DropList", "CodeTable", new { Area = "", filter = "ApproveState!P|A" }), "AssetsScrapList")%>
        </div>
           <%if (Model.ListMode == "approve" || Model.ListMode == "reapply")
          { %>
         <div class="AssetsScrapListColumn2">
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
