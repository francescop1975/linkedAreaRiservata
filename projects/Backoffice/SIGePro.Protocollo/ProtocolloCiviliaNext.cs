using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.CiviliaNext;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati.AggiungiAllegati;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegati;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegato;
using Init.SIGePro.Protocollo.CiviliaNext.Assegnazione;
using Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.AnnullaProtocollo;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.InvioProtocollo;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.Protocolla;
using Init.SIGePro.Protocollo.CiviliaNext.Titolario;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_CIVILIANEXT : ProtocolloBase
    {
        public override DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn)
        {
            var datiProtocollo = new DatiProtocolloRes();

            //recupero parametri
            var par = this.RecuperaParametri();

            //protocollazione
            var requestProtocollazione = new ProtocolloAdapter(par, protoIn).CreaRequest();
            var responseProtocollazione = new ProtocolloService(par).Protocolla(requestProtocollazione);

            try
            {
                this.AssegnaAutomaticamenteIlProtocollo(par, responseProtocollazione.Result.IdPratica.Value, protoIn);
            }
            catch (Exception ex)
            {
                datiProtocollo.Warning += $"Si è verificato un errore durante l'assegnazione: {ex.Message} ";
            }


            try
            {
                if (protoIn.Allegati.Count > 0)
                {
                    //caricamento allegati
                    var requestAllegati = new AggiungiAllegatiAdapter(par.IdRegistro, responseProtocollazione.Result.IdPratica.Value, protoIn.Allegati).CreaRequest();
                    new AllegatiService(par).AggiungiAllegato(requestAllegati);
                }
            }
            catch (Exception ex)
            {
                datiProtocollo.Warning += $"Si è verificato un errore durante la protocollazione dei documenti: {ex.Message}";
            }

            if (protoIn.Flusso == ProtocolloConstants.COD_PARTENZA && par.InviaMail) 
            {
                try
                {
                    var invioProtocolloRequest = new InvioProtocolloAdapter(par, protoIn, responseProtocollazione.Result.IdPratica.Value).CreaRequest();
                    var response = new ProtocolloService(par).InvioProtocollo(invioProtocolloRequest);
                }
                catch (Exception ex)
                {
                    datiProtocollo.Messaggio = ex.Message;
                }
            }


            datiProtocollo.AnnoProtocollo = responseProtocollazione.Result.DataRegistrazione.Value.ToString("yyyy");
            datiProtocollo.DataProtocollo = responseProtocollazione.Result.DataRegistrazione.Value.ToString("dd/MM/yyyy");
            datiProtocollo.IdProtocollo = responseProtocollazione.Result.IdPratica.Value.ToString();
            datiProtocollo.NumeroProtocollo = responseProtocollazione.Result.NumeroProtocollo.ToString();
            

            return datiProtocollo;
        }

        public override DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {

            var idPratica = long.Parse(idProtocollo);

            //recupero parametri
            var par = this.RecuperaParametri();

            //creazione request
            var request = new CercaPraticheAdapter(par, idPratica).Adatta();

            //lettura del protocollo
            var response = new CercaPraticheService(par).CercaPratiche(request);
            var protocolloLetto = response.Result.First();

            //lettura dei riferimenti degli allegati al protocollo
            var requestGetAllegati = new GetAllegatiAdapter(par, idPratica).CreaRequest();
            var responseGetAllegati = new AllegatiService(par).GetAllegati(requestGetAllegati);

            var retVal = new DatiProtocolloLetto
            {
                Allegati = responseGetAllegati
                            .Result
                            .Select(x => new AllOut
                            {
                                Commento = x.Descrizione,
                                IDBase = $"{idProtocollo}-{x.Id.Value}",
                                Serial = x.NomeFile,
                            })
                            .ToArray(),
                AnnoProtocollo = protocolloLetto.DatiRegistrazioneCorrispondente.DataProtocollo.Value.Year.ToString(),
                Annullato = protocolloLetto.DatiAnnullamento.Annullata ? ProtocolloEnum.IsAnnullato.si.ToString() : ProtocolloEnum.IsAnnullato.no.ToString(),
                Classifica = protocolloLetto.Titolario,
                Classifica_Descrizione = protocolloLetto.Titolario,
                DataAnnullamento = protocolloLetto.DatiAnnullamento?.DataAnnullamento?.ToString("dd/MM/yyyy"),
                DataProtocollo = protocolloLetto.DatiRegistrazioneCorrispondente.DataProtocollo.Value.ToString("dd/MM/yyyy"),
                IdProtocollo = idProtocollo,
                MittentiDestinatari = protocolloLetto
                                        .ListaCorrispondenti
                                        .Select(x => new MittDestOut
                                        {
                                            CognomeNome = String.IsNullOrEmpty(x.Denominazione) ? $"{x.Cognome} {x.Nome}" : x.Denominazione
                                        })
                                        .ToArray(),
                //InCaricoA_Descrizione = this.RecuperaAmministrazioneDaCodiceOrganigramma(idProtocollo.Split(Convert.ToChar("-"))[1] ),
                MotivoAnnullamento = protocolloLetto.DatiAnnullamento?.MotivoAnnullamento,
                NumeroProtocollo = protocolloLetto.DatiRegistrazioneCorrispondente.NumeroProtocollo.Value.ToString(),
                Oggetto = protocolloLetto.Oggetto,
                Origine = this.DecodificaFlussoProtocollo(protocolloLetto.DatiRegistrazioneCorrispondente.TipoProtocollo),
            };

            return retVal;
        }

        public override AllOut LeggiAllegato()
        {
            //recupero parametri
            var par = this.RecuperaParametri();

            var riferimenti = IdAllegato.Split(Convert.ToChar("-"));
            var idPratica = long.Parse(riferimenti[0]);
            var idAllegato = long.Parse(riferimenti[1]);

            var requestGetAllegato = new GetAllegatoAdapter(par, idPratica).CreaRequest(idAllegato);
            var responseGetAllegato = new AllegatiService(par).DownloadAllegato(requestGetAllegato);

            return new AllOut
            {
                IDBase = IdAllegato,
                Image = Convert.FromBase64String(responseGetAllegato.Result.File),
                Commento = responseGetAllegato.Result.Descrizione,
                ContentType = responseGetAllegato.Result.MimeType,
                Serial = responseGetAllegato.Result.NomeFile,
            };
        }

        public override void AnnullaProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo, string motivoAnnullamento, string noteAnnullamento)
        {
            //recupero parametri
            var par = this.RecuperaParametri();

            var request = new AnnullaProtocolloAdapter(par, long.Parse(idProtocollo)).CreaRequest(motivoAnnullamento);
            new ProtocolloService(par).AnnullaProtocollo(request);
        }

        public override DatiProtocolloAnnullato IsAnnullato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            var protocollo = this.LeggiProtocollo(idProtocollo, annoProtocollo, numeroProtocollo);
            Enum.TryParse<EnumAnnullato>(protocollo.Annullato, out var protAnnullato);

            return new DatiProtocolloAnnullato
            {
                Annullato = protAnnullato,
                MotivoAnnullamento = protocollo.MotivoAnnullamento,
            };
        }

        public override void AggiungiAllegati(string idProtocollo, string numeroProtocollo, DateTime? dataProtocollo, IEnumerable<ProtocolloAllegati> allegati)
        {
            var par = this.RecuperaParametri();

            var requestAllegati = new AggiungiAllegatiAdapter(par.IdRegistro, long.Parse(idProtocollo), allegati.ToList()).CreaRequest();
            new AllegatiService(par).AggiungiAllegato(requestAllegati);
        }


        public override ListaTipiClassifica GetClassifiche()
        {
            try
            {
                //recupero parametri
                var par = this.RecuperaParametri();

                var request = new EstraiTitolarioRequest();

                var response = new EstraiTitolarioService(par).EstraiTitolario(request);

                var retVal = new List<ListaTipiClassificaClassifica>();

                var titoli = response
                                .Titoli
                                .Where(x => !x.Eliminato);

                foreach (var titolo in titoli)
                {
                    var classi = titolo
                                    .Classi
                                    .Where(x => !x.Eliminato);

                    retVal.Add(new ListaTipiClassificaClassifica
                    {
                        Codice = titolo.Codice,
                        Descrizione = $"{titolo.Codice} - {titolo.Descrizione}"
                    });

                    foreach (var classe in classi)
                    {
                        retVal.Add(new ListaTipiClassificaClassifica
                        {
                            Codice = classe.Codice,
                            Descrizione = $"{classe.Codice} - {classe.Descrizione}"
                        });

                        var sottoclassi = classe
                                            .SottoClassi
                                            .Where(c => !c.Eliminato)
                                            .Select(f => new ListaTipiClassificaClassifica
                                            {
                                                Codice = f.Codice,
                                                Descrizione = $"{f.Codice} - {f.Descrizione}",
                                            })
                                            .ToArray();
                        if (sottoclassi.Count() > 0)
                        {
                            retVal.AddRange(sottoclassi);
                        }
                    }
                }

                if (retVal.Count() == 0)
                {
                    throw new Exception("Non è stato trovato nessun titolario in corso di validità");
                }

                return new ListaTipiClassifica
                {
                    Classifica = retVal.ToArray(),
                    Errore = null
                };
            }
            catch (Exception ex)
            {
                return new ListaTipiClassifica
                {
                    Errore = new ErroreProtocollo
                    {
                        Descrizione = ex.Message,
                        StackTrace = ex.StackTrace
                    }
                };
            }
        }

        private void AssegnaAutomaticamenteIlProtocollo(ParametriRegoleInfo parametri, long idPratica, DatiProtocolloIn datiProtocollo)
        {
            var request = new AssegnaPraticaAdapter(parametri, idPratica, datiProtocollo).Adatta();
            new AssegnaPraticaService(parametri).AssegnaPratica(request);
        }

        private ParametriRegoleInfo RecuperaParametri()
        {
            return new ParametriRegoleInfoAdapter(this._protocolloLogs, this._protocolloSerializer, base.DatiProtocollo.Token, base.DatiProtocollo.IdComuneAlias, base.DatiProtocollo.Software, base.DatiProtocollo.CodiceComune, base.Operatore).Adatta();
        }

        private string DecodificaFlussoProtocollo(string tipoProtocollo)
        {
            switch (tipoProtocollo.ToUpper())
            {
                case "INGRESSO":
                    {
                        return ProtocolloConstants.COD_ARRIVO;
                    }
                case "USCITA":
                    {
                        return ProtocolloConstants.COD_PARTENZA;
                    }
                case "INTERNO":
                    {
                        return ProtocolloConstants.COD_INTERNO;
                    }
            }
            return null;
        }
    }
}
