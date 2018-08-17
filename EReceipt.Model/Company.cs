using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EReceipt.Model
{
    public class Company
    {
        public Company()
        {
            this.Id = Guid.NewGuid();
            this.Token = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid Token  { get; set; }


        // One to Many Company Relationship

        public Guid aspnet_UsersUserId { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }


        // One Company to Many Relationship
        public virtual List<Invoice> Invoices { get; set; }


    }
}
