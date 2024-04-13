using Services.Contracts;

namespace Autorization_Microservice.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Byte[] Salt { get; set; } = new Byte[20];   // salt
        public Byte[] Hash { get; set; } = new Byte[20];   // hash
        public DateTime CreateDate { get; set; }
        public DateTime UpDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}
