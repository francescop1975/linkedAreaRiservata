using Init.SIGePro.Authentication;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneAntenneLucca
{
    public class AntenneLuccaService
    {
        private readonly string _idComune;
        private readonly DataBase _db;
        private readonly VerticalizzazioneAntenneLucca _configurazione;

        public AntenneLuccaService(DataBase db, string idComune, VerticalizzazioneAntenneLucca configurazione)
        {
            _idComune = idComune;
            _db = db;
            _configurazione = configurazione;
        }

        public IEnumerable<PraticaPerAntenna> GetListaPraticheByIdAntenna(string idAntenna)
        {
            var nomeCampoDinamico = this._configurazione.NomeCampoIdAntenna;

            if (!this._configurazione.Attiva)
            {
                throw new ArgumentException($"La verticalizzazione {this._configurazione.Nome} non è attiva");

            }

            if (String.IsNullOrEmpty(nomeCampoDinamico))
            {
                throw new ArgumentException($"Non è stato possibile recuperare il nome del campo dinamico, verificare la configurazione della verticalizzazione {this._configurazione.Nome}");
            }

            var sql = $@"SELECT 
                  istanze.codiceistanza,
                  istanze.numeroistanza,
                  istanze.data,
                  istanze.numeroprotocollo,
                  istanze.dataprotocollo,
                  alberoproc.descrizione_completa,
                  richiedenti.nominativo as ric_nominativo,
                  richiedenti.nome as ric_nome,
                  tecnici.nominativo as tec_nominativo,
                  tecnici.nome as ric_nome,
                  aziende.nominativo as az_nominativo
                FROM 
                  istanze 
                    INNER JOIN istanzedyn2dati ON
                      istanzedyn2dati.idcomune = istanze.idcomune AND
                      istanzedyn2dati.codiceistanza = istanze.codiceistanza

                    INNER JOIN dyn2_campi ON
                      dyn2_campi.idcomune = istanzedyn2dati.idcomune AND
                      dyn2_campi.id = istanzedyn2dati.fk_d2c_id

                    INNER JOIN alberoproc ON 
                      alberoproc.idcomune = istanze.idcomune AND
                      alberoproc.sc_id = istanze.codiceinterventoproc

                    INNER JOIN anagrafe richiedenti ON
                      richiedenti.idcomune = istanze.idcomune AND
                      richiedenti.codiceanagrafe = istanze.codicerichiedente

                    LEFT OUTER JOIN anagrafe tecnici ON
                      tecnici.idcomune = istanze.idcomune AND
                      tecnici.codiceanagrafe = istanze.codiceprofessionista

                    LEFT OUTER JOIN anagrafe aziende ON
                      aziende.idcomune = istanze.idcomune AND
                      aziende.codiceanagrafe = istanze.codicetitolarelegale

                                                                                    
                WHERE 
                  dyn2_campi.idcomune = {this._db.Specifics.QueryParameterName("idComune")} AND
                  dyn2_campi.nomecampo = {this._db.Specifics.QueryParameterName("nomeCampoDinamico")} AND
                  {this._db.Specifics.ToCharFunction("istanzedyn2dati.valore")} = {this._db.Specifics.QueryParameterName("idAntenna")}
                
                ORDER BY 
                  istanze.data desc";

            var istanze = this._db.ExecuteReader(sql,
                mp =>
                {
                    mp.AddParameter("idComune", this._idComune);
                    mp.AddParameter("nomeCampoDinamico", nomeCampoDinamico);
                    mp.AddParameter("idAntenna", idAntenna);
                },
                dr => new PraticaPerAntenna
                {
                    CodiceIstanza = dr.GetInt("codiceistanza").Value,
                    NumeroIstanza = dr.GetString("numeroIstanza"),
                    DataIstanza = dr.GetDateTime("data").Value,
                    NumeroProtocollo = dr.GetString("numeroprotocollo"),
                    DataProtocollo = dr.GetDateTime("dataprotocollo"),
                    Intervento = dr.GetString("descrizione_completa"),
                    Azienda = dr.GetString("az_nominativo"),
                    Richiedente = $"{dr.GetString("ric_nome")} {dr.GetString("ric_nominativo")}",
                    Tecnico = String.IsNullOrEmpty(dr.GetString("tec_nominativo")) ? "" : $"{dr.GetString("tec_nome")} {dr.GetString("tec_nominativo")}"
                });

            return istanze.Select(istanza => {

                istanza.AllegatiIntervento = GetAllegatiIntervento(istanza.CodiceIstanza);
                istanza.AllegatiEndoprocedimenti = GetAllegatiEndoprocedimenti(istanza.CodiceIstanza);
                istanza.AllegatiMovimenti = GetAllegatiMovimenti(istanza.CodiceIstanza);

                return istanza;
            });
        }

        private IEnumerable<FilePratica> GetAllegatiMovimenti(int codiceIstanza)
        {
            var sql = $@"SELECT
                              movimentiallegati.descrizione AS descrizione,
                              oggetti.nomefile,    
                              oggetti_metadati.valore AS uuid
                            FROM 
                              movimentiallegati

                                INNER JOIN movimenti ON
                                  movimenti.idcomune = movimentiallegati.idcomune AND 
                                  movimenti.codicemovimento = movimentiallegati.codicemovimento 

                                INNER JOIN oggetti ON                             
                                  oggetti.idcomune = movimentiallegati.idcomune AND 
                                  oggetti.codiceoggetto = movimentiallegati.codiceoggetto

                                INNER JOIN oggetti_metadati ON 
                                  oggetti_metadati.idcomune = oggetti.idcomune AND 
                                  oggetti_metadati.codiceoggetto = oggetti.codiceoggetto AND
                                  oggetti_metadati.chiave = 'UID'
                            WHERE 
                                movimenti.idcomune = {this._db.Specifics.QueryParameterName("idComune")} AND 
                                movimenti.codiceistanza = {this._db.Specifics.QueryParameterName("codiceIstanza")}";

            return this._db.ExecuteReader(sql,
                mp =>
                {
                    mp.AddParameter("idComune", this._idComune);
                    mp.AddParameter("codiceIstanza", codiceIstanza);
                },
                dr => new FilePratica
                {
                    Descrizione = dr.GetString("descrizione"),
                    NomeFile = dr.GetString("nomefile"),
                    Uid = dr.GetString("uuid")
                });
        }

        private IEnumerable<FilePratica> GetAllegatiEndoprocedimenti(int codiceIstanza)
        {
            var sql = $@"SELECT
                          istanzeallegati.allegatoextra AS descrizione,
                          oggetti.nomefile,    
                          oggetti_metadati.valore AS uuid
                        FROM 
                          istanzeallegati

                            INNER JOIN oggetti ON
                              oggetti.idcomune = istanzeallegati.idcomune AND 
                              oggetti.codiceoggetto = istanzeallegati.codiceoggetto

                            INNER JOIN oggetti_metadati ON 
                              oggetti_metadati.idcomune = oggetti.idcomune AND 
                              oggetti_metadati.codiceoggetto = oggetti.codiceoggetto AND
                              oggetti_metadati.chiave = 'UID'
                        WHERE 
                          istanzeallegati.idcomune = {this._db.Specifics.QueryParameterName("idComune")} AND 
                          istanzeallegati.codiceistanza = {this._db.Specifics.QueryParameterName("codiceIstanza")}";

            return this._db.ExecuteReader(sql,
                mp =>
                {
                    mp.AddParameter("idComune", this._idComune);
                    mp.AddParameter("codiceIstanza", codiceIstanza);
                },
                dr => new FilePratica
                {
                    Descrizione = dr.GetString("descrizione"),
                    NomeFile = dr.GetString("nomefile"),
                    Uid = dr.GetString("uuid")
                });
        }

        private IEnumerable<FilePratica> GetAllegatiIntervento(int codiceIstanza)
        {
            var sql = $@"SELECT
                          documentiistanza.documento as descrizione,
                          oggetti.nomefile,    
                          oggetti_metadati.valore as uuid
                        FROM 
                          documentiistanza

                            INNER JOIN oggetti ON
                              oggetti.idcomune = documentiistanza.idcomune AND 
                              oggetti.codiceoggetto = documentiistanza.codiceoggetto

                            INNER JOIN oggetti_metadati ON 
                              oggetti_metadati.idcomune = oggetti.idcomune AND 
                              oggetti_metadati.codiceoggetto = oggetti.codiceoggetto AND
                              oggetti_metadati.chiave = 'UID'
                        WHERE 
                          documentiistanza.idcomune = {this._db.Specifics.QueryParameterName("idComune")} AND 
                          documentiistanza.codiceistanza = {this._db.Specifics.QueryParameterName("codiceIstanza")}";

            return this._db.ExecuteReader(sql,
                mp =>
                {
                    mp.AddParameter("idComune", this._idComune);
                    mp.AddParameter("codiceIstanza", codiceIstanza);
                },
                dr => new FilePratica
                {
                    Descrizione = dr.GetString("descrizione"),
                    NomeFile = dr.GetString("nomefile"),
                    Uid = dr.GetString("uuid")
                });
        }
    }
}
