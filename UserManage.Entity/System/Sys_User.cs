using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManage.Entity.System
{
    [Table("Sys_User")]
    public class Sys_User
    {
        //基本信息
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string RoleId { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        //账号 
        public string UserPassword { get; set; }
        public string UserSecretkey { get; set; }
        public DateTime? PreviousVisitTime { get; set; }
        public DateTime? LastVisitTime { get; set; }
        public int LogOnCount { get; set; }
        [NotMapped]
        public int CustomerCount { get; set; }
    }
}
