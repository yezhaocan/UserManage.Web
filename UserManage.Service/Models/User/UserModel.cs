using System;
using UserManage.Entity;

namespace UserManage.Service.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string RoleId { get; set; }
        public int CustomerCount { get; set;}
        public DateTime CreateTime { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
    }
}
