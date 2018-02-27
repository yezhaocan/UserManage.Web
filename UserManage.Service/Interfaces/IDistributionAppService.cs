using System.Collections.Generic;
using UserManage.Base;
using UserManage.Base.Application;
using UserManage.Entity.distribution;
using UserManage.Entity.Source;

namespace UserManage.Service.Interfaces
{
    public interface IDistributionAppService : IAppService
    {
        PagedData<CustomerModel> GetCustomerModelPageData(Pagination pagination, string keyword, string userId=null);
        void DistributionDataSiurce(List<DataSource> list, bool isFp = true);
    }
}
