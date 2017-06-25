﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/QueryEntry.Master"
    Inherits="System.Web.Mvc.ViewPage<BusinessLogic.Report.Models.AssetsDepreciationReport.EntryModel>" %>

<%@ Import Namespace="WebApp.BaseWeb.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="BaseCommon.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="editor-field">
        <div class="fieldwidth1">
            <%:Html.AppLabelFor(m => m.AssetsNo, Model.PageId, "AssetsDepreciationReport")%>
            <%:Html.AppTextBoxFor(m => m.AssetsNo, Model.PageId, "AssetsDepreciationReport")%>
        </div>
        <div class="fieldwidth2">
            <%:Html.AppLabelFor(m => m.AssetsName, Model.PageId, "AssetsDepreciationReport")%>
            <%:Html.AppTextBoxFor(m => m.AssetsName, Model.PageId, "AssetsDepreciationReport")%>
        </div>
        <%:Html.ValidationMessageFor(m => m.AssetsNo)%>
        <%:Html.ValidationMessageFor(m => m.AssetsName)%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HiddenContent" runat="server">
<%:Html.AppHiddenFor(m => m.EntryGridId, Model.PageId)%>
</asp:Content>
