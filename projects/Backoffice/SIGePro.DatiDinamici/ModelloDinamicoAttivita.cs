using Init.SIGePro.DatiDinamici.Contesti;
using Init.SIGePro.DatiDinamici.Interfaces;
using System.Collections.Generic;

namespace Init.SIGePro.DatiDinamici
{
    public class ModelloDinamicoAttivita : ModelloDinamicoBase
    {
        public ModelloDinamicoAttivita(ModelloDinamicoLoader loader, int idModello,  int indice, bool readOnly, int? idStorico = null)
            : base(idModello, indice, readOnly, idStorico, loader)
        {
        }

        protected override ContestoModelloDinamico InizializzaContesto()
        {
            var templateReader = new ResourceScriptTemplateReader();

            return new ContestoModelloDinamico(Token, ContestoModelloEnum.Attivita, LeggiAttivita(), templateReader);
        }

        private IClasseContestoModelloDinamico LeggiAttivita()
        {
            return this.Loader.DataAccessFactory.GetClassLoader().LoadClass();
            //return DataAccessProvider.GetAttivitaManager().LeggiAttivita(IdComune, IdAttivita);
        }

        //protected override IDyn2DatiRepository GetDatiRepository()
        //{
        //    var idComune = this.IdComune;
        //    var idAttivita = this.IdAttivita;

        //    return new AttivitaDyn2DatiRepository(DataAccessProvider, idComune, idAttivita);
        //}

        //protected override IDyn2DatiStoricoRepository GetDatiStoricoRepository()
        //{
        //    var idComune = this.IdComune;
        //    var idAttivita = this.IdAttivita;

        //    return new AttivitaStoricoDyn2DatiRepository(DataAccessProvider, idComune, idAttivita, this.IdVersioneStorico.Value);
        //}


        //protected override void PopolaValoriCampi()
        //{
        //    IIAttivitaDyn2DatiManager mgr = DataAccessProvider.GetAttivitaDyn2DatiManager();

        //    foreach (var riga in Righe)
        //    {
        //        for (int idxColonna = 0; idxColonna < riga.NumeroColonne; idxColonna++)
        //        {
        //            CampoDinamicoBase cdb = riga[idxColonna];

        //            if (cdb == null)
        //                continue;

        //            List<IIAttivitaDyn2Dati> valoriUtente = mgr.GetValoriCampo(IdComune, IdAttivita, cdb.Id, IndiceModello);

        //            foreach (var valoreUtente in valoriUtente)
        //            {
        //                int indice = valoreUtente.IndiceMolteplicita.GetValueOrDefault(0);
        //                string valore = valoreUtente.Valore;
        //                string valoreDecodificato = valoreUtente.Valoredecodificato;

        //                while (cdb.ListaValori.Count < (indice + 1))
        //                    cdb.ListaValori.IncrementaMolteplicita();

        //                cdb.ListaValori[indice].Valore = valore;
        //                cdb.ListaValori[indice].ValoreDecodificato = valoreDecodificato;
        //            }

        //            // Per compatibilità con i vecchi dati dinamici
        //            if (cdb.ListaValori.Count == 0)
        //                cdb.ListaValori.IncrementaMolteplicita();

        //        }
        //    }
        //}

        //protected override void PopolaValoriCampiStorico()
        //{
        //    var mgrStorico = DataAccessProvider.GetAttivitaDyn2DatiStoricoManager();

        //    foreach (var riga in Righe)
        //    {
        //        for (int j = 0; j < riga.NumeroColonne; j++)
        //        {
        //            if (riga[j] == null) continue;

        //            int codiceCampo = riga[j].Id;

        //            List<IIAttivitaDyn2DatiStorico> valoriUtenteStorico = mgrStorico.GetValoriCampo(IdComune, IdAttivita, codiceCampo, IndiceModello, IdVersioneStorico.Value);

        //            CampoDinamicoBase cbd = riga[j];

        //            foreach (var valoreUtente in valoriUtenteStorico)
        //            {
        //                int indice = valoreUtente.IndiceMolteplicita.GetValueOrDefault(0);
        //                string valore = valoreUtente.Valore;
        //                string valoreDecodificato = valoreUtente.Valore;

        //                while (cbd.ListaValori.Count < (indice + 1))
        //                    cbd.ListaValori.IncrementaMolteplicita();

        //                cbd.ListaValori[indice].Valore = valore;
        //                cbd.ListaValori[indice].ValoreDecodificato = valoreDecodificato;
        //            }

        //            // Per compatibilità con i vecchi dati dinamici
        //            if (cbd.ListaValori.Count == 0)
        //                cbd.ListaValori.IncrementaMolteplicita();
        //        }
        //    }
        //}

        //protected override void SalvaValoriCampi(bool salvaStorico, IEnumerable<CampoDinamico> campiDaSalvare)
        //{
        //    DataAccessProvider.GetAttivitaDyn2DatiManager().SalvaValoriCampi(salvaStorico, this, campiDaSalvare);
        //}

        //protected override void EliminaValoriCampi(IEnumerable<CampoDinamico> campiDaEliminare)
        //{
        //    DataAccessProvider.GetAttivitaDyn2DatiManager().EliminaValoriCampi(this, campiDaEliminare);
        //}

    }
}
