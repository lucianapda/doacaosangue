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
            db.hemocentros.Add(new hemocentros("HETimbomo", "Timbó", "SC", "Testes", "89.080-260", "Meu Complemento"));
            db.hemocentros.Add(new hemocentros("IndaHemo", "Indaial", "SC", "DOis", "87.145-987", "Complemento 2"));
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
        public List<DoacaoSangueWS.hemocentros> obterHemocentroPorNome(string nome)
        {
            var db = new DoacaoSangueEntities();
            var hemocentro = from b in db.hemocentros
                              where b.nome.Contains(nome)
                              select b;
            return hemocentro.ToList<DoacaoSangueWS.hemocentros>();
        }



        //[HttpPost]
        //[Route("hemocentro")]
        //public void inserirHemocentro([FromBody]Hemocentro hemocentro)
        //{
        //    var context = new DoacaoSangueContext();
        //    context.hemocentros.Add(hemocentro);
        //    context.SaveChanges();
        //}

        //[HttpPut]
        //[Route("hemocentro")]
        //public void alterarHemocentro(Hemocentro hemocentro)
        //{
        //    var context = new DoacaoSangueContext();
        //    var hemocentroAux = context.hemocentros.Find(hemocentro.Codigo);
        //    if (hemocentroAux != null)
        //    {
        //        context.Entry(hemocentroAux).CurrentValues.SetValues(hemocentro);
        //        context.SaveChanges();
        //    }


        //}

        //[HttpDelete]
        //[Route("hemocentro/{id:id}")]
        //public void excluirHemocentro(int id)
        //{

        //}

    }
}
