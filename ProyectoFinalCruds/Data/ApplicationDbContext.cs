using Microsoft.EntityFrameworkCore;
using ProyectoFinalCruds.Models;

namespace ProyectoFinalCruds.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customers> customers { get; set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Orders> orders { get; set; }

        public DbSet<Products> products { get; set; }
        public DbSet<Product_Categories> product_categories { get; set; }

        public DbSet<Iventories> inventories { get; set; }

        public DbSet<Warehouses> warehouses { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Countries> countries { get; set; }

        public DbSet<Locations> locations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer) 
                .WithMany() 
                .HasForeignKey(o => o.CUSTOMER_ID);

            modelBuilder.Entity<Contacts>()
               .HasOne(o => o.Customer)
               .WithMany()
               .HasForeignKey(o => o.CUSTOMER_ID);

            modelBuilder.Entity<Products>()
               .HasOne(o => o.Categories)
               .WithMany()
               .HasForeignKey(o => o.CATEGORY_ID);

            modelBuilder.Entity<Iventories>()
               .HasOne(o => o.products)
               .WithMany()
               .HasForeignKey(o => o.PRODUCT_ID);

            modelBuilder.Entity<Iventories>()
               .HasOne(o => o.warehouses)
               .WithMany()
               .HasForeignKey(o => o.WAREHOUSE_ID);

            modelBuilder.Entity<Countries>()
               .HasOne(o => o.Regions)
               .WithMany()
               .HasForeignKey(o => o.REGION_ID);
        }
    }
}
