using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BaseCommon.Data;
using System.Data;
using BaseCommon.Basic;
using WebCommon.Init;
using BusinessCommon.Models.MainIndex;
using System.Collections;
using WebCommon.HttpBase;
using BusinessCommon.Repositorys;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class MainIndexController : BaseController
    {
        public MainIndexController()
        {
            ControllerName = "MainIndex";
        }

        public ActionResult Entry(string pageId, string viewTitle)
        {
            EntryModel model = new EntryModel();
            model.PageId = pageId;
            model.ViewTitle = viewTitle;
            model.FormId = "EntryForm";
            model.CssMergeUrl = Url.Action("MergeCss", "Tools", new { Area = "BusinessCommon" });
            model.EntryGridId = "EntryGrid";
            model.EntryGridLayout = EntryGridLayout();
            return View(model);

        }

        public ActionResult MergeCss()
        {
            try
            {
                FileCombine fc = new FileCombine();
                FilesAccess fa = new FilesAccess();
                ArrayList filelist = fa.GetAllFileName(Server.MapPath("~/Content/css/PageCss"));
                fc.CombineFile(filelist, Server.MapPath("~/Content/css/pagecss.css"));
                return Content("1", "text/html");
            }
            catch (Exception )
            {
                return Content("0", "text/html");
            }
        }


        public ActionResult DownloadPrintTools(string pageId, string primaryKey)
        {
            string fileName = Server.MapPath("~/Content/uploads/sqlite/" + "PrintKit.zip");
            return File(fileName, "text/plain", "PrintKit.zip");
        }


        protected GridLayout EntryGridLayout()
        {
            if (HttpContext.Cache["MainIndex_ApproveGridLayout"] == null)
            {
                CacheInit.CreateGridLayoutCache(HttpContext, "BusinessCommon", "MainIndex_Approve");
            }
            return (GridLayout)HttpContext.Cache["MainIndex_ApproveGridLayout"];
        }

        [HttpPost]
        public JsonResult EntryGridData()
        {
            UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
            Dictionary<string, object> paras = new Dictionary<string, object>();
            if (!paras.ContainsKey("approver"))
                paras.Add("approver", sysUser.UserId);
            MainIndexRepository mainRep = new MainIndexRepository();
            DataTable dt = mainRep.GetEntryGridDataTable(paras);
            var rows = DataTable2Object.Data(dt, EntryGridLayout().GridLayouts);
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = new { page = 1, total = rows.Length, rows = rows };
            return result;
        }


    }
}
