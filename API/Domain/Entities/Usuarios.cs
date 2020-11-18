using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Usuarios
    {
        public Usuarios() { }
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public DateTime? DtNascimento { get; set; }// aceita null, pois o documento do teste não especifica

        private Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

        public int CodEscolaridade { get; set; }
        public Escolaridade Escolaridade { get; set; }

        public Usuarios(int codEscolaridade, string nome, string sobreNome, string email, DateTime? dtNascimento)
        {
            this.CodEscolaridade = codEscolaridade;
            this.Nome = nome;
            this.SobreNome = sobreNome;
            this.DtNascimento = DtNascimento;
            this.Email = email;

            //if (rg.IsMatch(email))
            //   this.email = email;
            //else
            //  throw new Exception("Email inválido");
        }

        public Usuarios(int idusuario, int codEscolaridade, string nome, string sobreNome, string email, DateTime? dtnascimento)
        {
            this.IdUsuario = idusuario;
            this.CodEscolaridade = codEscolaridade;
            this.Nome = nome;
            this.SobreNome = sobreNome;
            this.DtNascimento = DtNascimento;
            this.Email = email;
        }


    }
}
