using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public string StatusName => Name ?? "Неизвестно";
    }
}
