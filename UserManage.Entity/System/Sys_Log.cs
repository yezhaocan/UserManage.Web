using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManage.Entity.System
{
    [Table("Sys_Log")]
    public class Sys_Log
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RealName { get; set; }
        public LogType Type { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
