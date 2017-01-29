
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NYCPaymentPortal.Models
{
    public class DOHMHPortalContext : DbContext
    {
        //public DbSet<Student> Students { get; set; }
        //public DbSet<Person> People { get; set; }
        public DbSet<Document> Documents { get; set; }
        //public DbSet<Invoice> Invoices { get; set; }
        //public DbSet<Customer> Customers { get; set; }

        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        //public DbSet<PRNewRequest> PRNewRequests { get; set; }
        public DbSet<PRSearch> PRSearchs { get; set; }
        
        
    }
}