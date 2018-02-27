using System;
using System.ComponentModel.DataAnnotations;

namespace UserManage.Entity.System
{
    public class Sys_Role
    {
        [Key]
        public string Id { get; set; }
        public string EnCode { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
    }
}
