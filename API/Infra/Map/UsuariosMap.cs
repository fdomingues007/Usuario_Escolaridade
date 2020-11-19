using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Map
{
  public class UsuariosMap : IEntityTypeConfiguration<Usuarios>
  {
    public void Configure(EntityTypeBuilder<Usuarios> builder)
    {
      builder.ToTable("Usuarios");
      builder.HasKey(x => x.IdUsuario);
      builder.Property(x => x.Nome);
      builder.Property(x => x.SobreNome);

      builder
        .HasOne(x => x.Escolaridade)
        .WithMany(x => x.ListaUsuarios)
        .HasForeignKey(x => x.CodEscolaridade);
    }
  }
}
