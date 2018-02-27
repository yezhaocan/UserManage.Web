using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManage.Service;
using UserManage.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using UserManage.Web.Common;
using System;
using UserManage.Entity.System;
using UserManage.Entity;
using UserManage.Base.Common;
using UserManage.Web.Models;
using UserManage.Web.VerifyCode;

namespace UserManage.Web
{
    public class AccountController : WebController
    {
        const string VerifyCodeKey = "session_verifycode";
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password/*经过md5加密后的密码*/, string verifyCode)
        {
            if (verifyCode.IsNullOrEmpty())
                return FailedMsg("请输入验证码");
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
                return FailedMsg("用户名/密码不能为空");

            string code = HttpContext.GetSession(VerifyCodeKey);
            HttpContext.RemoveSession(VerifyCodeKey);
            if (code.IsNullOrEmpty() || code.ToLower() != verifyCode.ToLower())
            {
                return FailedMsg("验证码错误，请重新输入");
            }

            userName = userName.Trim();
            var accountAppService = CreateService<IAccountAppService>();
            string ip = HttpContext.GetClientIP();

            if (!accountAppService.CheckLogin(userName, password, out Sys_User user, out string msg))
            {
                CreateService<ISysLogAppService>().Log("", userName, ip,"用户[{0}]登录失败：{1}".ToFormat(userName, msg));
                return FailedMsg(msg);
            }

            AdminSession session = new AdminSession
            {
                UserId = user.Id,
                UserName = user.UserName,
                RealName = user.RealName,
                RoleId = user.RoleId,
                LoginIP = ip,
                LoginTime = DateTime.Now,
                IsAdmin = user.UserName.ToLower() == AppConsts.AdminUserName
            };

            CurrentSession = session;

            CreateService<ISysLogAppService>().Log("登录成功");
            return SuccessMsg(msg);
        }
        public ActionResult Logout()
        {
            CurrentSession = null;
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult VerifyCode()
        {
            string verifyCode = VerifyCodeGenerator.GenVerifyCode();
            byte[] bytes = VerifyCodeHelper.GetVerifyCodeByteArray(verifyCode);

            HttpContext.SetSession(VerifyCodeKey, verifyCode);

            return File(bytes, @"image/Gif");
        }


        [LoginAuthorize]
        [HttpGet]
        public ActionResult Index()
        {
            var service = CreateService<IAccountAppService>();
            Sys_User user = service.GetById<Sys_User>(CurrentSession.UserId);
            Sys_Role role = string.IsNullOrEmpty(user.RoleId) ? null : service.GetById<Sys_Role>(user.RoleId);
            UserModel model = new UserModel
            {
                User = user,
                RoleName = role?.Name
            };
            ViewBag.UserInfo = model;
            ViewBag.WeChat = CreateService<IAccountAppService>().GetOperationsWeChatByUserId(CurrentSession.UserId);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPassword">明文</param>
        /// <param name="newPassword">明文</param>
        /// <returns></returns>
        [LoginAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            CreateService<IAccountAppService>().ChangePassword(oldPassword, newPassword);
            return SuccessMsg("密码修改成功");
        }
        [LoginAuthorize]
        [HttpPost]
        public ActionResult ModifyInfo(Service.Models.ModifyAccountInfoInput input)
        {
            CreateService<IAccountAppService>().ModifyInfo(input);
            return SuccessMsg("修改成功");
        }
    }
}
