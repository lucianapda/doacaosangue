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
        [Route("Pergunta/{id:int}")]
        public HttpResponseMessage RetornarPerguntas(int id)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = (from b in db.perguntas
                             where b.id == id
                             select b).FirstOrDefault();

            HttpResponseMessage resposta;

            if (perguntas != null)
                resposta = Request.CreateResponse(HttpStatusCode.OK, perguntas);
            else
                resposta = Request.CreateResponse(HttpStatusCode.NotFound, "Não existe pergunta com este ID.");

            return resposta;
        }

        [HttpGet]
        [Route("Pergunta")]
        public HttpResponseMessage ListarPerguntas()
        {
            var db = new DoacaoSangueEntities();
            var perguntas = from b in db.perguntas
                            select b;

            HttpResponseMessage resposta = Request.CreateResponse(HttpStatusCode.OK, perguntas.ToList());

            return resposta;
        }

        [HttpDelete]
        [Route("Pergunta/Deletar/{id:int}")]
        public HttpResponseMessage DeletarPerguntas(int id)
        {
            HttpResponseMessage resposta;
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == id).FirstOrDefault();
            if (perguntas != null)
            {
                db.perguntas.Remove(perguntas);
                db.SaveChanges();
                resposta = Request.CreateResponse(HttpStatusCode.OK, "Pergunta excluida com sucesso");
            }
            else
            {
                resposta = Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
            }

            return resposta;
        }

        [HttpPut]
        [Route("Pergunta/Alterar")]
        public HttpResponseMessage AlterarPerguntas([FromBody]perguntas pergunta)
        {
            HttpResponseMessage resposta;
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();

            if (perguntas != null)
            {
                perguntas.nome = pergunta.nome != null ? pergunta.nome : perguntas.nome;
                perguntas.resposta = pergunta.resposta != null ? pergunta.resposta : perguntas.resposta;
                db.SaveChanges();
                resposta = Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada");
            }
            else
            {
                resposta = Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
            }

            return resposta;
        }

        [HttpPut]
        [Route("Pergunta/Criar")]
        public HttpResponseMessage CriarPerguntas([FromBody]perguntas pergunta)
        {
            HttpResponseMessage resposta;
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();

            if (perguntas == null)
            {
                db.perguntas.Add(pergunta);
                db.SaveChanges();
                resposta = Request.CreateResponse(HttpStatusCode.Created, "Pergunta criada com sucesso");
            }
            else
            {
                resposta = Request.CreateResponse(HttpStatusCode.Conflict, "Pergunta ja existente, não foi possivel criar.");
            }

            return resposta;
        }
    }
}