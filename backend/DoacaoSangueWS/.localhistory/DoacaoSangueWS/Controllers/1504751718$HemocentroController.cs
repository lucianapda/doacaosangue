using DoacaoSangueWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HemocentroController : ApiController
    {
        // ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors
        [HttpGet]
        [Route("hemocentro/popular")]
        public void PopularHemocentros()
        {
            var db = new DoacaoSangueEntities();
            db.hemocentros.Add(new hemocentros() { nome = "Sangue tira sua saude Hemocentro", cidade = "Timbó", estado = "SC", complemento = "Been", cep = "89.080-260", descricao = "é nóis é do café memo" });
            db.hemocentros.Add(new hemocentros() { nome = "IndaHemo", cidade = "Indaial", estado = "SC", complemento = "Has", cep = "87.145-987", descricao = "Complemento 2" });
            db.hemocentros.Add(new hemocentros() { nome = "Vamos doar sangue", cidade = "Rio do Sul", estado = "SC", complemento = "Treta", cep = "87.145-987", descricao = "Complemento 3" });
            db.hemocentros.Add(new hemocentros() { nome = "Rio do Sul Hemo Blud", cidade = "Rio do Sul", estado = "SC", complemento = "Planted", cep = "97.845-657", descricao = "Prog de Noite" });
            db.SaveChanges();
        }


        [HttpGet]
        [Route("hemocentro")]
        public List<DoacaoSangueWS.hemocentros> retornarHemocentros()
        {
            var db = new DoacaoSangueEntities();
            var hemocentros = from b in db.hemocentros
                              orderby b.id
                              select b;
            return hemocentros.ToList<DoacaoSangueWS.hemocentros>();
        }

        [HttpGet]
        [Route("hemocentro/{id:int}")]
        public DoacaoSangueWS.hemocentros obterHemocentroPorId(int id)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = (from b in db.hemocentros
                              where b.id == id
                              select b).FirstOrDefault();
            return hemocentro;
        }

        [HttpGet]
        [Route("hemocentro/{nome}")]
        public List<DoacaoSangueWS.hemocentros> retornarHemocentrosPorNome(string nome)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = from b in db.hemocentros
                             where b.nome == nome
                             select b;
            return hemocentro.ToList<DoacaoSangueWS.hemocentros>();
        }

        [HttpGet]
        [Route("hemocentro/wildcard/{nome}")]
        public List<DoacaoSangueWS.hemocentros> retornarHemocentrosWildCard(string nome)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = from b in db.hemocentros
                             where b.nome == nome
                             select b;
            return hemocentro.ToList<DoacaoSangueWS.hemocentros>();
        }

        [HttpPost]
        [Route("hemocentro")]
        public void inserirHemocentro([FromBody]DoacaoSangueWS.hemocentros hemocentro)
        {
            var db = new DoacaoSangueEntities();
            db.hemocentros.Add(hemocentro);
            db.SaveChanges();
        }

        [HttpPut]
        [Route("hemocentro")]
        public void alterarHemocentro(DoacaoSangueWS.hemocentros hemocentro)
        {
            var db = new DoacaoSangueEntities();
            var hemocentroAux = db.hemocentros.Find(hemocentro.id);
            if (hemocentroAux != null)
            {
                db.Entry(hemocentroAux).CurrentValues.SetValues(hemocentro);
                db.SaveChanges();
            }
        }


            //}

            //[HttpDelete]
            //[Route("hemocentro/{id:id}")]
            //public void excluirHemocentro(int id)
            //{

            //}

        }
}
