<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseCommon.Models.TreeSelectModel>" %>
<%@ Import Namespace="BaseControl.HtmlHelpers" %>
<%@ Import Namespace="BaseCommon.Basic" %>
<%@ Import Namespace="WebCommon.Data" %>
<script type="text/javascript">
    $(document).ready(function () {
        var pageId = '<%=Model.PageId%>';
        var treeId = '<%=Model.TreeId%>';
        $("#SearchText" + pageId).keydown(function (event) {
            if (event.keyCode == 13) {
                var pySearch = $("#SearchText" + pageId).val();
                var urlStr = '<%=Model.SearchUrl%>';
                $.ajax({
                    type: 'POST',
                    url: urlStr,
                    data: { pageId: pageId, pySearch: pySearch },
                    success: function (jsonObj) {
                        if (jsonObj != "0") {
                            $("#DivDialogTree" + pageId + treeId).empty();
                            $("#DivDialogTree" + pageId + treeId).append(jsonObj);
                        } 
                    }
                });
            }
        });
    });
</script>
<div class="editor-field">
    <%:Html.AppTextBoxFor(m => m.SearchText, Model.PageId, "SearchTree")%>
</div>
<div id="DivDialogTree<%=Model.PageId %><%=Model.TreeId %>">
    <%:Html.AppTreeViewFor(Model.PageId, Model.TreeId, Model.DataTree, Model.ShowCheckBox, Model.SelectId)%>
</div>
