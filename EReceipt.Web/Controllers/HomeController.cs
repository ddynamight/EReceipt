using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EReceipt.Model;

namespace EReceipt.Web.Controllers
{
    public class HomeController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Home
        public ActionResult Index()
        {
            ViewData["invoiceCount"] = db.Invoices.Count();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}