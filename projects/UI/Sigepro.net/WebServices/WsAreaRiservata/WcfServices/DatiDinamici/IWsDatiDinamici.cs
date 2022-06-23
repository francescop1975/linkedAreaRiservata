using Init.SIGePro.Data;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices.DatiDinamici
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWsDatiDinamici" in both code and config file together.
    [ServiceContract]
    public interface IWsDatiDinamici
    {
        [OperationContract]
        RisultatoRicercaDatiDinamiciDto[] GetCompletionListRicerchePlus(string token, int idCampo, string partial, List<ValoreFiltroRicercaDto> filtri);

        [OperationContract]
        IstanzeDyn2Dati[] GetDyn2DatiByCodiceIstanza(string token, int codiceIstanza);

        [OperationContract]
        IstanzeDyn2Dati[] GetDyn2DatiByIdModello(string token, int codiceIstanza, int idModello, int indiceCampo);

        [OperationContract]
        ListaModelliDinamiciDomandaDto GetModelliDinamiciDaInterventoEEndo(string token, GetModelliDinamiciDaInterventoEEndoRequest request);

        [OperationContract]
        StrutturaModelloDinamicoDto GetStrutturaModelloDinamico(string token, int idModello);

        [OperationContract]
        RisultatoRicercaDatiDinamiciDto InitializeControlRicerchePlus(string token, int idCampo, string valore);

        [OperationContract]
        void RecuperaDocumentiIstanzaCollegata(string token, int codiceIstanzaOrigine, int idDomandaDestinazione);

        [OperationContract]
        DecodificaDto[] GetDecodificheAttive(string token, string tabella);
    }
}
