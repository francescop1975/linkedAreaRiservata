using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAllegati;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAltriDati;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAutorizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneBandiUmbria;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneBookmarks;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDatiExtra;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDelegaATrasmettere;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDocumenti;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneEndoprocedimenti;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneLocalizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneProcure;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneRiepiloghiSchedeDinamiche;
using Init.Sigepro.FrontEnd.AppLogic.ObjectSpace.PresentazioneIstanza;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda
{
    public class DomandaOnlineWriteInterface : IDomandaOnlineWriteInterface
    {
        private readonly PresentazioneIstanzaDbV2 _database;
        private readonly IDocumentiWriteInterface _documenti;
        private readonly IDatiDinamiciWriteInterface _datiDinamici;
        private readonly IRiepiloghiSchedeDinamicheWriteInterface _riepiloghiSchedeDinamiche;
        private readonly IEndoprocedimentiWriteInterface _endoprocedimenti;
        private readonly IDelegaATrasmettereWriteInterface _delegaATrasmettere;
        private readonly IOneriWriteInterface _oneri;
        private readonly ILocalizzazioniWriteInterface _localizzazioni;
        private readonly IAltriDatiWriteInterface _altriDati;
        private readonly IProcureWriteInterface _procure;
        private readonly IAnagraficheWriteInterface _anagrafiche;
        private readonly IAllegatiWriteInterface _allegati;
        private readonly IAutorizzazioniMercatiWriteInterface _autorizzazioni;
        private readonly IBandiUmbriaWriteInterface _bandiUmbria;
        private readonly IDatiExtraWriteinterface _datiExtra;
        private readonly IBookmarksWriteInterface _bookmarks;

        public DomandaOnlineWriteInterface(PresentazioneIstanzaDbV2 database)
        {
            this._database = database;

            this._documenti = new DocumentiWriteInterface(database);
            this._riepiloghiSchedeDinamiche = new RiepiloghiSchedeDinamicheWriteInterface(database);
            this._datiDinamici = new DatiDinamiciWriteInterface(database, this._riepiloghiSchedeDinamiche);
            this._endoprocedimenti = new EndoprocedimentiWriteInterface(database);
            this._delegaATrasmettere = new DelegaATrasmettereWriteInterface(database);
            this._oneri = new OneriWriteInterface(database);
            this._localizzazioni = new LocalizzazioniWriteInterface(database);
            this._altriDati = new AltriDatiWriteInterface(database);
            this._procure = new ProcureWriteInterface(database);
            this._anagrafiche = new AnagraficheWriteInterface(database);
            this._allegati = new AllegatiWriteInterface(database);
            this._autorizzazioni = new AutorizzazioniMercatiWriteInterface(database);
            this._bandiUmbria = new BandiUmbriaWriteInterface(database);
            this._datiExtra = new DatiExtraWriteInterface(database);
            this._bookmarks = new BookmarksWriteInterface(database);
        }

        #region IDomandaOnlineWriteInterface Members

        public IDocumentiWriteInterface Documenti
        {
            get { return this._documenti; }
        }

        public IDatiDinamiciWriteInterface DatiDinamici
        {
            get { return this._datiDinamici; }
        }

        public IRiepiloghiSchedeDinamicheWriteInterface RiepiloghiSchedeDinamiche
        {
            get { return this._riepiloghiSchedeDinamiche; }
        }

        public IEndoprocedimentiWriteInterface Endoprocedimenti
        {
            get { return this._endoprocedimenti; }
        }

        public IDelegaATrasmettereWriteInterface DelegaATrasmettere
        {
            get { return this._delegaATrasmettere; }
        }

        public IOneriWriteInterface Oneri
        {
            get { return this._oneri; }
        }

        public ILocalizzazioniWriteInterface Localizzazioni
        {
            get { return this._localizzazioni; }
        }

        public IAltriDatiWriteInterface AltriDati
        {
            get { return this._altriDati; }
        }


        public IProcureWriteInterface Procure
        {
            get { return this._procure; }
        }

        public IAnagraficheWriteInterface Anagrafiche
        {
            get { return this._anagrafiche; }
        }


        public IAllegatiWriteInterface Allegati
        {
            get { return this._allegati; }
        }

        public IAutorizzazioniMercatiWriteInterface AutorizzazioniMercati
        {
            get { return this._autorizzazioni; }
        }

        public IBandiUmbriaWriteInterface BandiUmbria
        {
            get { return this._bandiUmbria; }
        }

        public IDatiExtraWriteinterface DatiExtra
        {
            get { return this._datiExtra; }
        }


        public IBookmarksWriteInterface Bookmarks
        {
            get { return this._bookmarks; }
        }

        #endregion
    }
}
