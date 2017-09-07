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

        [HttpGet]
        [Route("doador/{id:int}")]
        public DoacaoSangueWS.doadores obterDoadorPorId(int id)
        {

        }
    }
}