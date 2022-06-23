using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Authentication
{
    public interface IAuthenticationInfo
    {
        string Alias { get; set; }
        DataBase CreateDatabase();
    }
}
