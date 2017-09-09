using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon.Common;
using BaseCommon.Data;
using System.Data;
using BaseCommon.Basic;
using WebCommon.Init;
using BusinessCommon.Models.Authority;
using WebCommon.HttpBase;

namespace WebApp.Areas.BusinessCommon.Controllers
{
    public class AuthorityController : BaseController
    {
        public AuthorityController()
        {
            ControllerName = "Authority";
        }

        [AppAuthorize]
        public ActionResult Entry(string pageId, string viewTitle)
        {
            try
            {
                ClearClientPageCache(Response);
                EntryModel model = new EntryModel();
                model.PageId = pageId;
                model.ViewTitle = viewTitle;
                model.FormId = "EntryForm";
                model.TreeId = "tree";
                model.SaveUrl = Url.Action("Entry", "Authority", new { Area = "BusinessCommon" });
                model.AuthorityTree = model.Repository.GetAuthorityTree("", false);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AuthorityController.Entry get", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        [AppAuthorize]
        [HttpPost]
        public ActionResult Entry(EntryModel model)
        {
            try
            {
                UserInfo sysUser = CacheInit.GetUserInfo(HttpContext);
                string hidenTreeId = model.TreeId + model.PageId + AppMember.HideString;
                try
                {
                    if (model.RadioValue == "group")
                        model.Repository.SaveForGroup(model.GroupNo, DataConvert.ToString(Request.Form[hidenTreeId]), sysUser);
                    else if (model.RadioValue == "user")
                        model.Repository.SaveForUser(model.UserNo, DataConvert.ToString(Request.Form[hidenTreeId]), sysUser);
                    model.HasError = "false";
                    model.IsUser = false;
                    model.GroupNo = "";
                    model.UserNo = "";
                }
                catch (Exception ex)
                {
                    model.Message = ex.Message;
                    model.HasError = "true";
                }
                model.AuthorityTree = model.Repository.GetAuthorityTree("", false);
                return View(model);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AuthorityController.Entry post", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }

        public ActionResult RefreshTree(string groupId, string userId)
        {
            try
            {
                EntryModel model = new EntryModel();
                DataTable dt = new DataTable();
                if (DataConvert.ToString(userId) == "")
                {
                    dt = model.Repository.GetAuthorityTreeId(groupId, true);
                }
                else
                {
                    dt = model.Repository.GetAuthorityTreeId(userId, false);
                }
                var checkIds = new object[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    checkIds[i] = new { menuId = DataConvert.ToString(dt.Rows[i]["menuId"]) };
                }

                return Json(new { menuIds = checkIds }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog(AppMember.AppText["SystemUser"], LogType.Error, "AuthorityController.RefreshTree", "[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace);
                return Content("[Message]:" + ex.Message + " [StackTrace]:" + ex.StackTrace, "text/html");
            }
        }


    }
}
