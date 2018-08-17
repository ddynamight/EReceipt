using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EReceipt.Model
{
    public class Invoice
    {
        public Invoice()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Description = "This is the payment for a product xyz";
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        // One to Many Invoice Relationships
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
