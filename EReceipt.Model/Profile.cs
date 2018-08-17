using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EReceipt.Model
{
    public class Profile
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname  { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image  { get; set; }

        // One to One Profile Relationship
        public virtual aspnet_Users aspnet_Users { get; set; }

        // One Profile to Many Relationship
        //public virtual List<Company> Companies { get; set; }
    }
}
