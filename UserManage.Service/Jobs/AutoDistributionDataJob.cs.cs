using DotNetEx.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pomelo.AspNetCore.TimedJob;
using ServiceReference;
using System;
using System.Collections.Generic;
using System.Text;
using UserManage.Data.IRepository;
using UserManage.Entity.Source;
using UserManage.Service.Interfaces;
using static ServiceReference.AdInfo_WebServiceSoapClient;

namespace UserManage.Service.Jobs
{
    public class AutoDistributionDataJob : Job
    {
        private const int Interval= 1000 * 3600 * 24 * 7;
        public  const string BeginTime= "2017-12-25 16:50:00";
        [Invoke(Begin = BeginTime, Interval = Interval, SkipWhileExecuting = true)]
        public void Run(IServiceProvider serviceProvide)
        {
            ISysLogAppService _sysLogAppService = serviceProvide.CreateScope().ServiceProvider.GetService<ISysLogAppService>() as ISysLogAppService;
            try
            {
                //执行任务
                IDistributionRepository _distributionRepository = serviceProvide.CreateScope().ServiceProvider.GetService<IDistributionRepository>() as IDistributionRepository;

                var malldate = _distributionRepository.GetLastSynTime();
                DateTime? iwmsdate = _distributionRepository.GetLastSynTime(false);
                //获取IWMS数据
                var result = _distributionRepository.GetDataSource(iwmsdate);
                //同步IWMS数据
                _distributionRepository.DistributionDataSiurce(result,false);
                AdInfo_WebServiceSoapClient adInfo_WebServiceSoapClient = new AdInfo_WebServiceSoapClient(EndpointConfiguration.AdInfo_WebServiceSoap);

                var key = "nrjmR4wjLNliD5dS";
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("EncryptText=");
                stringBuilder.Append(malldate.ToString());
                stringBuilder.Append("sign=");
                stringBuilder.Append(key);
                
                UMParam data = new UMParam() {
                    Date = malldate.ToString(),
                    Ciphertext= MD5Helper.MD5Encrypt(stringBuilder.ToString())//加密
                };
                var param= JsonConvert.SerializeObject(data);
                var mallData=  adInfo_WebServiceSoapClient.GetCustomerInfoAsync(param).Result;
                UMDataSource uMDataSource= JsonConvert.DeserializeObject<UMDataSource>(mallData.ToString());
                if (uMDataSource.Result)
                {
                    _distributionRepository.DistributionDataSiurce(uMDataSource.Data, false);
                    _sysLogAppService.Log("","系统","192.168.1.106","从商城获取客户数据成功,并导入数据库", Entity.LogType.System);
                }
                else
                {
                    _sysLogAppService.Log("", "系统", "192.168.1.106", "从商城获取客户数据失败，错误信息:" + uMDataSource.Msg, Entity.LogType.System);
                }
            }
            catch (Exception e)
            {
                _sysLogAppService.Log("", "系统", "192.168.1.106", "同步失败，错误信息:" +e.Message, Entity.LogType.Error);
            }
        }
    }
    public class UMParam
    {
        public string Date { get; set; }
        //加密结果
        public string Ciphertext { get; set; }
    }
    public class UMDataSource
    {

        public bool Result { get; set; }
        public string Msg { get; set; }
        public List<DataSource> Data { get; set; }
    }
}
