using Init.SIGePro.Manager.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Utils
{
    public class IdComuneResolver : IIdComuneResolver
    {
        private readonly IAuthenticationInfoResolver _resolver;

        public IdComuneResolver( IAuthenticationInfoResolver resolver )
        {
            this._resolver = resolver;
        }

        public string IdComune => this._resolver.Resolve().IdComune;
    }
}
