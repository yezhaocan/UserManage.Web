using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Base.Application
{
    public interface IUserSessionAppService
    {
        IUserSession UserSession { get; set; }
    }
}
