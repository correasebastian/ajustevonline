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
    public class PathsController : ApiController
    {
        private MockUltEntities db = new MockUltEntities();

        // GET: api/Paths
        public IQueryable<Paths> GetPaths()
        {
            return db.Paths;
        }

        // GET: api/Paths/5
        [ResponseType(typeof(Paths))]
        public async Task<IHttpActionResult> GetPaths(int id)
        {
            Paths paths = await db.Paths.FindAsync(id);
            if (paths == null)
            {
                return NotFound();
            }

            return Ok(paths);
        }

        // PUT: api/Paths/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaths(int id, Paths paths)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paths.Id)
            {
                return BadRequest();
            }

            db.Entry(paths).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PathsExists(id))
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

        // POST: api/Paths
        [ResponseType(typeof(Paths))]
        public async Task<IHttpActionResult> PostPaths(Paths paths)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paths.Add(paths);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = paths.Id }, paths);
        }

        // DELETE: api/Paths/5
        [ResponseType(typeof(Paths))]
        public async Task<IHttpActionResult> DeletePaths(int id)
        {
            Paths paths = await db.Paths.FindAsync(id);
            if (paths == null)
            {
                return NotFound();
            }

            db.Paths.Remove(paths);
            await db.SaveChangesAsync();

            return Ok(paths);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PathsExists(int id)
        {
            return db.Paths.Count(e => e.Id == id) > 0;
        }
    }
}