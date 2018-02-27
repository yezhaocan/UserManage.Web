using System;
using System.Collections.Generic;
using System.Linq;
using UserManage.Base;
using UserManage.Entity.distribution;
using UserManage.Entity.Source;

namespace UserManage.Data.IRepository
{
    public interface IDistributionRepository
    {
        List<DataSource> GetDataSource( DateTime? date);
        void DistributionDataSiurce(List<DataSource> list, bool isFp = true);
        PagedData<Customer> GetCustomerPageData(Pagination pagination, string keyword, string userId = null);
        DateTime? GetLastSynTime(bool isMall = true);
    }
}
