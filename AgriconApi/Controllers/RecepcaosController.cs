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
    public class RecepcaosController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        // GET: api/Recepcaos
        public IQueryable<Recepcao> GetRecepcao()
        {
            return db.Recepcao;
        }
        
        // GET: api/Recepcaos/5
        [ResponseType(typeof(Recepcao))]
        public IHttpActionResult GetRecepcao(long safraId, long propriedadeId)
        {
            PropriedadeSafra pp = db.PropriedadeSafra.FirstOrDefault(w => w.SafraId == safraId && w.PropriedadeId == propriedadeId);
            if (pp == null)
                return null;

            Recepcao recepcao = pp.Recepcao.FirstOrDefault();
            recepcao.PropriedadeSafra = null;

            return Ok(recepcao);
        }

        // GET: api/Recepcaos/5
        [ResponseType(typeof(Recepcao))]
        public IHttpActionResult GetRecepcao(long id)
        {
            Recepcao recepcao = db.Recepcao.Find(id);
            if (recepcao == null)
            {
                return NotFound();
            }

            return Ok(recepcao);
        }

        // PUT: api/Recepcaos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecepcao(long id, Recepcao recepcao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recepcao.Id)
            {
                return BadRequest();
            }

            db.Entry(recepcao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecepcaoExists(id))
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

        // POST: api/Recepcaos
        [ResponseType(typeof(Recepcao))]
        public IHttpActionResult PostRecepcao(Recepcao recepcao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recepcao.Add(recepcao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RecepcaoExists(recepcao.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = recepcao.Id }, recepcao);
        }

        // DELETE: api/Recepcaos/5
        [ResponseType(typeof(Recepcao))]
        public IHttpActionResult DeleteRecepcao(long id)
        {
            Recepcao recepcao = db.Recepcao.Find(id);
            if (recepcao == null)
            {
                return NotFound();
            }

            db.Recepcao.Remove(recepcao);
            db.SaveChanges();

            return Ok(recepcao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecepcaoExists(long id)
        {
            return db.Recepcao.Count(e => e.Id == id) > 0;
        }
    }
}