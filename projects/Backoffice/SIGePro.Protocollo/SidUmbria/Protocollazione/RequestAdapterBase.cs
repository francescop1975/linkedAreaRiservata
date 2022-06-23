using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.ProtocolloServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.SidUmbria.Protocollazione
{
    public class RequestAdapterBase
    {
        protected IDatiProtocollo _datiProto;
        protected VerticalizzazioniConfiguration _vert;
        protected ResolveDatiProtocollazioneService _datiIstanzaService;
        protected ProtocolloEnum.TipoProvenienza _provenienza;

        protected RequestAdapterBase(IDatiProtocollo datiProto, VerticalizzazioniConfiguration vert, ResolveDatiProtocollazioneService datiIstanzaService, ProtocolloEnum.TipoProvenienza provenienza)
        {
            this._datiProto = datiProto;
            this._vert = vert;
            this._datiIstanzaService = datiIstanzaService;
            this._provenienza = provenienza;
        }

        protected infoProtocollo AdattaDatiBase()
        {
            var protoFlusso = ProtocollazioneFlussoFactory.Create(this._datiProto, this._vert.DestinatarioCC);
            var protoIstMov = ProtocollazioneIstanzaMovimentoFactory.Create(this._datiIstanzaService);

            var allegati = this._datiProto.ProtoIn.Allegati;

            if (!_vert.DisabilitaControlloP7m)
            {
                if (this._provenienza == ProtocolloEnum.TipoProvenienza.ONLINE)
                {
                    allegati = this._datiProto.ProtoIn.Allegati.Where(x => x.Extension.EndsWith(".p7m")).ToList();
                }
            }

            string codiceMovimento = String.IsNullOrEmpty(_datiIstanzaService.CodiceMovimento) ? "0" : _datiIstanzaService.CodiceMovimento;

            var prefixAllegati = $"{_datiIstanzaService.CodiceIstanza}-{codiceMovimento}-{DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")}";
            var allegatiAdattati = AllegatiAdapter.Adatta(allegati, _vert.UsaFtp, prefixAllegati, _vert, this._datiProto.Ruolo, _vert.DisabilitaControlloP7m);

            var res = new infoProtocollo
            {
                corrispondenti = protoFlusso.GetCorrispondenti(),
                oggetto = this._datiProto.ProtoIn.Oggetto,
                idRichiesta = protoIstMov.IdentificativoRichiesta,
                tipo = protoFlusso.Flusso,
                tipologia = this._datiProto.ProtoIn.TipoDocumento,
                trasmissione = this._datiProto.ProtoIn.TipoSmistamento,
                nAllegati = allegati.Count(),
                allegati = allegatiAdattati
            };

            res.data = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);

            return res;
        }
    }
}
