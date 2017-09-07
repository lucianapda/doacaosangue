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
        public HttpResponseMessage RetornarPerguntasPorId(int id)
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

            HttpResponseMessage resposta = Request.CreateResponse(perguntas.ToList());

            return resposta;
        }
    }
}
