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
        [AllowAnonymous]
        [Route("Pergunta/{id:int}")]
        public HttpResponseMessage RetornarPerguntas(int id)
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

        [HttpGet]
        [AllowAnonymous]
        [Route("Pergunta")]
        public HttpResponseMessage ListarPerguntas()
        {
            var db = new DoacaoSangueEntities();
            var perguntas = from b in db.perguntas
                            select b;
            
            return Request.CreateResponse(HttpStatusCode.OK, perguntas.ToList());
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        [Route("Pergunta/{id:int}")]
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
                return Request.CreateResponse(HttpStatusCode.Created, "Pergunta criada com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Pergunta ja existente, não foi possivel criar.");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
                    return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada com sucesso");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Pergunta não encontrada");
                }
            }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [Route("Pergunta/Criar")]
        public HttpResponseMessage CriarPerguntas([FromBody]perguntas pergunta)
        {
            var db = new DoacaoSangueEntities();
            var perguntas = db.perguntas.Where(x => x.id == pergunta.id).FirstOrDefault();
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