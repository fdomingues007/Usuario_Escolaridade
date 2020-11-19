using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
  public class Escolaridade
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CodEscolaridade { get; set; }
    public string Nivel { get; set; }
    public List<Usuarios> ListaUsuarios { get; set; } = new List<Usuarios>();

  }
}
