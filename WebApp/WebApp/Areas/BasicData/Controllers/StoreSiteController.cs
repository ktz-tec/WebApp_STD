using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseCommon.Data;
using System.Data;
using BusinessLogic.BasicData.Models.StoreSite;
using WebCommon.Common;
using BaseCommon.Basic;
using System.Web.Caching;
using WebCommon.Init;
using BusinessLogic.BasicData.Repositorys;
using BusinessCommon.Repositorys;
using WebCommon.Data;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;
using BaseControl.HtmlHelpers;
using BaseCommon.Models;
using BusinessCommon.CommonBusiness;

namespace WebApp.Areas.BasicData.Controllers
{
    public class StoreSiteController : MasterController
    {
        StoreSiteRepository Repository;
        public StoreSiteController()
        {
            NotNeedCache = true;
            ControllerName = "StoreSite";
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;

        }

        protected override IMasterFactory CreateRepository()
        {
            Repository = new StoreSiteRepository();
            return new MasterRepositoryFactory<StoreSiteRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                model.GridPkField = "storeSiteId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                //    Update(EntryRepository, model, model.FormMode, model.StoreSiteId, model.ViewTitle);
                //}
                //SetThisEntryModel(model);
                //return View(model);
                if (Update(Repository, model, model.StoreSiteId) == 1)
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        public ActionResult Select(string pageId, string showCheckbox, string selectIds)
        {
            try
            {
                TreeSelectModel model = new TreeSelectModel();
                model.PageId = pageId;
                model.TreeId = TreeId.StoreSiteTreeId;
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                DataTable list = new DataTable();
                if (HttpContext.Cache["StoreSiteTree"] == null)
                {
                    StoreSiteRepository srep = new StoreSiteRepository();
                    list = srep.GetStoreSiteTree(sysUser);
                    HttpContext.Cache.Add("StoreSiteTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["StoreSiteTree"];
                }
                model.DataTree = list;
                if (showCheckbox == "true")
                    model.ShowCheckBox = true;
                model.SelectId = selectIds;
                model.SearchUrl = Url.Action("SearchTree", "StoreSite", new { Area = "BasicData" });
                return PartialView("TreeSelect", model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.Select", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [HttpPost]
        public ActionResult SearchTree(string pageId, string pySearch)
        {
            try
            {
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                StoreSiteRepository urep = new StoreSiteRepository();
                DataTable list = new DataTable();
                if (HttpContext.Cache["StoreSiteTree"] == null)
                {
                    list = urep.GetStoreSiteTree(sysUser);
                    //根据拼音首字母检索，现不用。
                    //DataColumn col = new DataColumn("PY");
                    //list.Columns.Add(col);
                    //foreach (DataRow dr in list.Rows)
                    //{
                    //    dr["PY"] = PinYin.GetFirstPinyin(DataConvert.ToString(dr["storeSiteName"]));
                    //}
                    HttpContext.Cache.Add("StoreSiteTree", list, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                else
                {
                    list = (DataTable)HttpContext.Cache["StoreSiteTree"];
                }
                var dtResult = TreeBusiness.GetSearchDataTable(pySearch, list);
                if (dtResult.Rows.Count > 0)
                {
                    string treeString = AppTreeView.TreeViewString(pageId, TreeId.StoreSiteTreeId, dtResult, "", false);
                    return Content(treeString, "text/html");
                }
                else
                {
                    return Content("0", "text/html");
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.SearchTree", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }

        }

        private List<DataRow> DistinctParent(List<string> parentIds, DataTable dtAll)
        {
            string insql = "";
            foreach (string pid in parentIds)
            {
                insql += "'" + pid + "',";
            }
            insql = insql.Substring(0, insql.Length - 1);
            var parentDrs = dtAll.Select(string.Format(" storeSiteId in ({0})", insql));
            List<DataRow> retList = new List<DataRow>();
            retList.AddRange(parentDrs);
            var pIds = parentDrs.Select(m => m.Field<string>("parentId")).Distinct().ToList();
            if (pIds.Count > 0)
            {
                var pDrs = DistinctParent(pIds, dtAll);
                retList.AddRange(pDrs);
            }
            return retList;
            //return retList.Distinct().ToList();
        }


        private void SetThisEntryModel(EntryModel model)
        {
            model.ParentUrl = Url.Action("DropList", "StoreSite", new { Area = "BasicData", currentId = model.ParentId });
            model.DialogUrl = Url.Action("Select", "StoreSite", new { Area = "BasicData" });
            model.AddFavoritUrl = Url.Action("AddFavorit", "StoreSite", new { Area = "BasicData", tableName = "StoreSite" });
            model.ReplaceFavoritUrl = Url.Action("ReplaceFavorit", "StoreSite", new { Area = "BasicData", tableName = "StoreSite" });
        }

        public override JsonResult DropList(string currentId, string pySearch)
        {
            try
            {
                ClearClientPageCache(Response);
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                StoreSiteRepository rep = new StoreSiteRepository();
                DataTable source = rep.GetDropListSource(sysUser.UserId, currentId, sysUser);
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "StoreSiteController.SearchTree", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

    }
}
