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
using LiveTree.Models;

namespace LiveTree.Controllers
{
    public class FactoriesController : ApiController
    {
        private LiveTreeContext db = new LiveTreeContext();

        // GET: api/Factories
        public IQueryable<Factory> GetFactories()
        {
            return db.Factories
                .Include(a=>a.Nodes);
        }

        // GET: api/Factories/5
        [ResponseType(typeof(Factory))]
        public async Task<IHttpActionResult> GetFactory(int id)
        {
            Factory factory = await db.Factories.FindAsync(id);
            if (factory == null)
            {
                return NotFound();
            }

            return Ok(factory);
        }

        // PUT: api/Factories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFactory(int id, Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factory.Id)
            {
                return BadRequest();
            }

            db.Entry(factory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactoryExists(id))
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

        // POST: api/Factories
        [ResponseType(typeof(Factory))]
        public async Task<IHttpActionResult> PostFactory(Factory factory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factories.Add(factory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = factory.Id }, factory);
        }

        // DELETE: api/Factories/5
        [ResponseType(typeof(Factory))]
        public async Task<IHttpActionResult> DeleteFactory(int id)
        {
            Factory factory = await db.Factories.FindAsync(id);
            if (factory == null)
            {
                return NotFound();
            }

            db.Factories.Remove(factory);
            await db.SaveChangesAsync();

            return Ok(factory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactoryExists(int id)
        {
            return db.Factories.Count(e => e.Id == id) > 0;
        }
    }
}