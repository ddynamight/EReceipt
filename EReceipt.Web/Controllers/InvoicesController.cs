using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EReceipt.Model;
using Newtonsoft.Json;

namespace EReceipt.Web.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Invoices
        public async Task<ActionResult> Index()
        {
            return View(await db.Invoices.Include("Company").Where(e => e.Company.aspnet_Users.UserName == User.Identity.Name).ToArrayAsync());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}