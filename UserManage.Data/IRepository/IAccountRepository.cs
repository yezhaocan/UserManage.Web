using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManage.Base;
using UserManage.Entity.distribution;
using UserManage.Entity.System;
using UserManage.Entity.Source;

namespace UserManage.Data.IRepository
{
    public interface IAccountRepository
    {
        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        Sys_User GetSys_UserByUserName(string userName);

        List<Sys_RoleAuthorize> GetSys_RoleAuthorizeList(string roleId);
        List<Sys_Module> GetSys_ModuleList(string roleId,bool isAdmin);

        List<Sys_Module> GetSys_ModuleList();
        List<Sys_Role> GetSys_RoleList(string keyword=null);
        PagedData<Sys_User> GetSys_UserList(Pagination pagination,string keyword);
        PagedData<OperationsWeChat> GetWeChatList(Pagination pagination, string keyword);
        bool ExistUserName(string userName);
        #region entity 操作
        TEntity GetById<TEntity>(object key) where TEntity : class;
        bool Update<TEntity>(TEntity entity) where TEntity : class;
        bool Delete<TEntity>(object id) where TEntity : class;
        bool Insert<TEntity>(TEntity entity) where TEntity : class;
        bool InsertRange<TEntity>(List<TEntity> list) where TEntity : class;
        DbSet<TEntity> Get<TEntity>() where TEntity : class;
        bool DeleteSys_RoleAuthorize(string roleId);
        List<OperationsWeChat> GetOperationsWeChatByUserId(string userId);
        Sys_User GetSys_UserByOperationsWeChatId(string operationsWeChatId);
        OperationsWeChat GetOperationsWeChatByWeChatId(string operationsWeChatId);
        void DeleteUser_OperationsWeChat_MappingByweChatId(string weChatId);
        List<AccountInfo> GetCustomerByAccountName(string accountName);
        Customer GetCustomerByMobile(string mobile);
        List<Customer> GetCustomerList(string id);
        #endregion
    }
}
