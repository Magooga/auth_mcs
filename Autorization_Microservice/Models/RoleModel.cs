namespace Autorization_Microservice.Models
{
    public class RoleModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted 
    }
}
