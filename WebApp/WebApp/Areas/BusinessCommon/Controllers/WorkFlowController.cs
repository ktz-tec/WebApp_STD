using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseCommon.Data;
using System.Data;
using WebCommon.Common;
using BaseCommon.Basic;
using System.Web.Caching;
using WebCommon.Init;
using BusinessCommon.Models.WorkFlow;
using BusinessCommon.Repositorys;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;
using BaseCommon.Models;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class WorkFlowController : MasterController
    {
        WorkFlowRepository Repository;
        public WorkFlowController()
        {
            ControllerName = "WorkFlow";
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;
        }

        protected override IMasterFactory CreateRepository()
        {
            Repository = new WorkFlowRepository();
            return new MasterRepositoryFactory<WorkFlowRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                model.GridPkField = "approveTable";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "WorkFlowController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        public ActionResult Entry(string pageId, string primaryKey, string formMode, string viewTitle)
        {
            try
            {
                ClearClientPageCache(Response);
                EntryModel model = new EntryModel();
                //model.PageFlag = "WorkFlow";
                Repository.SetModel(primaryKey, formMode, model);
                SetParentEntryModel(pageId, formMode, viewTitle, model);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "WorkFlowController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        [HttpPost]
        public ActionResult Entry(EntryModel model)
        {
            try
            {
                //if (CheckModelIsValid(model))
                //{
                //    Update(EntryRepository, model, model.FormMode, model.ApproveTable, model.ViewTitle);
                //}
                //return View(model);    
                if (Update(Repository, model, model.ApproveTable) == 1)
                {
                    if (model.FormMode == "new")
                        return View(model);
                    else
                        return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle });
                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "WorkFlowController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        protected override bool CheckSelfBeforeSave(EntryViewModel model)
        {
            var myModel = model as EntryModel;
            if (myModel.FormMode != "new")
            {
                if (Repository.HasApprovingFlow(myModel))
                {
                    model.HasError = "true";
                    model.Message = AppMember.AppText["WorkflowHasProcessing"];
                    return false;
                }

            }
            else
            {
                if (Repository.HasWorkFlowForTableName(myModel))
                {
                    model.HasError = "true";
                    model.Message = AppMember.AppText["WorkflowHasApproveTable"];
                    return false;
                }
            }
            return true;
        }
    }
}
