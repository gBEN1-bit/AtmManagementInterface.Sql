using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class States
    {
        public States()
        {
            CustomerDetails = new HashSet<CustomerDetails>();
            Lgas = new HashSet<Lgas>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<CustomerDetails> CustomerDetails { get; set; }
        public virtual ICollection<Lgas> Lgas { get; set; }
    }
}
