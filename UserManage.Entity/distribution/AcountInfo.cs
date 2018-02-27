using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserManage.Entity.distribution
{

    [Table("AccountInfo")]
    public class AccountInfo
    {
        [Key]
        public int Id { get; set; }
        public string AccountName { get; set; }

        public string CustomerName { get; set; }

        public string Mobile { get; set; }
    }
}
