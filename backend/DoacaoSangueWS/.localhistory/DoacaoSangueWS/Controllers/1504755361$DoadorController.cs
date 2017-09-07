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

        }

        [HttpGet]
        [Route("doador/{nome}")]
        public List<DoacaoSangueWS.doadores> RetornarHemocentrosPorNome(string nome)
        {
        }

        [HttpGet]
        [Route("doador/{nome}")]
        public List<DoacaoSangueWS.doadores> RetornarHemocentroWildCard(strgin nome)
        {

        }
}