using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "Client";
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ContactInfo { get; set; } = "";

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Article { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; } = "шт";
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string? ImagePath { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer? Manufacturer { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal FinalPrice => Price * (1 - Discount / 100);
        public bool HasDiscount => Discount > 0;

        // Полный путь к изображению
        [NotMapped]
        public string FullImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return "pack://application:,,,/Images/picture.png";

                return $"pack://application:,,,/Images/{ImagePath}";
            }
        }
    }

    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; }
        public string? PickupPointAddress { get; set; }
        public int UserId { get; set; }
        public string? PickupCode { get; set; }
        public int StatusId { get; set; }
        public decimal TotalAmount { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("StatusId")]
        public virtual OrderStatus? Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public decimal DiscountAtOrder { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        public decimal Total => Quantity * PriceAtOrder * (1 - DiscountAtOrder / 100);
    }

    public class StoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FurnitureStoreDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.PriceAtOrder).HasPrecision(18, 2);
        }
    }
}