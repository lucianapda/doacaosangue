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
        [Authorize(Roles ="")]
        [Route("Pergunta/{id:int}")]
        public HttpResponseMessage RetornarPerguntas(int id)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = (from b in db.perguntas
                             where b.id == id
                             select b).FirstOrDefault();
            
            if (perguntas != null)
                return Request.CreateResponse(HttpStatusCode.OK, perguntas);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Não existe pergunta com este ID.");
        }

        [HttpGet]
        [Route("Pergunta")]
        public HttpResponseMessage ListarPerguntas()
        {
            var db = new DoacaoSangueEntities();
            var perguntas = from b in db.perguntas
                            select b;
            
            return Request.CreateResponse(HttpStatusCode.OK, perguntas.ToList());
        }

        [HttpDelete]
        [Route("Pergunta/Deletar/{id:int}")]
        public HttpResponseMessage DeletarPerguntas(int id)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == id).FirstOrDefault();
            if (perguntas != null)
            {
                var conexoes = db.doacoes_perguntas.Where(x => x.id_pergunta == id);
                db.doacoes_perguntas.RemoveRange(conexoes);

                db.perguntas.Remove(perguntas);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Pergunta excluida com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
            }
        }

        [HttpPut]
        [Route("Pergunta/Alterar")]
        public HttpResponseMessage AlterarPerguntas([FromBody]perguntas pergunta)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();

            if (perguntas != null)
            {
                perguntas.nome = pergunta.nome != null ? pergunta.nome : perguntas.nome;
                perguntas.resposta = pergunta.resposta != null ? pergunta.resposta : perguntas.resposta;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
            }
        }

        [HttpPut]
        [Route("Pergunta/Criar")]
        public HttpResponseMessage CriarPerguntas([FromBody]perguntas pergunta)
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
    }
}