using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.DatiDinamici.VisibilitaCampi;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze
{
    public class IstanzeDyn2DatiRepository : IDyn2DatiRepository
    {
        //protected readonly IDyn2DataAccessProvider _dataAccessProvider;
        protected readonly int _codiceIstanza;
        protected readonly string _idComune;
        private readonly DataBase _database;

        public IstanzeDyn2DatiRepository(DataBase database, int codiceIstanza, string idComune)
        {
            _codiceIstanza = codiceIstanza;
            _idComune = idComune;
            _database = database;
        }

        private IstanzeDyn2DatiMgr CreateManager()
        {
            return new IstanzeDyn2DatiMgr(this._database);
        }

        public void EliminaValoriCampi(DatiIdentificativiModello modello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare)
        {
            var mgr = CreateManager();
            mgr.EliminaValoriCampi(this._idComune, this._codiceIstanza, modello, campiDaEliminare);
        }

        public SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello)
        {
            return GetValori(idModello, indiceModello);
        }

        public void SalvaValoriCampi(DatiIdentificativiModello idModello, IEnumerable<CampoDaSalvare> campiDaSalvare)
        {
            var mgr = CreateManager();

            mgr.SalvaValoriCampi(this._idComune, this._codiceIstanza, idModello, campiDaSalvare);
        }

        protected virtual SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValori(int idModello, int indiceModello)
        {
            var manager = CreateManager();
            var result = manager.GetValoriCampiDaIdModello(this._idComune, this._codiceIstanza, idModello, indiceModello);

            var rVal = new SerializableDictionary<int, IEnumerable<IValoreCampo>>();

            foreach (var key in result.Keys)
            {
                rVal[key] = result[key].Select(x => x);
            }

            return rVal;
        }

        public void SalvaCampiNonVisibili(DatiIdentificativiModello idModello, IEnumerable<IdValoreCampo> enumerable)
        {
        }
    }
}
