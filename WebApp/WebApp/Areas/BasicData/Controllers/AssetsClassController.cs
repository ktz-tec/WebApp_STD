﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseCommon.Data;
using System.Data;
using BusinessLogic.BasicData.Models.AssetsClass;
using WebCommon.Common;
using BaseCommon.Basic;
using System.Web.Caching;
using WebCommon.Init;
using BusinessLogic.BasicData.Repositorys;
using BusinessCommon.Repositorys;
using WebCommon.Data;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;
using BusinessLogic.AssetsBusiness.Repositorys;
using BaseCommon.Models;
using BaseControl.HtmlHelpers;
using BusinessCommon.CommonBusiness;

namespace WebApp.Areas.BasicData.Controllers
{
    public class AssetsClassController : MasterController
    {
        AssetsClassRepository Repository;
        public AssetsClassController()
        {
            ControllerName = "AssetsClass";
            NotNeedCache = true;
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;
        }
        protected override IMasterFactory CreateRepository()
        {
            Repository = new AssetsClassRepository();
            return new MasterRepositoryFactory<AssetsClassRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                model.GridPkField = "assetsClassId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                SetThisEntryModel(model);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                //    Update(EntryRepository, model, model.FormMode, model.AssetsClassId, model.ViewTitle);
                //}
                //SetThisEntryModel(model);
                //return View(model);
                if (Update(Repository, model, model.AssetsClassId) == 1)
                {
                    if (model.FormMode == "new")
                    {
                        SetThisEntryModel(model);
                        return View(model);
                    }
                    else
                        return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle });
                }
                else
                {
                    SetThisEntryModel(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        public ActionResult Select(string pageId, string showCheckbox, string selectIds)
        {
            try
            {
                TreeSelectModel model = new TreeSelectModel();
                model.PageId = pageId;
                model.TreeId = TreeId.AssetsClassTreeId;
                DataTable list = new DataTable();
                if (HttpContext.Cache["AssetsClassTree"] == null)
                {
                    AssetsClassRepository crep = new AssetsClassRepository();
                    list = crep.GetAssetsClassTree();
                    HttpContext.Cache.Add("AssetsClassTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["AssetsClassTree"];
                }
                model.DataTree = list;
                if (showCheckbox == "true")
                    model.ShowCheckBox = true;
                model.SelectId = selectIds;
                model.SearchUrl = Url.Action("SearchTree", "AssetsClass", new { Area = "BasicData" });
                return PartialView("TreeSelect", model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.Select", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [HttpPost]
        public ActionResult SearchTree(string pageId, string pySearch)
        {
            try
            {
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                AssetsClassRepository urep = new AssetsClassRepository();
                DataTable list = new DataTable();
                if (HttpContext.Cache["AssetsClassTree"] == null)
                {
                    list = urep.GetAssetsClassTree();
                    //DataColumn col = new DataColumn("PY");
                    //list.Columns.Add(col);
                    //foreach (DataRow dr in list.Rows)
                    //{
                    //    dr["PY"] = PinYin.GetFirstPinyin(DataConvert.ToString(dr["assetsClassName"]));
                    //}
                    HttpContext.Cache.Add("AssetsClassTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["AssetsClassTree"];
                }
                var dtResult = TreeBusiness.GetSearchDataTable(pySearch, list);
                if (dtResult.Rows.Count > 0)
                {
                    string treeString = AppTreeView.TreeViewString(pageId, TreeId.AssetsClassTreeId, dtResult, "", false);
                    return Content(treeString, "text/html");
                }
                else
                {
                    return Content("0", "text/html");
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.SearchTree", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }
       

        private void SetThisEntryModel(EntryModel model)
        {
            model.ParentUrl = Url.Action("DropList", "AssetsClass", new { Area = "BasicData", currentId = model.ParentId });
            model.DialogUrl = Url.Action("Select", "AssetsClass", new { Area = "BasicData" });
            model.AddFavoritUrl = Url.Action("AddFavorit", "AssetsClass", new { Area = "BasicData", tableName = "AssetsClass" });
            model.ReplaceFavoritUrl = Url.Action("ReplaceFavorit", "AssetsClass", new { Area = "BasicData", tableName = "AssetsClass" });
        }

        public JsonResult GetDefaultByAssetsClass(string assetsClassId)
        {
            try
            {
                ClearClientPageCache(Response);
                DataRow dr = Repository.GetModel(assetsClassId);
                string assetsClassNo = DataConvert.ToString(dr["assetsClassNo"]);
                AssetsRepository arep = new AssetsRepository();
                var selectList = new
                {
                    remainRate = DataConvert.ToDouble(dr["RemainRate"]),
                    durableYears = DataConvert.ToInt32(dr["durableYears"]),
                    unitId = DataConvert.ToString(dr["unitId"]),
                    depreciationType = DataConvert.ToString(dr["depreciationType"]),
                    assetsBarcode = arep.GetAssetsBarcode(assetsClassNo)
                };
                return Json(selectList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.GetDefaultByAssetsClass", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }


        public override JsonResult DropList(string currentId, string pySearch)
        {
            try
            {
                ClearClientPageCache(Response);
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                AssetsClassRepository rep = new AssetsClassRepository();
                DataTable source = rep.GetDropListSource(sysUser.UserId, currentId);
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AssetsClassController.DropList", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

    }
}
