using System;
using System.ComponentModel.DataAnnotations;

namespace UserManage.Entity.System
{
    public class Sys_Module
    {
        [Key]
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string EnCode { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UrlAddress { get; set; }
        public string OpenTarget { get; set; }
        public bool IsMenu { get; set; }
        public bool IsExpand { get; set; }
        public bool IsPublic { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }

        public bool IsEnabled { get; set; }
    }

}
