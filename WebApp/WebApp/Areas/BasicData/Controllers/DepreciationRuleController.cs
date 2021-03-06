﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseCommon.Data;
using System.Data;
using BusinessLogic.BasicData.Models.DepreciationRule;
using WebCommon.Common;
using BaseCommon.Basic;
using System.Web.Caching;
using WebCommon.Init;
using BusinessLogic.BasicData.Repositorys;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;

namespace WebApp.Areas.BasicData.Controllers
{
    public class DepreciationRuleController : MasterController
    {
        DepreciationRuleRepository Repository;
        public DepreciationRuleController()
        {
            ControllerName = "DepreciationRule";
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;
        }

        protected override IMasterFactory CreateRepository()
        {
            Repository = new DepreciationRuleRepository();
            return new MasterRepositoryFactory<DepreciationRuleRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                model.GridPkField = "depreciationRuleId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepreciationRuleController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                Repository.SetModel(primaryKey, formMode, model);
                SetParentEntryModel(pageId, formMode, viewTitle, model);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepreciationRuleController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                //    Update(EntryRepository, model, model.FormMode, model.DepreciationRuleId, model.ViewTitle);

                //}
                //return View(model);
                if (Update(Repository, model, model.DepreciationRuleId) == 1)
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepreciationRuleController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }


       
    }
}
