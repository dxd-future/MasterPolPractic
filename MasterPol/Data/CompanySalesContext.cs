using System.Data.Entity;
using MasterPol.Models;

namespace MasterPol.Data
{
    public class CompanySalesContext : DbContext
    {
        public CompanySalesContext() : base("name=CompanySalesConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }

    }
}