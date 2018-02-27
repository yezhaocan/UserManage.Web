using System;
using System.ComponentModel.DataAnnotations;
using UserManage.Base.Application;
using UserManage.Entity;

namespace UserManage.Service.Models
{
    public class ModifyAccountInfoInput : ValidationModel
    {
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }
        public string Description { get; set; }
    }
}
