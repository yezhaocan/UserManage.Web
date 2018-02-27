using System;
using UserManage.Base.Application;
using UserManage.Entity;

namespace UserManage.Service.Models
{
    public class AddUpdateUserInputBase :ValidationModel
    {
        public string RoleId { get; set; }
        public string RealName { get; set; }
        public string WeChatId { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
    }
}
