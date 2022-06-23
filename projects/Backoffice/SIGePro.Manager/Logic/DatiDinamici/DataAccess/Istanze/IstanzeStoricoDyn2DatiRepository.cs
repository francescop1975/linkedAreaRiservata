using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze
{
    public class IstanzeStoricoDyn2DatiRepository : IDyn2DatiStoricoRepository
    {
        private readonly int _idVersioneStorico;
        private readonly DataBase _database;
        private readonly string _idComune;
        private readonly int _codiceIstanza;

        public IstanzeStoricoDyn2DatiRepository(DataBase database, string idComune, int codiceIstanza, int idVersioneStorico)
        {
            _idVersioneStorico = idVersioneStorico;
            _database = database;
            _idComune = idComune;
            _codiceIstanza = codiceIstanza;
        }

        private IstanzeDyn2DatiStoricoMgr GetManager()
        {
            return new IstanzeDyn2DatiStoricoMgr(this._database);
        }

        public void SalvaStoricoModello(ModelloDinamicoBase modelloDinamicoBase)
        {
            var manager = GetManager();

            manager.SalvaStoricoModello(this._idComune, this._codiceIstanza, modelloDinamicoBase);
        }

        public SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello)
        {
            var manager = GetManager();
            var listaValori = manager.GetValoriCampiDaIdModello(_idComune, _codiceIstanza, idModello, indiceModello, _idVersioneStorico);

            return listaValori
                        .GroupBy(x => x.FkD2cId.Value)
                        .ToSerializableDictionary(x => x.Key, y => y.Cast<IValoreCampo>());
        }
    }
}
