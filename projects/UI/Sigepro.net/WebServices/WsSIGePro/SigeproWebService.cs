using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Init.SIGePro.Authentication;
using Init.SIGePro.Exceptions.Token;
using Init.SIGePro.Manager.Authentication;

namespace Sigepro.net.WebServices.WsSIGePro
{
	public class SigeproWebService : Ninject.Web.WebServiceBase
	{
		protected AuthenticationInfo CheckToken(string token)
		{
			AuthenticationInfo authInfo = AuthenticationManager.CheckToken(token);

			if (authInfo == null)
				throw new InvalidTokenException(token);

			return authInfo;
		}
	}
}
