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
    public class ReceiptController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        // GET: Receipt/Details
        public async Task<ActionResult> Details(Guid tag)
        {
            return View(await db.Invoices.SingleAsync(e => e.Id == tag));
        }
    }
}