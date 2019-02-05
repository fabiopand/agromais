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
    public class Login
    {
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }

    [Authorize]
    public class ProdutorsController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        public ProdutorsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Produtors
        public IQueryable<Produtor> GetProdutor()
        {
            return db.Produtor;
        }

        // GET: api/Produtors/5
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(Produtor))]
        [Route("api/login")]
        public IHttpActionResult GetLogin([FromBody] Login login)
        {
            Produtor produtor = db.Produtor.Where(w => w.CPF == login.Cpf && w.Senha == login.Senha).FirstOrDefault();
            produtor.Senha = null;

            if (produtor == null)
            {
                return NotFound();
            }

            return Ok(produtor);
        }

        // GET: api/Produtors/5
        [ResponseType(typeof(Produtor))]
        public IHttpActionResult GetProdutor(long id)
        {
            Produtor produtor = db.Produtor.Find(id);
            if (produtor == null)
            {
                return NotFound();
            }

            return Ok(produtor);
        }

        // PUT: api/Produtors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProdutor(long id, Produtor produtor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produtor.Id)
            {
                return BadRequest();
            }

            db.Entry(produtor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutorExists(id))
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

        // POST: api/Produtors
        [ResponseType(typeof(Produtor))]
        public IHttpActionResult PostProdutor(Produtor produtor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produtor.Add(produtor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProdutorExists(produtor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = produtor.Id }, produtor);
        }

        // DELETE: api/Produtors/5
        [ResponseType(typeof(Produtor))]
        public IHttpActionResult DeleteProdutor(long id)
        {
            Produtor produtor = db.Produtor.Find(id);
            if (produtor == null)
            {
                return NotFound();
            }

            db.Produtor.Remove(produtor);
            db.SaveChanges();

            return Ok(produtor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutorExists(long id)
        {
            return db.Produtor.Count(e => e.Id == id) > 0;
        }
    }
}