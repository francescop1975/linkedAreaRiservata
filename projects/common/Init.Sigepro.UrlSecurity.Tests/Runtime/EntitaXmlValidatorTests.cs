using Init.Sigepro.UrlSecurity.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Init.Sigepro.UrlSecurity.Tests.Runtime
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class EntitaXmlValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(RequestValidationException))]
        public void Se_contiene_escape_esadecimali_solleva_eccezione()
        {
            var validator = new EntitaXmlValidator();
            var urlParams = new List<ParametroDaValidare>();
            urlParams.Add(new ParametroDaValidare("param", @"&#x6d;&#x6f;&#x75;&#x73;&#x65", new HttpUtilityUrlDecoder()));

            validator.Validate(urlParams);
        }

        [TestMethod]
        public void Se_non_contiene_escape_esadecimali_non_solleva_eccezione()
        {
            var validator = new EntitaXmlValidator();
            var urlParams = new List<ParametroDaValidare>();
            urlParams.Add(new ParametroDaValidare("param", @"stringa valida", new HttpUtilityUrlDecoder()));

            validator.Validate(urlParams);
        }
    }
}
