using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoacaoSangueWS.Controllers
{
    public class DoacaoController : ApiController
    {

        [HttpGet]
        [Route("doacao/id/{id}")]
        public HttpResponseMessage RetornarDoacao(int id)
        {
            var db = new DoacaoSangueEntities();
            var doacao = from d in db.doacoes
                         join dd in db.doadores on d.id_doador equals dd.id
                         join h in db.hemocentros on dd.id_hemocentro equals h.id
                         join dp in db.doacoes_perguntas on d.id equals dp.id_doacao
                         join p in db.perguntas on dp.id_pergunta equals p.id
                         where d.id == id
                         select d;
            return Request.CreateResponse(HttpStatusCode.OK, doacao);
        }

        [HttpGet]
        [Route("doacao/id_hemocentro/{id}")]
        public HttpResponseMessage RetornarDoacoesPorHemocentro(int id)
        {

            var db = new DoacaoSangueEntities();
            var doacoes = from dc in db.doacoes
                          join dd in db.doadores on dc.id_doador equals dd.id
                          join h in db.hemocentros on dd.id_hemocentro equals h.id 
                          where h.id == id
                          select dc;
            return Request.CreateResponse(HttpStatusCode.OK, doacoes.ToList());
        }

        [HttpGet]
        [Route("doacao/hemocentro/{id}/data/{data}")]
        public HttpResponseMessage RetornarDoacoesPorHemocentroPorDataDoacao(int id, DateTime data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        [Route("doacao")]
        public HttpResponseMessage InserirDoacao([FromBody] DoacaoSangueWS.doacoes doacao)
        {
            var db = new DoacaoSangueEntities();
            db.doacoes.Add(doacao);
            return Request.CreateResponse(HttpStatusCode.Created, "Doação criada com sucesso!");
        }

        [HttpPut]
        [Route("doacao")]
        public HttpResponseMessage AlterarDoacao([FromBody] DoacaoSangueWS.doacoes doacao)
        {
            
            var db = new DoacaoSangueEntities();
            var doacaoAlterar = db.doacoes.Where(d => d.id == doacao.id).FirstOrDefault();
            if (doacaoAlterar != null)
            {
                doacaoAlterar.atendente = doacao.atendente.Length > 0 ? doacao.atendente : doacaoAlterar.atendente;
                doacaoAlterar.aceitavel = doacao.aceitavel;
                doacaoAlterar.litros = doacao.litros > 0 ? doacao.litros : doacaoAlterar.litros;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Doação alterada com sucesso");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Doação não encontrada");
        }

        [HttpDelete]
        [Route("doacao")]
        public HttpResponseMessage ExcluirDoacao(int id)
        {
            var db = new DoacaoSangueEntities();
            var doacao = db.doacoes.Where(x => x.id == id).FirstOrDefault();
            if(doacao != null)
            {
                db.doacoes.Remove(doacao);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Doação excluída com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Doação não encontrada");
            }
        }

    }
}
