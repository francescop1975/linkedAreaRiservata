using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Authentication
{
    public interface IResolveToken
    {
        string Token { get; }
    }

    public class ConstantToken : IResolveToken
    {
        private readonly string _token;

        public ConstantToken(string token)
        {
            _token = token;
        }

        public string Token => this._token;
    }
}
