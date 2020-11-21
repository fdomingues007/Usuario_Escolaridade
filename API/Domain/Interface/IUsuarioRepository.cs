using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
  public interface IUsuarioRepository : IRepositoryBase<Usuarios>
  {
    Task<IEnumerable<Usuarios>> GetUsuarioEscolaridade();
    Task<Usuarios> EmailExistente(string email, int idUsuario);
  }
}
