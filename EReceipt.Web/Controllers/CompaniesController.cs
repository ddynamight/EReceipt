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
    [Authorize]
    public class CompaniesController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            return View(await db.Companies.Include("Invoices").Where(e => e.aspnet_Users.UserName == User.Identity.Name).ToArrayAsync());
        }

        // GET: Companies/Create

        public ActionResult Create()
        {
            return View();
        }

        // GET: Companies/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company company)
        {

            company.aspnet_Users = await db.aspnet_Users.SingleAsync(e => e.UserName == User.Identity.Name);

            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.Entry(company).State = EntityState.Added;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(company);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}