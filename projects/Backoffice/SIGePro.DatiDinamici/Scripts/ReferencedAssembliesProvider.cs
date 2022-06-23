﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Init.SIGePro.DatiDinamici.Scripts
{
    internal class ReferencedAssembliesProvider
    {
        object _l = new object();
        static string[] _referencedAssemblies;
        // Contiene la lista dei files da non referenziare. Vanno inseriti tutti i nomi files
        // di dll native che possono essere inserite nella cartella bin dell'applicazione
        private static readonly string[] _blacklist = new string[]{
            "gsdll32.dll".ToUpperInvariant(),
            "gsdll64.dll".ToUpperInvariant()
        };

        private static readonly string[] _systemAssemblies = new string[]{
            "mscorlib.dll",
            "system.dll",
            "system.Core.dll",
            "system.data.dll",
            "System.Web.dll",
            "System.Xml.dll",
            "System.Web.Services.dll",
            "System.ServiceModel.dll",
            "System.Configuration.dll",
            "System.Data.OracleClient.dll",
            "System.Runtime.Serialization.dll"
        };

        internal ReferencedAssembliesProvider(Assembly assemblyinEsecuzione)
        {
            string referencePath = null;

            if (HttpContext.Current != null)
                referencePath = HttpContext.Current.Server.MapPath("~/bin");
            else
                referencePath = Path.GetDirectoryName(assemblyinEsecuzione.Location);

            lock (_l)
            {
                if (_referencedAssemblies == null)
                {
                    var dll = Directory.GetFiles(referencePath, "*.dll")
                                        .Where(x => !_blacklist.Contains(Path.GetFileName(x.ToUpperInvariant())));
                    _referencedAssemblies = _systemAssemblies.Union(dll).ToArray();
                }
            }
        }


        internal IEnumerable<string> GetAssemblyReferenziati()
        {
            return _referencedAssemblies;
        }
    }
}
