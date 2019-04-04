using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Library.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleItem> RoleItems { get; set; }
        public DbSet<PointOfSale> PointOfSales { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Point_Storage> Point_Storages { get; set; }
        public DbSet<IncomingInvoice> IncomingInvoices { get; set; }
        public DbSet<Invoice_Product> Invoice_Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
                       }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
