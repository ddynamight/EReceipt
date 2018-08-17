using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EReceipt.Model;

namespace EReceipt.Web.Controllers
{
    public class DashboardController : Controller
    {
        private EReceiptModel db = new EReceiptModel();


        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            ViewData["Companies"] = await db.Companies.CountAsync(e => e.aspnet_Users.UserName == User.Identity.Name);

            ViewData["Invoice"] = await db.Invoices.CountAsync(e => e.Company.aspnet_Users.UserName == User.Identity.Name);

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}