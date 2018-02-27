using System.Collections.Generic;
using UserManage.Base;
using UserManage.Base.Application;
using UserManage.Base.AutoMapper;
using UserManage.Data.IRepository;
using UserManage.Entity.distribution;
using UserManage.Entity.Source;
using UserManage.Service.Interfaces;

namespace UserManage.Service.Implements
{
    public class DistributionAppService: AdminAppService,IDistributionAppService
    {
        private readonly IDistributionRepository _distributionRepository;
        private readonly IAccountRepository _accountRepository;

        public DistributionAppService(IDistributionRepository distributionAppService, IAccountRepository accountRepository) 
        {
            _distributionRepository = distributionAppService;
            _accountRepository = accountRepository;
        }

        public void DistributionDataSiurce(List<DataSource> list, bool isFp = true)
        {
            _distributionRepository.DistributionDataSiurce(list, isFp);
        }

        public PagedData<CustomerModel> GetCustomerModelPageData(Pagination pagination, string keyword, string userId = null)
        {
            var result = _distributionRepository.GetCustomerPageData(pagination, keyword, userId);
            List<CustomerModel> list = new List<CustomerModel>();
            foreach (var item in result.DataList)
            {
                CustomerModel customerModel = new CustomerModel()
                {
                    Id = item.Id,
                    AccountName = item.AccountName,
                    CustomerMobile = item.CustomerMobile,
                    CustomerName = item.CustomerName,
                    CustomerWeChatId = item.CustomerWeChatId,
                    CustomerWeChatNick = item.CustomerWeChatNick,
                    OperatingTime = item.OperatingTime,
                    OperationsWeChatId = item.OperationsWeChatId,
                    State = (int)item.State,
                    Origin = (int)item.Origin,
                    Description = item.Description
                };
                list.Add(customerModel);
            }
            PagedData<CustomerModel> pageData = pagination.ToPagedData<CustomerModel>();
            pageData.DataList = list;
            pageData.TotalCount = result.TotalCount;
            return pageData;
        }


    }
}
