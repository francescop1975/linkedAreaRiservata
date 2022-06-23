using Init.SIGePro.Protocollo.Insiel3.Services;
using Init.SIGePro.Protocollo.Insiel3.Verticalizzazioni;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Insiel3.Protocollazione.MittentiDestinatari.GestioneAnagrafiche
{
    public class GestioneAnagraficheFactory
    {
        public static IGestioneAnagrafiche Create(InsielVerticalizzazioniConfiguration vert, ProtocolloLogs logs)
        {
            if (vert.TipoGestionePec == TipoGestioneAnagraficaEnum.TipoGestione.MONFALCONE)
            {
                return new AnagraficheMONFALCONE(logs, vert);
            }
            else if (vert.TipoGestionePec == TipoGestioneAnagraficaEnum.TipoGestione.RICERCA_CODICE_FISCALE)
            {
                return new AnagraficheRICERCACF(logs, vert);
            }
            else
            {
                return new AnagraficheRICERCADESC(logs, vert);
            }
        }
    }
}
