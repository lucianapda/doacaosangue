using DoacaoSangueWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class HemocentroController : ApiController
    {
        // ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors
        [HttpGet]
        [Route("hemocentro/popular")]
        private HttpResponseMessage PopularHemocentros()
        {
            try
            {
                var db = new DoacaoSangueEntities();
                db.hemocentros.Add(new hemocentros() { nome = "Sangue tira sua saude Hemocentro", cidade = "Timbó", estado = "SC", complemento = "Been", cep = "89.080-260", descricao = "é nóis é do café memo" });
                db.hemocentros.Add(new hemocentros() { nome = "IndaHemo", cidade = "Indaial", estado = "SC", complemento = "Has", cep = "87.145-987", descricao = "Complemento 2" });
                db.hemocentros.Add(new hemocentros() { nome = "Vamos doar sangue", cidade = "Rio do Sul", estado = "SC", complemento = "Treta", cep = "87.145-987", descricao = "Complemento 3" });
                db.hemocentros.Add(new hemocentros() { nome = "Rio do Sul Hemo Blud", cidade = "Rio do Sul", estado = "SC", complemento = "Planted", cep = "97.845-657", descricao = "Prog de Noite" });
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Ocorreu um erro ao popular hemocentros: \r\n {e.Message}");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Base populada com sucesso!");
        }

        [HttpGet]
        [Route("hemocentro")]
        public HttpResponseMessage RetornarHemocentros()
        {
            var db = new DoacaoSangueEntities();
            var hemocentros = from b in db.hemocentros
                              orderby b.id
                              select b;

            return Request.CreateResponse(HttpStatusCode.OK, hemocentros.ToList());
        }

        [HttpGet]
        [Route("hemocentro/{id:int}")]
        public HttpResponseMessage RetornarHemocentroPorId(int id)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = (from b in db.hemocentros
                              where b.id == id
                              select b).FirstOrDefault();

            if (hemocentro != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hemocentro);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "ID informado não corresponde a um Hemocentro existente");
            }
        }

        [HttpGet]
        [Route("hemocentro/{nome}")]
        public HttpResponseMessage RetornarHemocentrosPorNome(string nome)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = from b in db.hemocentros
                             where b.nome.Contains(nome)
                             select b;

            return Request.CreateResponse(HttpStatusCode.OK, hemocentro.ToList<DoacaoSangueWS.hemocentros>());
        }

        [HttpPost]
        [Route("hemocentro")]
        public HttpResponseMessage InserirHemocentro([FromBody]DoacaoSangueWS.hemocentros hemocentro)
        {
            try
            {
                var db = new DoacaoSangueEntities();
                db.hemocentros.Add(hemocentro);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Ocorreu um erro ao inserir hemocentro: \r\n {e.Message}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Hemocentro incluido com sucesso");
        }

        [HttpPut]
        [Route("hemocentro")]
        public HttpResponseMessage AlterarHemocentro(DoacaoSangueWS.hemocentros hemocentro)
        {
            var db = new DoacaoSangueEntities();
            var hemocentroAux = (from b in db.hemocentros
                                 where b.id == hemocentro.id
                                 select b).FirstOrDefault();
            if (hemocentroAux != null)
            {
                hemocentroAux.nome = hemocentro.nome != null ? hemocentro.nome : hemocentroAux.nome;
                hemocentroAux.cep = hemocentro.cep ?? hemocentroAux.cep;
                hemocentroAux.estado = hemocentro.estado != null ? hemocentro.estado : hemocentroAux.estado;
                hemocentroAux.cidade = hemocentro.cidade != null ? hemocentro.cidade : hemocentroAux.cidade;
                hemocentroAux.complemento = hemocentro.complemento != null ? hemocentro.complemento : hemocentroAux.complemento;
                hemocentroAux.descricao = hemocentro.descricao != null ? hemocentro.descricao : hemocentroAux.descricao;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"Ocorreu um erro ao alterar hemocentro: \r\n {e.Message}");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Hemocentro alterado com sucesso");
        }

        [HttpDelete]
        [Route("hemocentro/{id:int}")]
        public HttpResponseMessage ExcluirHemocentro(int id)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = db.hemocentros.Where(x => x.id == id).FirstOrDefault();
            if (hemocentro != null)
            {
                db.hemocentros.Remove(hemocentro);
                db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Hemocentro alterado com sucesso");
        }

    }
}
