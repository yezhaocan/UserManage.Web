using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserManage.Entity.distribution
{
    [Table("User_OperationsWeChat_Mapping")]
    public class User_OperationsWeChat_Mapping
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OperationsWeChatId { get; set; }
    }
}
