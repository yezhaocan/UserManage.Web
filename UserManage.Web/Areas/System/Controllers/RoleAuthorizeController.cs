using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserManage.Web.Common;
using UserManage.Entity.System;
using UserManage.Service.Interfaces;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.SystemManage)]
    [LoginAuthorize]
    public class RoleAuthorizeController : WebController
    {
        // GET: SystemManage/RoleAuthorize
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPermissionTree(string roleId)
        {
            var services = this.CreateService<IAccountAppService>();
            var moduledata = services.GetSys_ModuleList();
            var authorizedata = new List<Sys_RoleAuthorize>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = services.GetSys_RoleAuthorizeList(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (Sys_Module module in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Any(a => a.ParentId == module.Id);
                tree.Id = module.Id;
                tree.Text = module.Name;
                tree.Value = module.EnCode;
                tree.ParentId = module.ParentId;
                tree.Isexpand = true;
                tree.Complete = true;
                tree.Showcheck = true;
                tree.Checkstate = authorizedata.Count(t => t.ModuleId == module.Id);
                tree.HasChildren = hasChildren;
                tree.Img = module.Icon == "" ? "" : module.Icon;
                treeList.Add(tree);
            }

            return Content(TreeHelper.ConvertToJson(treeList));
        }
    }
}