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
using ajustevonline.Models;

namespace ajustevonline.UltControllers
{
    public class InspeccionesUltsController : ApiController
    {
        private MockUltEntities db = new MockUltEntities();

        // GET: api/InspeccionesUlts
        public IQueryable<InspeccionesUlt> GetInspeccionesUlt()
        {
            return db.InspeccionesUlt;
        }

        // GET: api/InspeccionesUlts/5
        [ResponseType(typeof(InspeccionesUlt))]
        public async Task<IHttpActionResult> GetInspeccionesUlt(int id)
        {
            InspeccionesUlt inspeccionesUlt = await db.InspeccionesUlt.FindAsync(id);
            if (inspeccionesUlt == null)
            {
                return NotFound();
            }

            return Ok(inspeccionesUlt);
        }

        // PUT: api/InspeccionesUlts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInspeccionesUlt(int id, InspeccionesUlt inspeccionesUlt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inspeccionesUlt.Id)
            {
                return BadRequest();
            }

            db.Entry(inspeccionesUlt).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspeccionesUltExists(id))
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

        // POST: api/InspeccionesUlts
        [ResponseType(typeof(InspeccionesUlt))]
        public async Task<IHttpActionResult> PostInspeccionesUlt(InspeccionesUlt inspeccionesUlt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InspeccionesUlt.Add(inspeccionesUlt);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = inspeccionesUlt.Id }, inspeccionesUlt);
        }

        // DELETE: api/InspeccionesUlts/5
        [ResponseType(typeof(InspeccionesUlt))]
        public async Task<IHttpActionResult> DeleteInspeccionesUlt(int id)
        {
            InspeccionesUlt inspeccionesUlt = await db.InspeccionesUlt.FindAsync(id);
            if (inspeccionesUlt == null)
            {
                return NotFound();
            }

            db.InspeccionesUlt.Remove(inspeccionesUlt);
            await db.SaveChangesAsync();

            return Ok(inspeccionesUlt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InspeccionesUltExists(int id)
        {
            return db.InspeccionesUlt.Count(e => e.Id == id) > 0;
        }
    }
}