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
    public class inspeccionesController : ApiController
    {
        private OnlineEntities db = new OnlineEntities();

        // GET: api/inspecciones
        public IQueryable<inspecciones> Getinspecciones()
        {
            return db.inspecciones;
        }

        // GET: api/inspecciones/5
        [ResponseType(typeof(inspecciones))]
        public async Task<IHttpActionResult> Getinspecciones(string id)
        {
            inspecciones inspecciones = await db.inspecciones.FindAsync(id);
            if (inspecciones == null)
            {
                return NotFound();
            }

            return Ok(inspecciones);
        }

        // PUT: api/inspecciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putinspecciones(string id, inspecciones inspecciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inspecciones.idInspeccion)
            {
                return BadRequest();
            }

            db.Entry(inspecciones).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!inspeccionesExists(id))
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

        // POST: api/inspecciones
        [ResponseType(typeof(inspecciones))]
        public async Task<IHttpActionResult> Postinspecciones(inspecciones inspecciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.inspecciones.Add(inspecciones);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (inspeccionesExists(inspecciones.idInspeccion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = inspecciones.idInspeccion }, inspecciones);
        }

        // DELETE: api/inspecciones/5
        [ResponseType(typeof(inspecciones))]
        public async Task<IHttpActionResult> Deleteinspecciones(string id)
        {
            inspecciones inspecciones = await db.inspecciones.FindAsync(id);
            if (inspecciones == null)
            {
                return NotFound();
            }

            db.inspecciones.Remove(inspecciones);
            await db.SaveChangesAsync();

            return Ok(inspecciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool inspeccionesExists(string id)
        {
            return db.inspecciones.Count(e => e.idInspeccion == id) > 0;
        }
    }
}