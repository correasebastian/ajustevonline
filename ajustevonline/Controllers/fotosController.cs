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

namespace ajustevonline.Controllers
{
    public class fotosController : ApiController
    {
        private OnlineEntities db = new OnlineEntities();

        // GET: api/fotos

        public IQueryable<fotos> Getfotos()
        {
            return db.fotos;
        }

        // GET: api/fotos/5
        [ResponseType(typeof(fotos))]
        public async Task<IHttpActionResult> Getfotos(string id)
        {
            fotos fotos = await db.fotos.FindAsync(id);
            if (fotos == null)
            {
                return NotFound();
            }

            return Ok(fotos);
        }

        // PUT: api/fotos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putfotos(string id, fotos fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fotos.idFoto)
            {
                return BadRequest();
            }

            db.Entry(fotos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!fotosExists(id))
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

        // POST: api/fotos
        [ResponseType(typeof(fotos))]
        public async Task<IHttpActionResult> Postfotos(fotos fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.fotos.Add(fotos);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (fotosExists(fotos.idFoto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fotos.idFoto }, fotos);
        }

        // DELETE: api/fotos/5
        [ResponseType(typeof(fotos))]
        public async Task<IHttpActionResult> Deletefotos(string id)
        {
            fotos fotos = await db.fotos.FindAsync(id);
            if (fotos == null)
            {
                return NotFound();
            }

            db.fotos.Remove(fotos);
            await db.SaveChangesAsync();

            return Ok(fotos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool fotosExists(string id)
        {
            return db.fotos.Count(e => e.idFoto == id) > 0;
        }
    }
}