using Domain.Entities;
using Domain.Interface;
using Infra.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository
{
  public class UsuarioRepository : RepositoryBase<Usuarios>, IUsuarioRepository
  {
    public UsuarioRepository(EfContext context) : base(context)
    {
    }
  }
}
