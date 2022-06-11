using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AtmManagementInterface.Data.ModelContext
{
    public partial class TblEjfileUploadedDocument
    {
        public int Id { get; set; }
        public string AtmId { get; set; }
        public string Brand { get; set; }
        public DateTime? TransactionDate { get; set; }
        public TimeSpan? Time { get; set; }
        public string Tns { get; set; }
        public string Pan { get; set; }
        public string TransactionType { get; set; }
        public string Currency { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AvailableAmt { get; set; }
        public decimal? LedgerAmt { get; set; }
        public decimal? SurCharge { get; set; }
        public string SourceAcct { get; set; }
        public string DestinationAcct { get; set; }
        public string Comment { get; set; }
        public string PostedBy { get; set; }
        public DateTime? DatePosted { get; set; }
    }
}
