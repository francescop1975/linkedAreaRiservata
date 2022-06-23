using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.ProtocolloInsielService3;
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Insiel3.Services;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.Insiel3.Protocollazione.MittentiDestinatari.GestioneAnagrafiche;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Insiel3.Verticalizzazioni;

namespace Init.SIGePro.Protocollo.Insiel3.Protocollazione.MittentiDestinatari
{
    public static class DestinatarioIOPExtensions
    {
        public static DestinatarioIOPInsProto GetDestinatarioIOPFromAnagrafe(this ProtocolloAnagrafe anagrafica, ProtocolloService srv, InsielVerticalizzazioniConfiguration vert, ProtocolloLogs logs)
        {
            var factory = GestioneAnagraficheFactory.Create(vert, logs);
            factory.Gestisci(new AnagraficaService(anagrafica), srv);

            var retVal = new DestinatarioIOPInsProto
            {
                descrizione = factory.Nominativo,
                invioTelemPec = vert.InviaPec,
                invioTelemPecSpecified = true,
                invioTelemMail = anagrafica.Pec
            };

            if (!String.IsNullOrEmpty(anagrafica.ModalitaTrasmissione))
            {
                retVal.modalitaTrasmissione = anagrafica.ModalitaTrasmissione;
            }

            return retVal;

        }

        public static DestinatarioIOPInsProto GetDestinatarioIOPFromAmministrazione(this Amministrazioni amm, ProtocolloService srv, InsielVerticalizzazioniConfiguration vert, ProtocolloLogs logs)
        {
            var factory = GestioneAnagraficheFactory.Create(vert, logs);
            factory.Gestisci(new AmministrazioneService(amm), srv);

            var retVal = new DestinatarioIOPInsProto()
            {
                descrizione = factory.Nominativo,
                invioTelemPec = vert.InviaPec,
                invioTelemPecSpecified = true,
                invioTelemMail = amm.PEC
            };

            if (!String.IsNullOrEmpty(amm.ModalitaTrasmissione))
            {
                retVal.modalitaTrasmissione = amm.ModalitaTrasmissione;
            }

            return retVal;
        }
    }
}
