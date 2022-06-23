﻿using System;
using Init.SIGePro.Protocollo.Insiel3.LeggiProtocollo;
using Init.SIGePro.Protocollo.Insiel3.Services;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloInsiel3FilesTransferService;
using Init.SIGePro.Protocollo.ProtocolloInsielService3;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.WsDataClass;

namespace Init.SIGePro.Protocollo.Insiel3.Storico
{
    public class ServiceWrapperStorico
    {
        private ParametriStorici _par;
        private ProtocolloSerializer _protocolloSerializer;
        private ProtocolloLogs _protocolloLogs;

        private bool _usaLivelliClassifica = true;

        public ServiceWrapperStorico(ParametriStorici par, ProtocolloSerializer protocolloSerializer, ProtocolloLogs protocolloLogs)
        {
            this._par = par;
            this._protocolloSerializer = protocolloSerializer;
            this._protocolloLogs = protocolloLogs;
        }


        internal DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string separatore, string annoProtocollo, string numeroProtocollo, string codiceUfficio)
        {
            try
            {
                using (var ws = new ProtocolloService(this._par.URLLeggiProtocollo, this._protocolloLogs, this._protocolloSerializer, this._par.Utente).CreaWebService())
                {

                    var utente = new Utente { Item = this._par.Utente, ItemElementName = ItemChoiceType.codice };

                    var request = new DettagliProtocolloRequest
                    {
                        utente = utente,
                        registrazione = LeggiProtocolloRequest(idProtocollo, separatore, annoProtocollo, numeroProtocollo, codiceUfficio, verso.A)
                    };

                    this._protocolloSerializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloStoricoRequestFileName, request);

                    var serviceResponse = ws.dettagliProtocollo(request);

                    if (!serviceResponse.esito)
                    {
                        var err = (Errore)serviceResponse.Item;

                        if (err.codice == "-1000" )
                        {
                            this._protocolloLogs.Info("PROTOCOLLO NON TROVATO CON FLUSSO A, TENTATIVO CON FLUSSO P");
                            request = new DettagliProtocolloRequest
                            {
                                utente = utente,
                                registrazione = LeggiProtocolloRequest(idProtocollo, separatore, annoProtocollo, numeroProtocollo, codiceUfficio, verso.P)
                            };
                            this._protocolloSerializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloStoricoRequestFileName, request);
                            serviceResponse = ws.dettagliProtocollo(request);
                            if (!serviceResponse.esito)
                            {
                                err = (Errore)serviceResponse.Item;
                                throw new Exception(String.Format("CODICE {0}, DESCRIZIONE: {1}", err.codice, err.descrizione));
                            }
                        }
                        else
                        {
                            throw new Exception(String.Format("CODICE {0}, DESCRIZIONE: {1}", err.codice, err.descrizione));
                        }
                    }

                    var response =  (DettagliProtocollo)serviceResponse.Item;
                    return new LeggiProtocolloOutputAdapter(response, this._protocolloLogs).Adatta(this._usaLivelliClassifica);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private ProtocolloRequest LeggiProtocolloRequest(string idProtocollo, string separatore, string annoProtocollo, string numeroProtocollo, string codiceUfficio, verso direzione)
        {
            return String.IsNullOrEmpty(idProtocollo) ? LeggiProtocolloDaNumeroEAnno(annoProtocollo, numeroProtocollo, codiceUfficio, direzione) : LeggiProtocolloDaId( idProtocollo, separatore );
        }
        private ProtocolloRequest LeggiProtocolloDaNumeroEAnno(string annoProtocollo, string numeroProtocollo, string codiceUfficio, verso direzione )
        {
            return new ProtocolloRequest
            {
                Item = new ProtocolloRequestIdentificatoreProt
                {
                    anno = annoProtocollo,
                    numero = numeroProtocollo,
                    codiceRegistro = this._par.CodiceRegistro,
                    codiceUfficio = codiceUfficio,
                    verso = direzione
                }
            };
        }
        private ProtocolloRequest LeggiProtocolloDaId(string idProtocollo, string separatore)
        {

            var idProto = new IdProtocolloAdapter( idProtocollo, separatore).Adatta();

            return new ProtocolloRequest
            {
                Item = new IdProtocollo
                {
                    progDoc = idProto.ProgDoc,
                    progMovi = idProto.ProgMovi
                }
            };
        }

        internal AllOut LeggiAllegatoStorico( string idDocumento, string idProtocollo, string separatore)
        {
            try
            {
                var dettaglioAllegato = GetDettagliAllegato(Convert.ToInt64(idDocumento), idProtocollo, separatore);
                var fileAllegato = DownloadAllegato(dettaglioAllegato.idFile);

                return new AllOut
                {
                    Image = fileAllegato.binaryData,
                    IDBase = dettaglioAllegato.idFile,
                    Serial = dettaglioAllegato.name,
                    Commento = dettaglioAllegato.name
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DettaglioAllegato GetDettagliAllegato(long idDocumento, string idProtocollo, string separatore)
        {
            try
            {
                using (var ws = new ProtocolloService(this._par.URLLeggiProtocollo, this._protocolloLogs, this._protocolloSerializer, this._par.Utente).CreaWebService())
                {

                    var utente = new Utente { Item = this._par.Utente, ItemElementName = ItemChoiceType.codice };
                    var idProto = new IdProtocolloAdapter(idProtocollo, separatore).Adatta();

                    var request = new DownloadDocumentoRequest
                    {
                        utente = utente,
                        idDoc = idDocumento,
                        registrazione = new ProtocolloRequest
                        {
                            Item = new IdProtocollo
                            {
                                progDoc = idProto.ProgDoc,
                                progMovi = idProto.ProgMovi
                            }
                        }
                    };

                    this._protocolloSerializer.Serialize(ProtocolloLogsConstants.LeggiAllegatoStoricoRequest, request);
                    var serviceResponse = ws.downloadDocumento(request);

                    if (!serviceResponse.esito)
                    {
                        var err = (Errore)serviceResponse.Item;
                        throw new Exception(String.Format("CODICE: {0}, DESCRIZIONE: {1}", err.codice, err.descrizione));
                    }

                    var dettaglioAllegato = (DettaglioAllegato)serviceResponse.Item;
                    this._protocolloLogs.InfoFormat("LETTURA DEL DOCUMENTO AVVENUTA CORRETTAMENTE, NOME FILE RESTITUITO: {0}", dettaglioAllegato.name);
                    return dettaglioAllegato;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private DownloadFileType DownloadAllegato(string idFile)
        {

            try
            {
                using (var ws = new AllegatiService(this._par.URLLeggiAllegati, this._protocolloLogs, this._protocolloSerializer).CreaWebService())
                {
                    var request = new DownloadFileRequest { idFile = idFile };
                    this._protocolloLogs.InfoFormat("RICHIESTA DI DOWNLOAD FILE ID: {0}", request.idFile);

                    var response = ws.downloadFile(request);


                    if (!response.esito)
                    {
                        var err = (ErroreType)response.Item;
                        throw new Exception(String.Format("CODICE: {0}, DESCRIZIONE: {1}", err.codice, err.descrizione));
                    }

                    this._protocolloLogs.InfoFormat("DOWNLOAD FILE ID: {0} DA WS EFFETTUATO CORRETTAMENTE", request.idFile);

                    return (DownloadFileType)response.Item;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("IL DOWNLOAD DEL FILE DA WS HA RESTITUITO IL SEGUENTE ERRORE: {0}", ex.Message), ex);
            }
        }
    }
}
