using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserManage.Entity.distribution
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerWeChatId { get; set; }
        public string CustomerWeChatNick { get; set; }
        public OriginType Origin { get; set; }
        public StateType  State { get; set; }
        public DateTime OperatingTime { get; set; }
        public string OperationsWeChatId { get; set; }
        public string Description { get; set; }
        public DateTime? SyncTime { get; set; }

    }
    public enum OriginType
    {
        /// <summary>
        /// （天猫）拉维莱特酒类专营店
        /// </summary>
        TMall = 1,
        /// <summary>
        /// （京东）红酒世界酒类旗舰店
        /// </summary>
        JD = 2,
        /// <summary>
        /// （苏宁）红酒世界酒类专营店
        /// </summary>
        SUNING = 3,
        /// <summary>
        /// （国美）红酒世界酒类专营店
        /// </summary>
        GuoMei = 4,
        /// <summary>
        /// (百度Mall)旗舰店
        /// </summary>
        BaiDuMall = 5,
        /// <summary>
        /// (顺丰)红酒世界
        /// </summary>
        SF = 10,
        /// <summary>
        /// 一号店
        /// </summary>
        YHD = 11,
        /// <summary>
        /// 天虹
        /// </summary>
        TianHong = 12,
        /// <summary>
        /// (建行)红酒世界官方旗舰店
        /// </summary>
        JH = 13,
        /// <summary>
        /// （中信）红酒世界官方旗舰店
        /// </summary>
        ZhongXin = 14,
        /// <summary>
        /// （工行）红酒世界官方旗舰店
        /// </summary>
        GongHang = 15,
        /// <summary>
        /// 中信1855列级套装
        /// </summary>
        ZX1855_1 = 16,
        /// <summary>
        /// 中信1855列级庄套装
        /// </summary>
        ZX1855_2 = 17,
        /// <summary>
        /// 中信勃艮第之夜套装
        /// </summary>
        ZX_bgd = 18,
        /// <summary>
        /// 中信波尔多左右岸对比品鉴活动
        /// </summary>
        ZX_bed = 19,
        /// <summary>
        /// 批发客户订单店铺
        /// </summary>
        //Pifa = 20,
        /// <summary>
        /// 中民保险经纪
        /// </summary>
        ZhongMin = 21,
        /// <summary>
        /// 中信春节临时店铺
        /// </summary>
        ZXCJ = 22,
        /// <summary>
        /// 中信春节临时店铺
        /// </summary>
        ZXCJ2 = 817,
        /// <summary>
        /// 红酒世界淘宝企业店铺
        /// </summary>
        TaoBao = 23,
        /// <summary>
        /// (京东)波尔多列级庄专营店
        /// </summary>
        JD2 = 26,
        /// <summary>
        /// 商城
        /// </summary>
        Mall = 6,
        /// <summary>
        /// 亚马逊
        /// </summary>
        YMX=27,
        /// <summary>
        /// 民生银行员工福利商城
        /// </summary>
        MSYH = 28,
        /// <summary>
        /// 交通银行交博汇团购
        /// </summary>
        JTYH = 29,
        /// <summary>
        /// 杭州礼管家平台
        /// </summary>
        HZLGJ = 30,
        /// <summary>
        /// 浦发银行信用卡网上商城
        /// </summary>
        PFYH = 31,
        /// <summary>
        /// 未记录
        /// </summary>
        //NULL=32,
        /// <summary>
        /// 翼社区
        /// </summary>
        YSQ = 33
    }    
    
    public enum StateType
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Untreated=1,
        /// <summary>
        /// 已联系，未加微信
        /// </summary>
        contactedNoWeChat= 2,
        /// <summary>
        /// 已加微信
        /// </summary>
        AddWeChat = 3,

    }
}
