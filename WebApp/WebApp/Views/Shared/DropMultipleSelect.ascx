﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseCommon.Models.DropMultipleSelectModel>" %>
<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="WebCommon.Data" %>
<script type="text/javascript">
    $(document).ready(function () {
        var pageId = '<%=Model.PageId%>';
        var treeId = '<%=Model.TreeId%>';
        $("#btnConfirm" + pageId).click(function (event) {
            var fieldIdObj = $("#FieldIdObj" + pageId).val();
            if (fieldIdObj.substring(0, 1) != "#") {
                fieldIdObj = "#" + fieldIdObj;
            }
            var valstring = '';
            var displaystring = '';
            var treeObj = $.fn.zTree.getZTreeObj(treeId + pageId);
            var nodes = treeObj.getCheckedNodes(true);
            $.each(nodes, function (n, vlaue) {
                var node = nodes[n];
                $.each(node, function (i, name) {
                    if (i == 'id') {
                        valstring = valstring + name + ',';
                    }
                    if (i == 'name') {
                        displaystring = displaystring + name + ',';
                    }
                });
            });
            if (fieldIdObj.substring(0, 9) != "#workFlow") {
                $(fieldIdObj + "Display").val(displaystring.substring(0, displaystring.length - 1));
                $(fieldIdObj).val(valstring.substring(0, valstring.length - 1));
            }
            else {
                $(fieldIdObj).val(displaystring.substring(0, displaystring.length - 1) + "[" + valstring.substring(0, valstring.length - 1) + "]");
            }

            setTimeout(function () { $(fieldIdObj).change(); }, 800);
            $(fieldIdObj + "DropDiv").hide();
        });
        $("#BtnClose" + pageId).click(function (event) {
            var fieldIdObj = $("#FieldIdObj" + pageId).val();
            if (fieldIdObj.substring(0, 1) != "#") {
                fieldIdObj = "#" + fieldIdObj;
            }
            $(fieldIdObj + "DropDiv").hide();
        });

   
    });
</script>
<div id="DivDropMultipleTree<%=Model.PageId %>">
    <%:Html.AppTreeViewFor(Model.PageId, Model.TreeId, Model.DataTree, Model.ShowCheckBox, Model.PkId)%>
</div>
<div style="float: right">
    <%:Html.AppNormalButton(Model.PageId, "btnConfirm", AppMember.AppText["BtnConfirm"])%>
    <%:Html.AppNormalButton(Model.PageId, "BtnClose", AppMember.AppText["BtnClose"])%>
</div>
<%:Html.AppHiddenFor(m => m.FieldIdObj, Model.PageId)%>
