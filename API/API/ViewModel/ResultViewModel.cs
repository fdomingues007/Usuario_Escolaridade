using System;
using System.Collections.Generic;
using System.Text;

namespace API.ViewModel
{
  public class ResultViewModel
  {
    public ResultViewModel() { }

    public ResultViewModel(bool showmessage, string message, object data)
    {
      this.showmessage = showmessage;
      this.message = message;
      this.data = data;
    }

    public bool showmessage { get; set; }
    public string message { get; set; }
    public object data { get; set; }
  }
}
