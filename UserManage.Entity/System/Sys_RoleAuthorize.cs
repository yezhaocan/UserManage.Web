using System;
using System.ComponentModel.DataAnnotations;

namespace UserManage.Entity.System
{
    public class Sys_RoleAuthorize
    {
        [Key]
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string ModuleId { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserId { get; set; }
    }

}
