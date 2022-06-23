using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;


namespace Init.SIGePro.Protocollo.Acaris.Folder
{
    public interface IFolderTypeResolver
    {
        RepositoryId RepositoryId { get; }
        PrincipalId PrincipalId { get; }
        enumFolderObjectType TypeId { get; }
        PropertiesType Properties { get; }
        string Voce { get; }
        int IdTitolario { get; }
        string CodiceSerie { get; }
        string OggettoFascicolo { get; }
        ParametriRegoleInfo Configurazione { get; }
    }
}