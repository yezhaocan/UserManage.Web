using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.Base.IdStrategy;

namespace UserManage.Base.Application
{
    public static class AppServiceExtension
    {
        public static string CreateId(this IAppService service)
        {
            return IdHelper.CreateGuid();
        }
    }
}
