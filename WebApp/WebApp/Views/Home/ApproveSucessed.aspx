<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ApproveSucessed</title>
     <script type="text/javascript">
         $(document).ready(function () {
             //#region 公共变量 主页的刷新表格按钮
             var pageId = '001';
             var gridId = 'EntryGrid' + pageId;
             var btnRefresh = 'btnRefresh' + gridId;
             //#endregion 公共变量

             //审批完后，刷新主页的审批列表。
             $('#' + btnRefresh, '#t_' + gridId).click();
         });
    </script>
</head>
<body>
    <div>
     <h2>操作成功!</h2>
    </div>
</body>
</html>
