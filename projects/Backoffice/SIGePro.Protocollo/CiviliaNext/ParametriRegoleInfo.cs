using Init.SIGePro.Manager.Utils;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext
{
    public class ParametriRegoleInfo
    {
        public ProtocolloLogs Logger { get; set; }
        public ProtocolloSerializer Serializer { get; set; }
        public string AssegnaPraticaURL { get; set; }
        public string AggiungiAllegatoURL { get; internal set; }
        public string AnnullaProtocolloURL { get; internal set; }
        public string CercaPraticheURL { get; internal set; }
        public string CodiceLivelloOrganigramma { get; internal set; }
        public long IdCodiceAOO { get; internal set; }
        public bool InviaMail { get; internal set; }
        public long IdOperatore { get; internal set; }
        public string ClientID { get; internal set; }
        public string ClientSercret { get; internal set; }
        public string UrlOAuth { get; internal set; }
        public string ProtocollazioneURL { get; internal set; }
        public string GrantType => "client_credentials";
        public string Resource { get; internal set; }
        public long IdRegistro { get; internal set; }
        public string Token { get; internal set; }
        public string EstraiTitolarioURL { get; internal set; }
        public string GetAllegatiURL { get; internal set; }
        public string GetAllegatoURL { get; internal set; }

        public string RicercaLivelloURL { get; set; }
        public string IdCasellaEmail { get; set; }
        public string InviaProtocolloURL { get; set; }
    }
}
