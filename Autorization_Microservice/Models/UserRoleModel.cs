using Domain.Entities;

namespace Autorization_Microservice.Models
{
    public class UserRoleModel
    {
        public long User_Id { get; set; }
        public long Role_Id { get; set; }
        public string Role_name { get; set; }
        public DateTime UpDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}
