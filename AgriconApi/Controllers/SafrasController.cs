using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AgriconApi.Models;

namespace AgriconApi.Controllers
{
    [Authorize]
    public class SafrasController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        public SafrasController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Safras
        public IQueryable<Safra> GetSafra()
        {
            return db.Safra;
        }

        // GET: api/Safras/5
        [ResponseType(typeof(Safra))]
        public IHttpActionResult GetSafra(long id)
        {
            Safra safra = db.Safra.Find(id);
            if (safra == null)
            {
                return NotFound();
            }

            return Ok(safra);
        }

        // PUT: api/Safras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSafra(long id, Safra safra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != safra.Id)
            {
                return BadRequest();
            }

            db.Entry(safra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafraExists(id))
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

        // POST: api/Safras
        [ResponseType(typeof(Safra))]
        public IHttpActionResult PostSafra(Safra safra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Safra.Add(safra);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SafraExists(safra.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = safra.Id }, safra);
        }

        // DELETE: api/Safras/5
        [ResponseType(typeof(Safra))]
        public IHttpActionResult DeleteSafra(long id)
        {
            Safra safra = db.Safra.Find(id);
            if (safra == null)
            {
                return NotFound();
            }

            db.Safra.Remove(safra);
            db.SaveChanges();

            return Ok(safra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SafraExists(long id)
        {
            return db.Safra.Count(e => e.Id == id) > 0;
        }
    }
}