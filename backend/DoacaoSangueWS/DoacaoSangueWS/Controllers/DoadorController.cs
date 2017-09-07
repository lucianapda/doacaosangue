using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoacaoSangueWS.Controllers
{
    public class DoadorController : ApiController
    {

        [HttpGet]
        [Route("doador")]
        public HttpResponseMessage RetornarDoadores()
        {
            var db = new DoacaoSangueEntities();
            var doador = from d in db.doadores
                         join h in db.hemocentros on  d.id_hemocentro  equals h.id
                         orderby d.nome
                         select d;
            return Request.CreateResponse(HttpStatusCode.OK, doador.ToList());
        }

        [HttpGet]
        [Route("doador/{id:int}")]
        public HttpResponseMessage RetornarDoadorPorId(int id)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código deve ser informado");
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var doador = (from d in db.doadores
                              orderby d.nome
                              where d.id == id
                              select d).FirstOrDefault();
                if (doador == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Doador não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, doador);
                }
            }
        }

        [HttpPost]
        [Route("doador")]
        public HttpResponseMessage InserirHemocentro([FromBody]DoacaoSangueWS.doadores doador)
        {

            var db = new DoacaoSangueEntities();
            var perguntas = db.doadores.Where(x => x.id == doador.id).FirstOrDefault();

            if (perguntas == null)
            {
                db.doadores.Add(doador);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Doador criada com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Doador ja existente, não foi possivel criar.");
            }
        }

        [HttpPut]
        [Route("doador")]
        public HttpResponseMessage AlterarDoador(DoacaoSangueWS.doadores doador)
        {
            if (doador.id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código deve ser informado");
            }
            else
            {
                var db = new DoacaoSangueEntities();
                var doadorParaAlterar = (from d in db.doadores
                                         where d.id == doador.id
                                         select d).FirstOrDefault();
                if (doadorParaAlterar != null)
                {
                    doadorParaAlterar.id_hemocentro = doador.id_hemocentro == 0 ? doador.id_hemocentro : doadorParaAlterar.id_hemocentro;
                    doadorParaAlterar.nome = RetornarValido(doador.nome, doadorParaAlterar.nome);
                    doadorParaAlterar.sobrenome = RetornarValido(doador.sobrenome, doadorParaAlterar.sobrenome);
                    doadorParaAlterar.tipo_sanguineo = RetornarValido(doador.tipo_sanguineo, doadorParaAlterar.tipo_sanguineo);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Alteração realizada com sucesso");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Doador não encontrada");
                }
            }
        }

        [HttpDelete]
        [Route("doador/{id:int}")]
        public HttpResponseMessage ExcluirDoador(int id)
        {
            var db = new DoacaoSangueEntities();
            var doador = db.doadores.Where(x => x.id == id).FirstOrDefault();
            if (doador != null)
            {
                db.doadores.Remove(doador);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Doador excluído com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Doador não encontrada");
            }
        }

        private string RetornarValido(string valorNovo, string valorAntigo)
        {
            return valorNovo != null ? valorNovo : valorAntigo;
        }
    }
}