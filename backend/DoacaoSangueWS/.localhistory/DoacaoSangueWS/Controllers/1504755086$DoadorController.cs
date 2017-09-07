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
        public List<DoacaoSangueWS.doadores> retornarDoadores()
        {
           
        }

        [HttpPost]
        [Route("hemocentro")]
        public void inserirHemocentro([FromBody]DoacaoSangueWS.hemocentros hemocentro)
        {
        }

        [HttpGet]
        [Route("doador/{id:int}")]
        public DoacaoSangueWS.doadores retornarDoadorPorId(int id)
        {

        }

        [HttpGet]
        [Route("doador/{nome}")]
        public List<DoacaoSangueWS.hemocentros> retornarHemocentrosPorNome(string nome)
        {
        }
}