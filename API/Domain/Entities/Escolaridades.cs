using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Escolaridade
    {
        public int CodEscolaridade { get; set; }
        public string Nivel { get; set; }
        public List<Usuarios> ListaUsuarios { get; set; } = new List<Usuarios>();
    }
}
