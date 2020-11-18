using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Security
{
  public class SigningConfigurations
  {

    private const string SECRET_KEY = "7DEDA764-7708-4642-AE74-5FB5EB411300";

    public SigningCredentials SigningCredentials { get; }
    private readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

    public SigningConfigurations()
    {
      SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
    }
  }
}
