using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace NYCPaymentPortal.Models
{
    public class Document
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<string> _categoryList = new List<string>() { "Invoice", "Payment Authorization Memo", "Receiving Report", "Inspection Report", "Email", "Timesheets", "Receipts", "Supporting Document", "Other" };
        public List<string> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; }
        }
    }


    //public partial class Invoice
    //{
        
    //    public int PaymentID { get; set; }
    //    public string InvoiceNumber { get; set; }
    //    public string SubInvoice { get; set; }
    //    public Nullable<int> InvoiceAmount { get; set; }
    //    public Nullable<System.DateTime> ServicePeriodFrom { get; set; }
    //    public Nullable<System.DateTime> ServicePeriodTo { get; set; }
    //    public Nullable<System.DateTime> InvoiceDate { get; set; }
    //    public Nullable<System.DateTime> InvoiceSubmissionDate { get; set; }
    //    public string InvoiceDescription { get; set; }
    //    public string AttentionTo { get; set; }
    //    public string PRNumber { get; set; }
    //    public string PRStatus { get; set; }

    //}
    //public class SearchCriterionModel
    //{
    //    public string Keyword { get; set; }
    //}

    public class SearchResultModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
    
}

