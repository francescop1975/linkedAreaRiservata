using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Logic.GestioneCommissioni.Protocollazione;
using Init.SIGePro.Protocollo.Manager;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.WsDataClass;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.GestioneCommissioni
{
    public class ProtocollazioneCommissioniService : IProtocollazioneCommissioneService
    {
        private readonly AuthenticationInfo _authInfo;

        public ProtocollazioneCommissioniService(AuthenticationInfo authInfo)
        {
            _authInfo = authInfo;
        }
        public EsitoProtocollazioneCommissione ProtocollaParereEsterno(int codiceIstanza, IRisolviMittenteProtocolloCommissione mittente, IRisolviOggettoProtocolloCommissione oggetto, IRisolviAllegatiProtocolloCommissione allegati)
        {
            try
            {
                var istanza = this.GetIstanza(codiceIstanza);
                var software = istanza.SOFTWARE;
                var codiceComune = istanza.CODICECOMUNE;
                var dati = new Dati
                {
                    Classifica = null,      // PARAMETRI OBBLIGATORI: lascio risolvere al servizio di protocollazione
                    Destinatari = null,     //
                    TipoDocumento = null,   //

                    Oggetto = oggetto.OggettoProtocollo,

                    Mittenti = mittente.Mittente == null ? null : new DatiMittenti
                    {
                        Anagrafe = mittente.Mittente.Anagrafiche.Select(x => new DatiAnagrafici
                        {
                            Cod = x.CodiceAnagrafe.ToString() 
                        }).ToList(),

                        Amministrazione = mittente.Mittente.Amministrazioni.Select(x => new DatiAnagrafici
                        {
                            Cod = x.CodiceAmministrazione.ToString()
                        }).ToList()
                    },

                    Allegati = allegati.Allegati.Select(x => new Allegato
                    {
                        Cod = x.CodiceOggetto.ToString(),
                        Descrizione = x.NomeFile,
                        InviaTramitePec = false
                    }).ToArray()
                };

                var source = (int)ProtocolloEnum.Source.PROT_IST_MOV_AUT_BO;
                var ambito = ProtocolloEnum.AmbitoProtocollazioneEnum.NESSUNO;
                var provenienza = ProtocolloEnum.TipoProvenienza.ONLINE;


                var mgr = new ProtocolloMgr(this._authInfo, software, codiceComune, ambito, istanza);

                var esito = mgr.Protocollazione(provenienza, dati, source);

                if (esito.Errore != null)
                {
                    return EsitoProtocollazioneCommissione.Fallito(esito.Errore.Descrizione, esito.Errore.StackTrace);
                }

                return EsitoProtocollazioneCommissione.Riuscito(esito.IdProtocollo, esito.NumeroProtocollo, esito.DataProtocollo);
            }
            catch (Exception ex)
            {
                return EsitoProtocollazioneCommissione.Fallito(ex);
            }
        }

        private DatiMittenti RisolviMittenti()
        {
            throw new NotImplementedException();
        }

        private Istanze GetIstanza(int codiceIstanza)
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                return new IstanzeMgr(db).GetById(this._authInfo.IdComune, codiceIstanza);
            }
        }
    }
}
