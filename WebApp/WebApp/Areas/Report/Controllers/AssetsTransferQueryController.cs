using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BusinessLogic.Report.Models.AssetsTransferQuery;
using BaseCommon.Data;
using WebCommon.Init;
using BaseCommon.Basic;
using System.Data;
using BusinessLogic.Report.Repositorys;
using System.Text;
using WebCommon.HttpBase;

namespace WebApp.Areas.Report.Controllers
{
    public class AssetsTransferQueryController : QueryController
    {

        public AssetsTransferQueryController()
        {
            ControllerName = "AssetsTransferQuery";
            Repository = new AssetsTransferQueryRepository();
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
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsTransferQueryController.Entry", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        private void SetThisModel(EntryModel model)
        {
            model.GridHeight = 370;
        }
    }
}
