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
        [HttpGet]
        //[AllowAnonymous]
        [Route("usuario/popular")]
        public HttpResponseMessage Popular()
        {
            var db = new DoacaoSangueEntities();
            db.usuarios.Add(new usuarios() { login = "admin", nome = "admin", privilegio = PrivilegioUsuario.Administrador.ToString(), senha = "admin" });
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "sucesso");
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("usuario/login")]
        public HttpResponseMessage Login([FromBody]DoacaoSangueWS.usuarios user)
        {
            if(user.login == null || user.login.Trim() == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Login não pode ser vazio");
            }
            if(user.senha == null || user.senha.Trim() == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Senha não pode ser vazia");
            }

            var db = new DoacaoSangueEntities();
            var userAux = (from u in db.usuarios
                           where u.login == user.login &&
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
        //[Authorize(Roles = "Administrador")]
        [Route("usuario")]
        public HttpResponseMessage InserirUsuario(usuarios user)
        {
            if (user.login == null || user.login.Trim() == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Login não pode ser vazio");
            }
            if (user.login.Length > 30)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Login não pode ser maior que 30 caracteres");
            }
            if (user.nome == null || user.nome.Trim() == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nome não pode ser vazio");
            }
            if (user.nome.Length > 100)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nome não pode ser maior que 100 caracteres");
            }
            if (user.senha == null || user.senha.Trim() == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Senha não pode ser vazia");
            }
            if (user.senha.Length < 10)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Senha deve conter no mínimo 10 caracteres");
            }
            if (user.senha.Length > 30)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Senha deve conter no máximo 30 caracteres");
            }

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

            return Request.CreateResponse(HttpStatusCode.Conflict, "Usuario ja existente, não foi possivel criar.");
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("usuario/tipoUsuario")]
        public HttpResponseMessage TipoUsuario()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ListaTipoUsuario());
        }

        private List<string> ListaTipoUsuario()
        {
            var lista = new List<string>();

            lista.Add(PrivilegioUsuario.Administrador.ToString());
            lista.Add(PrivilegioUsuario.Doador.ToString());

            return lista;
        }
    }
}
