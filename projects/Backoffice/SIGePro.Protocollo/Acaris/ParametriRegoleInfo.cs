using Init.SIGePro.Protocollo.Acaris.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris
{
    public class ParametriRegoleInfo
    {

        public RepositoryId RepositoryID;

        public PrincipalId PrincipalID;
        public IdAoo IdAoo { get; internal set; }
        public IdStruttura IdStruttura { get; internal set; }
        public IdNodo IdNodo { get; internal set; }

        public string BackOfficePortUrl { get; internal set; }
        public string DocumentPortUrl { get; internal set; }
        public string ManagementPortUrl { get; internal set; }
        public string NavigationPortUrl { get; internal set; }
        public string ObjectPortUrl { get; internal set; }
        public string OfficialBookPortUrl { get; internal set; }
        public string RelationshipsPortUrl { get; internal set; }

        public string AppKey { get; internal set; }
        public string CodiceFiscale { get; internal set; }
        public int AnniConservazioneCorrente { get; internal set; }
        public int AnniConservazioneGenerale { get; internal set; }
        public int IdGradoVitalita { get; internal set; }
        public int IdTitolario { get; internal set; }
        
        public string SerieFascicoliVerticalizzazione { get; internal set; }
    }
}
