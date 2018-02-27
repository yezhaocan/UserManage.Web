using System;
using UserManage.Base.Application;

namespace UserManage.Service
{
    public abstract class AppServiceBase : IAppServiceFactoryProvider, IDisposable
    {
        public IAppServiceFactory ServiceFactory { get; set; }

        protected AppServiceBase()
            : this(null)
        {
        }
        protected AppServiceBase(IAppServiceFactory serviceFactory)
        {
            this.ServiceFactory = serviceFactory;
        }

        public void Dispose()
        {
            
        }
    }
}
