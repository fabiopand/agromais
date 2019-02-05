using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using AgriconApi.Models;

namespace AgriconApi.Controllers
{
    [Authorize]
    public class ResultadoSimulacaosController : ApiController
    {
        private AgriconWebApiEntities db = new AgriconWebApiEntities();

        public ResultadoSimulacaosController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/ResultadoSimulacaos
        public IQueryable<ResultadoSimulacao> GetResultadoSimulacao()
        {
            IQueryable<ResultadoSimulacao> res = db.ResultadoSimulacao;

            List<PropriedadeSafra> propriedades = db.PropriedadeSafra.ToList();
            List<Safra> safras = db.Safra.ToList();

            res.ForEachAsync(w =>
            {
                PropriedadeSafra ps = propriedades.FirstOrDefault(k => k.Id == w.PropriedadeSafraId);
                if (ps != null)
                {
                    Safra sf = safras.FirstOrDefault(t => t.Id == ps.SafraId);
                    if (sf != null)
                    {                        
                        w.SafraDescricao = sf.Descricao;
                    }
                }
            });

            Thread.Sleep(1000);

            return res;
        }

        // GET: api/ResultadoSimulacaos/5
        [ResponseType(typeof(ResultadoSimulacao))]
        public IHttpActionResult GetResultadoSimulacao(long id)
        {
            ResultadoSimulacao resultadoSimulacao = db.ResultadoSimulacao.Find(id);
            if (resultadoSimulacao == null)
            {
                return NotFound();
            }

            return Ok(resultadoSimulacao);
        }

        // PUT: api/ResultadoSimulacaos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResultadoSimulacao(long id, ResultadoSimulacao resultadoSimulacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resultadoSimulacao.Id)
            {
                return BadRequest();
            }

            db.Entry(resultadoSimulacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadoSimulacaoExists(id))
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

        // POST: api/ResultadoSimulacaos
        [ResponseType(typeof(ResultadoSimulacao))]
        public IHttpActionResult PostResultadoSimulacao(ResultadoSimulacao resultadoSimulacao)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PropriedadeSafra val = db.PropriedadeSafra.FirstOrDefault(w =>
              w.PropriedadeId == resultadoSimulacao.PropriedadeId.Value &&
              w.SafraId == resultadoSimulacao.SafraId.Value);

            resultadoSimulacao.PropriedadeSafraId = val.Id;
            resultadoSimulacao.PropriedadeSafra = val;

            db.ResultadoSimulacao.Add(resultadoSimulacao);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResultadoSimulacaoExists(resultadoSimulacao.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = resultadoSimulacao.Id }, resultadoSimulacao);
        }

        // DELETE: api/ResultadoSimulacaos/5
        [ResponseType(typeof(ResultadoSimulacao))]
        public IHttpActionResult DeleteResultadoSimulacao(long id)
        {
            ResultadoSimulacao resultadoSimulacao = db.ResultadoSimulacao.Find(id);
            if (resultadoSimulacao == null)
            {
                return NotFound();
            }

            db.ResultadoSimulacao.Remove(resultadoSimulacao);
            db.SaveChanges();

            return Ok(resultadoSimulacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultadoSimulacaoExists(long id)
        {
            return db.ResultadoSimulacao.Count(e => e.Id == id) > 0;
        }
    }
}