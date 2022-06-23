using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.DatiDinamici.VisibilitaCampi;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Anagrafe
{
    public class AnagrafeDyn2DatiRepository : IDyn2DatiRepository
    {
        protected readonly int _idAnagrafe;
        protected readonly string _idComune;
        private readonly DataBase _database;

        public AnagrafeDyn2DatiRepository(DataBase database, int idAnagrafe, string idComune)
        {
            this._idAnagrafe = idAnagrafe;
            this._idComune = idComune;
            _database = database;
        }

        public void EliminaValoriCampi(DatiIdentificativiModello modello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare)
        {
            AnagrafeDyn2DatiMgr manager = GetManager();
            manager.EliminaValoriCampi(this._idComune, this._idAnagrafe, modello, campiDaEliminare);
        }

        private AnagrafeDyn2DatiMgr GetManager()
        {
            return new AnagrafeDyn2DatiMgr(this._database);
        }

        public virtual SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello)
        {
            var manager = GetManager();
            var valori = manager.GetValoriCampiDaIdModello(this._idComune, this._idAnagrafe, idModello, indiceModello);

            return valori.GroupBy(x => x.FkD2cId.Value).ToSerializableDictionary(x => x.Key, y => y.Cast<IValoreCampo>());
        }

        public virtual void SalvaValoriCampi(DatiIdentificativiModello modello, IEnumerable<CampoDaSalvare> campiDaSalvare)
        {
            var mgr = GetManager();
            mgr.SalvaValoriCampi(this._idComune, this._idAnagrafe, modello, campiDaSalvare);
        }

        public void SalvaCampiNonVisibili(DatiIdentificativiModello idModello, IEnumerable<IdValoreCampo> enumerable)
        {
            // throw new NotImplementedException();
        }
    }
}
