using Init.SIGePro.Protocollo.Auriga.SharedInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Init.SIGePro.Protocollo.Auriga
{
    public static class Utility
    {
        public static LivelloGerarchiaType[] GetLivelloGerarchiaDaClassifica( string classifica )
        {
            if (!String.IsNullOrEmpty(classifica))
            {
                var retVal = new List<LivelloGerarchiaType>();
                var c = classifica.Split('.');

                for (int i = 0; i < c.Length; i++)
                {
                    retVal.Add(new LivelloGerarchiaType
                    {
                        Nro = (i + 1).ToString(),
                        Codice = c[i]
                    });
                }

                return retVal.ToArray();
            }
            return null;
        }

        public static string HtmlEncodeContent(string xml)
        {
            if(!String.IsNullOrEmpty(xml))
            {
                xml = HttpUtility.HtmlEncode(xml);
                xml = xml.Replace("&lt;", "<");
                xml = xml.Replace("&quot;", "\"");
                xml = xml.Replace("&gt;", ">");
            }

            return xml;
        }
    }
}
