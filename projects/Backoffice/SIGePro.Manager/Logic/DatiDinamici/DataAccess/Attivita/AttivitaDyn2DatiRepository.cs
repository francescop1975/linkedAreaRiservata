using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.DatiDinamici.VisibilitaCampi;
using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Attivita
{
    public class AttivitaDyn2DatiRepository : IDyn2DatiRepository
    {
        protected readonly int _idAttivita;
        private readonly ISchedeDinamicheAttivitaService _schedeDinamicheAttivitaService;

        public AttivitaDyn2DatiRepository(ISchedeDinamicheAttivitaService schedeDinamicheAttivitaService, int idAttivita)
        {
            _idAttivita = idAttivita;
            _schedeDinamicheAttivitaService = schedeDinamicheAttivitaService;
        }

        public void EliminaValoriCampi(DatiIdentificativiModello modello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare)
        {
            this._schedeDinamicheAttivitaService.EliminaValoriCampi(this._idAttivita, modello, campiDaEliminare);
        }

        public virtual SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello)
        {
            var valori = this._schedeDinamicheAttivitaService.GetValoriCampiDaIdModello(this._idAttivita, idModello, indiceModello);

            return valori.GroupBy(x => x.FkD2cId.Value).ToSerializableDictionary(x => x.Key, y => y.Cast<IValoreCampo>());
        }

        public virtual void SalvaValoriCampi(DatiIdentificativiModello idModello, IEnumerable<CampoDaSalvare> campiDaSalvare)
        {
            this._schedeDinamicheAttivitaService.SalvaValoriCampi(this._idAttivita, idModello, campiDaSalvare);
        }

        public void SalvaCampiNonVisibili(DatiIdentificativiModello idModello, IEnumerable<IdValoreCampo> enumerable)
        {
        }
    }
}
