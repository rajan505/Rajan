//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NYCPaymentPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentRequest
    {
        public Nullable<double> VendorCode { get; set; }
        public double PaymentReqId { get; set; }
        public string FMSRegistration { get; set; }
        public string PRStatus { get; set; }
        public Nullable<System.DateTime> VendorInvoiceNumber { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<System.DateTime> InvoiceSubmittedDate { get; set; }
        public string ServicePeriod { get; set; }
        public Nullable<double> InvoiceAmount { get; set; }
    }
}
