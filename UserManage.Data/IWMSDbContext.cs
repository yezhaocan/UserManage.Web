using Microsoft.EntityFrameworkCore;
using UserManage.Entity.distribution;
using UserManage.Entity.System;

namespace UserManage.Data
{
    public class IWMSDbContext : DbContext
    {
        public IWMSDbContext(DbContextOptions<IWMSDbContext> options):base(options)
        {

        }
    }
}
