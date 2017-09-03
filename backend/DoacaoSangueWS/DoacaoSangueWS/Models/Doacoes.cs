using System;

namespace WebApplication1.Models
{
    public class Doacoes
    {

        public int id { get; set; }
        public DateTime dataDoacao { get; set; }
        public string atendente { get; set; }
        public float litros { get; set; }

    }
}