using Microsoft.EntityFrameworkCore;
using UserManage.Entity.distribution;
using UserManage.Entity.System;

namespace UserManage.Data
{
    public class MallDbContext : DbContext
    {
        public MallDbContext(DbContextOptions<MallDbContext> options):base(options)
        {

        }
    }
}
