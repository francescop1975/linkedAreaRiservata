using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.WsDataClass;
using Init.SIGePro.Verticalizzazioni;

namespace Init.SIGePro.Protocollo.Manager
{
    public static class ProtocolloAllegatiExtensions
    {
        public static ProtocolloAllegati ToProtocolloAllegati(this Allegato allegato, Oggetti oggetto, string percorso, string contentType, ProtocolloMgr.NomeFileAllegato nomeAllegato, VerticalizzazioneProtocolloAttivo protoAttivo, List<ProtocolloAllegati> allegatiPrecedenti)
        {
            // var oggetto = protoAllegatiMgr.GetById(idComune, Convert.ToInt32(allegato.Cod));
            // var percorso = protoAllegatiMgr.GetPercorsoOggetto(idComune, Convert.ToInt32(allegato.Cod));

            // var nomeAllegato = new ProtocolloMgr.NomeFileAllegato(idComune, codiceComune, oggetto, allegato.Descrizione, protoAttivo.NomeFileMaxLength);

            var protoAllegati = new ProtocolloAllegati
            {
                MimeType = contentType,// protoAllegatiMgr.GetContentType(oggetto),
                Extension = nomeAllegato.GetEstensione(),
                NOMEFILE = nomeAllegato.GetNomeCompleto(protoAttivo.NomefileOrigine, allegatiPrecedenti, protoAttivo.CreaCopiaFile),
                Descrizione = nomeAllegato.GetDescrizioneFileCopia(allegato.Descrizione, allegatiPrecedenti, protoAttivo.CreaCopiaDescrFile, 0, protoAttivo.DescrFileMaxLength),
                CODICEOGGETTO = oggetto.CODICEOGGETTO,
                IDCOMUNE = oggetto.IDCOMUNE,
                OGGETTO = oggetto.OGGETTO,
                Percorso = percorso
            };

            protoAllegati.RimuoviCaratteriNonValidiDaNomeFile(protoAttivo.ListaCaratteriDaEliminare);

            return protoAllegati;
        }
    }
}
