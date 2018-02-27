using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManage.Base;
using UserManage.Base.Application;
using UserManage.Entity.distribution;
using UserManage.Entity.System;
using UserManage.Service.Models;
using UserManage.Entity.Source;

namespace UserManage.Service.Interfaces
{
    public interface IAccountAppService : IAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password">经过md5加密后的密码</param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckLogin(string userName, string password, out Sys_User user, out string msg);
        List<Sys_Role> GetSys_RoleList(string keyword=null);
        List<Sys_RoleAuthorize> GetSys_RoleAuthorizeList(string roleId);
        List<Sys_Module> GetMenuList(string roleId);
        List<OperationsWeChat> GetOperationsWeChatList();
        List<SimpleModel> GetOperationsWeChatList2();
        List<Sys_Module> GetSys_ModuleList();
        PagedData<Sys_User> GetSys_UserPageData(Pagination pagination, string keyword);
        PagedData<OperationsWeChat> GetWeChatPageData(Pagination pagination, string keyword);

        void AddModule(AddModuleInput input);
        void UpdateModule(UpdateModuleInput input);
        #region entity 操作
        TEntity GetById<TEntity>(object key) where TEntity : class;
        bool Delete<TEntity>(object key) where TEntity : class;
        bool Insert<TEntity>(TEntity entity) where TEntity : class;
        bool InsertRange<TEntity>(List<TEntity> entity) where TEntity : class;
        List<OperationsWeChat> GetOperationsWeChatByUserId(string userId);
        bool Update<TEntity>(TEntity entity) where TEntity : class;
        List<AccountInfo> GetCustomerByAccountName(string accountName);
        DbSet<TEntity> Get<TEntity>() where TEntity : class;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPassword">明文</param>
        /// <param name="newPassword">明文</param>
        void ChangePassword(string oldPassword, string newPassword);
        void ModifyInfo(ModifyAccountInfoInput input);
        void RevisePassword(string userId, string newPassword);
        bool ExistUserName(string userName);
        Sys_User GetSys_UserByUserName(string userName);
        Sys_User GetSys_UserByOperationsWeChatId(string weChatId);
        bool DeleteSys_RoleAuthorize(string roleId);
        OperationsWeChat GetOperationsWeChatByWeChatId(string operationsWeChatId);
        void DeleteUser_OperationsWeChat_MappingByweChatId(string weChatId);
        Customer GetCustomerByMobile(string mobile);
    }
}
