using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.Visura.QueryRicerca;
using log4net;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Visura
{
    public class VisuraPraticheV3Service
    {
        private readonly AuthenticationInfo _authInfo;
        private readonly ILog _log = LogManager.GetLogger(typeof(VisuraPraticheV3Service));

        public VisuraPraticheV3Service(AuthenticationInfo authInfo)
        {
            _authInfo = authInfo;
        }


        public RisultatoVisuraPraticaV3 GetListaPratiche(RichiestaListaPraticheV3 richiesta)
        {
            var limiteRecords = 200;

            using (var db = this._authInfo.CreateDatabase())
            {
                var query = new QueryRicercaPratiche(db, this._authInfo.IdComune);

                query.AddCondition(new SoftwarePraticaCondition(db, richiesta.Software));
                query.AddCondition(new CodiceFiscalePersonaAventeTitoloCondition(db, richiesta.PersonaAventeTitolo));
                query.AddCondition(new NomeOCfRichiedenteCondition(db, richiesta.NomeOCfRichiedente));
                query.AddCondition(new NumeroPraticaCondition(db, richiesta.NumeroIstanza));
                query.AddCondition(new MappaliCondition(db, richiesta.DatiCatastali));
                query.AddCondition(new DataIstanzaCondition(db, richiesta.PeriodoPresentazione));
                query.AddCondition(new IndirizzoCondition(db, richiesta.Indirizzo));
                query.AddCondition(new DatiProtocolloCondition(db, richiesta.DatiProtocollo));
                query.AddCondition(new ScCodiceInterventoCondition(db, GetScCodiceDaCodiceIntervento(db,richiesta.CodiceIntervento)));
                query.AddCondition(new NumeroAutorizzazioneCondition(db, richiesta.NumeroAutorizzazione));
                query.AddCondition(new OggettoPraticaCondition(db, richiesta.Oggetto));
                query.AddCondition(new StatoPraticaCondition(db, richiesta.StatoPratica));
                query.AddCondition(new EscludiInterventiCondition(db));
                query.AddCondition(new FabbricatoCondition(db, richiesta.Fabbricato));
                query.AddCondition(new PosizioneArchivioCondition(db, richiesta.PosizioneArchivio) );

                var queryVisura = query.GetQuery();

                this._log.Debug($"Query visura pratiche: {queryVisura}");

                var queryConta = $"select count(*) from ({queryVisura}) cntTbl";

                var count = db.ExecuteScalar(queryConta, 0, mp =>
                {
                    query.GetParameters().ToList().ForEach(x => mp.AddParameter(x.ParameterName, x.Value));
                });

                if (count > limiteRecords)
                {
                    return new RisultatoVisuraPraticaV3
                    {
                        LimiteRecordsSuperato = true
                    };
                }

                var pratiche = db.ExecuteReader(queryVisura,
                    mp =>
                    {
                        query.GetParameters().ToList().ForEach(x => mp.AddParameter(x.ParameterName, x.Value));
                    },
                    dr => new VisuraListItemV3
                    {
                        Azienda = (dr.GetString("AZ_NOMINATIVO") + " " + dr.GetString("AZ_NOME")).Trim(),
                        Civico = dr.GetString("PR_CIVICO"),
                        CodiceIstanza = dr.GetInt("IDPRATICA").Value,
                        DataPresentazione = dr.GetDateTime("DATAPRESENTAZIONE").Value,
                        DataProtocollo = dr.GetDateTime("DATAPROTOCOLLO"),
                        Foglio = dr.GetString("FOGLIO"),
                        LocalizzazioneConCivico = dr.GetString("INDIRIZZO_PREFISSO") + " " + dr.GetString("INDIRIZZO_DESCRIZIONE") + " " + dr.GetString("PR_CIVICO"),
                        NumeroIstanza = dr.GetString("NUMEROPRATICA"),
                        NumeroProtocollo = dr.GetString("NUMEROPROTOCOLLO"),
                        Oggetto = dr.GetString("OGGETTO"),
                        Particella = dr.GetString("PARTICELLA"),
                        PosizioneArchivio = dr.GetString("POSIZIONEARCHIVIO"),
                        CodiceArea = "-",
                        Operatore = dr.GetString("RESPONSABILE"),
                        Progressivo = "-",
                        Richiedente = (dr.GetString("RIC_NOMINATIVO") + " " + dr.GetString("RIC_NOME")).Trim(),
                        Software = dr.GetString("DESCSOFTWARE"),
                        Stato = dr.GetString("STATOPRATICA"),
                        Subalterno = dr.GetString("SUB"),
                        TipoCatasto = dr.GetString("TIPOCATASTO"),
                        TipoIntervento = dr.GetString("DESCRIZIONEINTERVENTO"),
                        TipoProcedura = dr.GetString("PROCEDURA"),
                        Uuid = dr.GetString("UUID")
                    });


                return new RisultatoVisuraPraticaV3
                {
                    LimiteRecordsSuperato = false,
                    Pratiche = pratiche.ToArray()
                };
            }
        }

        private string GetScCodiceDaCodiceIntervento(DataBase db, int? codiceIntervento)
        {
            if (codiceIntervento.HasValue)
            {
                var query = $"select sc_codice from alberoproc where idcomune = {db.Specifics.QueryParameterName("idcomune")} and sc_id = {db.Specifics.QueryParameterName("codiceIntervento")}";
                var parameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition("idcomune", this._authInfo.IdComune),
                    new ParameterDefinition("codiceIntervento", codiceIntervento)
                };

                var scCodice = db.ExecuteScalar<object>(query, null, mp =>
                {
                    parameters.ToList().ForEach(x => mp.AddParameter(x.ParameterName, x.Value));
                });

                return (scCodice == DBNull.Value) ? null : scCodice.ToString();
            }
            return null;
        }
    }
}
