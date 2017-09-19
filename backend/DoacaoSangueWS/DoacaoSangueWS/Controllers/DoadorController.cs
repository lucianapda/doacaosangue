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
        [Authorize(Roles = "Administrador")]
        [Route("doador")]
        public HttpResponseMessage RetornarDoadores()
        {
            var db = new DoacaoSangueEntities();
            var doador = from d in db.doadores
                         join h in db.hemocentros on d.id_hemocentro equals h.id
                         orderby d.nome
                         select d;
            return Request.CreateResponse(HttpStatusCode.OK, doador.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
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
        [AllowAnonymous]
        [Route("doador")]
        public HttpResponseMessage InserirDoador([FromBody]DoacaoSangueWS.doadores doador)
        {
            var db = new DoacaoSangueEntities();

            if(doador.id != 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código do doador não deve ser informado.");
            }
            if (doador.id_hemocentro == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código do hemocentro deve ser informado.");
            }
            if (doador.id_hemocentro < 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código do hemocentro não pode ser negativo");
            }

            var hemocentro = (from h in db.doadores where h.id == doador.id_hemocentro select h).FirstOrDefault();
            if (hemocentro == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Código do hemocentro não é válido");
            }
            if (doador.nome == null || doador.nome == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nome não pode ser vazio.");
            }
            if (doador.nome.Length > 100)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nome não conter mais que 100 caracteres.");
            }
            if (doador.sobrenome == null || doador.sobrenome == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Sobrenome deve ser informado");
            }
            if (doador.sobrenome.Length > 100)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Sobrenome não conter mais que 100 caracteres.");
            }
            if (doador.data_nascimento == DateTime.MinValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Data de nascimento deve ser informada");
            }
            if (doador.data_nascimento.Date >= DateTime.Now.Date)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Data de nascimento deve ser inferior a data atual.");
            }
            if (doador.tipo_sanguineo == null || doador.tipo_sanguineo == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo sanguíneo deve ser informado");
            }
            if (doador.tipo_sanguineo.Length > 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo sanguíneo não deve conter mais do que seis caracteres.");
            }
        
            db.doadores.Add(doador);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Doador(a) criado(a) com sucesso!");
          
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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