﻿using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Prisma.Classificazione
{
    public class ClassificheServiceWrapper : ExtendedServiceWrapper
    {
        private IProtocolloSerializer _serializer;
        public ClassificheServiceWrapper(string url, ProtocolloLogs logs, IProtocolloSerializer serializer, CredentialsInfo credential) : base(url, logs, credential)
        {
            this._serializer = serializer;
        }

        public ClassificheOutXML LeggiClassifiche(ClassificheInXML request)
        {
            try
            {
                using (var ws = base.CreaWebService())
                {
                    using (OperationContextScope scope = new OperationContextScope(ws.InnerChannel))
                    {
                        base.AggiungiCredenzialiAContextScope();
                        var requestXml = this._serializer.Serialize(ProtocolloLogsConstants.ListaClassificheRequest, request);
                        base.Logs.Info($"CHIAMATA A GET CLASSIFICHE {requestXml}");
                        var responseXml = ws.getClassifiche(base.Credentials.Username, base.Credentials.Token, requestXml);

                        base.Logs.Info("DESERIALIZZAZIONE DEI DATI DI RISPOSTA DA GET CLASSIFICHE");
                        var response = this._serializer.Deserialize<ClassificheOutXML>(responseXml);
                        base.Logs.Info("CHIAMATA A GET CLASSIFICHE AVVENUTA CORRETTAMENTE");

                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE IL RECUPERO DEL TITOLARIO, {ex.Message}", ex);
            }
        }

    }
}
