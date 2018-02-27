using System;
using Microsoft.AspNetCore.Mvc;
using UserManage.Web.Common;
using UserManage.Service.Interfaces;
using UserManage.Base;
using UserManage.Entity.distribution;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.Distribution)]
    [LoginAuthorize]
    public class WeChatController : WebController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<OperationsWeChat> pagedData = CreateService<IAccountAppService>().GetWeChatPageData(pagination, keyword);
            return SuccessData(pagedData);
        }

        [HttpPost]
        public ActionResult Add(OperationsWeChat input)
        {
            input.CreateTime = DateTime.Now;
            this.CreateService<IAccountAppService>().Insert(input);
            return AddSuccessMsg();
        }
        [HttpPost]
        public ActionResult Update(OperationsWeChat input)
        {
            var entity = CreateService<IAccountAppService>().GetById<OperationsWeChat>(input.Id);
            entity.WeChatId = input.WeChatId;
            entity.Mobile = input.Mobile;
            entity.WeChatNick = input.WeChatNick;
            entity.Description = input.Description;
            entity.IsEnabled = input.IsEnabled;
            CreateService<IAccountAppService>().Update(entity);
            return UpdateSuccessMsg();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (id.IsNullOrEmpty())
                return FailedMsg("id 不能为空");

            this.CreateService<IAccountAppService>().Delete<OperationsWeChat>(int.Parse(id));
            return DeleteSuccessMsg();
        }

    }
}