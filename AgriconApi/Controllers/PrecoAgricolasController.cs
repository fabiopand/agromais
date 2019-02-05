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
using AgriconApi.Filters;
using AgriconApi.Models;

namespace AgriconApi.Controllers
{
    [Authorize]
    public class PrecoAgricolasController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        // GET: api/PrecoAgricolas
        public IQueryable<PrecoAgricola> GetPrecoAgricola()
        {
            return db.PrecoAgricola;
        }

        // GET: api/PrecoAgricolas/5
        [ResponseType(typeof(PrecoAgricola))]
        public IHttpActionResult GetPrecoAgricola(long id)
        {
            PrecoAgricola precoAgricola = db.PrecoAgricola.Find(id);
            if (precoAgricola == null)
            {
                return NotFound();
            }

            return Ok(precoAgricola);
        }

        // PUT: api/PrecoAgricolas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrecoAgricola(long id, PrecoAgricola precoAgricola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != precoAgricola.Id)
            {
                return BadRequest();
            }

            db.Entry(precoAgricola).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecoAgricolaExists(id))
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

        // POST: api/PrecoAgricolas
        [ResponseType(typeof(PrecoAgricola))]
        public IHttpActionResult PostPrecoAgricola(PrecoAgricola precoAgricola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PrecoAgricola.Add(precoAgricola);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PrecoAgricolaExists(precoAgricola.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = precoAgricola.Id }, precoAgricola);
        }

        // DELETE: api/PrecoAgricolas/5
        [ResponseType(typeof(PrecoAgricola))]
        public IHttpActionResult DeletePrecoAgricola(long id)
        {
            PrecoAgricola precoAgricola = db.PrecoAgricola.Find(id);
            if (precoAgricola == null)
            {
                return NotFound();
            }

            db.PrecoAgricola.Remove(precoAgricola);
            db.SaveChanges();

            return Ok(precoAgricola);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrecoAgricolaExists(long id)
        {
            return db.PrecoAgricola.Count(e => e.Id == id) > 0;
        }
    }
}