using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace Init.Sigepro.FrontEnd.LR13.Utils
{
    public partial class DatiCompilazioneForm
    {

        protected static string GetNomeFileConfigurazione(string idComune, string software, int codiceIntervento)
        {
            var logger = LogManager.GetLogger(typeof(DatiCompilazioneForm));

            logger.DebugFormat("Verifica dell'esistenza di un file di configurazione per l'idcomune {0}, software {1} e codiceIntervento {2}", idComune, software, codiceIntervento);

            string fileName = "~/" + idComune + "_" + software + "_" + codiceIntervento.ToString() + "_FormCompilazione.xml";

            if (File.Exists(HttpContext.Current.Server.MapPath(fileName)))
                return fileName;

            logger.DebugFormat("Il file {0} non � stato trovato", fileName);

            fileName = "~/" + software + "_" + codiceIntervento.ToString() + "_FormCompilazione.xml";

            if (File.Exists(HttpContext.Current.Server.MapPath(fileName)))
                return fileName;

            logger.DebugFormat("Il file {0} non � stato trovato", fileName);

            fileName = "~/LR13_" + codiceIntervento.ToString() + "_FormCompilazione.xml";

            if (File.Exists(HttpContext.Current.Server.MapPath(fileName)))
                return fileName;

            logger.DebugFormat("Il file {0} non � stato trovato", fileName);

            fileName = String.Empty;

            return fileName;
        }

        public static bool VerificaEsistenzaFileConfigurazione(string idComune, string software, int codiceIntervento)
        {
            string fileName = GetNomeFileConfigurazione(idComune, software, codiceIntervento);

            return !String.IsNullOrEmpty(fileName);
        }

        public static DatiCompilazioneForm Load(string idComune, string software, int codiceIntervento)
        {
            var logger = LogManager.GetLogger(typeof(DatiCompilazioneForm));


            string fileName = GetNomeFileConfigurazione(idComune, software, codiceIntervento);

            if (String.IsNullOrEmpty(fileName))
            {
                logger.ErrorFormat("Non � stato trovato un file di configurazione per l'idcomune {0}, software {1} e codiceIntervento {2}", idComune, software, codiceIntervento);

                throw new ConfigurationErrorsException("Impossibile trovare il file di configurazione per il form di compilazione dati LR13");
            }

            DatiCompilazioneForm dcf = null;

            using (FileStream fs = File.OpenRead(HttpContext.Current.Server.MapPath(fileName)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(DatiCompilazioneForm));
                dcf = (DatiCompilazioneForm)xs.Deserialize(fs);
            }

            return dcf;
        }
    }
}
