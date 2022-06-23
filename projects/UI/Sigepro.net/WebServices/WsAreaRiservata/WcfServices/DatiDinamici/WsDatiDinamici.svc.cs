using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using Init.SIGePro.Manager.Logic.DatiDinamici.RicercheSigepro;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using Init.SIGePro.Manager.Logic.GestioneDomandaOnLine;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices.DatiDinamici
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WsDatiDinamici" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WsDatiDinamici.svc or WsDatiDinamici.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class WsDatiDinamici : WcfServiceBase, IWsDatiDinamici
    {
        private readonly IDomandaOnlineService _domandeOnlineService;

        public WsDatiDinamici(IDomandaOnlineService domandeOnlineService)
        {
            this._domandeOnlineService = domandeOnlineService;
        }

        public DecodificaDto[] GetDecodificheAttive(string token, string tabella)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                return new DecodificheService(db, authInfo.IdComune).GetDecodificheAttive(tabella)
                    .Select( x => new DecodificaDto
                    {
                        Chiave = x.Chiave,
                        FlgDisabilitato = x.FlgDisabilitato,
                        Idcomune = x.Idcomune,
                        Raggruppamento = x.Raggruppamento,
                        Tabella = x.Tabella,
                        Valore = x.Valore
                    })
                    .ToArray();
            }
        }

        public ListaModelliDinamiciDomandaDto GetModelliDinamiciDaInterventoEEndo(string token, GetModelliDinamiciDaInterventoEEndoRequest request)
        {
            var authInfo = CheckToken(token);

            // Leggo l'albero degli interventi
            var db = authInfo.CreateDatabase();

            var alberoProcMgr = new AlberoProcMgr(db);
            var inventarioProcMgr = new InventarioProcedimentiMgr(db);

            var rVal = new ListaModelliDinamiciDomandaDto();

            if (request.CodiceIntervento <= 0)
            {
                rVal.SchedeIntervento = Enumerable.Empty<SchedaDinamicaInterventoDto>().ToList();
            }
            else
            {
                rVal.SchedeIntervento = alberoProcMgr.GetSchedeDinamicheFoDaIdIntervento(authInfo.IdComune, request.CodiceIntervento);
            }

            rVal.SchedeEndoprocedimenti = inventarioProcMgr.GetSchedeDinamicheDaEndoprocedimentiList(authInfo.IdComune, request.ListaEndo, request.ListaTipiLocalizzazioni, request.IgnoraTipiLocalizzazione);

            return rVal;
        }

        // Copia tutte le properties della classe di origine in una nuova classe di destinazione
        // Se la classe di destinazione non contiene una property con lo stesso nome solleva un'eccezione
        private static TDst QuickMap<TDst>(object src, Expression<Func<TDst>> expr) where TDst : new()
        {
            var dstType = typeof(TDst);
            var srcType = src.GetType();

            var srcProps = srcType.GetProperties();
            TDst classInstance = default(TDst);

            if (src != null)
            {
                classInstance = new TDst();

                foreach (var prop in srcProps)
                {
                    var dstProp = dstType.GetProperty(prop.Name);

                    if (dstProp == null) continue;

                    dstProp.SetValue(classInstance, prop.GetValue(src, null), null);
                }
            }

            var expressionBody = (MemberExpression)expr.Body;
            var argName = expressionBody.Member.Name;
            var param = Expression.Parameter(typeof(TDst));
            var setter = Expression.Lambda<Action<TDst>>(
                Expression.Assign(
                    expressionBody,
                    param
                ),
                param
            ).Compile();
            setter(classInstance);

            return classInstance;
        }

        public StrutturaModelloDinamicoDto GetStrutturaModelloDinamico(string token, int idModello)
        {
            var authInfo = CheckToken(token);

            var rVal = new StrutturaModelloDinamicoDto();

            using (var db = authInfo.CreateDatabase())
            {
                try
                {
                    db.Connection.Open();

                    string idComune = authInfo.IdComune;

                    rVal.Modello = new Dyn2ModelliTMgr(db).GetById(idComune, idModello);
                    rVal.Struttura = new Dyn2ModelliDMgr(db).GetListImpl(idComune, idModello);
                    rVal.ScriptsModello = new Dyn2ModelliScriptMgr(db).GetList(idComune, idModello);
                    rVal.ScriptsCampi = new Dyn2CampiScriptMgr(db).GetListDaIdModello(idComune, idModello);
                    rVal.CampiDinamici = new Dyn2CampiMgr(db).GetList(idComune, idModello);
                    rVal.ProprietaCampiDinamici = new Dyn2CampiProprietaMgr(db).GetListDaIdModello(idComune, idModello);
                    rVal.Testi = new Dyn2ModelliDTestiMgr(db).GetList(idComune, idModello);

                    return rVal;
                }
                finally
                {
                    db.Connection.Close();
                }
            }
        }

        public RisultatoRicercaDatiDinamiciDto[] GetCompletionListRicerchePlus(string token, int idCampo, string partial, List<ValoreFiltroRicercaDto> filtri)
        {
            var f = filtri.Select(x => new ValoreFiltroRicerca
            {
                nome = x.nome,
                val = x.val
            });

            return new RicercheDatiDinamiciService(token).GetCompletionList(idCampo, partial, f)
                .Select(x => new RisultatoRicercaDatiDinamiciDto
                {
                    Label = x.Label,
                    Value = x.Value
                })
                .ToArray();
        }

        public RisultatoRicercaDatiDinamiciDto InitializeControlRicerchePlus(string token, int idCampo, string valore)
        {
            var val = new RicercheDatiDinamiciService(token).InitializeControl(idCampo, valore);

            return val == null ? null : new RisultatoRicercaDatiDinamiciDto
            {
                Label = val.Label,
                Value = val.Value
            };
        }

        public IstanzeDyn2Dati[] GetDyn2DatiByIdModello(string token, int codiceIstanza, int idModello, int indiceCampo)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                return new IstanzeDyn2DatiMgr(db).GetDyn2DatiByIdModello(authInfo.IdComune, codiceIstanza, idModello, indiceCampo);
            }

        }

        public IstanzeDyn2Dati[] GetDyn2DatiByCodiceIstanza(string token, int codiceIstanza)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                return new IstanzeDyn2DatiMgr(db).GetListByCodiceIstanza(authInfo.IdComune, codiceIstanza);
            }

        }

        public void RecuperaDocumentiIstanzaCollegata(string token, int codiceIstanzaOrigine, int idDomandaDestinazione)
        {
            var authInfo = CheckToken(token);

            this._domandeOnlineService.RecuperaDocumentiIstanzaCollegata(codiceIstanzaOrigine, idDomandaDestinazione);

        }
    }
}
