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
        private HttpResponseMessage PopularHemocentros()   //////////////////////////////////////////////////////////////////////////// falta retornos
        {
            var db = new DoacaoSangueEntities();
            db.hemocentros.Add(new hemocentros() { nome = "Sangue tira sua saude Hemocentro", cidade = "Timbó", estado = "SC", complemento = "Been", cep = "89.080-260", descricao = "é nóis é do café memo" });
            db.hemocentros.Add(new hemocentros() { nome = "IndaHemo", cidade = "Indaial", estado = "SC", complemento = "Has", cep = "87.145-987", descricao = "Complemento 2" });
            db.hemocentros.Add(new hemocentros() { nome = "Vamos doar sangue", cidade = "Rio do Sul", estado = "SC", complemento = "Treta", cep = "87.145-987", descricao = "Complemento 3" });
            db.hemocentros.Add(new hemocentros() { nome = "Rio do Sul Hemo Blud", cidade = "Rio do Sul", estado = "SC", complemento = "Planted", cep = "97.845-657", descricao = "Prog de Noite" });
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Lista de Hemocentros populada!");
        }

        [HttpGet]
        [Route("hemocentro")]
        public HttpResponseMessage RetornarHemocentros() /// antes: List<DoacaoSangueWS.hemocentros>
        {
            var db = new DoacaoSangueEntities();
            var hemocentros = from b in db.hemocentros
                              orderby b.id
                              select b;
            return Request.CreateResponse(HttpStatusCode.OK, hemocentros.ToList<DoacaoSangueWS.hemocentros>());
        }

        [HttpGet]
        [Route("hemocentro/{id:int}")]
        public HttpResponseMessage RetornarHemocentroPorId(int id) /// antes: DoacaoSangueWS.hemocentros
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código ID do hemocentro deve ser informado."); // Não passou id, valor vem como 0.
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var hemocentro = (from b in db.hemocentros
                                  where b.id == id
                                  select b).FirstOrDefault();

                if (hemocentro == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrado.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, hemocentro);
                }
            }
        }

        [HttpGet]
        [Route("hemocentro/{nome}")]
        public HttpResponseMessage RetornarHemocentrosPorNome(string nome) // antes: List<DoacaoSangueWS.hemocentros>
        {
            if (String.IsNullOrEmpty(nome))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nome vazio."); // Não passou o nome, valor vem como 0.
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var hemocentro = from b in db.hemocentros
                                 where b.nome == nome
                                 select b;

                if (hemocentro == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrado.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, hemocentro.ToList<DoacaoSangueWS.hemocentros>());
                }
                
            }
        }

        /*
        [HttpGet]
        [Route("hemocentro/wildcard/{nome}")]
        public List<DoacaoSangueWS.hemocentros> RetornarHemocentrosWildCard(string nome)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = from b in db.hemocentros
                             where b.nome == nome
                             select b;
            return hemocentro.ToList<DoacaoSangueWS.hemocentros>();
        }
        */

        [HttpPost]
        [Route("hemocentro")]
        public HttpResponseMessage InserirHemocentro([FromBody]DoacaoSangueWS.hemocentros hemocentro) // antes: void
        {
            var db = new DoacaoSangueEntities();
            var hemocentroAux = db.hemocentros.Where(x => x.id == hemocentro.id).FirstOrDefault();
            
            if (hemocentroAux == null)
            {
                db.hemocentros.Add(hemocentroAux);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Hemocentro criado com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Hemocentro ja existente, não foi possivel criar.");
            }

        }

        [HttpPut]
        [Route("hemocentro")]
        public HttpResponseMessage AlterarHemocentro(DoacaoSangueWS.hemocentros hemocentro) // antes: void
        {
            if (hemocentro.id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ID do hemocentro deve ser informado.");
            }
            else
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
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada com sucesso");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrada");
                }
            }
        }

        [HttpDelete]
        [Route("hemocentro/{id:int}")]
        public HttpResponseMessage ExcluirHemocentro(int id) // antes: void
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = db.hemocentros.Where(x => x.id == id).FirstOrDefault();

            if (hemocentro != null)
            {
                db.hemocentros.Remove(hemocentro);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, $"Hemocentro {id.ToString()} excluido com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hemocentro não encontrado");
            }
        }

    }
}
