using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class TblUserInformation
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public DateTime? Age { get; set; }
        public string BranchId { get; set; }
        public string Comment { get; set; }
        public string CompanyId { get; set; }
        public string Department { get; set; }
        public string DeptCode { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string JobTitle { get; set; }
        public string Miscode { get; set; }
        public string Nationality { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinEmail { get; set; }
        public string NextOfKinGender { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhone { get; set; }
        public string PcCode { get; set; }
        public string Phone { get; set; }
        public string Rank { get; set; }
        public string RelationShip { get; set; }
        public string StaffName { get; set; }
        public string StaffNo { get; set; }
        public byte[] Staffsignature { get; set; }
        public string State { get; set; }
        public string Unit { get; set; }
        public string UnitCode { get; set; }
        public bool? Updated { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageTitle { get; set; }
        public int? DesignationCode { get; set; }
        public bool? Approved { get; set; }
        public bool? Disapproved { get; set; }
        public bool? Saved { get; set; }
        public string BranchLocation { get; set; }
        public string CoyName { get; set; }
        public string Status { get; set; }
        public string Locked { get; set; }
    }
}
