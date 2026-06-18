using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Product
    {
        [Key]
        public string Article { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? SupplierId { get; set; }       // FK → Supplier
        public int? ManufacturerId { get; set; }   // FK → Manufacturer
        public int? CategoryId { get; set; }       // FK → Category
        public int Discount { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? PhotoFileName { get; set; }

        // Навигационные свойства
        public Supplier? Supplier { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Category? Category { get; set; }
    }
}
