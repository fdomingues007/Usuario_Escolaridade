using Domain.Entities;
using Domain.Interface;
using Infra.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<Usuarios> EmailExistente(string email, int idUsuario)
    {
      var usuario = new Usuarios();

      if (idUsuario == 0) // add
        usuario = await _efContext.Usuarios.Where(x => x.Email == email).FirstOrDefaultAsync();
      else // update
        usuario = await _efContext.Usuarios.Where(x => x.Email == email && x.IdUsuario != idUsuario).FirstOrDefaultAsync();

      return usuario;
  }

  public async Task<IEnumerable<Usuarios>> GetUsuarioEscolaridade()
  {
    return await _efContext.Usuarios.Include(x => x.Escolaridade).AsNoTracking().ToListAsync();
  }
}
}
