﻿using Init.SIGePro.Protocollo.JIride.PEC;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione.LeggiProtocollo
{
    public class LeggiProtocolloService
    {
        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;

        public LeggiProtocolloService(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;
        }

        public LeggiProtocolloResponse Leggi(LeggiProtocolloRequest request) 
        {

            this._protocolloLogs.Debug("Inizio metodo LeggiProtocollo");

            if (request.IdProtocollo.HasValue )
            {
                this._protocolloLogs.InfoFormat("idProtocollo: {0}, idProtocollo dopo formattazione: {1}", request.IdProtocolloVBG, request.IdProtocollo);
            }

            var response = new LeggiProtocolloResponse
            {
                DocumentoOut = this.LeggiProtocolloDocumento(request),
                IsCopia = request.IsCopia
            };


            if (!String.IsNullOrEmpty(request.UrlPec) && request.VisualizzaRicevutePec)
            {
                this._protocolloLogs.Debug("Inizio recupero eventuali esiti consegna/accettazione della PEC");
                var adapter = new PecAdapter(_protocolloSerializer);

                var requestXml = adapter.AdattaVerificaInvio(response.DocumentoOut.IdDocumento.ToString(), request.Operatore, request.Ruolo);

                var pecServiceWrapper = new PECServiceWrapper(request.UrlPec, this._protocolloLogs, this._protocolloSerializer);
                var responseVerificaInvio = pecServiceWrapper.VerificaInvio(requestXml, request.CodiceAmministrazione, request.AOO);
                this._protocolloLogs.Info($"RISPOSTA DESERIALIZZATA CORRETTAMENTE, NUMERO ACCETTAZIONI: {responseVerificaInvio.NumAccettazioni}");

                if (responseVerificaInvio.NumAccettazioni > 0)
                {
                    response.Accettazioni = responseVerificaInvio.AccettazioniItem.DatiAccettazioniItem.AccettazioneItem;
                }

                if (responseVerificaInvio.NumConsegne > 0)
                {
                    response.Consegne = responseVerificaInvio.ConsegneItem.DatiConsegneItem.ConsegneItem;
                }
            }

            return response;
           
        }


        private DocumentoOutXml LeggiProtocolloDocumento(LeggiProtocolloRequest request)
        {
            
            _protocolloLogs.InfoFormat($"Inizio metodo LeggiProtocolloDocumento, idprotocollo {request.IdProtocollo}, annoprotocollo {request.AnnoProtocollo}, numeroprotocollo {request.NumeroProtocollo}");
            if (request.IdProtocollo.HasValue || (request.NumeroProtocollo.HasValue && request.AnnoProtocollo.HasValue))
            {
                DocumentoOutXml docOut;
                var client = new LeggiProtocolloWSClient(this._protocolloLogs, this._protocolloSerializer, request.Url);

                GC.Collect();
                if ((!request.IdProtocollo.HasValue || request.UsaNumeroAnnoPerLettura) && request.NumeroProtocollo.HasValue)
                {
                    docOut = client.LeggiProtocollo(request.AnnoProtocollo.Value, request.NumeroProtocollo.Value, request.Operatore, request.Ruolo, request.CodiceAmministrazione, request.AOO);
                }
                else
                {
                    docOut = client.LeggiDocumento(request.IdProtocollo.Value, request.Operatore, request.Ruolo, request.CodiceAmministrazione, request.AOO);
                }

                return docOut;
            }
            else
                throw new Exception("NON È POSSIBILE RILEGGERE IL PROTOCOLLO/DOCUMENTO");
        }
    }
}
