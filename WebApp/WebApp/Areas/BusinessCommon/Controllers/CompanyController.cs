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
using BusinessCommon.Models.Company;
using BusinessCommon.Repositorys;
using WebCommon.Data;
using BaseCommon.Repositorys;
using WebCommon.HttpBase;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class CompanyController : MasterController
    {
        CompanyRepository Repository;
        public CompanyController()
        {
            ControllerName = "Company";
            CacheExpiryMinute = 5;
            CachePriority = CacheItemPriority.High;
        }

        protected override IMasterFactory CreateRepository()
        {
            Repository = new CompanyRepository();
            return new MasterRepositoryFactory<CompanyRepository>(Repository);
        }

        [AppAuthorize]
        public ActionResult List(string pageId, string viewTitle)
        {
            try
            {
                ListModel model = new ListModel();
                SetParentListModel(pageId, viewTitle, model);
                model.GridPkField = "departmentId";
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "CompanyController.List", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
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
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "CompanyController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        [HttpPost]
        public ActionResult Entry(EntryModel model)
        {
            try
            {
                if (Update(Repository, model, model.DepartmentId) == 1)
                {
                    if (model.FormMode == "new")
                    {
                        return View(model);
                    }
                    else
                        return RedirectToAction("List", new { pageId = model.PageId, viewTitle = model.ViewTitle });
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "CompanyController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

 
        public override JsonResult DropList(string currentId,string pySearch)
        {
            try
            {
                ClearClientPageCache(Response);
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                CompanyRepository rep = new CompanyRepository();
                DataTable source = rep.GetDropListSource(sysUser);
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "CompanyController.DropList", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

        public JsonResult AllDropList()
        {
            try
            {
                DepartmentRepository rep = new DepartmentRepository();
                DataTable source = rep.GetDropListSource();
                List<DropListSource> dropList = rep.DropList(source, "");
                return DropListJson(dropList);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "CompanyController.AllDropList", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return new JsonResult();
            }
        }

    }
}
