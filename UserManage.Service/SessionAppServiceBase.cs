using System;
using UserManage.Base;
using UserManage.Base.Application;

namespace UserManage.Service
{
    public abstract class SessionAppServiceBase<TSession> : AppServiceBase, IUserSessionAppService where TSession : class, IUserSession
    {
        protected SessionAppServiceBase(IAppServiceFactory serviceFactory, IUserSession session)
            : base(serviceFactory)
        {
            this.ServiceFactory = serviceFactory;
            this.UserSession = session;
        }

        public IUserSession UserSession
        {
            get
            {
                return this.Session;
            }
            set
            {
                if (value == null)
                {
                    this.Session = null;
                    return;
                }

                TSession s = value as TSession;
                this.Session = s ?? throw new ArgumentException();
            }
        }
        public TSession Session { get; set; }
    }

}
