﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoacaoSangueWS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DoacaoSangueEntities : DbContext
    {
        public DoacaoSangueEntities()
            : base("name=DoacaoSangueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<doacoes> doacoes { get; set; }
        public virtual DbSet<doacoes_perguntas> doacoes_perguntas { get; set; }
        public virtual DbSet<doadores> doadores { get; set; }
        public virtual DbSet<hemocentros> hemocentros { get; set; }
        public virtual DbSet<perguntas> perguntas { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
    }
}
