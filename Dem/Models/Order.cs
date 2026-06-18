using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int PickupPointId { get; set; }
        public int UserId { get; set; }

        // Внешний ключ на статус
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public OrderStatus Status { get; set; } = null!;

        public string PickupCode { get; set; } = string.Empty;
        [NotMapped]
        public string StatusName => Status?.Name ?? "Неизвестно";

        [NotMapped]
        public string ClientFullName
        {
            get
            {
                if (User == null) return "Не указан";
                return $"{User.LastName} {User.FirstName} {User.Patronymic}".Trim();
            }
        }
        // Навигационные свойства
        public PickupPoint PickupPoint { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
