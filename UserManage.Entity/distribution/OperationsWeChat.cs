using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManage.Entity.distribution
{
    /// <summary>
    /// 运营微信
    /// </summary>
    [Table("OperationsWeChat")]
    public class OperationsWeChat
    {
        [Key]
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string WeChatId { get; set; }
        public string WeChatNick { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
    }
}
