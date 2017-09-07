using DoacaoSangueWS.Models;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Doacao
    {

        public int id { get; set; }
        public DateTime data { get; set; }
        public string atendente { get; set; }
        public float litros { get; set; }
        public bool aceitavel { get; set; }
        public List<Pergunta> perguntas { get; set; }
    }
}