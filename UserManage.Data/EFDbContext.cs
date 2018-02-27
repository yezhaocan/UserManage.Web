using Microsoft.EntityFrameworkCore;
using UserManage.Entity.distribution;
using UserManage.Entity.Source;
using UserManage.Entity.System;

namespace UserManage.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
        }

        public EFDbContext(DbContextOptions<EFDbContext> options):base(options)
        {

        }
        public DbSet<Sys_User> User { get; set; }
        public DbSet<Sys_Log> Log { get; set; }
        public DbSet<Sys_RoleAuthorize> Sys_RoleAuthorize { get; set; }
        public DbSet<Sys_Role> Sys_Role { get; set; }
        public DbSet<Sys_Module> Sys_Module { get; set; }
        public DbSet<OperationsWeChat> OperationsWeChat { get; set; }
        public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<User_OperationsWeChat_Mapping> User_OperationsWeChat_Mapping { get; set; }
    }
}
