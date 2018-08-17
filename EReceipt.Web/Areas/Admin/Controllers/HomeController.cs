using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EReceipt.Model;

namespace EReceipt.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Admin/Home
        public async Task<ActionResult> Index()
        {
            #region //Counting Entities
                ViewData["companyCount"] = await db.Companies.CountAsync();
                ViewData["invoiceCount"] = await db.Invoices.CountAsync();
                ViewData["userCount"] = await db.aspnet_Users.CountAsync(e => e.UserName != "admin");
                ViewData["blogCount"] = 0;

            #endregion

            return View();
        }
    }
}