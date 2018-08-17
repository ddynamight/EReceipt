using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EReceipt.Model;
using Newtonsoft.Json;

namespace EReceipt.Web.Api
{
    public class InvoicesController : ApiController
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: api/Invoices
        public IQueryable<Invoice> GetInvoices()
        {
            return db.Invoices;
        }

        // GET: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> GetInvoice(Guid id)
        {
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.Id)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Invoices
        [ResponseType(typeof(Invoice))]
        [AcceptVerbs("OPTIONS", "POST")]
        public async Task<IHttpActionResult> PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = invoice.Id }, invoice);

            string url = "http://localhost:8087/Receipt/Details/" + invoice.Id.ToString();

            return Redirect(url);
        }

        // DELETE: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> DeleteInvoice(Guid id)
        {
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(Guid id)
        {
            return db.Invoices.Count(e => e.Id == id) > 0;
        }
    }
}