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
    public class PropriedadesController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        public PropriedadesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Propriedades
        public IQueryable<Propriedade> GetPropriedade()
        {
            return db.Propriedade;
        }

        [ResponseType(typeof(Propriedade))]
        public IQueryable<Propriedade> GetPropriedade(long safraId, long produtorId)
        {
            Produtor produtor = db.Produtor.FirstOrDefault(w => w.Id == produtorId);
            if (produtor == null)
                return null;

            produtor.Propriedade = db.Propriedade.Where(w => w.ProdutorId == produtorId).ToList();

            List<long> propriedadeIds = produtor.Propriedade.Select(w => w.Id).ToList();

            return db.PropriedadeSafra.Where(w => 
                w.SafraId == safraId && 
                propriedadeIds.Contains(w.PropriedadeId.Value)
            ).Select(w => w.Propriedade);
        }

        // GET: api/Propriedades/5
        [ResponseType(typeof(Propriedade))]
        public IHttpActionResult GetPropriedade(long id)
        {
            Propriedade propriedade = db.Propriedade.Find(id);

            propriedade.Produtor = db.Produtor.Where(w => w.Id == propriedade.ProdutorId).FirstOrDefault();

            if (propriedade == null)
            {
                return NotFound();
            }

            return Ok(propriedade);
        }

        // PUT: api/Propriedades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropriedade(long id, Propriedade propriedade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propriedade.Id)
            {
                return BadRequest();
            }

            db.Entry(propriedade).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropriedadeExists(id))
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

        // POST: api/Propriedades
        [ResponseType(typeof(Propriedade))]
        public IHttpActionResult PostPropriedade(Propriedade propriedade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Propriedade.Add(propriedade);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PropriedadeExists(propriedade.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = propriedade.Id }, propriedade);
        }

        // DELETE: api/Propriedades/5
        [ResponseType(typeof(Propriedade))]
        public IHttpActionResult DeletePropriedade(long id)
        {
            Propriedade propriedade = db.Propriedade.Find(id);
            if (propriedade == null)
            {
                return NotFound();
            }

            db.Propriedade.Remove(propriedade);
            db.SaveChanges();

            return Ok(propriedade);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropriedadeExists(long id)
        {
            return db.Propriedade.Count(e => e.Id == id) > 0;
        }
    }
}