using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Manager.Authentication;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager.Utils
{
    public class DatabaseCreator : IDatabaseCreator
    {
        private readonly IAuthenticationInfoResolver _resolver;

        public DatabaseCreator( IAuthenticationInfoResolver resolver )
        {
            this._resolver = resolver;
        }
        public DataBase Create()
        {
            return this._resolver.Resolve().CreateDatabase();
        }
    }
}
