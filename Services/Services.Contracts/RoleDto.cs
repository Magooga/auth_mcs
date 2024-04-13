using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpDate { get; set; }
        public bool Deleted { get; set; } // Deleted // 1 - deleted, 0 - not deleted
    }
}
