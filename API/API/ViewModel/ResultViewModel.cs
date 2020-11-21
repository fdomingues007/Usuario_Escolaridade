using System;
using System.Collections.Generic;
using System.Text;

namespace API.ViewModel
{
  public class ResultViewModel
  {
    public ResultViewModel() { }

    public ResultViewModel(bool erro, bool showmessage, string message, object data)
    {
      this.Erro = erro;
      this.Showmessage = showmessage;
      this.Message = message;
      this.Data = data;
    }

    public bool Erro { get; set; }
    public bool Showmessage { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
  }
}
