using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.ProtocolloInsielMercatoService;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.InsielMercato.Services;
using Init.SIGePro.Protocollo.InsielMercato.Verticalizzazioni;
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.InsielMercato.LeggiProtocollo;

namespace Init.SIGePro.Protocollo.InsielMercato.Protocollazione
{
    public class ProtocollazionePartenza : IProtocollazione
    {
        IDatiProtocollo _datiProto;
        ProtocollazioneService _wrapper;
        VerticalizzazioniConfiguration _vert;
        Istanze _istanza;

        public ProtocollazionePartenza(IDatiProtocollo datiProto, ProtocollazioneService wrapper, VerticalizzazioniConfiguration vert, Istanze istanza)
        {
            _datiProto = datiProto;
            _wrapper = wrapper;
            _vert = vert;
            _istanza = istanza;
        }

        public direction1 Flusso
        {
            get { return direction1.P; }
        }

        public sender[] GetMittenti()
        {
            return new sender[] { new sender { code = _datiProto.Uo } };
        }

        public recipient[] GetDestinatari()
        {
            var destinatariAnagrafe = _datiProto.AnagraficheProtocollo.Select(x => x.ToRecipientAnagrafe());

            var destinatariAmministrazioni = _datiProto.AmministrazioniEsterne.Select(x => x.ToRecipientAmministrazione());

            var destinatari = destinatariAnagrafe.Union(destinatariAmministrazioni);

            var dic = destinatari.GroupBy(x => x.description.ToUpperInvariant());
            var res = dic.Select(x => x.First()).ToArray();

            return res;
        }

        public document[] GetAllegati()
        {
            var allegati = _datiProto.ProtoIn.Allegati.Select(x => new document
            {
                name = x.NOMEFILE,
                file = x.OGGETTO,
                primary = false,
                primarySpecified = true
            }).ToArray();

            if (allegati.Length > 0)
            {
                var firstElement = allegati.First();
                firstElement.primary = true;
                firstElement.primarySpecified = true;
            }

            return allegati; ;
        }

        public string Registro
        {
            get { return _datiProto.Uo; }
        }

        public string CodiceUfficioOperante
        {
            get { return _datiProto.Ruolo; }
        }

        public DateTime? DataSpedizione
        {
            get { return DateTime.MaxValue; }
        }
    }
}
