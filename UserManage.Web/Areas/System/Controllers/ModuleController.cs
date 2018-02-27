using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserManage.Web.Common;
using UserManage.Entity.System;
using UserManage.Service.Interfaces;
using UserManage.Service.Models;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.SystemManage)]
    [LoginAuthorize]
    public class ModuleController : WebController
    {
        // GET: SystemManage/Module
        public ActionResult Index()
        {
            var data = CreateService<IAccountAppService>().GetSys_ModuleList();
            var treeList = new List<TreeSelectModel>();

            foreach (Sys_Module item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel
                {
                    id = item.Id,
                    text = item.Name,
                    parentId = item.ParentId
                };
                treeList.Add(treeModel);
            }

            ViewBag.Menus = TreeHelper.ConvertToJson(treeList);
            return View();
        }

        public ActionResult GetModels(string keyword)
        {
            var data = CreateService<IAccountAppService>().GetSys_ModuleList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = TreeHelper.TreeWhere(data, a => a.Name.Contains(keyword), a => a.Id, a => a.ParentId);
            }

            List<DataTableTree> ret = new List<DataTableTree>();
            DataTableTree.AppendChildren(data, ref ret, null, 0, a => a.Id, a => a.ParentId);

            return SuccessData(ret);
        }

        [HttpPost]
        public ActionResult Add(AddModuleInput input)
        {
            input.CreateUserId = CurrentSession.UserId;
            input.CreateTime = DateTime.Now;
            CreateService<IAccountAppService>().AddModule(input);
            return AddSuccessMsg();
        }
        [HttpPost]
        public ActionResult Update(UpdateModuleInput input)
        {
            input.LastModifyTime = DateTime.Now;
            input.LastModifyUserId = CurrentSession.UserId;
            CreateService<IAccountAppService>().UpdateModule(input);
            return UpdateSuccessMsg();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (id.IsNullOrEmpty())
                return FailedMsg("id 不能为空");

            CreateService<IAccountAppService>().Delete<Sys_Module>(id);
            return DeleteSuccessMsg();
        }

    }
}