using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using Init.SIGePro.Manager.Logic.GestioneOneri;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.Formule
{
    public class CosapPisaService
    {
        private readonly AuthenticationInfo _authInfo;

        public CosapPisaService(AuthenticationInfo authInfo)
        {
            this._authInfo = authInfo;

        }

        public IEnumerable<StradarioReferente> ReferentiConfigurati()
        {

            using (var db = this._authInfo.CreateDatabase())
            {
                var sql = $"select * from pisa_cosap_referenti where idcomune = {db.Specifics.QueryParameterName("idcomune")} order by codicestrada, kminiziale";
                return db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                }, dr => new StradarioReferente
                {
                    IdComune = dr.GetString("idcomune"),
                    Id = dr.GetInt("id").Value,
                    CodiceStrada = dr.GetString("codicestrada"),
                    KmIniziale = dr.GetDouble("kminiziale").Value,
                    KmFinale = dr.GetDouble("kmfinale").Value,
                    CodiceCapoCantone = dr.GetInt("codicecapocantone"),
                    CodiceFunzionarioPO = dr.GetInt("codicefunzionariopo"),
                    CodiceTecnicoZona = dr.GetInt("codicetecnicozona").Value

                });
            }
        }

        /*
        public void InserisciTecnicoZona(int codiceIstanza, string codiceStrada, int kmIniziale, int metroIniziale)
        {

            var codiceTecnico = GetTecnicoZona(codiceStrada, kmIniziale, metroIniziale);

            var configurazione = GetConfigurazione();
            8
            if (codiceTecnico.HasValue)
            {
                new IstanzeServiceWrapper(this._authInfo.Token).InserisciTecnicoZonaNonPresente(codiceIstanza, codiceTecnico.Value, configurazione.FkTipoSoggettoTecnicoZona);
            }

        }
        */
        public void ImpostaOneri(int codiceIstanza, int codiceCausale, double importo)
        {
            new OneriService(this._authInfo).Inserisci(codiceIstanza, codiceCausale, importo);
        }

        private Configurazione GetConfigurazione()
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var sql = $"select * from pisa_cosap_configurazione where idcomune = {db.Specifics.QueryParameterName("idcomune")}";
                return db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                }, dr => new Configurazione
                {
                    IdComune = dr.GetString("idcomune"),
                    Id = dr.GetInt("id").Value,
                    CodiceMovimento = dr.GetString("fk_movimento_assegnazione_area")
                }).FirstOrDefault();
            }
        }

        public AreaDiCompetenza GetAreaDiCompetenza(string codiceStrada, int kmIniziale, int metroIniziale)
        {
            if (!String.IsNullOrEmpty(codiceStrada))
            {
                var inizio = kmIniziale + (metroIniziale / 1000.0d);

                var referentiConfigurati = this.ReferentiConfigurati();


                var codiceAmministrazione = referentiConfigurati
                    .Where(x =>
                        x.CodiceStrada == codiceStrada &&
                        x.KmIniziale <= inizio &&
                        x.KmFinale >= inizio)
                   .FirstOrDefault()?
                   .CodiceTecnicoZona;

                if (codiceAmministrazione.HasValue)
                {
                    return new AreaDiCompetenza
                    {
                        CodiceAmministrazione = codiceAmministrazione.Value,
                        Amministrazione = new AmministrazioniMgr(this._authInfo.CreateDatabase())
                                                .GetById(this._authInfo.IdComune, codiceAmministrazione.Value)?
                                                .AMMINISTRAZIONE
                    };
                }

            }
            return null;
        }

        public double GetImportoTariffa(string tipocalcolo, string durata, string tipooccupazione, string tipostrada, string tiposoglia, string occupazionestradale)
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var sql = $@"select tariffa from pisa_cosap_tariffe where 
                                idcomune = {db.Specifics.QueryParameterName("idcomune")} and
                                tipocalcolo = {db.Specifics.QueryParameterName("tipocalcolo")} and 
                                durata = {db.Specifics.QueryParameterName("durata")} and 
                                tipooccupazione = {db.Specifics.QueryParameterName("tipooccupazione")} and 
                                tipostrada = {db.Specifics.QueryParameterName("tipostrada")} and 
                                tiposoglia = {db.Specifics.QueryParameterName("tiposoglia")}";
                if (!String.IsNullOrEmpty(occupazionestradale))
                    sql += $" and  occupazionestradale = {db.Specifics.QueryParameterName("occupazionestradale")}";




                return db.ExecuteScalar(sql, 0.0d, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                    mapParameters.AddParameter("tipocalcolo", tipocalcolo);
                    mapParameters.AddParameter("durata", durata);
                    mapParameters.AddParameter("tipooccupazione", tipooccupazione);
                    mapParameters.AddParameter("tipostrada", tipostrada);
                    mapParameters.AddParameter("tiposoglia", tiposoglia);
                    if (!String.IsNullOrEmpty(occupazionestradale))
                        mapParameters.AddParameter("occupazionestradale", occupazionestradale);

                });
            }
        }

        public string GetTipoTariffaDaIntervento(int codiceIntervento)
        {


            using (var db = this._authInfo.CreateDatabase())
            {
                var scCodice = new AlberoProcMgr(db).GetById(codiceIntervento, this._authInfo.IdComune).SC_CODICE;

                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);
                return decodificheService.GetDecodificheAttive("INTCAL")
                    .Where(x => x.Chiave == scCodice.Substring(0, 2))
                    .Select(x => x.Valore)
                    .FirstOrDefault();
            }

        }

        public string GetUnitaMisuraDaSoglia(string idSoglia)
        {

            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);

                return decodificheService.GetDecodificheAttive("MISURA").Where(x => x.Raggruppamento.Contains(idSoglia)).Select(x => x.Valore).FirstOrDefault();
            }

        }

        public double GetQuantitaByUnitaMisura(int codiceIstanza, string idUnitaMisura)
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);

                var nomeCampo = decodificheService.GetDecodificheAttive("MISCAM").Where(x => x.Chiave == idUnitaMisura).Select(x => x.Valore).FirstOrDefault();

                if (!String.IsNullOrEmpty(nomeCampo))
                {
                    var valore = new IstanzeDyn2DatiMgr(db)
                         .GetListByCodiceIstanza(this._authInfo.IdComune, codiceIstanza)
                         .Where(x => x.CampoDinamico.Nomecampo == nomeCampo)?
                         .Select(x => x.Valore)
                         .FirstOrDefault();

                    if (!String.IsNullOrEmpty(valore))
                        return Convert.ToDouble(valore);
                }

                return 0.0d;
            }
        }

        public IEnumerable<RiepilogoSoglie> GetRiepilogoSoglie(int codiceIstanza, string tipocalcolo, string durata, string idTipologiaOccupazione, string tipostrada, string occupazionestradale)
        {

            if (String.IsNullOrEmpty(tipocalcolo) || String.IsNullOrEmpty(durata) || String.IsNullOrEmpty(idTipologiaOccupazione) || String.IsNullOrEmpty(tipostrada))
                return Enumerable.Empty<RiepilogoSoglie>();


            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);
                return decodificheService.GetDecodificheAttive("SOGLIE")
                    .Where(x => x.Raggruppamento.Contains(idTipologiaOccupazione))
                    .Select(soglia =>
                    {
                        var unitaMisura = this.GetUnitaMisuraDaSoglia(soglia.Chiave);
                        var importo = this.GetImportoTariffa(tipocalcolo, durata, idTipologiaOccupazione, tipostrada, soglia.Chiave, occupazionestradale);
                        var qt = this.GetQuantita(codiceIstanza, soglia.Chiave, unitaMisura);

                        return new RiepilogoSoglie(soglia.Valore, importo, unitaMisura, qt);
                    });
            }
        }

        public double GetQuantita(int codiceIstanza, string idSoglia, string idUnitaMisura)
        {

            using (var db = this._authInfo.CreateDatabase())
            {


                var quantita = this.GetQuantitaByUnitaMisura(codiceIstanza, idUnitaMisura);
                if (quantita == 0.0d) { quantita = 1; }


                var sql = $@"select 
                                maggioredi, finoa 
                            from 
                                pisa_cosap_soglierange 
                            where 
                                idcomune = {db.Specifics.QueryParameterName("idcomune")} and 
                                idsoglia = {db.Specifics.QueryParameterName("idsoglia")} and 
                                maggioredi < {db.Specifics.QueryParameterName("maggioredi")} 
                            order by 
                                maggioredi asc, {db.Specifics.NvlFunction("finoa", "999999") } asc";

                var qtInSoglia = db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                    mapParameters.AddParameter("idsoglia", idSoglia);
                    mapParameters.AddParameter("maggioredi", quantita);
                }, dr => new
                {
                    maggioreDi = dr.GetDouble("maggioredi").Value,
                    finoA = dr.GetDouble("finoA").GetValueOrDefault(double.MaxValue)
                }).FirstOrDefault();

                if (qtInSoglia == null)
                    return quantita;

                return (quantita > qtInSoglia.finoA) ? qtInSoglia.finoA : (quantita - qtInSoglia.maggioreDi);

            }

        }

        public string GetCategoriaStradarioByCodice(int codiceStradario, DateTime dataValidita)
        {
            try
            {
                using (var db = this._authInfo.CreateDatabase())
                {
                    var stradario = new StradarioMgr(db).GetById(this._authInfo.IdComune, codiceStradario);

                    if (stradario == null || (stradario.DATAVALIDITA.HasValue && DateTime.Compare(dataValidita, stradario.DATAVALIDITA.Value) > 0))
                        return null;

                    return stradario.CODVIARIO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Impossibile trovare una categoria per il codice stradario");
            }
        }

        public string GetTipoOccupazioneDaIntervento(int idIntervento)
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                return new AlberoProcMgr(db).GetPrimaOccorrenzaMetadato(this._authInfo.IdComune, idIntervento, "PISA_TIPOOCCUPAZIONE");
            }

        }

        public Int32 InserisciStradario(int codiceIstanza, int codiceStradario)
        {


            using (var db = this._authInfo.CreateDatabase())
            {
                var istanza = new IstanzeMgr(db).GetById(this._authInfo.IdComune, codiceIstanza);

                var auth = new Authorization
                {
                    alias = this._authInfo.Alias,
                    software = istanza.SOFTWARE,
                    token = this._authInfo.Token
                };

                var service = new IstanzeStradarioJsonService(auth, ParametriConfigurazione.Get.RestIstanzeStradarioJsonUrl);

                var stradari = service.GetStradari(codiceIstanza);
                var stradario = stradari.Where(x => x.CODICESTRADARIO == codiceStradario.ToString()).FirstOrDefault();

                if (stradario == null)
                {
                    stradario = new IstanzeStradario();
                    stradario.IDCOMUNE = this._authInfo.IdComune;
                    stradario.CODICEISTANZA = codiceIstanza.ToString();
                    stradario.CODICESTRADARIO = codiceStradario.ToString();
                    stradario.PRIMARIO = "1";
                    service.InserisciStradario(stradario);
                }

                return Convert.ToInt32(stradario.ID);

            }
        }

        public StradarioRuoloFunzionario InserisciRuoloFunzionario(int codiceIstanza, int codiceStradario, double kmDal, double kmAl)
        {
            var ruoli = this.GetRuoliFunzionari(codiceStradario, kmDal, kmAl);
            if (ruoli.Count() == 0)
            {
                return null;
            }

            var ruolo = ruoli.First();

            using (var db = this._authInfo.CreateDatabase())
            {

                var sql = $@"select 
                                count(*) as conteggio 
                            from
                                istanzeruoli
                            where
                                idcomune = {db.Specifics.QueryParameterName("idcomune")} and 
                                codiceistanza = {db.Specifics.QueryParameterName("codiceistanza")} and
                                idruolo = {db.Specifics.QueryParameterName("idruolo")} ";
                var conteggio = db.ExecuteScalar(sql, 0, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                    mapParameters.AddParameter("codiceistanza", codiceIstanza);
                    mapParameters.AddParameter("idruolo", ruolo.IdRuolo);
                });

                if (conteggio == 0)
                {
                    sql = $@"insert into istanzeruoli
                                    (idcomune,codiceistanza,idruolo) 
                                values 
                                (
                                    {db.Specifics.QueryParameterName("idcomune")},
                                    {db.Specifics.QueryParameterName("codiceistanza")},
                                    {db.Specifics.QueryParameterName("idruolo")}
                                )";
                    db.ExecuteNonQuery(sql, mapParameters =>
                    {
                        mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                        mapParameters.AddParameter("codiceistanza", codiceIstanza);
                        mapParameters.AddParameter("idruolo", ruolo.IdRuolo);
                    }
                    );
                }
            }

            return ruolo;

        }

        public IEnumerable<StradarioRuoloFunzionario> GetRuoliFunzionari(int codiceStradario, double kmDal, double kmAl)
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var sql = $@"select  
                                ruoli.id, ruoli.ruolo 
                            from 
                                pisa_cosap_ruolistrade 
                                    inner join ruoli on pisa_cosap_ruolistrade.idcomune = ruoli.idcomune and pisa_cosap_ruolistrade.idruolo = ruoli.id 
                            where 
                                pisa_cosap_ruolistrade.idcomune = {db.Specifics.QueryParameterName("idcomune")} and
                                pisa_cosap_ruolistrade.codicestradario = {db.Specifics.QueryParameterName("codicestradario")} and
                                pisa_cosap_ruolistrade.kmdal >= {db.Specifics.QueryParameterName("kmdal")} and
                                pisa_cosap_ruolistrade.kmal >= {db.Specifics.QueryParameterName("kmal")} 
                            order by pisa_cosap_ruolistrade.kmdal asc, pisa_cosap_ruolistrade.kmal asc";
                return db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", this._authInfo.IdComune);
                    mapParameters.AddParameter("codicestradario", codiceStradario);
                    mapParameters.AddParameter("kmdal", kmDal);
                    mapParameters.AddParameter("kmal", kmAl);

                }, dr => new StradarioRuoloFunzionario
                {
                    IdRuolo = dr.GetInt("id").Value,
                    Ruolo = dr.GetString("ruolo")
                });
            }
        }
    }
}
