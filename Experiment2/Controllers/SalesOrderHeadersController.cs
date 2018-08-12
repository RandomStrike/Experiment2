using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Experiment2.Models;

namespace Experiment2.Controllers
{
    public class SalesOrderHeadersController : Controller
    {
        private AdventureWorks2014Entities db = new AdventureWorks2014Entities();
       

        // GET: SalesOrderHeaders
        public ActionResult Index()
        {
            DateTime dtStart = Convert.ToDateTime(  System.Web.HttpContext.Current.Session["StartDate"]);
            DateTime dtEnd = Convert.ToDateTime(System.Web.HttpContext.Current.Session["EndDate"]);

            var q = (from soh in db.SalesOrderHeaders
                     join sod in db.SalesOrderDetails on soh.SalesOrderID equals sod.SalesOrderID
                     join c in db.Customers on soh.CustomerID equals c.CustomerID
                     join be in db.BusinessEntities on c.CustomerID equals be.BusinessEntityID
                     join p in db.People on be.BusinessEntityID equals p.BusinessEntityID
                     //Store Name May be though Sales order header SalesPersonID
                     join sp in db.SalesPersons on soh.SalesPersonID equals sp.BusinessEntityID into spj
                     from ssp in spj.DefaultIfEmpty()
                     join s1 in db.Stores on soh.SalesPersonID equals s1.SalesPersonID into sj
                     from subs1 in sj.DefaultIfEmpty()
                     //Store Name may be though Customer
                     join s2 in db.Stores on c.StoreID equals s2.BusinessEntityID into ssj
                     from subs2 in ssj.DefaultIfEmpty()
                     where soh.OrderDate >= dtStart && soh.OrderDate <= dtEnd  
                     orderby p.LastName, soh.SalesOrderNumber

                     select new
                     {
                         SoldBy = ((soh.SalesPersonID == 0) ? ((c.StoreID == 0) ? string.Empty : subs2.Name) : subs1.Name),
                         SoldTo = String.Concat(p.FirstName," ",p.LastName),
                         soh.AccountNumber ,
                         InvoiceNumber = soh.SalesOrderNumber ,
                         soh.PurchaseOrderNumber, 
                         soh.SalesOrderID  ,
                         soh.OrderDate,
                         soh.DueDate ,
                         InvoiceTotal = soh.TotalDue  ,
                         ProductNumber = sod.ProductID ,
                         sod.OrderQty ,
                         UnitNet = sod.UnitPrice ,
                         sod.LineTotal
                     }
                    );

            var reporting = new List<ReportModel>();

            foreach (var t in q)
            {
                reporting.Add(new ReportModel()
                {
                    SoldBy = t.SoldBy,
                    SoldTo = t.SoldTo,
                    AccountNumber = t.AccountNumber,
                    InvoiceNumber = t.InvoiceNumber,
                    PurchaseOrderNumber = t.PurchaseOrderNumber,
                    SalesOrderID = t.SalesOrderID,
                    OrderDate = t.OrderDate,
                    DueDate = t.DueDate,
                    TotalDue = t.InvoiceTotal,
                    ProductNumber = t.ProductNumber,
                    Quantity = t.OrderQty,
                    UnitPrice = t.UnitNet,
                    LineTotal = t.LineTotal
                });
            }

            return View(reporting.ToList());

        }

        //// POST: SalesOrderHeaders/SetStartDate?startdate="09/1/2011"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetStartDate(string startdate)
        {
            System.Web.HttpContext.Current.Session["StartDate"] = startdate;
            return Json(new { Status = "Success" });
        }

        //// POST: SalesOrderHeaders/SetEndDate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetEndDate(string enddate)
        {
            System.Web.HttpContext.Current.Session["EndDate"] = enddate;
            return Json(new { Status = "Success" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
