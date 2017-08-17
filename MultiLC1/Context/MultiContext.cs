using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MultiLC.Models;

namespace MultiLC.Context
{
    public class MultiContext: DbContext
    {

        public MultiContext() : base("MultiContext")
    {
        }

        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Puntos> Puntos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}