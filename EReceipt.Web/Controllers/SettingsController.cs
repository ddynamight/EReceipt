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
    public class SettingsController : Controller
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: Settings
        public async Task<ActionResult> Index()
        {
            return View(await db.Profiles.SingleAsync(e => e.aspnet_Users.UserName == User.Identity.Name));
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}