using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class HemocentroContext :DbContext
    {

        public DbSet<Hemocentro> hemocentros { get; set; }
        public DbSet<Doador> doadores { get; set; }
        public DbSet<Doacoes> doacoes { get; set; }

    }
}