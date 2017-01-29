using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NYCPaymentPortal.Models;
using System.Data.SqlClient;
using System.Linq.Dynamic;

namespace NYCPaymentPortal.Controllers
{
    public class PaymentController : Controller
    {
        private DOHMHPortalContext db = new DOHMHPortalContext();



        // GET: Payment
        public ActionResult Index()
        {
            //if (Session["Documents"] != null)
            //{
            //    return View((List<Document>)Session["Documents"]);
            //}
            //Session["Documents"] = db.Documents.ToList();
            //TempData["PaymentRequest"] = PaymentRequest;
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        public ActionResult PRDetails(int PRRequestID)
        {

            return View("PRDetails");
        }

        //[HttpPost]
        //public JsonResult Edit(int id, string category, string fileName)
        //{
        //    Document document = db.Documents.Find(id);
        //    document.Category = category;
        //    document.FileName = fileName;
        //    db.SaveChanges();
        //    return Json(new { document }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost, ActionName("Delete")]
        //public JsonResult DeleteConfirmed(int id)
        //{
        //    //List<Document> document = (List<Document>)Session["Documents"];
        //    //document = document.Where(p => p.ID != id).ToList();
        //    Document document = db.Documents.Find(id);
        //    db.Documents.Remove(document);
        //    db.SaveChanges();
        //    //Session["Documents"] = document;
        //    bool result = true;
        //    return Json(new { result }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]    
        ////To add Records into database     
        //public JsonResult AddDocumentsRow(Document doc)
        //{
        //    db.Documents.Add(doc);
        //    db.SaveChanges();
        //    bool result = true;
        //    return Json(new { result }, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult PortalSearch()
        //{
        //    return View(db.PRSearchs.ToList());
        //}
        //public ActionResult PortalNewRequestSearch()
        //{
        //    return View(db.PRNewRequests.ToList());
        //}
        public ActionResult PaymentRequestView(int pageSize = 1, string sort = "custid", string sortDir = "ASC")
        {
            //var totalRows = docModel.CountDocument();

            bool Dir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? true : false;

            //var document = docModel.GetDocumentPage(page, pageSize, sort, Dir);
            //var data = new PagedDocumentModel()
            //{
            //    TotalRows = totalRows,
            //    PageSize = pageSize,
            //    Document = document
            //};

            using (NorthwindEntities context = new NorthwindEntities())
            {
                return View("PaymentRequest", context.PaymentRequests.ToList());
            }
        }
        public PartialViewResult PaymentGridSearch(string txtPRNumber, string txtVendorInvoice, string ddlVendorCode, string ddlPRStatus, string FMSRegistration, string txtMHYContract)
        {
            List<PaymentRequest> paymentCollection = new List<PaymentRequest>();

            paymentCollection = db.PaymentRequests.ToList();

            if (Request.IsAjaxRequest())
            {

                if ((!string.IsNullOrEmpty(txtPRNumber) || !string.IsNullOrEmpty(txtVendorInvoice) || !string.IsNullOrEmpty(ddlVendorCode) || !string.IsNullOrEmpty(ddlPRStatus) || !string.IsNullOrEmpty(FMSRegistration) || !string.IsNullOrEmpty(txtMHYContract))
                    && paymentCollection != null && paymentCollection.Count > 0)
                {
                    //var searchedlist = (from list in paymentCollection
                    //                    where list.FirstName.IndexOf(searchFString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                        //|| list.UserName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                    && list.LastName.IndexOf(searchLString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                    select list
                    //                        ).ToList();
                    return PartialView("_GridPartialView", null);

                }
                else
                {

                    return PartialView("_GridPartialView", paymentCollection);
                }
            }
            else
            {
                return PartialView("_GridPartialView", paymentCollection);
            }


        }


        public ActionResult PaymentRequest(int page = 1, string sort = "PRStatus", string sortdir = "asc", string search = "", int GridpageSize = 5)
        {
            int pageSize = GridpageSize;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetPaymentRequests(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }
        public List<PaymentRequest> GetPaymentRequests(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (NorthwindEntities dc = new NorthwindEntities())
            {
                var v = (from a in dc.PaymentRequests
                         where
                             //a.FirstName.Contains(search) ||
                             //a.LastName.Contains(search) ||
                             //a.EmailID.Contains(search) ||
                                 a.FMSRegistration.Contains(search) ||
                                 a.PRStatus.Contains(search)
                         select a
                             );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }

        public ActionResult VendorSelection()
        {
            var model = new VendorSelection
            {
                SelectedItemIds = new[] { "1", "3" },
                Items = Enumerable.Range(1, 10).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = " item " + x
                })
            };

            ViewBag.CurrentUserContact = lstContact[0]; // current user contact info.
            ViewBag.vendorName= vendorUsers; // current user contact info.

            return View("VendorSelection", model);
        }
        [HttpPost]
        public ActionResult VendorSelection(string[] selectedItemIds)
        {
            // here you will get the list of selected item ids
            // where you could process them
            // If you need to redisplay the same view make sure that 
            // you refetch the model items once again from the database
            return View("VendorSelection");
        }

        public ActionResult Admin()
        {
            ViewBag.CurrentAdminContact = lstContact[0]; // current user contact info.
            return View("Admin");
        }
        [HttpPost]
        public ActionResult Admin(int page = 1, string sort = "VendorName", string sortdir = "asc", int GridpageSize = 5)
        {
            ViewBag.CurrentAdminContact = lstContact[0]; // current user contact info.
            return View("Admin");
        }
        public ActionResult Users(int page = 1, string sort = "VendorName", string sortdir = "asc", int GridpageSize = 5)
        {
            
            ViewBag.CurrentUserContact = lstContact[0]; // current user contact info.
            ViewBag.AdminContactList = lstContact; // possible of multiple admins
            
            // To Do: fetch data from SP and store in object before passing into Model.

            int pageSize = GridpageSize;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetUsers(sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            
            return View("Users", data);
        }
        public List<VendorUser> GetUsers(string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            //using (NorthwindEntities dc = new NorthwindEntities())
            //{
                var v = (from a in vendorUsers select a);
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            //}
        }

        #region Sample Model Data
        public static List<Models.VendorSelection> SelectedVendors = new List<Models.VendorSelection>()
            {
                //new Models.VendorSelection { 

                // vendorname = "ABC Corp",
                // vendorusername = "John Doe",
                // contactNumber = "718-878-8555",
                // email = "abce@gmail.com"
                //},
                //new Models.VendorSelection { 
                // vendorname = "EFTG Corp",
                // vendorusername = "John Doe",
                // contactNumber = "718-878-8555",
                // email = "abce@gmail.com"
                //}
            };
        public static List<Models.ContactInformation> lstContact = new List<Models.ContactInformation>()
        {
            new Models.ContactInformation { 
             username = "John Doe",
             contactNumber = "718-878-8555",
             email = "abce@gmail.com"
            },
            new Models.ContactInformation { 
             username = "Joe Smith",
             contactNumber = "718-654-8555",
             email = "abce@gmail.com"
            },
            new Models.ContactInformation { 
             username = "Tom Petty",
             contactNumber = "718-654-8555",
             email = "abce@gmail.com"
            }
        };
        public static List<Models.VendorUser> vendorUsers = new List<Models.VendorUser>()
        {
            new Models.VendorUser { 
             userid = 1,
             FirstName = "John",
             LastName = "Doe",
             Phone = "718-878-8555",
             VendorName = "ABC Co.",
             VendorCode = "VC00115431",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
            userid = 2,
             FirstName = "Steve",
             LastName = "Morrison",
             Phone = "347-878-8555",
             VendorName = "ABC & Sales Co.",
             VendorCode = "VC00159888",
             role = "Vendor Invoice Processor",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 3,
             FirstName = "Mathew",
             LastName = "Hoggard",
             Phone = "718-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 4,
             FirstName = "Mathew",
             LastName = "Hoggard",
             Phone = "718-878-8555",
             VendorName = "ABC Org.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 5,
             FirstName = "Mathew",
             LastName = "Hoggard",
             Phone = "347-878-8555",
             VendorName = "ABC LLC.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 6,
             FirstName = "Paul",
             LastName = "Smith",
             Phone = "929-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 7,
             FirstName = "Steve",
             LastName = "Hoggard",
             Phone = "147-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 8,
             FirstName = "Sabrina",
             LastName = "Powel",
             Phone = "987-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 9,
             FirstName = "Kerrey",
             LastName = "Washington",
             Phone = "646-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            },
            new Models.VendorUser { 
             userid = 10,
             FirstName = "Phil",
             LastName = "Jackson",
             Phone = "718-878-8555",
             VendorName = "ABC Business.",
             VendorCode = "VC0077898",
             role = "Vendor Viewer",
             status = "Approved"
            }
        };
        #endregion 

        #region Documents CRUD
        DocumentModelServices docModel = new DocumentModelServices();

        public PartialViewResult _documents(int pageSize = 5, int page = 1, string sort = "id", string sortDir = "ASC")
        {
            //const int pageSize = 2;
            var totalRows = docModel.CountDocument();

            bool Dir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? true : false;

            var document = docModel.GetDocumentPage(page, pageSize, sort, Dir);
            var data = new PagedDocumentModel()
            {
                TotalRows = totalRows,
                PageSize = pageSize,
                Document = document
            };
            return PartialView("_documents", data);
        }

        public ActionResult Documents(int pageSize = 5, int page = 1, string sort = "id", string sortDir = "ASC")
        {
            //const int pageSize = 2;
            var totalRows = docModel.CountDocument();

            bool Dir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? true : false;

            var document = docModel.GetDocumentPage(page, pageSize, sort, Dir);
            var data = new PagedDocumentModel()
            {
                TotalRows = totalRows,
                PageSize = pageSize,
                Document = document
            };
            return View("WebGridCRUD", data);
        }

        [HttpGet]
        public JsonResult UpdateRecord(int id, string description)
        {
            bool result = false;
            try
            {
                result = docModel.UpdateDocument(id, description);

            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult SaveRecord(string category, string fileName, string description)
        {
            bool result = false;
            try
            {
                result = docModel.SaveDocument(category, fileName, description);
            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRecord(int id)
        {
            bool result = false;
            try
            {
                result = docModel.DeleteDocument(id);

            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
