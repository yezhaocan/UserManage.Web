using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManage.Entity.System;

namespace UserManage.Web.Models
{
    public class UserModel
    {
        public Sys_User User { get; set; }
        public string RoleName { get; set; }
    }
}
