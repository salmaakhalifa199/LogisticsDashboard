using LogisticsApis.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApis.Data
{
    public class LogisticsAPIDbContext : DbContext
    {
        public LogisticsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }


    }
}
