using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManage.Web.Common;
using UserManage.Entity.System;
using UserManage.Base;
using UserManage.Service.Interfaces;
using UserManage.Service.Models;
using System;
using UserManage.Base.Exceptions;
using UserManage.Base.Common;
using UserManage.Base.Security;
using UserManage.Base.IdStrategy;
using UserManage.Entity.distribution;
using System.Linq;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.SystemManage)]
    [LoginAuthorizeAttribute]
    public class UserController : WebController
    {
        // GET: SystemManage/User
        public ActionResult Index()
        {
            List<SelectOption> roleList = SelectOption.CreateList(this.CreateService<IAccountAppService>().GetSys_RoleList());

            List<SelectOption> WeChatList = SelectOption.CreateList(this.CreateService<IAccountAppService>().GetOperationsWeChatList2());

            ViewBag.WeChatList = WeChatList;
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<Sys_User> pagedData = CreateService<IAccountAppService>().GetSys_UserPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }
        [HttpGet]
        public ActionResult GetWeChats(string userId)
        {
            List<OperationsWeChat> pagedData = CreateService<IAccountAppService>().GetOperationsWeChatByUserId(userId);
            return SuccessData(pagedData);
        }

        [HttpPost]
        public ActionResult Add(AddUserInput input)
        {
            var services = CreateService<IAccountAppService>();
            if (services.ExistUserName(input.UserName))
            {
                throw new InvalidDataException("用户名[{0}]已存在".ToFormat(input.UserName));
            }
            else {
                string userSecretkey = UserHelper.GenUserSecretkey();
                string encryptedPassword = PasswordHelper.Encrypt(input.Password, userSecretkey);
                Sys_User user = new Sys_User
                {
                    UserName = input.UserName,
                    RoleId = input.RoleId,
                    RealName = input.RealName,
                    IsEnabled = input.IsEnabled,
                    Description = input.Description,
                    UserSecretkey = userSecretkey,
                    UserPassword = encryptedPassword,
                    CreateUserId = CurrentSession.UserId,
                    CreateTime =DateTime.Now,
                    LogOnCount=0
                };
                services.Insert(user);
            }            
            return AddSuccessMsg();
        }
        [HttpPost]
        public ActionResult Update(UpdateUserInput input)
        {
            input.Validate();
            var services = this.CreateService<IAccountAppService>();
           
            var user = services.GetById<Sys_User>(input.Id);
            user.RoleId = input.RoleId;
            user.RealName = input.RealName;
            user.IsEnabled = input.IsEnabled;
            user.Description = input.Description;
            user.LastModifyUserId = CurrentSession.UserId;
            user.LastModifyTime = DateTime.Now;
            services.Update(user);            
            return this.UpdateSuccessMsg();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            var services = CreateService<IAccountAppService>();
            services.Delete<Sys_User>(id);
            return this.DeleteSuccessMsg();
        }

        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RevisePassword(string userId, string newPassword)
        {
            if (userId.IsNullOrEmpty())
                return this.FailedMsg("userId 不能为空");

            this.CreateService<IAccountAppService>().RevisePassword(userId, newPassword);
            return SuccessMsg("重置密码成功");
        }
        
       [HttpPost]
        public ActionResult BindWeChat(string userId, string OperationsWeChatId)
        {
            var services = CreateService<IAccountAppService>();
            if (userId.IsNullOrEmpty())
                return FailedMsg("userId 不能为空");
            User_OperationsWeChat_Mapping user_OperationsWeChat_Mapping = new User_OperationsWeChat_Mapping
            {
                UserId = userId,
                OperationsWeChatId = OperationsWeChatId
            };
            if (!services.GetOperationsWeChatByWeChatId(OperationsWeChatId).IsEnabled)
            {
                return FailedMsg("该客服已被禁用");
            }
            if (services.GetSys_UserByOperationsWeChatId(OperationsWeChatId) != null)
            {
                return FailedMsg("该客服微信已经绑定了账号");
            }
            services.Insert(user_OperationsWeChat_Mapping);
            return SuccessMsg("绑定客服微信成功");
        }
        [HttpPost]
        public ActionResult UnbundledWeChat(string weChatId)
        {
            var services = CreateService<IAccountAppService>();
            if (weChatId.IsNullOrEmpty())
                return FailedMsg("weChatId 不能为空");
            services.DeleteUser_OperationsWeChat_MappingByweChatId(weChatId);
            return SuccessMsg("解绑成功");
        }
    }
}