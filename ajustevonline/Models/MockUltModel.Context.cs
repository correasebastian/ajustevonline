﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ajustevonline.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MockUltEntities : DbContext
    {
        public MockUltEntities()
            : base("name=MockUltEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<InspeccionesUlt> InspeccionesUlt { get; set; }
        public virtual DbSet<Paths> Paths { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
    }
}