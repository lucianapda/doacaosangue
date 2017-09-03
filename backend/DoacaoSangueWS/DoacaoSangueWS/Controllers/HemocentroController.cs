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

        [HttpGet]
        [Route("hemocentro")]
        public List<Hemocentro> retornarHemocentros()
        {
            List<Hemocentro> hemocentros = new List<Hemocentro>();
            return hemocentros;
        }

        [HttpGet]
        [Route("hemocentro/{id:int}")]
        public Hemocentro obterHemocentroPorId(int id)
        {
            Hemocentro hemocentro = new Hemocentro();
            return hemocentro;
        }

        [HttpGet]
        [Route("hemocentro/{nome}")]
        public Hemocentro obterHemocentroPorNome(string nome)
        {
            Hemocentro hemocentro = new Hemocentro();
            return hemocentro;
        }

        [HttpPost]
        [Route("hemocentro")]
        public void inserirHemocentro()
        {

        }

        [HttpPut]
        [Route("hemocentro")]
        public void alterarHemocentro(Hemocentro hemocentro)
        {

        }

        [HttpDelete]
        [Route("hemocentro/{id:id}")]
        public void excluirHemocentro(int id)
        {

        }

    }
}
