using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoacaoSangueWS.Models
{
    public class Pergunta
    {
        public int id { get; set; }
        public string nome { get; set; }
        public bool resposta { get; set; }

    }
}