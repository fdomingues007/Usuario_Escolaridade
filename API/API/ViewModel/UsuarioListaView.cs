using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
  public class UsuarioListaView
  {
    public int idusuario { get; set; }
    public int codescolaridade { get; set; }
    public string nivel { get; set; }
    public string nome { get; set; }
    public string sobrenome { get; set; }
    public DateTime? dtnascimento { get; set; }// aceita null, pois o documento do teste não especifica
    public DateTime? dtinclusao { get; set; }

  }
}
