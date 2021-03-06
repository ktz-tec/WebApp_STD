﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BusinessLogic.AssetsBusiness.Models.MonthRecord;
using BusinessLogic.AssetsBusiness.Repositorys;
using BaseCommon.Data;
using WebCommon.Init;
using WebCommon.HttpBase;
using BaseCommon.Basic;

namespace WebApp.Areas.AssetsBusiness.Controllers
{
    public class MonthRecordController : MaintainController
    {
        protected MonthRecordRepository Repository;
        public MonthRecordController()
        {
            ControllerName = "MonthRecord";
            Repository = new MonthRecordRepository();
        }

        [AppAuthorize]
        public ActionResult Entry(string pageId, string primaryKey, string formMode, string viewTitle)
        {
            try
            {
                ClearClientPageCache(Response);
                EntryModel model = new EntryModel();
                SetParentEntryModel(pageId, formMode, viewTitle, model);
                model.CustomClick = true;
                model.AssetsClassUrl = Url.Action("DropList", "AssetsClass", new { Area = "BasicData", currentId = model.AssetsClassId });
                model.AssetsClassDialogUrl = Url.Action("Select", "AssetsClass", new { Area = "BasicData" });
                model.AssetsClassAddFavoritUrl = Url.Action("AddFavorit", "AssetsClass", new { Area = "BasicData", tableName = "AssetsClass" });
                model.AssetsClassReplaceFavoritUrl = Url.Action("ReplaceFavorit", "AssetsClass", new { Area = "BasicData", tableName = "AssetsClass" });
                model.DepartmentUrl = Url.Action("DropList", "Department", new { Area = "BusinessCommon", currentId = model.DepartmentId });
                model.DepartmentDialogUrl = Url.Action("Select", "Department", new { Area = "BusinessCommon" });
                model.DepartmentAddFavoritUrl = Url.Action("AddFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
                model.DepartmentReplaceFavoritUrl = Url.Action("ReplaceFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
                model.StoreSiteUrl = Url.Action("DropList", "StoreSite", new { Area = "BasicData", currentId = model.StoreSiteId });
                model.StoreSiteDialogUrl = Url.Action("Select", "StoreSite", new { Area = "BasicData" });
                model.StoreSiteAddFavoritUrl = Url.Action("AddFavorit", "StoreSite", new { Area = "BasicData", tableName = "StoreSite" });
                model.StoreSiteReplaceFavoritUrl = Url.Action("ReplaceFavorit", "StoreSite", new { Area = "BasicData", tableName = "StoreSite" });
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "MonthRecordController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        [HttpPost]
        public ActionResult Entry(EntryModel model)
        {
            try
            {
                if (CheckModelIsValid(model))
                {
                    Update(Repository, model, model.ViewTitle);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "MonthRecordController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [HttpPost]
        public ActionResult Check(EntryModel model)
        {
            try
            {
                string ret = "";
                ret = Repository.Check(model);
                return Content(ret, "text/html");
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "MonthRecordController.Check", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

    }
}
