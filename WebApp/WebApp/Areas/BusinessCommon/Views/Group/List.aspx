<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage<BusinessCommon.Models.Group.ListModel>" %>

<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="editor-field">
        <div class="CommonColumn">
            <%:Html.AppLabelFor(m => m.GroupNo, Model.PageId)%>
            <%:Html.AppTextBoxFor(m => m.GroupNo, Model.PageId)%>
        </div>
        <div class="CommonColumn">
            <%:Html.AppLabelFor(m => m.GroupName, Model.PageId)%>
            <%:Html.AppTextBoxFor(m => m.GroupName, Model.PageId)%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
