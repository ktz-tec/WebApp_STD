﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BaseCommon.Data;
using System.Data;
using BaseCommon.Basic;
using WebCommon.Init;
using BusinessCommon.Models.Tools;
using System.Collections;
using WebCommon.HttpBase;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class ToolsController : BaseController
    {
        public ToolsController()
        {
            ControllerName = "Tools";
        }

        [AppAuthorize]
        public ActionResult Entry(string pageId, string viewTitle)
        {
            try
            {
                EntryModel model = new EntryModel();
                model.PageId = pageId;
                model.ViewTitle = viewTitle;
                model.FormId = "EntryForm";
                model.CssMergeUrl = Url.Action("MergeCss", "Tools", new { Area = "BusinessCommon" });
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "ToolsController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
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
            try
            {
                string fileName = Server.MapPath("~/Content/uploads/sqlite/" + "PrintKit.zip");
                return File(fileName, "text/plain", "PrintKit.zip");
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "ToolsController.DownloadPrintTools", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }


    }
}
