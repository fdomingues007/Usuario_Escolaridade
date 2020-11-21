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
    [StringLength(50)]
    public string Nivel { get; set; }
    //public List<Usuarios> ListaUsuarios { get; set; } = new List<Usuarios>();

    [ForeignKey ("CodEscolaridade")]
    public ICollection<Usuarios> usuarios { get; set; }

  }
}
