using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ProductArticle { get; set; } = string.Empty;
        public int Quantity { get; set; }

        [NotMapped]
        public string ProductName => Product?.Name ?? "";

        [NotMapped]
        public decimal ProductPrice => Product?.Price ?? 0;

        [NotMapped]
        public decimal TotalSum => ProductPrice * Quantity;

        // Навигационные свойства
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;


    }
}
