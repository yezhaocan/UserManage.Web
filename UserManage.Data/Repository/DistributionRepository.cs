using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UserManage.Base.IdStrategy;
using UserManage.Data.IRepository;
using UserManage.Entity.distribution;
using UserManage.Entity.Source;
using UserManage.Base;
using UserManage.Entity;
using UserManage.Entity.System;

namespace UserManage.Data.Repository
{
    public class DistributionRepository:  IDistributionRepository
    {
        private readonly EFDbContext _dbContext;
       // private readonly MallDbContext _mallContext;
        private readonly IWMSDbContext _iwmsContext;
        public DistributionRepository(EFDbContext dbContext,/* MallDbContext mallContext,*/ IWMSDbContext iwmsContext)
        {
            _dbContext = dbContext;
            //_mallContext = mallContext;
            _iwmsContext = iwmsContext;
        }
        public List<DataSource> GetDataSource( DateTime? dateTime)
        {
            List<DataSource> list = new List<DataSource>();
            
            using (var connection = _iwmsContext.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = " select c.sd_id as sdid,c.receiver_nam as name,c.receiver_mobile as mobile from (" +
                                        " select a.receiver_mobile from(" +
                                        " select row_number() over(partition by receiver_mobile order by pay_time) as rownum ,o.pay_time,o.receiver_mobile from TSyn_order_info o where o.order_status = 1 and o.sd_id != 6 ) a where a.rownum = 1 ";
                    if (dateTime != null) {
                        command.CommandText += " and  a.pay_time >= '" + Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd hh:mm:ss") + "' and a.pay_time <= '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'";
                    }
                    command.CommandText += " ) b inner join TSyn_order_info c on b.receiver_mobile = c.receiver_mobile where c.order_status = 1 and c.sd_id not in (6,7,8,9,20,24,25) group by c.sd_id,c.receiver_nam,c.receiver_mobile;";
                    using (SqlDataReader reader = command.ExecuteReader() as SqlDataReader)
                    {
                        while (reader.Read())
                        {
                            DataSource ds = new DataSource
                            {
                                AccountName = null,
                                CustomerName = reader["name"].ToString(),
                                Mobile = reader["mobile"].ToString(),
                                Origin = (OriginType)Convert.ToInt32(reader["sdid"].ToString()),
                            };
                            list.Add(ds);
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 分配客户给运营
        /// </summary>
        /// <param name="list"></param>
        public void DistributionDataSiurce(List<DataSource> list,bool isFp=true)
        {
            using (var trans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var owcList = _dbContext.OperationsWeChat.Where(p => p.IsEnabled).OrderBy(p=>Guid.NewGuid()).ToList();
                    var count = owcList.Count();
                    var loop = 0;
                    var result = list.GroupBy(p => p.Origin);
                    foreach (var item in result)
                    {
                        if (item.Key != OriginType.Mall)
                        {
                            foreach (var it in item)
                            {
                                if (_dbContext.Customer.Where(p => p.CustomerMobile == it.Mobile).FirstOrDefault() == null
                                    && _dbContext.AccountInfo.Where(p=>p.Mobile==it.Mobile).FirstOrDefault()==null)
                                {
                                    Customer customer = new Customer
                                    {
                                        Id = IdHelper.CreateGuid(),
                                        AccountName = it.AccountName,
                                        CustomerName = it.CustomerName,
                                        CustomerMobile = it.Mobile,
                                        State = StateType.Untreated,
                                        Origin = it.Origin,
                                        CustomerWeChatId = "",
                                        CustomerWeChatNick = "",
                                        OperatingTime = DateTime.Now,
                                        OperationsWeChatId = !isFp ?"": owcList[loop].WeChatId,
                                        SyncTime = DateTime.Now,                                        
                                    };
                                    _dbContext.Customer.Add(customer);
                                    loop++;
                                    if (loop >= count) { loop = 0; }
                                }
                            }
                        }
                        else
                        {
                            var data = item.GroupBy(p => p.AccountName);
                            foreach (var it in data)
                            {
                                var a1 = from a in _dbContext.Customer
                                             join b in it
                                             on a.CustomerMobile equals b.Mobile
                                             select a;
                                var a2 = from a in _dbContext.AccountInfo
                                             join b in it
                                             on a.Mobile equals b.Mobile
                                             select a;
                                if (a1.FirstOrDefault() == null &&
                                    a2.FirstOrDefault() == null
                                    )
                                {
                                    if (it.Count() == 1)
                                    {
                                        //商城账号
                                        Customer customer = new Customer
                                        {
                                            Id = IdHelper.CreateGuid(),
                                            AccountName = it.FirstOrDefault().AccountName,
                                            CustomerName = it.FirstOrDefault().CustomerName,
                                            CustomerMobile = it.FirstOrDefault().Mobile,
                                            State = StateType.Untreated,
                                            Origin = item.Key,
                                            CustomerWeChatId = "",
                                            CustomerWeChatNick = "",
                                            OperatingTime = DateTime.Now,
                                            OperationsWeChatId = !isFp ? "" : owcList[loop].WeChatId,
                                            SyncTime = DateTime.Now,
                                        };
                                        _dbContext.Customer.Add(customer);
                                        loop++;
                                        if (loop >= count) { loop = 0; }
                                    }
                                    else
                                    {

                                        Customer customer = new Customer
                                        {
                                            Id = IdHelper.CreateGuid(),
                                            AccountName = it.Key,
                                            CustomerName = "",
                                            CustomerMobile = "",
                                            State = StateType.Untreated,
                                            Origin = item.Key,
                                            CustomerWeChatId = "",
                                            CustomerWeChatNick = "",
                                            OperatingTime = DateTime.Now,
                                            OperationsWeChatId = !isFp ? "" : owcList[loop].WeChatId,
                                            SyncTime = DateTime.Now,
                                        };
                                        foreach (var a in it)
                                        {
                                            AccountInfo accountInfo = new AccountInfo
                                            {
                                                AccountName = a.AccountName,
                                                CustomerName = a.CustomerName,
                                                Mobile = a.Mobile,
                                            };
                                            _dbContext.AccountInfo.Add(accountInfo);
                                        }
                                        _dbContext.Customer.Add(customer);
                                        loop++;
                                        if (loop >= count) { loop = 0; }
                                    }
                                }
                            }
                        }
                    }
                    _dbContext.SaveChanges();
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Commit();
                    throw e;

                }
            }
        }
        public PagedData<Customer> GetCustomerPageData(Pagination pagination, string keyword, string userId = null)
        {
            //获取账号下的客户
            var result = keyword != null ? _dbContext.Customer.Where(p => p.AccountName.Contains(keyword) || p.CustomerName.Contains(keyword) || p.CustomerWeChatId.Contains(keyword)||p.CustomerMobile.Contains(keyword)) : _dbContext.Customer;
            if (userId != null)
            {
                result = from a in _dbContext.User.Where(p => p.Id == userId)
                         join b in _dbContext.User_OperationsWeChat_Mapping on a.Id equals b.UserId
                         join c in result on b.OperationsWeChatId equals c.OperationsWeChatId
                         select c;
            }
            return result.OrderBy(p=>p.State).ThenByDescending(p=>p.OperatingTime).TakePageData(pagination);
        }

        public DateTime? GetLastSynTime(bool isMall = true)
        {
            if (_dbContext.Customer == null)
            {
                return null;
            }
            else
            {
                var c1 = _dbContext.Customer.Where(p => p.Origin == OriginType.Mall).OrderByDescending(p => p.SyncTime).FirstOrDefault();
                var c2 = _dbContext.Customer.Where(p => p.Origin != OriginType.Mall).OrderByDescending(p => p.SyncTime).FirstOrDefault();
                if (isMall)
                {
                    if (c1 == null)
                    {
                        return null;
                    }
                    else
                    {
                        return c1.SyncTime;
                    }

                }
                else
                {
                    if (c2 == null)
                    {
                        return null;
                    }
                    else
                    {
                        return c2.SyncTime;
                    }
                }

            }
        }
    }
}
