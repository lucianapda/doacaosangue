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
    public enum PrivilegioUsuario
    {
        Administrador,
        Doador
    }

    public class UsuarioController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Usuario/Popular")]
        public HttpResponseMessage Popular()
        {
            var db = new DoacaoSangueEntities();
            db.usuarios.Add(new usuarios() { login = "admin", nome = "admin", privilegio = PrivilegioUsuario.Administrador.ToString(), senha = "admin" });

            return Request.CreateResponse(HttpStatusCode.OK, "sucesso");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Usuario/Login")]
        public HttpResponseMessage Login(usuarios user)
        {
            var db = new DoacaoSangueEntities();
            var userAux = (from u in db.usuarios
                         where u.nome == user.nome &&
                         u.senha == user.senha
                         select u).FirstOrDefault();

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

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        [Route("Usuario")]
        public HttpResponseMessage CriarUsuario(usuarios user)
        {
            var db = new DoacaoSangueEntities();
            var usuario = db.usuarios.Where(x => x.login == user.login).FirstOrDefault();

            if (!ListaTipoUsuario().Contains(user.privilegio))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Privilégio informado não existe");
            }
            else if (usuario == null)
            {
                db.usuarios.Add(user);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Usuario criado com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Usuario ja existente, não foi possivel criar.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Usuario/TipoUsuario")]
        public HttpResponseMessage TipoUsuario()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ListaTipoUsuario());
        }

        public List<string> ListaTipoUsuario()
        {
            var lista = new List<string>();

            lista.Add(PrivilegioUsuario.Administrador.ToString());
            lista.Add(PrivilegioUsuario.Doador.ToString());

            return lista;
        }
    }
}
