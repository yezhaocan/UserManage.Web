using System.ComponentModel.DataAnnotations;
using UserManage.Base.Application;
using UserManage.Base.AutoMapper;
using UserManage.Entity.System;

namespace UserManage.Service.Models
{
    public class AddOrUpdateRoleInputBase : ValidationModel
    {
        public string EnCode { get; set; }
        [RequiredAttribute(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public string PermissionIds { get; set; }

        public string[] GetPermissionIds()
        {
            if (this.PermissionIds == null)
                return new string[0];

            return this.PermissionIds.Split(',');
        }
    }

    [MapToType(typeof(Sys_Role))]
    public class AddRoleInput : AddOrUpdateRoleInputBase
    {

    }

    [MapToType(typeof(Sys_Role))]
    public class UpdateRoleInput : AddOrUpdateRoleInputBase
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
