using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Infra.Map;
using Microsoft.EntityFrameworkCore;

namespace Infra.DB
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
           : base(options)
        { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Escolaridade> Escolaridade { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Settings.ConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new UsuariosMap());
            modelBuilder.ApplyConfiguration(new EscolaridadeMap());

        }
    }
}
