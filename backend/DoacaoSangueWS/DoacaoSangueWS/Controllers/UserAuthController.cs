using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DoacaoSangueWS.Controllers
{
    public class User
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
    }

    public class UserAuthController : ApiController
    {

        [HttpGet]
        [Route("Pergunta/{id:int}")]
        public HttpResponseMessage Login(User user)
        {

        }
    }
}
