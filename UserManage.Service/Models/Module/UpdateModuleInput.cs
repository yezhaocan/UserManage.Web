using System;
using System.ComponentModel.DataAnnotations;
using UserManage.Base.AutoMapper;
using UserManage.Entity.System;

namespace UserManage.Service.Models
{
    [MapToType(typeof(Sys_Module))]
    public class UpdateModuleInput : AddOrUpdateModuleInputBase
    {
        [Required(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
    }
}
