using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Security
{
  public class TokenJwtOptions
  {
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int Seconds { get; set; }
  }
}
