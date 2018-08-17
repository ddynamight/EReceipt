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

namespace EReceipt.Web.Api
{
    public class CompaniesController : ApiController
    {
        private EReceiptModel db = new EReceiptModel();

        // GET: api/Companies
        [AcceptVerbs("OPTIONS", "GET")]
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies.OrderBy(e => e.Name);
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> GetCompany(Guid id)
        {
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompany(Guid id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.Id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Companies.Add(company);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompanyExists(company.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> DeleteCompany(Guid id)
        {
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            await db.SaveChangesAsync();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(Guid id)
        {
            return db.Companies.Count(e => e.Id == id) > 0;
        }
    }
}