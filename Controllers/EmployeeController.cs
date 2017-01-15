using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NYCPaymentPortal.Models;
using System.Data.SqlClient;

namespace NYCPaymentPortal.Controllers
{
    public class EmployeeController : Controller
    {

       
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginDetails();
            return View(model);
        }
        // Post method
        [HttpPost]
        public ActionResult Login(LoginDetails model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate user, pass values to db
                if (model.EmailAddress == "rajan")
                {

                }
                else
                { 
                   
                }
            }
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ProcessData()
        {
                        
            string connectionString = @"Data Source=T2UA1282FC3\SQLEXPRESS;Initial Catalog=Northwind; Integrated Security=true";
            string queryString = "SELECT * from  PaymentRequest WHERE PaymentID = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        //JQueryDataTables.Models.Invoice objPayment = new JQueryDataTables.Models.Invoice();
                        //objPayment.InvoiceNumber = reader[0].ToString();
                        //objPayment.SubInvoice = reader[0].ToString();
                        ////objPayment.InvoiceAmount = Convert.ToDouble(reader[0].ToString());
                        //objPayment.ServicePeriodFrom = Convert.ToDateTime(reader[0].ToString());
                        //objPayment.ServicePeriodTo = Convert.ToDateTime(reader[0].ToString());
                        ////objPayment.InvoiceSubmissionDate = Convert.ToDateTime(reader[0].ToString());
                        //objPayment.InvoiceDate = Convert.ToDateTime(reader[0].ToString());
                        //objPayment.InvoiceDescription = reader[0].ToString();
                        //objPayment.AttentionTo = reader[0].ToString();
                        //objPayment.PRNumber = reader[0].ToString();
                        //objPayment.PRStatus = reader[0].ToString();

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                }
            }

           
            return View("AddPaymenentRequest");
        }
        [HttpPost]
        public ActionResult ProcessData(string submit)
        {
            switch (submit)
            {
                case "Save":
                    ViewData["FeaturedProduct"] = "Hellow World";
                    return View("AddPaymenentRequest");
                    break;
                case "Process":
                    // Do something
                    break;
                default:
                    break;
            }
            return View("AddPaymenentRequest");
        }

        [HttpGet]
        //public JsonResult getEmployees()
        //{
        //    var jsonData = new
        //    {
        //        data = from emp in db.Employees.ToList() select emp
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult AddRequest()
        {
            return View("AddRequest");
        }

        public ActionResult AddPaymenentRequest()
        {
            return View("AddPaymenentRequest");
        }


        
    }
}