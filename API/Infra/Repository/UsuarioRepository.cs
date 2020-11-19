using Domain.Entities;
using Domain.Interface;
using Infra.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
  public class UsuarioRepository : RepositoryBase<Usuarios>, IUsuarioRepository
  {
    private readonly EfContext _efContext;
    public UsuarioRepository(EfContext context) : base(context)
    {
      _efContext = context;
    }
   
    public async Task<IEnumerable<Usuarios>> GetUsuarioEscolaridade()
    {
      return await _efContext.Usuarios.Include(x => x.Escolaridade).AsNoTracking().ToListAsync();
    }
  }
}
