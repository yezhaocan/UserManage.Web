using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace UserManage.Data
{
    public abstract class EFDbContextBase
    {
        IServiceProvider serviceProvider;
        EFDbContext _dbContext;
        public EFDbContextBase() {
            var services = new ServiceCollection();
            serviceProvider = services.BuildServiceProvider();
        }

        public EFDbContext DbContext
        {
            get
            {
                if (this._dbContext == null)
                    using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var db = serviceScope.ServiceProvider.GetService<EFDbContext>();
                        this._dbContext = db;
                    }
                return this._dbContext;
            }
            set
            {
                this._dbContext = value;
            }
        }        

        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
