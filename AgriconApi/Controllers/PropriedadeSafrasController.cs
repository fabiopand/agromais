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
    public class PropriedadeSafrasController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        // GET: api/PropriedadeSafras
        public IQueryable<PropriedadeSafra> GetPropriedadeSafra()
        {
            return db.PropriedadeSafra;
        }

        // GET: api/PropriedadeSafras/5
        [ResponseType(typeof(PropriedadeSafra))]
        public IHttpActionResult GetPropriedadeSafra(long propriedadeId, long safraId)
        {
            PropriedadeSafra propriedadeSafra = db.PropriedadeSafra.FirstOrDefault(w => w.PropriedadeId == propriedadeId && w.SafraId == safraId);

            if (propriedadeSafra == null)
            {
                return NotFound();
            }

            return Ok(propriedadeSafra);
        }

        // GET: api/PropriedadeSafras/5
        [ResponseType(typeof(PropriedadeSafra))]
        public IHttpActionResult GetPropriedadeSafra(long id)
        {
            PropriedadeSafra propriedadeSafra = db.PropriedadeSafra.Find(id);
            if (propriedadeSafra == null)
            {
                return NotFound();
            }

            return Ok(propriedadeSafra);
        }

        // PUT: api/PropriedadeSafras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropriedadeSafra(long id, PropriedadeSafra propriedadeSafra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propriedadeSafra.Id)
            {
                return BadRequest();
            }

            db.Entry(propriedadeSafra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropriedadeSafraExists(id))
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

        // POST: api/PropriedadeSafras
        [ResponseType(typeof(PropriedadeSafra))]
        public IHttpActionResult PostPropriedadeSafra(PropriedadeSafra propriedadeSafra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropriedadeSafra.Add(propriedadeSafra);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PropriedadeSafraExists(propriedadeSafra.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = propriedadeSafra.Id }, propriedadeSafra);
        }

        // DELETE: api/PropriedadeSafras/5
        [ResponseType(typeof(PropriedadeSafra))]
        public IHttpActionResult DeletePropriedadeSafra(long id)
        {
            PropriedadeSafra propriedadeSafra = db.PropriedadeSafra.Find(id);
            if (propriedadeSafra == null)
            {
                return NotFound();
            }

            db.PropriedadeSafra.Remove(propriedadeSafra);
            db.SaveChanges();

            return Ok(propriedadeSafra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropriedadeSafraExists(long id)
        {
            return db.PropriedadeSafra.Count(e => e.Id == id) > 0;
        }
    }
}