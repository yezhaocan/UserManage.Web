using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserManage.Entity.distribution
{
    public class CustomerModel
    {
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerWeChatId { get; set; }
        public string CustomerWeChatNick { get; set; }
        public int Origin { get; set; }
        public int  State { get; set; }
        public DateTime OperatingTime { get; set; }
        public string OperationsWeChatId { get; set; }
        public string Description { get; set; }


    }
}
