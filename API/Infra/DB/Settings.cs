using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DB
{
  public static class Settings
  {
    const string servidor = "ADMIN-PC\\SQLEXPRESS";
    const string bd = "Fabio";
    const string usuariobd = "sa";
    const string senhabd = "teste123";
    // DEV
    public static string ConnectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=Fabio;Persist Security Info=True;User ID=sa;Password=teste123;";
  }
}
