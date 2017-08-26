<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BusinessLogic.AssetsBusiness.Models.AssetsPurchase.PurchaseModel>" %>
<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<%@ Import Namespace="WebCommon.Data" %>
<%:Html.AppBeginForm(Model.PageId, Model.FormId)%>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.AssetsName, Model.PageId)%>
    <%:Html.AppTextBoxFor(m => m.AssetsName, Model.PageId, "Common150")%>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.DepartmentId, Model.PageId)%>
    <%:Html.AppTreeDialogFor(m => m.DepartmentId, Model.PageId, Model.DepartmentUrl, Model.DepartmentDialogUrl, AppMember.AppText["DepartmentSelect"], TreeId.DepartmentTreeId, Model.DepartmentAddFavoritUrl, Model.DepartmentReplaceFavoritUrl, "Common150")%>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.StoreSiteId, Model.PageId)%>
    <%:Html.AppTreeDialogFor(m => m.StoreSiteId, Model.PageId, Model.StoreSiteUrl, Model.StoreSiteDialogUrl, AppMember.AppText["StoreSiteSelect"], TreeId.StoreSiteTreeId, Model.StoreSiteAddFavoritUrl, Model.StoreSiteReplaceFavoritUrl, "Common150")%>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.UsePeople, Model.PageId)%>
    <%if (AppMember.UsePeopleControlLevel == UsePeopleControlLevel.High)
      { %>
    <%:Html.AppDropDownListFor(m => m.UsePeople, Model.PageId, Url.Action("DropList", "User", new { Area = "BusinessCommon", filterExpression = "departmentId=" + DFT.SQ + DataConvert.ToString(Model.DepartmentId) + DFT.SQ }), "Common150")%>
    <%}
      else
      { %>
    <%:Html.AppAutoCompleteFor(m => m.UsePeople, Model.PageId, "Common150", Model.UserSource)%>
    <%}  %>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.Keeper, Model.PageId)%>
    <%if (AppMember.UsePeopleControlLevel == UsePeopleControlLevel.High)
      { %>
    <%:Html.AppDropDownListFor(m => m.Keeper, Model.PageId, Url.Action("DropList", "User", new { Area = "BusinessCommon", filterExpression = "departmentId=" + DFT.SQ + DataConvert.ToString(Model.DepartmentId) + DFT.SQ }), "Common150")%>
    <%}
      else
      { %>
    <%:Html.AppAutoCompleteFor(m => m.Keeper, Model.PageId, "Common150", Model.UserSource)%>
    <%}  %>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.AssetsValue, Model.PageId)%>
    <%:Html.AppTextBoxFor(m => m.AssetsValue, Model.PageId, "Common150")%>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.SupplierName, Model.PageId)%>
    <%:Html.AppTextBoxFor(m => m.SupplierName, Model.PageId, "Common150")%>
</div>
<div class="editor-field">
    <%:Html.AppLabelFor(m => m.Remark, Model.PageId)%>
    <%:Html.AppTextBoxFor(m => m.Remark, Model.PageId, "Common150")%>
</div>
<%:Html.AppHiddenFor(m => m.AssetsPurchaseDetailId, Model.PageId)%>
<%:Html.AppEndForm() %>
