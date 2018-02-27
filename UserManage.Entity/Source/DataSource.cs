using UserManage.Entity.distribution;

namespace UserManage.Entity.Source
{
    public class DataSource
    {
        public int Id { get; set; }
        /// <summary>
        /// 商城账号（iwms=>null）
        /// </summary>
        public string AccountName { get; set; }

        public string CustomerName { get; set; }

        public string Mobile { get; set; }

        public OriginType Origin { get; set; }
    }
}
