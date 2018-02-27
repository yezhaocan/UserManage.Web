using UserManage.Base;
using UserManage.Base.Application;

namespace UserManage.Service
{
    public class AdminAppService : SessionAppServiceBase<AdminSession>
    {

        protected AdminAppService(IAppServiceFactory serviceFactory, IUserSession session) : base(serviceFactory, session)
        {
        }

        protected AdminAppService()
            : this(null, null)
        {           
        }
    }
}
