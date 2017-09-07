using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DoacaoSangueWS.Controllers
{
    public class PerguntaController : ApiController
    {

        [HttpGet]
        [Route("pergunta")]
        public HttpResponseMessage RetornarPerguntas()
        {
            var db = new DoacaoSangueEntities();
            var perguntas = from b in db.perguntas
                            select b;
            return Request.CreateResponse(HttpStatusCode.OK, perguntas.ToList());
        }

        [HttpGet]
        [Route("pergunta/{id:int}")]
        public HttpResponseMessage RetornarPerguntaPorId(int id)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código deve ser informado");
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var perguntas = (from b in db.perguntas
                                 where b.id == id
                                 select b).FirstOrDefault();

                if (perguntas != null)
                    return Request.CreateResponse(HttpStatusCode.OK, perguntas);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada.");
            }
        }

        [HttpPost]
        [Route("pergunta")]
        public HttpResponseMessage InserirPergunta([FromBody]perguntas pergunta)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();

            if (perguntas == null)
            {
                db.perguntas.Add(pergunta);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Pergunta criada com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Pergunta ja existente, não foi possivel criar.");
            }
        }

        [HttpPut]
        [Route("pergunta")]
        public HttpResponseMessage AlterarPergunta([FromBody]perguntas pergunta)
        {
            if (pergunta.id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código deve ser informado");
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();

                if (perguntas != null)
                {
                    perguntas.nome = pergunta.nome != null ? pergunta.nome : perguntas.nome;
                    perguntas.resposta = pergunta.resposta != null ? pergunta.resposta : perguntas.resposta;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada com sucesso");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
                }
            }
        }

        [HttpDelete]
        [Route("pergunta/{id:int}")]
        public HttpResponseMessage ExcluirPergunta(int id)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == id).FirstOrDefault();
            if (perguntas != null)
            {
                db.perguntas.Remove(perguntas);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Pergunta excluída com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
            }
        }

    }
}