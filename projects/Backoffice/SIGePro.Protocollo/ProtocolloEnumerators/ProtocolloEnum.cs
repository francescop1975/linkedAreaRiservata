﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.ProtocolloEnumerators
{
    public static class ProtocolloEnum
    {
        public enum Source { NONPROTOCOLLARE = 0, INSERIMENTO_NORMALE = 1, ON_LINE = 2, INSERIMENTO_RAPIDO = 4, CONTR_RAMO_PADRE = 8, PROT_IST_MOV_AUT_BO = 16 }
        public enum TipoProvenienza { BACKOFFICE, ONLINE };
        public enum SourceFascicolazione { NONFASCICOLARE = 0, INSERIMENTO_NORMALE = 1, ON_LINE = 2, INSERIMENTO_RAPIDO = 4, CONTR_RAMO_PADRE = 8, FASC_IST_MOV_AUT_BO = 16 }
        //public enum Operatore { CODICEOPERATORE, SIGEPRO_USERID, RESPONSABILE, MATRICOLA, CODICEFISCALE, COD_UTE_DOCER }
        public enum TipiProtocollo
        {
            DEFAULT,
            NESSUNO,
            PROTOCOLLO_AIDA,
            PROTOCOLLO_ACARIS,
            PROTOCOLLO_APSYSTEMS,
            PROTOCOLLO_ARCHIFLOW,
            PROTOCOLLO_AURIGA,
            PROTOCOLLO_CIVILIA,
            PROTOCOLLO_CIVILIANEXT,
            PROTOCOLLO_DATAGRAPH,
            PROTOCOLLO_DEFAULT,
            PROTOCOLLO_DELTA,
            PROTOCOLLO_DOCAREA,
            PROTOCOLLO_DOCER,
            PROTOCOLLO_DOCPRO,
            PROTOCOLLO_DOCSPA,
            PROTOCOLLO_EGRAMMATA,
            PROTOCOLLO_EGRAMMATA2,
            PROTOCOLLO_EPROT,
            PROTOCOLLO_FOLIUM,
            PROTOCOLLO_GEPROT,
            PROTOCOLLO_HALLEY,
            PROTOCOLLO_HALLEY2,
            PROTOCOLLO_INSIEL,
            PROTOCOLLO_INSIEL2,
            PROTOCOLLO_INSIEL3,
            PROTOCOLLO_INSIELMERCATO,
            PROTOCOLLO_INSIELMERCATO2,
            PROTOCOLLO_ITCITY,
            PROTOCOLLO_IRIDE,
            PROTOCOLLO_IRIDE2,
            PROTOCOLLO_JIRIDE,
            PROTOCOLLO_JPROTOCOLLO,
            PROTOCOLLO_JPROTOCOLLO2,
            PROTOCOLLO_KIBERNETES,
            PROTOCOLLO_MICROSIS,
            PROTOCOLLO_PADOC,
            PROTOCOLLO_PAL,
            PROTOCOLLO_PALEO,
            PROTOCOLLO_PINDARO,
            PROTOCOLLO_PRISMA,
            PROTOCOLLO_PROTOINF,
            PROTOCOLLO_SICRAWEB,
            PROTOCOLLO_SIDOP,
            PROTOCOLLO_SIDUMBRIA,
            PROTOCOLLO_SIGEDO,
            PROTOCOLLO_SIGEPRO,
            PROTOCOLLO_SIPRWEB,
            PROTOCOLLO_SIPRWEBTEST,
            PROTOCOLLO_STUDIOK,
            PROTOCOLLO_TINN,
            PROTOCOLLO_URBI 
        }
        public enum TipiFascicolazione { CREA, CAMBIA }
        public enum TipoTestoTipo { Protocollazione, Fascicolazione }
        public enum AmbitoProtocollazioneEnum { DA_ISTANZA, DA_MOVIMENTO, DA_PANNELLO_PEC, NESSUNO };
        public enum IsAnnullato { si, no, nondefinito, warning };
        public enum TipoMittenteEnum { AZIENDA_RICHIEDENTE = 0, RICHIEDENTE = 1, AZIENDA = 2, AZIENDA_TECNICO = 3, TECNICO = 4 };
        public enum FornitoreDocAreaEnum { ADS, DATAMANAGEMENT, DATAGRAPH, MAGGIOLI, NON_DEFINITO };


    }
}
