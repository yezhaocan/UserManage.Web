using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManage.Base;
using UserManage.Base.Common;
using UserManage.Base.Exceptions;
using UserManage.Data.IRepository;
using UserManage.Entity.distribution;
using UserManage.Entity.System;

namespace UserManage.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EFDbContext _dbContext;
        public AccountRepository(EFDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Sys_User GetSys_UserByUserName(string userName)
        {
            var result = from a in _dbContext.User
                         where a.UserName == userName && a.IsEnabled
                         select a;
            return result.FirstOrDefault();
        }

        public List<Sys_RoleAuthorize> GetSys_RoleAuthorizeList(string roleId)
        {
            return _dbContext.Sys_RoleAuthorize.Where(p => p.RoleId == roleId).ToList();
        }
        public List<Sys_Module> GetSys_ModuleList(string roleId, bool isAdmin)
        {
            //if (isAdmin)
            //{
            //    return _dbContext.Sys_Module.OrderBy(p => p.SortCode).ToList();
            //}
            //else {
                var query = from a in _dbContext.Sys_Role.Where(p => p.IsEnabled && p.Id == roleId)
                            join b in _dbContext.Sys_RoleAuthorize on a.Id equals b.RoleId
                            join c in _dbContext.Sys_Module on b.ModuleId equals c.Id
                            select c;
                return query.OrderBy(t => t.SortCode).ToList();
            //}
        }

        public List<Sys_Module> GetSys_ModuleList()
        {
            //没有系统菜单权限，64A1C550-2C61-4A8C-833D-ACD0C012260F
            return _dbContext.Sys_Module.Where(p=>p.Id!= "64A1C550-2C61-4A8C-833D-ACD0C012260F").OrderBy(p => p.SortCode).ToList();
        }
        public List<Sys_Role> GetSys_RoleList(string keyword=null)
        {
            var entity = _dbContext.Sys_Role.Where(p => p.IsEnabled);
            var result = keyword.IsNullOrEmpty() ? entity : entity.Where(p => p.Name.Contains(keyword) || p.EnCode.Contains(keyword));
            return result.ToList();
        }

        public PagedData<Sys_User> GetSys_UserList(Pagination pagination,string keyword)
        {
            var result = keyword!=null? _dbContext.User.Where(p => p.UserName.Contains(keyword) || p.RealName.Contains(keyword)): _dbContext.User;
                result=result/*.Where(p => p.UserName != AppConsts.AdminUserName)*/.OrderByDescending(p=>p.CreateTime);
            return result.TakePageData(pagination);
        }

        public PagedData<OperationsWeChat> GetWeChatList(Pagination pagination, string keyword)
        {
            var result = keyword != null ? _dbContext.OperationsWeChat.Where(p => p.WeChatId.Contains(keyword) || p.WeChatNick.Contains(keyword)) : _dbContext.OperationsWeChat;
            result = result.OrderByDescending(p => p.CreateTime);
            return result.TakePageData(pagination);
        }
        public bool ExistUserName(string userName)
        {
            return _dbContext.User.Where(p => p.UserName == userName).Any();
        }

        #region entity 操作

        public TEntity GetById<TEntity>(object key) where TEntity : class
        {
            return _dbContext.Find<TEntity>(key);
        }

        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                _dbContext.Update(entity);
                _dbContext.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public bool Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public bool InsertRange<TEntity>(List<TEntity> list) where TEntity : class
        {
            try
            {
                _dbContext.AddRange(list);
                _dbContext.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }
        public bool Delete<TEntity>(object id) where TEntity : class
        {
            try
            {
                _dbContext.Remove(_dbContext.Find<TEntity>(id));
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public DbSet<TEntity> Get<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public bool DeleteSys_RoleAuthorize(string roleId)
        {
            try
            {
                var res = _dbContext.Sys_RoleAuthorize.Where(p => p.RoleId == roleId);
                _dbContext.RemoveRange(res);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public List<OperationsWeChat> GetOperationsWeChatByUserId(string userId)
        {
            var res= from a in _dbContext.User.Where(p => p.Id == userId)
                   join b in _dbContext.User_OperationsWeChat_Mapping
                   on a.Id equals b.UserId
                   join c in _dbContext.OperationsWeChat 
                   on b.OperationsWeChatId equals c.WeChatId
                   select c;
            return res.ToList();
        }

        public Sys_User GetSys_UserByOperationsWeChatId(string operationsWeChatId)
        {
            var result = from a in _dbContext.User.Where(p => p.IsEnabled)
                         join b in _dbContext.User_OperationsWeChat_Mapping.Where(p => p.OperationsWeChatId == operationsWeChatId)
                         on a.Id equals b.UserId
                         select a;
            return result.FirstOrDefault();
        }

        public OperationsWeChat GetOperationsWeChatByWeChatId(string operationsWeChatId)
        {
            return _dbContext.OperationsWeChat.Where(p => p.WeChatId == operationsWeChatId).FirstOrDefault();
        }

        public void DeleteUser_OperationsWeChat_MappingByweChatId(string weChatId)
        {
            try
            {
                var result = _dbContext.User_OperationsWeChat_Mapping.Where(p => p.OperationsWeChatId == weChatId).ToList();
                _dbContext.User_OperationsWeChat_Mapping.RemoveRange(result);
                 _dbContext.SaveChanges();
            }
            catch (Exception e)
            {

                throw new InvalidDataException(e.Message);
            }
        }

        public List<AccountInfo> GetCustomerByAccountName(string accountName)
        {
            return _dbContext.AccountInfo.Where(p => p.AccountName == accountName).ToList();
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return _dbContext.Customer.Where(p => p.CustomerMobile == mobile).FirstOrDefault();
        }

        public List<Customer> GetCustomerList(string id)
        {
            var result = from a in _dbContext.User_OperationsWeChat_Mapping.Where(p => p.UserId == id)
                        join b in _dbContext.Customer
                        on a.OperationsWeChatId equals b.OperationsWeChatId
                        select b;
            return result.ToList();
        }
        #endregion
    }
}
