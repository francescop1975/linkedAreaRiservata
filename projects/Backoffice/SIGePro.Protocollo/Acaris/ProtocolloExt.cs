using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.Serialize;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris
{
    public class ProtocolloExt
    {
        public DataBase Db { get; }
        public ProtocolloLogs Logger { get; }
        public string Token { get; }
        public string IdComuneAlias { get; }
        public string Idcomune { get; }
        public string CodiceComune { get; }
        public string Software { get; }
        public IProtocolloSerializer Serialier { get; }
        public ParametriRegoleInfo Configurazione { get; }
        public DatiProtocolloIn DatiProtocollo { get; }
        public int? CodiceIstanza { get; internal set; }
        public string NumeroIstanza { get; }
        public string DescrizioneLavori { get; }
        public int? CodiceMovimento { get; internal set; }
        public List<String> Soggetti { get; }
        public string InterventoIstanza { get; }
        

        public ProtocolloExt(IProtocolloSerializer serializer, ProtocolloLogs logger, DatiProtocolloIn datiProtocollo, ProtocolloBase protocollo)
        {
            this.Serialier = serializer;
            this.Logger = logger;
            this.Token = protocollo.DatiProtocollo.Token;
            this.IdComuneAlias = protocollo.DatiProtocollo.IdComuneAlias;
            this.Idcomune = protocollo.DatiProtocollo.IdComune;
            this.CodiceComune = protocollo.DatiProtocollo.CodiceComune;
            this.Software = protocollo.DatiProtocollo.Software;
            this.Db = protocollo.DatiProtocollo.Db;
            this.DatiProtocollo = datiProtocollo;

            if (!String.IsNullOrEmpty(protocollo.DatiProtocollo.CodiceIstanza)) {
                this.CodiceIstanza = Convert.ToInt32(protocollo.DatiProtocollo.CodiceIstanza);
            }
            logger.DebugFormat($"Codice istanza {this.CodiceIstanza}");

            this.NumeroIstanza = protocollo.DatiProtocollo.NumeroIstanza;
            logger.DebugFormat($"Numero istanza {this.NumeroIstanza}");

            this.DescrizioneLavori = protocollo.DatiProtocollo.Istanza.LAVORI;
            logger.DebugFormat($"DescrizioneLavori {this.DescrizioneLavori}");

            if (!String.IsNullOrEmpty(protocollo.DatiProtocollo.CodiceMovimento))
            {
                this.CodiceMovimento = Convert.ToInt32(protocollo.DatiProtocollo.CodiceMovimento);
            }
            logger.DebugFormat($"Codice movimento {this.CodiceMovimento}");

            this.Soggetti = new List<string>();
            if (datiProtocollo.Mittenti.Anagrafe?.Count > 0)
            {
                this.Soggetti.AddRange(datiProtocollo.Mittenti.Anagrafe?.Select(x => x.GetNomeCompleto()));
            }
            if (datiProtocollo.Mittenti.Amministrazione?.Count > 0)
            {
                this.Soggetti.AddRange(datiProtocollo.Mittenti.Amministrazione?.Select(x => x.AMMINISTRAZIONE ));
            }
            logger.DebugFormat($"Soggetti {String.Join(" - ", this.Soggetti)}");

            this.InterventoIstanza = protocollo.DatiProtocollo.Istanza.Intervento.DescrizioneCompleta;
            logger.DebugFormat($"InterventoIstanza {this.InterventoIstanza}");

            var codiceAmministrazione = (datiProtocollo.Flusso == "A") ? datiProtocollo.Destinatari.Amministrazione[0].CODICEAMMINISTRAZIONE : datiProtocollo.Mittenti.Amministrazione[0].CODICEAMMINISTRAZIONE;

            this.Configurazione = new ParametriRegoleInfoAdapter(serializer, protocollo.Operatore, Convert.ToInt32(codiceAmministrazione), protocollo.DatiProtocollo).Adatta();

            logger.DebugFormat($"AnniConservazioneCorrente {this.Configurazione.AnniConservazioneCorrente}");
            logger.DebugFormat($"AnniConservazioneGenerale {this.Configurazione.AnniConservazioneGenerale}");
            logger.DebugFormat($"AppKey {this.Configurazione.AppKey}");
            logger.DebugFormat($"BackOfficePortUrl {this.Configurazione.BackOfficePortUrl}");
            logger.DebugFormat($"CodiceFiscale {this.Configurazione.CodiceFiscale}");
            logger.DebugFormat($"IdAOO {this.Configurazione.IdAoo}");
            logger.DebugFormat($"IdGradoVitalita {this.Configurazione.IdGradoVitalita}");
            logger.DebugFormat($"IdNodo {this.Configurazione.IdNodo}");
            logger.DebugFormat($"IdStruttura {this.Configurazione.IdStruttura}");
            logger.DebugFormat($"ObjectPortUrl {this.Configurazione.ObjectPortUrl}");
            logger.DebugFormat($"OfficialBookPortUrl {this.Configurazione.OfficialBookPortUrl}");
            logger.DebugFormat($"RepositoryID {this.Configurazione.RepositoryID}");

        }

        
    }
}
