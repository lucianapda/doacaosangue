using DoacaoSangueWS.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DoacaoSangueWS.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("Usuario/Login")]
        public HttpResponseMessage Login(usuarios user)
        {
            var db = new DoacaoSangueEntities();
            var userAux = (from u in db.usuarios
                         where u.nome == user.nome &&
                         u.senha == user.senha
                         select u).FirstOrDefault();

            HttpResponseMessage resposta;
            if (userAux != null)
            {
                HttpContext.Current.User = new CustomPrincipal(userAux);
                return Request.CreateResponse(HttpStatusCode.OK, $"Login bem sucedido, bem vindo {userAux.nome}");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Login e/ou senha incorretos");
            }
        }
    }
}
