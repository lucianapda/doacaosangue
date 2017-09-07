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
        public List<DoacaoSangueWS.doadores> RetornarDoadores()
        {
            var db = new DoacaoSangueEntities();
            var doador = from d in db.doadores
                          orderby d.nome
                          select d;
            return doador.ToList<DoacaoSangueWS.doadores>();
        }

        [HttpPost]
        [Route("doador")]
        public void inserirHemocentro([FromBody]DoacaoSangueWS.doadores doador)
        {
        }

        [HttpPut]
        [Route("doador")]
        public void alterarDoador(DoacaoSangueWS.doadores doador)
        {

        }

        [HttpGet]
        [Route("doador/{id:int}")]
        public DoacaoSangueWS.doadores RetornarDoadorPorId(int id)
        {
            var db = new DoacaoSangueEntities();
            var doador = (from d in db.doadores
                         orderby d.nome
                         where d.id == id
                         select d).FirstOrDefault();
            return doador;
        }

        [HttpGet]
        [Route("doador/{nome}")]
        public List<DoacaoSangueWS.doadores> RetornarHemocentrosPorNome(string nome)
        {
            var db = new DoacaoSangueEntities();
            var doador = from d in db.doadores
                         orderby d.nome
                         where d.nome == nome
                         select d;
            return doador.ToList<DoacaoSangueWS.doadores>();
        }

        [HttpGet]
        [Route("doador/{nome}")]
        public List<DoacaoSangueWS.doadores> RetornarHemocentroWildCard(string nome)
        {
            var db = new DoacaoSangueEntities();
            var doador = from d in db.doadores
                         orderby d.nome
                         where d.nome.Contains(nome)
                         select d;
            return doador.ToList<DoacaoSangueWS.doadores>();
        }
}