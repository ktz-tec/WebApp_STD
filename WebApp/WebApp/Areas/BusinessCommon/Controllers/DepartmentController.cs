﻿using System;
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
using BusinessCommon.Models.Department;
using BusinessCommon.Repositorys;
using WebCommon.Data;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;
using BaseControl.HtmlHelpers;
using BaseCommon.Models;
using BusinessCommon.CommonBusiness;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class DepartmentController : MasterController
    {
        DepartmentRepository Repository;
        public DepartmentController()
        {
            ControllerName = "Department";
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;
        }

        protected override IMasterFactory CreateRepository()
        {
            Repository = new DepartmentRepository();
            return new MasterRepositoryFactory<DepartmentRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                SetThisListModel(model);
                model.GridPkField = "departmentId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                //    Update(EntryRepository, model, model.FormMode, model.DepartmentId, model.ViewTitle);
                //}
                //SetThisEntryModel(model);
                //return View(model);
                if (Update(Repository, model, model.DepartmentId) == 1)
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        public ActionResult Select(string pageId,string showCheckbox,string selectIds)
        {
            try
            {
                TreeSelectModel model = new TreeSelectModel();
                model.PageId = pageId;
                model.TreeId = TreeId.DepartmentTreeId;
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                DataTable list = new DataTable();
                if (HttpContext.Cache["DepartmentTree"] == null)
                {
                    DepartmentRepository drep = new DepartmentRepository();
                    list = drep.GetDepartmentTree(sysUser);
                    HttpContext.Cache.Add("DepartmentTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["DepartmentTree"];
                }
                model.DataTree = list;
                if (showCheckbox == "true")
                    model.ShowCheckBox = true;
                model.SelectId = selectIds;
                model.SearchUrl = Url.Action("SearchTree", "Department", new { Area = "BusinessCommon" });
                return PartialView("TreeSelect", model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.Select", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [HttpPost]
        public ActionResult SearchTree(string pageId, string pySearch)
        {
            try
            {
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                DepartmentRepository urep = new DepartmentRepository();
                DataTable list = new DataTable();
                if (HttpContext.Cache["DepartmentTree"] == null)
                {
                    list = urep.GetDepartmentTree(sysUser);
                    //DataColumn col = new DataColumn("PY");
                    //list.Columns.Add(col);
                    //foreach (DataRow dr in list.Rows)
                    //{
                    //    dr["PY"] = PinYin.GetFirstPinyin(DataConvert.ToString(dr["departmentName"]));
                    //}
                    HttpContext.Cache.Add("DepartmentTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["DepartmentTree"];
                }
                var dtResult = TreeBusiness.GetSearchDataTable(pySearch, list);
                if (dtResult.Rows.Count > 0)
                {
                    string treeString = AppTreeView.TreeViewString(pageId, TreeId.DepartmentTreeId, dtResult, "", false);
                    return Content(treeString, "text/html");
                }
                else
                {
                    return Content("0", "text/html");
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.SearchTree", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
           
        }

        private void SetThisEntryModel(EntryModel model)
        {
            //model.ParentUrl = Url.Action("DepartmentDropList", "DropList", new { Area = "", filterExpression = "departmentId=" + DFT.SQ + model.ParentId + DFT.SQ });
            model.ParentUrl = Url.Action("DropList", "Department", new { Area = "BusinessCommon", currentId = model.ParentId });
            model.DialogUrl = Url.Action("Select", "Department", new { Area = "BusinessCommon" });
            model.AddFavoritUrl = Url.Action("AddFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
            model.ReplaceFavoritUrl = Url.Action("ReplaceFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
        }

        private void SetThisListModel(ListModel model)
        {
            //model.ParentUrl = Url.Action("DepartmentDropList", "DropList", new { Area = "", filterExpression = "departmentId=" + DFT.SQ + model.ParentId + DFT.SQ });
            model.ParentUrl = Url.Action("DropList", "Department", new { Area = "BusinessCommon", currentId = model.ParentId });
            model.DialogUrl = Url.Action("Select", "Department", new { Area = "BusinessCommon" });
            model.AddFavoritUrl = Url.Action("AddFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
            model.ReplaceFavoritUrl = Url.Action("ReplaceFavorit", "Department", new { Area = "BusinessCommon", tableName = "AppDepartment" });
        }

        public override JsonResult DropList(string currentId, string pySearch)
        {
            try
            {
                ClearClientPageCache(Response);
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                DepartmentRepository rep = new DepartmentRepository();
                DataTable source = rep.GetDropListSource(sysUser.UserId, currentId);
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.DropList", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

        public JsonResult AllDropList()
        {
            try
            {
                ClearClientPageCache(Response);
                DepartmentRepository rep = new DepartmentRepository();
                DataTable source = rep.GetDropListSource();
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "DepartmentController.AllDropList", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

    }
}
