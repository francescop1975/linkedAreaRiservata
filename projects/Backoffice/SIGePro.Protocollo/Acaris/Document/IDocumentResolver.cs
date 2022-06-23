
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Entity;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Document
{
    public interface IDocumentResolver
    {
        string ObjectWSUrl { get; }
        string DocumentWsUrl { get; }
        string ManagementWsUrl { get; }

        string NavigationWSUrl { get; }
        List<string> MittentiPF { get; }
        List<string> MittentiPG { get; }
        List<string> DestinatariPG { get; }
        List<string> DestinatariPF { get; }
        int NumeroAllegati { get; }
        string OggettoDocumentoPrincipale { get; }
        RepositoryId RepositoryId { get; }
        PrincipalId PrincipalId { get; }
        string AnnotazioneDocPrincipale { get; }
        string UtenteEsteso { get; }
        ParametriRegoleInfo Configurazione { get; }
        string Flusso { get; }
        
    }
}
