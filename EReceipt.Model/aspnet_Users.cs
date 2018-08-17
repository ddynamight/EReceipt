using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EReceipt.Model
{
    public partial class aspnet_Users
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }

        //One aspnet_User to One Entity Relationships
        public virtual aspnet_Membership aspnet_Membership { get; set; }
        public virtual Profile Profile { get; set; }

        // One aspnet_User to Many Entity Relationships
        public virtual List<Company> Companies { get; set; }
    }
}
