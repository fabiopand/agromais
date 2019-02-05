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
    public class DespesasController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        // GET: api/Despesas
        public IQueryable<Despesas> GetDespesas()
        {
            return db.Despesas;
        }
        
        [ResponseType(typeof(Despesas))]
        public IHttpActionResult GetDespesas(long propriedadeId, long safraId)
        {
            PropriedadeSafra propriedadeSafra = db.PropriedadeSafra.FirstOrDefault(w =>
                w.SafraId == safraId &&
                w.PropriedadeId.HasValue &&
                w.PropriedadeId.Value == propriedadeId);

            if (propriedadeSafra == null)
                return null;

            Despesas despesas = propriedadeSafra.Despesas.FirstOrDefault();
            despesas.PropriedadeSafra = null;
            return Ok(despesas);
        }

        // GET: api/Despesas/5
        [ResponseType(typeof(Despesas))]
        public IHttpActionResult GetDespesas(long id)
        {
            Despesas despesas = db.Despesas.Find(id);
            if (despesas == null)
            {
                return NotFound();
            }

            return Ok(despesas);
        }

        // PUT: api/Despesas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDespesas(long id, Despesas despesas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != despesas.Id)
            {
                return BadRequest();
            }

            db.Entry(despesas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DespesasExists(id))
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

        // POST: api/Despesas
        [ResponseType(typeof(Despesas))]
        public IHttpActionResult PostDespesas(Despesas despesas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Despesas.Add(despesas);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DespesasExists(despesas.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = despesas.Id }, despesas);
        }

        // DELETE: api/Despesas/5
        [ResponseType(typeof(Despesas))]
        public IHttpActionResult DeleteDespesas(long id)
        {
            Despesas despesas = db.Despesas.Find(id);
            if (despesas == null)
            {
                return NotFound();
            }

            db.Despesas.Remove(despesas);
            db.SaveChanges();

            return Ok(despesas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DespesasExists(long id)
        {
            return db.Despesas.Count(e => e.Id == id) > 0;
        }
    }
}