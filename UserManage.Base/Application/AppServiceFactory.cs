using System;

namespace UserManage.Base.Application
{
    public class AppServiceFactory : IAppServiceFactory
    {
        IServiceProvider _serviceProvider = null;

        public AppServiceFactory(IServiceProvider serviceProvider)
            : this(serviceProvider, null)
        {
        }
        public AppServiceFactory(IServiceProvider serviceProvider, IUserSession session)
        {
            this._serviceProvider = serviceProvider;
            this.Session = session;
        }

        public IUserSession Session { get; set; }

        public virtual T CreateService<T>() where T : IAppService
        {
            object serviceObj = this._serviceProvider.GetService(typeof(T));
            if (serviceObj == null)
                throw new Exception("Can not find the service.");

            T service = (T)serviceObj;

            IAppServiceFactoryProvider factoryProvider = service as IAppServiceFactoryProvider;
            if (factoryProvider != null)
            {
                factoryProvider.ServiceFactory = this;
            }

            if (service is IUserSessionAppService)
            {
                ((IUserSessionAppService)service).UserSession = this.Session;
            }

            return service;
        }

        public void Dispose()
        {

        }
    }
}
