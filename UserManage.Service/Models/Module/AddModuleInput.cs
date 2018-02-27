using System;
using UserManage.Base.AutoMapper;
using UserManage.Entity.System;

namespace UserManage.Service.Models
{
    [MapToType(typeof(Sys_Module))]
    public class AddModuleInput : AddOrUpdateModuleInputBase
    {
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
    }

}
