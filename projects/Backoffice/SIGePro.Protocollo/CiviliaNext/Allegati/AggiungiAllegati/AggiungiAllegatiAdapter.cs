using Init.SIGePro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Allegati.AggiungiAllegati
{
    public class AggiungiAllegatiAdapter
    {
        private long _idPratica;
        private long _idRegistro;
        private List<ProtocolloAllegati> _allegati;

        public AggiungiAllegatiAdapter(long idRegistro, long idPratica, List<ProtocolloAllegati> allegati)
        {
            this._idRegistro = idRegistro;
            this._idPratica = idPratica;
            this._allegati = allegati;
        }

        public AggiungiAllegatiRequest CreaRequest()
        {
            return new AggiungiAllegatiRequest
            {
                IdPratica = this._idPratica,
                Allegati = this._allegati
                                .Select( (x,y) => new AllegatoWS
                                { 
                                    Descrizione = x.Descrizione,
                                    DescrizioneFascicolo = null,
                                    File = Convert.ToBase64String( x.OGGETTO ),
                                    Id = null,
                                    IdSingoloFattura = null,
                                    IsPrincipale = y == 0,
                                    MimeType = x.MimeType,
                                    NomeFile = x.NOMEFILE,
                                    Percorso = null,
                                    Titolo = null
                                })
                                .ToList(),
                IdRegistro = this._idRegistro,
                GeneraMarcatura = false
            };
        }
    }
}
