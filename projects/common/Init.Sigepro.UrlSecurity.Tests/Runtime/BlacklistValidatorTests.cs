using Init.Sigepro.UrlSecurity.Configuration;
using Init.Sigepro.UrlSecurity.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init.Sigepro.UrlSecurity.Tests.Runtime
{
    [TestClass]
    public class BlacklistValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(RequestValidationException))]
        public void Se_parametro_contiene_parola_in_blacklist_solleva_eccezione()
        {
            var configuration = new StaticUrlSecurityConfiguration(true, new[] { "blacklisted" }, Enumerable.Empty<string>());
            var validator = new BlacklistValidator(configuration);
            var urlParams = new List<ParametroDaValidare>();
            urlParams.Add(new ParametroDaValidare("param", "parametro blacklisted", new HttpUtilityUrlDecoder()));

            validator.Validate(urlParams);
        }

        [TestMethod]
        public void Se_parametro_non_contiene_parola_in_blacklist_non_solleva_eccezione()
        {
            var configuration = new StaticUrlSecurityConfiguration(true, new[] { "blacklisted" }, Enumerable.Empty<string>());
            var validator = new BlacklistValidator(configuration);
            var urlParams = new List<ParametroDaValidare>();
            urlParams.Add(new ParametroDaValidare("param", "parametro asd asd ", new HttpUtilityUrlDecoder()));

            validator.Validate(urlParams);
        }
    }
}
