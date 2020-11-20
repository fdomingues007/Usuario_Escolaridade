using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Map
{
  public class EscolaridadeMap : IEntityTypeConfiguration<Escolaridade>
  {
    public void Configure(EntityTypeBuilder<Escolaridade> builder)
    {
      builder.ToTable("Escolaridade");
      builder.HasKey(x => x.CodEscolaridade);
      builder.Property(x => x.Nivel);

      builder.HasData(
        new Escolaridade { CodEscolaridade = 1, Nivel = "Infantil" },
        new Escolaridade { CodEscolaridade = 2, Nivel = "Fundamental" },
        new Escolaridade { CodEscolaridade = 3, Nivel = "Médio" },
        new Escolaridade { CodEscolaridade = 4, Nivel = "Superior" } );
    }
  }
}
