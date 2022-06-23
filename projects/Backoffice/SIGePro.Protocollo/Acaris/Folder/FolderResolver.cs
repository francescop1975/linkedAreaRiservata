using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Folder
{
    public abstract class FolderResolver : IFolderTypeResolver //, IDocumentResolver
    {
        public ParametriRegoleInfo Parametri { get; }
        public RepositoryId RepositoryId => this.Parametri.RepositoryID;
        public PrincipalId PrincipalId => this.Parametri.PrincipalID;
        public string ObjectWSUrl => this.Parametri.ObjectPortUrl;
        public string DocumentWsUrl => this.Parametri.DocumentPortUrl;
        public int IdTitolario => this.Parametri.IdTitolario;

        public abstract string CodiceSerie { get; }
        public string ManagementWsUrl => this.Parametri.ManagementPortUrl;
        public string NavigationWSUrl => this.Parametri.NavigationPortUrl;
        public FolderResolver(ParametriRegoleInfo parametri)
        {
            this.Parametri = parametri;
        }

        public abstract enumFolderObjectType TypeId { get; }
        public abstract PropertiesType Properties { get; }
        public abstract string Voce { get; }
        public abstract string OggettoDocumentoPrincipale { get; }
        public abstract List<string> MittentiPG { get; }
        public abstract List<string> MittentiPF { get; }
        public abstract List<string> DestinatariPG { get; }
        public abstract List<string> DestinatariPF { get; }

        public abstract int NumeroAllegati { get; }
        public abstract string NomeFilePrincipale { get; }
        public abstract string AnnotazioneDocPrincipale { get; }
        public abstract string UtenteEsteso { get; }
        public abstract string OggettoFascicolo { get; }
        public abstract IEnumerable<OggettiMetadati> GetMetadati(int codiceOggetto);

        public abstract IdFolder idFolder { get; }

        public ParametriRegoleInfo Configurazione => this.Parametri;
    }
}
