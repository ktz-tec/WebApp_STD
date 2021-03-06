﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BaseCommon.Data;
using WebCommon.Init;
using BaseCommon.Basic;
using System.Data;
using BusinessLogic.Report.Repositorys;
using System.Text;
using WebCommon.HttpBase;
using BusinessCommon.Repositorys;
using BusinessCommon.Models.SystemLog;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class SystemLogController : QueryController
    {

        public SystemLogController()
        {
            ControllerName = "SystemLog";
            Repository = new SystemLogRepository();
        }

        [AppAuthorize]
        public ActionResult Entry(string pageId, string formMode, string viewTitle)
        {
            try
            {
                EntryModel model = new EntryModel();
                model.EntryGridId = "EntryGrid";
                SetParentEntryModel(pageId, viewTitle, formMode, model);
                SetThisModel(model);
                model.LogDate1 = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                model.LogDate2 = DateTime.Today.ToString("yyyy-MM-dd");
                model.LogType = "Info";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "SystemLogController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        private void SetThisModel(EntryModel model)
        {
            model.GridHeight = 380;
            
        }

    }
}
