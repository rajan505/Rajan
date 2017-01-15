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

namespace NYCPaymentPortal.Controllers
{
    public class PaymentController : Controller
    {
        private DOHMHPortalContext db = new DOHMHPortalContext();       

        // GET: Payment
        public ActionResult Index()
        {
            //List<UserModel> users = UserModel.getUsers();
            //return View(users);

            //if (Session["Documents"] != null)
            //{
            //    return View((List<Document>)Session["Documents"]);
            //}
            //Session["Documents"] = db.Documents.ToList();
            //TempData["PaymentRequest"] = PaymentRequest;
            
            return View(db.Documents.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Edit(int id, string category, string fileName)
        {
            Document document = db.Documents.Find(id);
            document.Category = category;
            document.FileName = fileName;
            db.SaveChanges();
            return Json(new { document }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            //List<Document> document = (List<Document>)Session["Documents"];
            //document = document.Where(p => p.ID != id).ToList();
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            //Session["Documents"] = document;
            bool result = true;
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]    
        //To add Records into database     
        public JsonResult AddDocumentsRow(Document doc)
        {
            db.Documents.Add(doc);
            db.SaveChanges();
            bool result = true;
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult PortalSearch()
        //{
        //    return View(db.PRSearchs.ToList());
        //}
        //public ActionResult PortalNewRequestSearch()
        //{
        //    return View(db.PRNewRequests.ToList());
        //}
        public ActionResult PaymentRequestView()
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var users = context.PaymentRequests.ToList() ;
                return View("PaymentRequest", users);
            }

            
        }
        public PartialViewResult Search(string searchFString, string searchLString, string searchVcodeString)
        {
            //List<User> userListCollection = new List<User>();
            List<PaymentRequest> userListCollection = new List<PaymentRequest>();

            userListCollection = db.PaymentRequests.ToList();

            if (Request.IsAjaxRequest())
            {

                if ((!string.IsNullOrEmpty(searchFString) || !string.IsNullOrEmpty(searchLString)) && userListCollection != null && userListCollection.Count > 0)
                {
                    //var searchedlist = (from list in userListCollection
                    //                    where list.FirstName.IndexOf(searchFString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                        //|| list.UserName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                    && list.LastName.IndexOf(searchLString, StringComparison.OrdinalIgnoreCase) >= 0
                    //                    select list
                    //                        ).ToList();

                    return PartialView("_GridPartialView",null);

                }
                else
                {

                    return PartialView("_GridPartialView", userListCollection);
                }
            }
            else
            {
                return PartialView("_GridPartialView", userListCollection);
            }


        }
    }
}
