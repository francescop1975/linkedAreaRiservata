using Init.SIGePro.Protocollo.Acaris;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.MailTipoService;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.ProtocolloServices.MailTipo
{
    public class MailTipoCustomService : MailTipoService
    {
        private IProtocolloSerializer _serializer { get; }
        private string _token { get; }
        private int _codiceMailTipo { get; }
        private int? _codiceIstanza { get; }
        private int? _codiceMovimento { get; }

        public MailTipoCustomService(ProtocolloExt protocollo, int codiceMailTipo) : base(protocollo.Logger)
        {
            this._serializer = protocollo.Serialier;
            this._token = protocollo.Token;
            this._codiceMailTipo = codiceMailTipo;
            this._codiceIstanza = protocollo.CodiceIstanza;
            this._codiceMovimento = protocollo.CodiceMovimento;
        }

        public MailTipoCustomService(ProtocolloLogs logger, IProtocolloSerializer serializer, string token, int codiceMailTipo, int? codiceIstanza, int? codiceMovimento) : base(logger)
        {
            this._serializer = serializer;
            this._token = token;
            this._codiceMailTipo = codiceMailTipo;
            this._codiceIstanza = codiceIstanza;
            this._codiceMovimento = codiceMovimento;
        }

        public override MailTipoType GetMailTipo()
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    var request = new MailtipoRequest
                    {
                        token = this._token,
                        codicemailtipo = this._codiceMailTipo,
                        codiceistanza = this._codiceIstanza,
                        codicemovimento = this._codiceMovimento
                    };

                    this._serializer.Serialize(ProtocolloLogsConstants.MailTipoProtocolloSoapRequestFileName, request);

                    Logger.InfoFormat("Chiamata a mail e testo tipo, vedi request su file {0}", ProtocolloLogsConstants.MailTipoProtocolloSoapResponseFileName);

                    var response = ws.Mailtipo(request);
                    if (response != null)
                    {
                        return new MailTipoType
                        {
                            Oggetto = response.oggetto,
                            Corpo = response.corpo
                        };
                    }

                    return new MailTipoType();
                }
            }
            catch (Exception ex)
            {
                throw Logger.LogErrorException("ERRORE GENERATO DURANTE IL RECUPERO DELL'OGGETTO DALLE MAIL E TESTI TIPO DA ISTANZA", ex);
            }
        }
    }
}
