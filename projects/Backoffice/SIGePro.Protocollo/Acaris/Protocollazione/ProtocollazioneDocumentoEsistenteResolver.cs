using Init.SIGePro.Protocollo.Acaris.Protocollazione.Registrazioni;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione
{
    public class ProtocollazioneDocumentoEsistenteResolver : AProtocollazioneResolver
    {
        private IRegistrazioneAPIResolver _registrazioneAPIResolver;


        public ProtocollazioneDocumentoEsistenteResolver(IRegistrazioneAPIResolver registrazioneAPIResolver, ParametriRegoleInfo parametri) : base(parametri)
        {
            this._registrazioneAPIResolver = registrazioneAPIResolver;
        }
        public override RegistrazioneRequest InfoRichiestaCreazione
        {
            get
            {
                return new ProtocollazioneDocumentoEsistente
                {
                    aooProtocollanteId = new ObjectIdType
                    {
                        value = this.Parametri.IdAoo.IdAcaris
                    },
                    senzaCreazioneSoggettiEsterni = true,
                    classificazioneId = new ObjectIdType
                    {
                        value = this._registrazioneAPIResolver.ClassificazioneID
                    },
                    registrazioneAPI = this._registrazioneAPIResolver.ResolveRegistrazione()
                };
            }
        }

        public override enumTipoRegistrazioneDaCreare TipologiaCreazione => enumTipoRegistrazioneDaCreare.ProtocollazioneDocumentoEsistente;
    }
}
