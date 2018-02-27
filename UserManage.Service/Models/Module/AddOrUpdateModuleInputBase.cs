using System.ComponentModel.DataAnnotations;
using UserManage.Base.Application;

namespace UserManage.Service.Models
{
    public class AddOrUpdateModuleInputBase : ValidationModel
    {
        public string ParentId { get; set; }
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UrlAddress { get; set; }
        public string OpenTarget { get; set; }
        public bool IsMenu { get; set; }
        public bool IsExpand { get; set; }
        public bool IsPublic { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
    }
}
