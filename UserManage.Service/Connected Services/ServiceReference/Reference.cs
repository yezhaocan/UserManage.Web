﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.AdInfo_WebServiceSoap")]
    public interface AdInfo_WebServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetProductAdInfoByType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetProductAdInfoByTypeAsync(int type, int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetProductAdInfoByWineId", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetProductAdInfoByWineIdAsync(string wineId, int picSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNewProductAdInfoByWineId", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetNewProductAdInfoByWineIdAsync(string wineId, int picSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNewProductAdInfoByAreaIdAndPrice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetNewProductAdInfoByAreaIdAndPriceAsync(string areaId, string price, int needcount, int picSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNewProductAdInfoByAreaIdPriceAndColumnType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetNewProductAdInfoByAreaIdPriceAndColumnTypeAsync(string areaId, string price, int needcount, int picSize, int columnType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNewProductAdInfoByChateauIdAndPrice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetNewProductAdInfoByChateauIdAndPriceAsync(string ChateauId, int needcount, int picSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertProductAdInfoClickRecord", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> InsertProductAdInfoClickRecordAsync(int fromType, int productId, int isSeriesValue, int type, int productType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetProductInShopOnLine", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetProductInShopOnLineAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetShopTopicCategoryInfoList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetShopTopicCategoryInfoListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetShopTopicCategoryTopList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetShopTopicCategoryTopListAsync(int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllShopTopicCategoryList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetAllShopTopicCategoryListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertMallWineInfoOfHK", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> InsertMallWineInfoOfHKAsync(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCustomerInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> GetCustomerInfoAsync(string param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    public interface AdInfo_WebServiceSoapChannel : ServiceReference.AdInfo_WebServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "0.5.0.0")]
    public partial class AdInfo_WebServiceSoapClient : System.ServiceModel.ClientBase<ServiceReference.AdInfo_WebServiceSoap>, ServiceReference.AdInfo_WebServiceSoap
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AdInfo_WebServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(AdInfo_WebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), AdInfo_WebServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AdInfo_WebServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AdInfo_WebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AdInfo_WebServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AdInfo_WebServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AdInfo_WebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> GetProductAdInfoByTypeAsync(int type, int count)
        {
            return base.Channel.GetProductAdInfoByTypeAsync(type, count);
        }
        
        public System.Threading.Tasks.Task<string> GetProductAdInfoByWineIdAsync(string wineId, int picSize)
        {
            return base.Channel.GetProductAdInfoByWineIdAsync(wineId, picSize);
        }
        
        public System.Threading.Tasks.Task<string> GetNewProductAdInfoByWineIdAsync(string wineId, int picSize)
        {
            return base.Channel.GetNewProductAdInfoByWineIdAsync(wineId, picSize);
        }
        
        public System.Threading.Tasks.Task<string> GetNewProductAdInfoByAreaIdAndPriceAsync(string areaId, string price, int needcount, int picSize)
        {
            return base.Channel.GetNewProductAdInfoByAreaIdAndPriceAsync(areaId, price, needcount, picSize);
        }
        
        public System.Threading.Tasks.Task<string> GetNewProductAdInfoByAreaIdPriceAndColumnTypeAsync(string areaId, string price, int needcount, int picSize, int columnType)
        {
            return base.Channel.GetNewProductAdInfoByAreaIdPriceAndColumnTypeAsync(areaId, price, needcount, picSize, columnType);
        }
        
        public System.Threading.Tasks.Task<string> GetNewProductAdInfoByChateauIdAndPriceAsync(string ChateauId, int needcount, int picSize)
        {
            return base.Channel.GetNewProductAdInfoByChateauIdAndPriceAsync(ChateauId, needcount, picSize);
        }
        
        public System.Threading.Tasks.Task<string> InsertProductAdInfoClickRecordAsync(int fromType, int productId, int isSeriesValue, int type, int productType)
        {
            return base.Channel.InsertProductAdInfoClickRecordAsync(fromType, productId, isSeriesValue, type, productType);
        }
        
        public System.Threading.Tasks.Task<string> GetProductInShopOnLineAsync()
        {
            return base.Channel.GetProductInShopOnLineAsync();
        }
        
        public System.Threading.Tasks.Task<string> GetShopTopicCategoryInfoListAsync()
        {
            return base.Channel.GetShopTopicCategoryInfoListAsync();
        }
        
        public System.Threading.Tasks.Task<string> GetShopTopicCategoryTopListAsync(int count)
        {
            return base.Channel.GetShopTopicCategoryTopListAsync(count);
        }
        
        public System.Threading.Tasks.Task<string> GetAllShopTopicCategoryListAsync()
        {
            return base.Channel.GetAllShopTopicCategoryListAsync();
        }
        
        public System.Threading.Tasks.Task<string> InsertMallWineInfoOfHKAsync(string data)
        {
            return base.Channel.InsertMallWineInfoOfHKAsync(data);
        }
        
        public System.Threading.Tasks.Task<string> GetCustomerInfoAsync(string param)
        {
            return base.Channel.GetCustomerInfoAsync(param);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AdInfo_WebServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.AdInfo_WebServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.AdInfo_WebServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://api.wine-world.com:30005/WebServices/AdInfo_WebService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.AdInfo_WebServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://api.wine-world.com:30005/WebServices/AdInfo_WebService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            AdInfo_WebServiceSoap,
            
            AdInfo_WebServiceSoap12,
        }
    }
}
