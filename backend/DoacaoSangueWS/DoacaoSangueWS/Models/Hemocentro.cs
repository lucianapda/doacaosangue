using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Hemocentro 
    {

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }

        public virtual List<Models.Doador> Doadores { get; set; }
       
    }
}