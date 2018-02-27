using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManage.Base;
using UserManage.Base.Common;
using UserManage.Base.Exceptions;
using UserManage.Base.Security;
using UserManage.Data.IRepository;
using UserManage.Entity.distribution;
using UserManage.Entity.System;
using UserManage.Service.Interfaces;
using UserManage.Service.Models;
using UserManage.Base.AutoMapper;

namespace UserManage.Service.Implements
{
    public class AccountAppService : AdminAppService, IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISysLogAppService _sysLogAppService;

        public AccountAppService(IAccountRepository accountRepository,ISysLogAppService sysLogAppService)
        {
            _accountRepository = accountRepository;
            _sysLogAppService = sysLogAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password">前端传过来的是经过md5加密后的密码</param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckLogin(string userName, string password, out Sys_User user, out string msg)
        {
            userName.NotNullOrEmpty();
            password.NotNullOrEmpty();

            user = null;
            msg = null;

            var account=_accountRepository.GetSys_UserByUserName(userName);

            if (account == null)
            {
                msg = "账户不存在，请重新输入";
                return false;
            }

            if (!account.IsEnabled)
            {
                msg = "账户被系统锁定,请联系管理员";
                return false;
            }
            string dbPassword = PasswordHelper.EncryptMD5Password(password, account.UserSecretkey);
            if (dbPassword != account.UserPassword)
            {
                msg = "密码不正确，请重新输入";
                return false;
            }

            DateTime lastVisitTime = DateTime.Now;
            account.LogOnCount++;
            account.PreviousVisitTime = account.LastVisitTime;
            account.LastVisitTime = lastVisitTime;
            //更新登陆信息
            _accountRepository.Update(account);
            user = account;
            return true;
        }


        public void RevisePassword(string userId, string newPassword)
        {

            userId.NotNullOrEmpty("用户 Id 不能为空");
            PasswordHelper.EnsurePasswordLegal(newPassword);

            var user = _accountRepository.GetById<Sys_User>(userId);
            if (user == null)
                throw new InvalidDataException("用户不存在");

            string userSecretkey = UserHelper.GenUserSecretkey();
            string encryptedPassword = PasswordHelper.Encrypt(newPassword, userSecretkey);
            user.UserSecretkey = userSecretkey;
            user.UserPassword = encryptedPassword;
            _accountRepository.Update(user);
            _sysLogAppService.Log(user.Id,user.RealName,Session.LoginIP, "重置用户[{0}]密码".ToFormat(userId));
        }

        public bool ExistUserName(string UserName)
        {
            return _accountRepository.ExistUserName(UserName);
        }

        public List<Sys_RoleAuthorize> GetSys_RoleAuthorizeList(string roleId)
        {
            return _accountRepository.GetSys_RoleAuthorizeList(roleId);
        }
        public List<Sys_Module> GetMenuList(string roleId)
        {
            var data = new List<Sys_Module>();
            data = _accountRepository.GetSys_ModuleList(roleId, this.Session.IsAdmin);
            return data;
        }
        public void AddModule(AddModuleInput input)
        {
            input.Validate();
            Sys_Module sys_Module = new Sys_Module();
            AceMapper.Map(input, sys_Module);
            _accountRepository.Insert(sys_Module);
        }

        public void UpdateModule(UpdateModuleInput input)
        {
            input.Validate();
            var entity = _accountRepository.GetById<Sys_Module>(input.Id);
            AceMapper.Map(input, entity);
            _accountRepository.Update(entity);
        }

        public List<Sys_Module> GetSys_ModuleList()
        {
            return _accountRepository.GetSys_ModuleList();
        }
        public List<Sys_Role> GetSys_RoleList(string keyword = null)
        {
           return _accountRepository.GetSys_RoleList(keyword);
        }

        public PagedData<Sys_User> GetSys_UserPageData(Pagination pagination, string keyword)
        {
            var result=  _accountRepository.GetSys_UserList(pagination, keyword);

            foreach (var item in result.DataList)
            {
                item.CustomerCount = _accountRepository.GetCustomerList(item.Id).Count();
            }            
            return result;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="oldPassword">明文</param>
        ///// <param name="newPassword">明文</param>
        public void ChangePassword(string oldPassword, string newPassword)
        {
            PasswordHelper.EnsurePasswordLegal(newPassword);

            AdminSession session = Session;

            var user = _accountRepository.GetSys_UserByUserName(session.UserName);
            string encryptedOldPassword = PasswordHelper.Encrypt(oldPassword, user.UserSecretkey);

            if (encryptedOldPassword != user.UserPassword)
                throw new InvalidDataException("旧密码不正确");

            string newUserSecretkey = UserHelper.GenUserSecretkey();
            string newEncryptedPassword = PasswordHelper.Encrypt(newPassword, newUserSecretkey);
            user.UserSecretkey = newUserSecretkey;
            user.UserPassword = newEncryptedPassword;
            
            if (_accountRepository.Update(user))
            {
                _sysLogAppService.Log(user.Id,user.RealName,session.LoginIP,"用户[{0}]修改密码".ToFormat(session.UserName));

            }//更新密码
        }

        public void ModifyInfo(ModifyAccountInfoInput input)
        {
            input.Validate();
            var session = this.Session;
            Sys_User user = _accountRepository.GetById<Sys_User>(session.UserId);
            user.LastModifyUserId = session.UserId;
            user.LastModifyTime = DateTime.Now;
            user.RealName = input.RealName;
            user.Description = input.Description;
            _accountRepository.Update(user);
        }

        public PagedData<OperationsWeChat> GetWeChatPageData(Pagination pagination, string keyword)
        {

            var result = _accountRepository.GetWeChatList(pagination, keyword);
            return result;
        }
        #region entity 操作

        public TEntity GetById<TEntity>(object key) where TEntity : class
        {
            return _accountRepository.GetById<TEntity>(key);
        }
        public bool Delete<TEntity>(object key) where TEntity : class
        {
            return _accountRepository.Delete<TEntity>(key);
        }
        public bool Insert<TEntity>(TEntity entity) where TEntity : class
        {
            return _accountRepository.Insert<TEntity>(entity);
        }
        public bool InsertRange<TEntity>(List<TEntity> list) where TEntity : class
        {
            return _accountRepository.InsertRange<TEntity>(list);
        }
        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            return _accountRepository.Update<TEntity>(entity);
        }
        public DbSet<TEntity> Get<TEntity>() where TEntity : class
        {
            return _accountRepository.Get<TEntity>();
        }
        #endregion
        public List<OperationsWeChat> GetOperationsWeChatList()
        {
           return _accountRepository.Get<OperationsWeChat>().Where(p=>p.IsEnabled).OrderByDescending(p=>p.CreateTime).ToList();
        }

        public Sys_User GetSys_UserByUserName(string userName)
        {
            return _accountRepository.GetSys_UserByUserName(userName);
        }

        public Sys_User GetSys_UserByOperationsWeChatId(string OperationsWeChatId)
        {
            return _accountRepository.GetSys_UserByOperationsWeChatId(OperationsWeChatId);
        }

        public bool DeleteSys_RoleAuthorize(string roleId)
        {
            return _accountRepository.DeleteSys_RoleAuthorize(roleId);
        }

        public List<SimpleModel> GetOperationsWeChatList2()
        {
           return  _accountRepository.Get<OperationsWeChat>().Where(p => p.IsEnabled).OrderByDescending(p => p.CreateTime)
                .Select(p=>new SimpleModel {
                    Id=p.WeChatId,
                    Name=p.WeChatNick+"("+p.WeChatId+")"
                })
                .ToList();
        }

        public List<OperationsWeChat> GetOperationsWeChatByUserId(string userId)
        {
            return _accountRepository.GetOperationsWeChatByUserId(userId);
        }

        public OperationsWeChat GetOperationsWeChatByWeChatId(string operationsWeChatId)
        {
            return _accountRepository.GetOperationsWeChatByWeChatId(operationsWeChatId);
        }

        public void DeleteUser_OperationsWeChat_MappingByweChatId(string weChatId)
        {
            _accountRepository.DeleteUser_OperationsWeChat_MappingByweChatId(weChatId);
        }

        public List<AccountInfo> GetCustomerByAccountName(string accountName)
        {
            return _accountRepository.GetCustomerByAccountName(accountName);
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return _accountRepository.GetCustomerByMobile(mobile);
        }
    }
}
