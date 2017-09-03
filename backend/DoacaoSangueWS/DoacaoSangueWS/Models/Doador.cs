using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Doador
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoSanguineo { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }

        public string NomeCompleto
        {
            get { return $"{Nome} {Sobrenome}"; }
        }

        public virtual List<Models.Doacoes> Doacoes { get; set; }




    }
}