using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class UserRole : IEntity<long>
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long User_Id { get; set; }
        public User User { get; set; }
        public long Role_Id { get; set; }
        //public string Role_Name { get; set; }
        public Role Role { get; set; }
        public DateTime UpDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}
