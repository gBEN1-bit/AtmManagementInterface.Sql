using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class CustomerDetails
    {
        public CustomerDetails()
        {
            Lgas = new HashSet<Lgas>();
        }

        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Otp { get; set; }
        public bool IsOtpvalidated { get; set; }
        public DateTime OtpGeneratedDateTime { get; set; }
        public int? TblStatestateId { get; set; }

        public virtual States TblStatestate { get; set; }
        public virtual ICollection<Lgas> Lgas { get; set; }
    }
}
