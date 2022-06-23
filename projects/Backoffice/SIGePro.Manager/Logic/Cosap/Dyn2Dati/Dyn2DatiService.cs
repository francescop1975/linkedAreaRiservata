using Init.SIGePro.Authentication;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze;
using PersonalLib2.Data;
using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.Cosap.Dyn2Dati
{
    public class Dyn2DatiService
    {
        private readonly AuthenticationInfo _authInfo;
        private readonly int _codiceIstanza;
        private readonly IstanzeDyn2DatiRepository _repository;

        public Dyn2DatiService(AuthenticationInfo authInfo, int codiceIstanza)
        {
            this._authInfo = authInfo;
            this._codiceIstanza = codiceIstanza;
            this._repository = new IstanzeDyn2DatiRepository(this._authInfo.CreateDatabase(), codiceIstanza, this._authInfo.IdComune);
        }

        public void SalvaValoriCampi(int idModello, IEnumerable<Dyn2Dato> dati)
        {
            foreach (var dato in dati)
            {
                this.SalvaValoreCampo(idModello, dato);
            }
        }

        public void SalvaValoreCampo(int idModello, CampoDaSalvare campoDaSalvare)
        {
            var datiModello = new DatiIdentificativiModello(idModello, 0);

            var campiDaSalvare = new List<CampoDaSalvare>
            {
                campoDaSalvare
             };

            this._repository.SalvaValoriCampi(datiModello, campiDaSalvare);
        }

        public void SalvaValoreCampo(int idModello, Dyn2Dato dato)
        {
            var campo = new CampoDaSalvare(dato.IdCampoDinamico.Value, "", false);
            campo.AggiungiValore(dato.Valore, dato.ValoreDecodificato);

            this.SalvaValoreCampo(idModello, campo);
        }

        public Dictionary<string, List<Dyn2Dato>> RecuperaDaIstanza(string contesto)
        {

            using (var db = this._authInfo.CreateDatabase())
            {
                string sql = $@"select
                                    dyn2_metadati.contesto_campo, istanzedyn2dati.codiceistanza, dyn2_metadati.fk_campodinamico_id,
                                    dyn2_campi.nomecampo,
                                    istanzedyn2dati.indice, istanzedyn2dati.indice_molteplicita,
                                    istanzedyn2dati.valore, istanzedyn2dati.valoredecodificato
                                from
                                    dyn2_metadati_contesti
                                        inner join dyn2_metadati on 
                                            dyn2_metadati_contesti.idcomune = dyn2_metadati.idcomune and 
                                            dyn2_metadati_contesti.id = dyn2_metadati.fk_contesto_id
                                        inner join dyn2_campi on
                                            dyn2_metadati.idcomune = dyn2_campi.idcomune and 
                                            dyn2_metadati.fk_campodinamico_id = dyn2_campi.id 
                                        left join istanzedyn2dati on 
                                            dyn2_metadati.idcomune = istanzedyn2dati.idcomune and 
                                            dyn2_metadati.fk_campodinamico_id = istanzedyn2dati.fk_d2c_id and 
                                            istanzedyn2dati.codiceistanza = {db.Specifics.QueryParameterName("codiceIstanza")}
                                where
                                    dyn2_metadati_contesti.idcomune = {db.Specifics.QueryParameterName("idComune")} and
                                    dyn2_metadati_contesti.contesto = {db.Specifics.QueryParameterName("contesto")}
                                order by
                                 istanzedyn2dati.fk_d2c_id asc,
                                 istanzedyn2dati.indice_molteplicita asc";

                return db.ExecuteReader(
                    sql,
                    mp =>
                    {
                        mp.AddParameter("contesto", contesto);
                        mp.AddParameter("idComune", this._authInfo.IdComune);
                        mp.AddParameter("codiceIstanza", this._codiceIstanza);
                    },
                    dr => new Dyn2Dato
                    {
                        ContestoCampo = dr.GetString("contesto_campo"),
                        CodiceIstanza = dr.GetInt("codiceistanza"),
                        IdCampoDinamico = dr.GetInt("fk_campodinamico_id"),
                        Indice = dr.GetInt("indice"),
                        IndiceMolteplicita = dr.GetInt("indice_molteplicita"),
                        NomeCampoDinamico = dr.GetString("nomecampo"),
                        Valore = dr.GetString("valore"),
                        ValoreDecodificato = dr.GetString("valoredecodificato")
                    })
                    .GroupBy(x => x.ContestoCampo)
                    .ToDictionary(x => x.Key, y => y.ToList());
            }

        }
    }
}
