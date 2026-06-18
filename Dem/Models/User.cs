using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }   // внешний ключ
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;
        public string FirstName {get;set; }
        public string LastName {get;set; }  
        public string Patronymic { get;set; }   
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
