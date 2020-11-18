using Domain.Entities;
using Domain.Interface;
using Infra.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository
{

  public class EscolaridadeRepository : RepositoryBase<Escolaridade>, IEscolaridadeRepository
  {
    public EscolaridadeRepository(EfContext context) : base(context)
    {
    }
  }
}
