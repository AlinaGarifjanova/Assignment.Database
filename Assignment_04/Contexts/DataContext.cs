using Assignment_04.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Contexts
{
    public class DataContext : DbContext
    {

        // Konstruktorn för DataContext som tar emot DbContextOptions
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // Representerar databastabeller för varje entitet
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<CustomerTypeEntity> CustomersType { get; set; }
        public DbSet<AddressEntity> Address { get; set; }
        public DbSet<CategoryEntity> Categorys { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<RegionEntity> Region { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }

        // Metod som körs vid konfiguration av modellen
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera nyckeln för OrderDetailsEntity som består av OrderId och ProductId
            modelBuilder.Entity<OrderDetailsEntity>().HasKey(x => new { x.OrderId, x.ProductId });
        }
    }
}
