using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseCommon.Data;
using System.Data;
using BusinessLogic.AssetsBusiness.Models.AssetsMaintain;
using WebCommon.Common;
using BaseCommon.Basic;
using System.Web.Caching;
using WebCommon.Init;
using BusinessCommon.Repositorys;
using System.Data.Common;
using BusinessLogic.AssetsBusiness.Repositorys;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;

namespace WebApp.Areas.AssetsBusiness.Controllers
{
    public class AssetsMaintainController : ApproveMasterController
    {
        AssetsMaintainRepository Repository;
        public AssetsMaintainController()
        {
            ControllerName = "AssetsMaintain";
        }


        protected override IApproveMasterFactory CreateRepository()
        {
            Repository = new AssetsMaintainRepository();
            return new ApproveMasterRepositoryFactory<AssetsMaintainRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle, string listMode)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, listMode, "AssetsMaintain", model);
                model.GridPkField = "assetsMaintainId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsMaintainController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                SetParentEntryModel(pageId, primaryKey, formMode, viewTitle, model);
                SetThisEntryModel(model);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsMaintainController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        [HttpPost]
        public ActionResult Entry(EntryModel model, string approveReturn)
        {
            try
            {
                //if (model.FormMode != "approve")
                //{
                //    if (CheckModelIsValid(model))
                //        Update(EntryRepository, model, model.FormMode, model.AssetsMaintainId, model.ViewTitle);
                //    if (model.FormMode == "reapply")
                //        return RedirectToAction("List", "AssetsMaintain", new { Area = "AssetsBusiness", pageId = model.PageId, viewTitle = model.ViewTitle, approvemode = model.FormMode });
                //    else
                //    {
                //        SetMustModel(model);
                //        return View(model);
                //    }
                //}
                //else
                //{
                //    return DealApprove(EntryRepository, model, approveReturn);
                //}

                if (Update(Repository, model, model.AssetsMaintainId, approveReturn) == 1)
                {
                    if (model.FormMode == "approve" || model.FormMode == "reapply")
                        return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle, listMode = model.FormMode });
                    else if (model.FormMode == "new" || model.FormMode == "new2")
                    {
                        EntryModel newModel = new EntryModel();
                        SetParentEntryModel(model.PageId, "", model.FormMode, model.ViewTitle, newModel);
                        SetThisEntryModel(newModel);
                        return View(newModel);
                    }
                    else
                    {
                        if (model.FormMode == "actual")
                            return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle, listMode = model.FormMode });
                        else
                            return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle });
                    }
                }
                else
                {
                    if (model.FormMode == "approve")
                    {
                        Repository.SetModel(model.ApprovePkValue, model.FormMode, model);
                        SetParentEntryModel(model.PageId, model.ApprovePkValue, model.FormMode, model.ViewTitle, model);
                    }
                    SetThisEntryModel(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsMaintainController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [HttpPost]
        public ActionResult GetAutoNo()
        {
            try
            {
                string no = AutoNoGenerator.GetMaxNo("AssetsMaintain");
                return Content(no, "text/html");
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsMaintainController.GetAutoNo", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        protected void SetThisEntryModel(EntryModel model)
        {
            model.EntryGridId = "EntryGrid";
            model.EntryGridLayout = EntryGridLayout(model.FormMode);
            model.SelectUrl = Url.Action("Select", "AssetsManage", new { Area = "AssetsBusiness" });
        }

    }

}
