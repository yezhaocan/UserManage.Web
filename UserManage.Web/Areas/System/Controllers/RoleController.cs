using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManage.Web.Common;
using UserManage.Entity.System;
using UserManage.Service.Interfaces;
using UserManage.Service.Models;
using UserManage.Base.AutoMapper;
using System;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.SystemManage)]
    [LoginAuthorize]
    public class RoleController : WebController
    {
        // GET: SystemManage/Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetModels(string keyword)
        {
            List<Sys_Role> data = CreateService<IAccountAppService>().GetSys_RoleList(keyword);
            return SuccessData(data);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Add(AddRoleInput input)
        {
            input.Validate();
            var services = CreateService<IAccountAppService>();
            Sys_Role role = new Sys_Role();
            AceMapper.Map(input, role);
            role.CreateUserId = CurrentSession.UserId;
            role.CreateTime = DateTime.Now;
            services.Insert(role);
            string[] permissionIds = input.GetPermissionIds();
            List<Sys_RoleAuthorize> roleAuthorizeEntitys = CreateRoleAuthorizes(role.Id, permissionIds);
            foreach (var roleAuthorizeEntity in roleAuthorizeEntitys)
            {
                services.Insert(roleAuthorizeEntity);
            }

            return AddSuccessMsg();
        }
        private List<Sys_RoleAuthorize> CreateRoleAuthorizes(string roleId, string[] permissionIds)
        {
            List<Sys_RoleAuthorize> roleAuthorizeEntitys = new List<Sys_RoleAuthorize>();
            foreach (var moduleId in permissionIds)
            {
                Sys_RoleAuthorize roleAuthorizeEntity = new Sys_RoleAuthorize
                {
                    RoleId = roleId,
                    ModuleId = moduleId,
                    CreateTime = DateTime.Now,
                    CreateUserId=CurrentSession.UserId                    
                };
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }

            return roleAuthorizeEntitys;
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Update(UpdateRoleInput input)
        {

            input.Validate();

            var services = CreateService<IAccountAppService>();
            Sys_Role role = services.GetById<Sys_Role>(input.Id);

            role.NotNull("角色不存在");

            AceMapper.Map(input, role);
            string[] permissionIds = input.GetPermissionIds();
            List<Sys_RoleAuthorize> roleAuthorizeEntitys = CreateRoleAuthorizes(role.Id, permissionIds);
            role.LastModifyTime = DateTime.Now;
            role.LastModifyUserId = CurrentSession.UserId;
            services.Update(role);
            services.DeleteSys_RoleAuthorize(role.Id);
            foreach (var roleAuthorizeEntity in roleAuthorizeEntitys)
            {
            services.Insert(roleAuthorizeEntity);
            }
            return UpdateSuccessMsg();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var services = CreateService<IAccountAppService>();
            services.Delete<Sys_Role>(id);
            services.DeleteSys_RoleAuthorize(id);
            return DeleteSuccessMsg();
        }
    }
}