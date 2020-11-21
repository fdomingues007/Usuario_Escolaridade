using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
  public class Usuarios
  {
    public Usuarios() { }
    public Usuarios(int codEscolaridade, string nome, string sobreNome, string email, DateTime? dtnascimento)
    {
      this.CodEscolaridade = codEscolaridade;
      this.Nome = nome;
      this.SobreNome = sobreNome;
      this.DtNascimento = dtnascimento;
      this.Email = email;
    }
    public Usuarios(int idusuario, int codEscolaridade, string nome, string sobreNome, string email, DateTime? dtnascimento)
    {
      this.IdUsuario = idusuario;
      this.CodEscolaridade = codEscolaridade;
      this.Nome = nome;
      this.SobreNome = sobreNome;
      this.DtNascimento = dtnascimento;
      this.Email = email;
    }

    public int IdUsuario { get; set; }
    [StringLength(90)]
    public string Nome { get; set; }
    [StringLength(200)]
    public string SobreNome { get; set; }
    [StringLength(200)]
    public string Email { get; set; }
    public DateTime? DtNascimento { get; set; }// aceita null, pois o documento do teste não especifica
    public int CodEscolaridade { get; set; }
    public Escolaridade Escolaridade { get; set; }

    private TudoOk tudoOk = new TudoOk();

    public TudoOk Valida(string nome, string email, DateTime? dtnascimento)
    {
      Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
      if (rg.IsMatch(email))
        tudoOk.Codigo = 0;
      else
      {
        tudoOk.Codigo = 1;
        tudoOk.Descricao = "E-mail inválido";
      }

      if (String.IsNullOrEmpty(nome))
      {
        tudoOk.Codigo = 2;
        tudoOk.Descricao = "É obrigatório preencher o nome!";
      }
      else if (nome.Length < 3 || nome.Length > 90)
      {
        tudoOk.Codigo = 3;
        tudoOk.Descricao = " O campo nome tem que possuir entre 3 e 90 caracteres!";
      }
      else if (email.Length > 200)
      {
        tudoOk.Codigo = 4;
        tudoOk.Descricao = " O tamanho do e-mail não pode ser maior que 200 caracteres!";
      }
      else if (DtNascimento > DateTime.Now)
      {
        tudoOk.Codigo = 5;
        tudoOk.Descricao = "Data de nascimento não pode ser maior que hoje!";
      }

      return tudoOk;
    }

  }
}
