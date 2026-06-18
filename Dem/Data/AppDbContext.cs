using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Data
{
 
        public class AppDbContext : DbContext
        {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<PickupPoint> PickupPoints => Set<PickupPoint>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Замените строку подключения на вашу
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-MGC0MB3;Initial Catalog=DEMО;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уникальные индексы для справочников
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<OrderStatus>().HasIndex(os => os.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Manufacturer>().HasIndex(m => m.Name).IsUnique();
            modelBuilder.Entity<Supplier>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();

            // Каскадное удаление: при удалении заказа удаляются его позиции
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .OnDelete(DeleteBehavior.Cascade);
        }

        
        }
    
}
