using System.Numerics;

namespace Domain.Entities
{
    public class User : IEntity<long>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Byte[] Salt { get; set; } = new Byte[20];   // salt // for password
        public Byte[] Hash { get; set; } = new Byte[20];   // hash // for password
        public List<Role> Roles { get; set; } // navigation property // для связи с таблицей Role через UserRole // у каждого user может быть несколько ролей
        public List<UserRole> UserRoles { get; set; } // navigation property // для связи с таблицей UserRole // связь с третьей таблицей для нормировки связи многие ко многим 
        public DateTime CreateDate { get; set; }
        public DateTime UpDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}