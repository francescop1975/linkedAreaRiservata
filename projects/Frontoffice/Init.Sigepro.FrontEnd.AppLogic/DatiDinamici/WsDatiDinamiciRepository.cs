using System;
using System.Collections.Generic;
using System.Linq;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.WebServices;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.WsVbgDatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{
    internal class WsDatiDinamiciRepository : IDatiDinamiciRepository
	{
		IAliasResolver _aliasResolver;
        private readonly WsDatiDinamiciServiceCreator _serviceCreator;

        public WsDatiDinamiciRepository(IAliasResolver aliasResolver, WsDatiDinamiciServiceCreator sc)
		{
			this._aliasResolver = aliasResolver;
            this._serviceCreator = sc;
        }

        public ListaModelliDinamiciDomandaDto GetSchedeDaInterventoEEndo(int intervento, IEnumerable<int> endo, IEnumerable<string> tipiLocalizzazioni, UsaTipiLocalizzazioniPerSelezionareSchedeDinamiche usaTipiLocalizzazioni)
		{
			using (var ws = _serviceCreator.CreateClient())
			{
				var req = new GetModelliDinamiciDaInterventoEEndoRequest
				{
					CodiceIntervento = intervento,
					ListaEndo = endo.ToArray(),
					ListaTipiLocalizzazioni = tipiLocalizzazioni.ToArray(),
					IgnoraTipiLocalizzazione = usaTipiLocalizzazioni == UsaTipiLocalizzazioniPerSelezionareSchedeDinamiche.No
				};

				return ws.Service.GetModelliDinamiciDaInterventoEEndo(ws.Token, req);
			}
		}

		public RisultatoRicercaDatiDinamiciDto[] GetCompletionList(int idCampo, string partial, ValoreFiltroRicercaDto[] filtri)
		{
			using (var ws = _serviceCreator.CreateClient())
			{
				return ws.Service.GetCompletionListRicerchePlus(ws.Token, idCampo, partial, filtri);
			}
		}

		public RisultatoRicercaDatiDinamiciDto InitializeControl(int idCampo, string value)
		{
			using (var ws = _serviceCreator.CreateClient())
			{
				return ws.Service.InitializeControlRicerchePlus(ws.Token, idCampo, value);
			}
		}

        Lazy<ModelloDinamicoCache> _cacheModello = null;

		public ModelloDinamicoCache GetCacheModelloDinamico(int idModello)
		{
            if (this._cacheModello == null || this._cacheModello.Value.Modello.Id.Value != idModello)
            {
                this._cacheModello = new Lazy<ModelloDinamicoCache>(() =>
                {
                    using (var ws = _serviceCreator.CreateClient())
                    {
                        var struttura = ws.Service.GetStrutturaModelloDinamico(ws.Token, idModello);


                        var rVal = new ModelloDinamicoCache
                        {
                            Modello = struttura.Modello,
                            ScriptsModello = PopolaScriptsModello(struttura),
                            Struttura = new List<IDyn2DettagliModello>(struttura.Struttura),
                            ListaCampiDinamici = PopolaCampiDinamici(struttura),
                            ListaTesti = PopolaListaTesti(struttura.Testi),
                            ScriptsCampiDinamici = PopolaScriptscampiDinamici(struttura.ScriptsCampi),
                            ProprietaCampiDinamici = PopolaProprietaCampi(struttura.ProprietaCampiDinamici)
                        };

                        rVal.Modello.FlgStoricizza = 0;

                        return rVal;
                    }
                });
            }

            return this._cacheModello.Value;

            //var sessionKey = $"ModelloDinamicoCache.{this._aliasResolver.AliasComune}.{idModello.ToString()}";

            //return CacheHelper.GetOrAdd(sessionKey, () =>
            //{

            //});            
		}

		private Dictionary<int, List<IDyn2ProprietaCampo>> PopolaProprietaCampi(Dyn2CampiProprieta[] proprieta)
		{
			var rVal = new Dictionary<int, List<IDyn2ProprietaCampo>>();

			for (int i = 0; i < proprieta.Length; i++)
			{
				var el = proprieta[i];

				var idCampo = el.FkD2cId.Value;

				if (!rVal.ContainsKey(idCampo))
					rVal.Add(idCampo, new List<IDyn2ProprietaCampo>());

				rVal[idCampo].Add(el);
			}

			return rVal;
		}

		private Dictionary<int, Dictionary<TipoScriptEnum, IDyn2ScriptCampo>> PopolaScriptscampiDinamici(Dyn2CampiScript[] scriptsCampi)
		{
			var rVal = new Dictionary<int, Dictionary<TipoScriptEnum, IDyn2ScriptCampo>>();

			for (int i = 0; i < scriptsCampi.Length; i++)
			{
				var el = scriptsCampi[i];

				var idCampo = el.FkD2cId.Value;
				var contesto = (TipoScriptEnum)Enum.Parse(typeof(TipoScriptEnum), el.Evento);

				if (!rVal.ContainsKey(idCampo))
					rVal.Add(idCampo, new Dictionary<TipoScriptEnum, IDyn2ScriptCampo>());

				rVal[idCampo].Add(contesto, el);

			}

			return rVal;
		}

		private SerializableDictionary<int, IDyn2TestoModello> PopolaListaTesti(Dyn2TestoModello[] testi)
		{
			var rVal = new SerializableDictionary<int, IDyn2TestoModello>();

			for (int i = 0; i < testi.Length; i++)
			{
				var el = testi[i];
				rVal.Add(el.Id.Value, el);
			}

			return rVal;
		}

		private SerializableDictionary<int, IDyn2Campo> PopolaCampiDinamici(StrutturaModelloDinamicoDto struttura)
		{
			var rVal = new SerializableDictionary<int, IDyn2Campo>();

			for (int i = 0; i < struttura.CampiDinamici.Length; i++)
			{
				var el = struttura.CampiDinamici[i];
				rVal.Add(el.Id.Value, el);
			}

			return rVal;
		}

		private Dictionary<TipoScriptEnum, IDyn2ScriptModello> PopolaScriptsModello(StrutturaModelloDinamicoDto struttura)
		{
			var rVal = new Dictionary<TipoScriptEnum, IDyn2ScriptModello>();

			for (int i = 0; i < struttura.ScriptsModello.Length; i++)
			{
				var el = struttura.ScriptsModello[i];
				var contesto = (TipoScriptEnum)Enum.Parse(typeof(TipoScriptEnum), el.Evento);

				rVal.Add(contesto, el);
			}

			return rVal;
		}
	}
}
