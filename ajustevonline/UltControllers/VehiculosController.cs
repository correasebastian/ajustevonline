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
    public class VehiculosController : ApiController
    {
        private MockUltEntities db = new MockUltEntities();

        // GET: api/Vehiculos
        public IQueryable<Vehiculos> GetVehiculos()
        {
            return db.Vehiculos;
        }

        // GET: api/Vehiculos/5
        [ResponseType(typeof(Vehiculos))]
        public async Task<IHttpActionResult> GetVehiculos(int id)
        {
            Vehiculos vehiculos = await db.Vehiculos.FindAsync(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            return Ok(vehiculos);
        }

        // PUT: api/Vehiculos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVehiculos(int id, Vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculos.Id)
            {
                return BadRequest();
            }

            db.Entry(vehiculos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculosExists(id))
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

        // POST: api/Vehiculos
        [ResponseType(typeof(Vehiculos))]
        public async Task<IHttpActionResult> PostVehiculos(Vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehiculos.Add(vehiculos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vehiculos.Id }, vehiculos);
        }

        // DELETE: api/Vehiculos/5
        [ResponseType(typeof(Vehiculos))]
        public async Task<IHttpActionResult> DeleteVehiculos(int id)
        {
            Vehiculos vehiculos = await db.Vehiculos.FindAsync(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            db.Vehiculos.Remove(vehiculos);
            await db.SaveChangesAsync();

            return Ok(vehiculos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiculosExists(int id)
        {
            return db.Vehiculos.Count(e => e.Id == id) > 0;
        }
    }
}