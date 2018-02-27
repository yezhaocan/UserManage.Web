using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UserManage.Base;
using UserManage.Entity.System;
using UserManage.Service.Interfaces;
using UserManage.Web.Common;

namespace UserManage.Web
{
    [LoginAuthorize]
    public class HomeController : WebController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.CurrentSession = this.CurrentSession;
            return View();
        }
        public ActionResult Default()
        {
            ViewBag.CurrentSession = this.CurrentSession;
            return View();
        }
        [HttpGet]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                user = "",
                authorizeMenu = GetMenuList(),
            };
            return SuccessData(data);
        }

        [SkipLoginAuthorize]
        public ActionResult Error()
        {
            return View();
        }

        object GetMenuList()
        {
            var roleId = CurrentSession.RoleId;
            var service = CreateService<IAccountAppService>();
            List<Sys_Module> menuList = service.GetMenuList(roleId);

            menuList.ForEach(a =>
            {
                if (a.UrlAddress.IsNotNullOrEmpty())
                {
                    a.UrlAddress = Url.Content(a.UrlAddress);
                }
            });

            return ToMenuJson(menuList, null);
        }
        string ToMenuJson(List<Sys_Module> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<Sys_Module> entities = data.FindAll(t => t.ParentId == parentId);
            if (entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    string strJson = JsonHelper.Serialize(item);
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
    }
}