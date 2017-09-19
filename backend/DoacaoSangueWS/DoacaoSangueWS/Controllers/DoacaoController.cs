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
        //[Authorize(Roles = "Doador")]
        [Route("doacao/{id}")]
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
            if (doacao == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "ID de doação inexistente.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, doacao);
        }

        [HttpGet]
        //[Authorize(Roles = "Administrador")]
        [Route("doacao/hemocentro/{id}")]
        public HttpResponseMessage RetornarDoacoesPorHemocentro(int id)
        {

            var db = new DoacaoSangueEntities();
            var doacoes = from dc in db.doacoes
                          join dd in db.doadores on dc.id_doador equals dd.id
                          join h in db.hemocentros on dd.id_hemocentro equals h.id 
                          where h.id == id
                          select dc;
            if (doacoes == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Sem doações para este hemocentro.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, doacoes.ToList());
        }

        [HttpGet]
        //[Authorize(Roles = "Administrador")]
        [Route("doacao/hemocentro/{id}/data/{data}")]
        public HttpResponseMessage RetornarDoacoesPorHemocentroPorDataDoacao(int id, DateTime data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        //[Authorize(Roles = "Doador")]
        [Route("doacao")]
        public HttpResponseMessage InserirDoacao([FromBody] DoacaoSangueWS.doacoes doacao)
        {
            var db = new DoacaoSangueEntities();
            db.doacoes.Add(doacao);
            return Request.CreateResponse(HttpStatusCode.Created, "Doação criada com sucesso!");
        }

        [HttpPut]
        //[Authorize(Roles = "Doador")]
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
        //[Authorize(Roles = "Administrador")]
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

        [HttpPost]
        //[Authorize(Roles = "Doador")]
        [Route("doacao/perguntas")]
        public HttpResponseMessage RelacionarPergunta([FromBody]DoacaoSangueWS.doacoes_perguntas doacao_pergunta)
        {
            if(doacao_pergunta.id_doacao == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código da doação não pode ser igual ou menor que 0");
            }

            if(doacao_pergunta.id_pergunta <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código da pergunta não pode ser igual ou menor que 0");
            }

            var db = new DoacaoSangueEntities();
            var doacao = (from d in db.doacoes
                            where d.id == doacao_pergunta.id_doacao
                          select d).FirstOrDefault();
            if(doacao == null){
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Doação não encontrado");
            }

            var pergunta = (from p in db.perguntas
                            where p.id == doacao_pergunta.id_pergunta
                            select p).FirstOrDefault();
            if(pergunta == null){
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Pergunta não encontrada");
            }

            var doacaoPergunta = (from dp in db.doacoes_perguntas
                            where dp.id_pergunta == doacao_pergunta.id_pergunta && dp.id_doacao == doacao_pergunta.id_doacao
                                  select dp).FirstOrDefault();
            if(doacaoPergunta != null){
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Pergunta já relacionada com doação");
            }

            db.doacoes_perguntas.Add(new doacoes_perguntas()
            {
                id_pergunta = doacao_pergunta.id_pergunta,
                id_doacao = doacao_pergunta.id_doacao,
                resposta = doacao_pergunta.resposta
            });
            return Request.CreateResponse(HttpStatusCode.Created, "Realacionamento realizado com sucesso");

        }

    }
}
