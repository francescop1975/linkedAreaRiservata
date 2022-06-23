﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.DocPro.Adapters;
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.DocPro.Interfaces;
using Init.SIGePro.Protocollo.DocPro.Builders;

namespace Init.SIGePro.Protocollo.DocPro.Configurations
{
    public class DocProSegnaturaParamConfiguration
    {
        public readonly string Operatore;
        public readonly string Flusso;
        public readonly string Classifica;
        public readonly string TipoDocumento;
        public readonly string Oggetto;
        public readonly DocProVerticalizzazioneParametriAdapter VertParams;
        //public readonly string DomicilioElettronico;
        public readonly DateTime DataRicevimento;
        public readonly IDatiDocProSegnaturaApplicativoProtocollo DatiIndirizzoApplicativoProtocollo;

        public DocProSegnaturaParamConfiguration(DocProVerticalizzazioneParametriAdapter vertParams, string operatore, DatiProtocolloIn datiProtoIn, 
                                                 /*string domicilioElettronico,*/ DateTime dataRicevimento)
        {
            
            
            VertParams = vertParams;
            Operatore = operatore;
            Classifica = datiProtoIn.Classifica;
            TipoDocumento = datiProtoIn.TipoDocumento;
            Oggetto = datiProtoIn.Oggetto;
            //DomicilioElettronico = domicilioElettronico;
            DataRicevimento = dataRicevimento;
            ListaMittDest listMittDest = null;

            if (datiProtoIn.Flusso == ProtocolloConstants.COD_ARRIVO)
            {
                Flusso = ProtocolloConstants.COD_ARRIVO_DOCAREA;
                listMittDest = datiProtoIn.Mittenti;
            }
            else if (datiProtoIn.Flusso == ProtocolloConstants.COD_PARTENZA)
            {
                Flusso = ProtocolloConstants.COD_PARTENZA_DOCAREA;
                listMittDest = datiProtoIn.Destinatari;
            }
            else if (datiProtoIn.Flusso == ProtocolloConstants.COD_INTERNO)
            {
                Flusso = ProtocolloConstants.COD_INTERNO_DOCAREA;
                listMittDest = datiProtoIn.Destinatari;
            }
            else
                throw new Exception(String.Format("FLUSSO {0} non supportato", datiProtoIn.Flusso));

            var listAmministrazioniEsterne = listMittDest.Amministrazione.Where(x => String.IsNullOrEmpty(x.PROT_UO) && String.IsNullOrEmpty(x.PROT_RUOLO)).ToList();
            var listAmministrazioniInterne = listMittDest.Amministrazione.Where(x => !String.IsNullOrEmpty(x.PROT_UO) || !String.IsNullOrEmpty(x.PROT_RUOLO)).ToList();

            if (listMittDest.Anagrafe.Count > 0)
                DatiIndirizzoApplicativoProtocollo = new DocProSegnaturaApplicativoProtocolloIndirizziAnagraficaBuilder(listMittDest.Anagrafe[0]);
            else if (listAmministrazioniEsterne.Count > 0)
                DatiIndirizzoApplicativoProtocollo = new DocProSegnaturaApplicativoProtocolloIndirizziAmministrazioneBuilder(listAmministrazioniEsterne[0]);
            else if ( listAmministrazioniInterne.Count > 0)
                DatiIndirizzoApplicativoProtocollo = new DocProSegnaturaApplicativoProtocolloIndirizziAmministrazioneBuilder(listAmministrazioniInterne[0]);
            else
                throw new Exception("NON SONO PRESENTI MITTENTI DESTINATARI");
            

        }
    }
}
