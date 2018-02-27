using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManage.Base;
using UserManage.Entity.distribution;
using UserManage.Service.Interfaces;
using UserManage.Web.Common;
using System;
using UserManage.Entity.Source;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Text;

namespace UserManage.Web.Areas
{
    [Area(AreaNames.Distribution)]
    [LoginAuthorize]
    public class MyCustomerController : WebController
    {
        private IHostingEnvironment _hostingEnvironment;
        public MyCustomerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult Index()
        {
            List<SelectOption> WeChatList = SelectOption.CreateList(CreateService<IAccountAppService>().GetOperationsWeChatList2());
            ViewBag.Origins = Origins();
            ViewBag.States = States();
            ViewBag.WeChatList = WeChatList;
            return View();
        }
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            var userId = CurrentSession.UserId;
            PagedData<CustomerModel> pagedData = CreateService<IDistributionAppService>().GetCustomerModelPageData(pagination, keyword, userId);
            return SuccessData(pagedData);
        }
        public ActionResult Transfer()
        {
            List<SelectOption> WeChatList = SelectOption.CreateList(CreateService<IAccountAppService>().GetOperationsWeChatList2());
            ViewBag.Origins = Origins();
            ViewBag.States = States();
            ViewBag.WeChatList = WeChatList;
            return View();
        }
        [HttpPost]
        public ActionResult Import(IFormFile excelfile)
        {
            var services = CreateService<IDistributionAppService>();
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            try
            {
                using (FileStream fs = new FileStream(file.ToString(), FileMode.Create))
                {
                    excelfile.CopyTo(fs);
                    fs.Flush();
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    List<DataSource> list  = new List<DataSource>();
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataSource customer = new DataSource
                        {
                            AccountName = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                            CustomerName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Mobile = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Origin = (OriginType)Convert.ToInt32(worksheet.Cells[row, 4].Value),                           
                        };
                        list.Add(customer);
                    }
                    services.DistributionDataSiurce(list,false);
                    return SuccessMsg();
                }
            }
            catch (Exception ex)
            {
                return SuccessMsg("导入错误:"+ex.Message);
            }
        }
        private List<SelectOption> Origins() {
            List<SelectOption> Origins = new List<SelectOption>() {
                new SelectOption{Text = "红酒世界网上商城",Value = "6"},
                new SelectOption{Text = "（天猫）拉维莱特酒类专营店",Value = "1" },
                new SelectOption{Text = "（京东）红酒世界酒类旗舰店",Value = "2" },
                new SelectOption{Text = "（苏宁）红酒世界酒类专营店",Value = "3"},
                new SelectOption{Text = "（国美）红酒世界酒类专营店",Value = "4" },
                new SelectOption{Text = "(百度Mall)旗舰店",Value = "5" },
                new SelectOption{Text = "(顺丰)红酒世界",Value = "10"},
                new SelectOption{Text = "一号店",Value = "11" },
                new SelectOption{Text = "天虹",Value = "12" },
                new SelectOption{Text = "(建行)红酒世界官方旗舰店",Value = "13"},
                new SelectOption{Text = "（中信）红酒世界官方旗舰店",Value = "14" },
                new SelectOption{Text = "（工行）红酒世界官方旗舰店",Value = "15" },
                new SelectOption{Text = "中信1855列级套装",Value = "16"},
                new SelectOption{Text = "中信1855列级庄套装",Value = "17" },
                new SelectOption{Text = "中信勃艮第之夜套装",Value = "18" },
                new SelectOption{Text = "中信波尔多左右岸对比品鉴活动",Value = "19"},
                new SelectOption{Text = "中民保险经纪",Value = "21"},
                new SelectOption{Text = "中信春节临时店铺",Value = "22" },
                new SelectOption{Text = "红酒世界淘宝企业店铺",Value = "23" },
                new SelectOption{Text = "(京东)波尔多列级庄专营店",Value = "26"},
                new SelectOption{Text = "亚马逊",Value = "27"},
                new SelectOption{Text = "民生银行员工福利商城",Value = "28" },
                new SelectOption{Text = "交通银行交博汇团购",Value = "29" },
                new SelectOption{Text = "杭州礼管家平台",Value = "30"},
                new SelectOption{Text = "浦发银行信用卡网上商城",Value = "31" },
                new SelectOption{Text = "翼社区",Value = "33"}
            };
            return Origins;
        }
        private List<SelectOption> States() {

            List<SelectOption> States = new List<SelectOption>() {
                new SelectOption{Text = "未处理",Value = "1" },
                new SelectOption{Text = "已联系，未加微信",Value = "2" },
                new SelectOption{Text = "已加微信",Value = "3"}
            };
            return States;
        }
        public ActionResult GetAllCustomerModels(Pagination pagination, string keyword)
        {
            PagedData<CustomerModel> pagedData = CreateService<IDistributionAppService>().GetCustomerModelPageData(pagination, keyword);
            return SuccessData(pagedData);
        }
        public ActionResult GetCustomerByAccountName(string AccountName)
        {
            List<AccountInfo> data = new List<AccountInfo>();
            if (AccountName.IsNotNullOrWhiteSpace())
            {
                data = CreateService<IAccountAppService>().GetCustomerByAccountName(AccountName);
            }
            return SuccessData(data);
        }
        public ActionResult BundledCustomer(string customerId, string dataId)
        {
            var services = CreateService<IAccountAppService>();
            var customer = services.GetById<Customer>(customerId);
            var dataSource = services.GetById<AccountInfo>(Convert.ToInt32(dataId));
            customer.CustomerName = dataSource.CustomerName;
            customer.CustomerMobile = dataSource.Mobile;
            customer.OperatingTime = DateTime.Now;
            services.Update(customer);
            return SuccessMsg("操作成功");
        }
        public ActionResult Update(CustomerModel customerModel)
        {
            var services = CreateService<IAccountAppService>();
            var customer = services.GetById<Customer>(customerModel.Id);
            customer.CustomerWeChatId = customerModel.CustomerWeChatId;
            customer.CustomerWeChatNick= customerModel.CustomerWeChatNick;
            customer.State = (StateType) customerModel.State;
            customer.OperatingTime = DateTime.Now;
            customer.Description = customerModel.Description;
            services.Update(customer);
            return UpdateSuccessMsg();
        }
        [HttpPost]
        public ActionResult Transfer(CustomerModel customerModel)
        {
            var services = CreateService<IAccountAppService>();
            var customer = services.GetById<Customer>(customerModel.Id);
            customer.CustomerWeChatId = customerModel.CustomerWeChatId;
            customer.CustomerWeChatNick = customerModel.CustomerWeChatNick;
            customer.State = (StateType)customerModel.State;
            customer.OperationsWeChatId = customerModel.OperationsWeChatId;
            customer.Description = customerModel.Description;
            customer.OperatingTime = DateTime.Now;
            services.Update(customer);
            return UpdateSuccessMsg();
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerModel customerModel)
        {
            var services = CreateService<IAccountAppService>();
            Customer customer = new Customer()
            {
                AccountName = customerModel.AccountName,
                CustomerName = customerModel.CustomerName,
                CustomerMobile = customerModel.CustomerMobile,
                CustomerWeChatId = customerModel.CustomerWeChatId,
                CustomerWeChatNick = customerModel.CustomerWeChatNick,
                Origin = (OriginType)customerModel.Origin,
                State=(StateType)customerModel.State,
                Description=customerModel.Description,
                OperationsWeChatId=customerModel.OperationsWeChatId,
                OperatingTime=DateTime.Now,   
            };
            services.Insert(customer);
            return AddSuccessMsg();
        }
    }
}
