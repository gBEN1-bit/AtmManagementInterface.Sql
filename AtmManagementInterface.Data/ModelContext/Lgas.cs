using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class Lgas
    {
        public int Lgaid { get; set; }
        public string Lganame { get; set; }
        public int StateId { get; set; }
        public int CustomerId { get; set; }

        public virtual CustomerDetails Customer { get; set; }
        public virtual States State { get; set; }
    }
}
