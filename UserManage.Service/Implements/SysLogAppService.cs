using System;
using System.Reflection;
using UserManage.Base.IdStrategy;
using UserManage.Data.IRepository;
using UserManage.Entity;
using UserManage.Entity.System;
using UserManage.Service;
using UserManage.Service.Interfaces;

namespace UserManage.Service.Implements
{
    public class SysLogAppService : AdminAppService, ISysLogAppService
    {
        private readonly IAccountRepository _accountRepository;

        public SysLogAppService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Log(string description,LogType logType= LogType.Operation)
        {
            Sys_Log log = CreateLog(logType,  description);
            _accountRepository.Insert(log);
        }
        public void Log(string userId, string realName, string ip, string description, LogType logType = LogType.Operation)
        {
            Sys_Log log = CreateLog(userId, realName, ip, logType, description);
            _accountRepository.Insert(log);
        }

        /// <summary>
        /// 创建一个实体。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">为 null 则使用 IdHelper.CreateGuid()</param>
        /// <returns></returns>
        protected T CreateEntity<T>(object id = null) where T : new()
        {
            T entity = new T();
            Type entityType = typeof(T);

            entity.SetPropertyValue("Id", id ?? IdHelper.CreateGuid());
            entity.SetPropertyValue("CreationTime", DateTime.Now);

            if (Session != null)
                entity.SetPropertyValue("CreateUserId", Session.UserId);

            PropertyInfo prop_IsDeleted = entityType.GetProperty("IsValid");
            if (prop_IsDeleted != null)
            {
                prop_IsDeleted.SetValue(entity, false);
            }

            return entity;
        }
        Sys_Log CreateLog(LogType logType, string description)
        {
            string userId = null;
            string realName = null;
            string ip = null;

            if (Session != null)
            {
                userId = Session.UserId;
                realName = Session.RealName;
                ip = Session.LoginIP;
            }

            return CreateLog(userId, realName, ip, logType, description);
        }
        Sys_Log CreateLog(string userId, string realName, string ip, LogType logType,string description)
        {
            Sys_Log entity = new Sys_Log
            {
                UserId = userId,
                RealName = realName,
                Type = logType,
                IP = ip,
                Description = description,
                CreationTime = DateTime.Now
            };

            return entity;
        }
    }
}
