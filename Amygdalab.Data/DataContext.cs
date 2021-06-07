using Amygdalab.Core.Identity;
using Amygdalab.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Data
{
    public class DataContext : DbContext
    {
       
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
              .HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Product>()
        .Property(p => p.CostPrice)
        .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
      .Property(p => p.SellingPrice)
      .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductHistory>()
 .Property(p => p.CostPrice)
 .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ProductHistory>()
      .Property(p => p.SellingPrice)
      .HasColumnType("decimal(18,2)");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
