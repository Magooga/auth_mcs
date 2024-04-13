using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : IEntity<long>
   {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }     
        public List<User> Users { get; set; } // navigation property // для связи с таблицей User через UserRole // у каждой роли может быть несколько юзеров
        public List<UserRole> UserRoles { get; set; } // navigation property // для связи с таблицей UserRole // связь с третьей таблицей для нормировки связи многие ко многим
        public DateTime CreateDate { get; set; }
        public DateTime UpDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}