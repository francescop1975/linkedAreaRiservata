using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public class StrutturaModelloBuilder : IStrutturaModelloRepository
    {
        private readonly string _idComune;
        private readonly IDyn2DataAccessFactory _dataAccessFactory;

        public StrutturaModelloBuilder(string idComune, IDyn2DataAccessFactory dataAccessFactory)
        {
            this._idComune = idComune;
            this._dataAccessFactory = dataAccessFactory;
        }

        private IEnumerable<IDyn2DettagliModello> GetStrutturaRighe(int idModello)
        {
            var mgr = this._dataAccessFactory.GetDettagliModelloManager();

            return mgr.GetList(this._idComune, idModello);
        }

        private Utils.SerializableDictionary<int, IDyn2Campo> GetListaCampi(int idModello)
        {
            var mgr = this._dataAccessFactory.GetCampiManager();

            return mgr.GetListaCampiDaIdModello(this._idComune, idModello);
        }

        private Utils.SerializableDictionary<int, IDyn2TestoModello> GetListaTesti(int idModello)
        {
            var mgr = this._dataAccessFactory.GetTestoModelloManager();

            return mgr.GetListaTestiDaIdModello(this._idComune, idModello);
        }

        public ModelloDinamicoBase.StrutturaModello CostruisciStrutturaModello(ModelloDinamicoBase modello)
        {
            var righe = new List<RigaModelloDinamico>();
            var righeMultiple = new List<RigaModelloDinamico>();
            var campi = new List<CampoDinamicoBase>();
            var gruppi = new GruppoRigheCollection();

            var strutturaRighe = this.GetStrutturaRighe(modello.IdModello);
            var listaCampiModello = this.GetListaCampi(modello.IdModello);
            var listaTestiModello = this.GetListaTesti(modello.IdModello);

            // Crea le righe del modello popolando la collection m_righe
            foreach (var dettaglio in strutturaRighe)
            {
                var posVerticaleCorretta = dettaglio.Posverticale.GetValueOrDefault(0);
                var posOrizzontaleCorretta = dettaglio.Posorizzontale.GetValueOrDefault(0) - 1;

                // TODO: Trovare un modo migliore per generare le righe perché questo spreca troppi cicli
                while (righe.Count <= dettaglio.Posverticale)
                    righe.Add(new RigaModelloDinamico(posVerticaleCorretta));

                var riga = righe[posVerticaleCorretta];

                // Tipo riga
                riga.TipoRiga = (TipoRigaEnum)dettaglio.FlgMultiplo.GetValueOrDefault(0);

                if (riga.TipoRiga != TipoRigaEnum.Singola)
                {
                    if (!righeMultiple.Contains(riga))
                    {
                        righeMultiple.Add(riga);
                    }
                }

                // Flag spezza tabella
                riga.InterrompiTabellaDopo = dettaglio.FlgSpezzaTabella.GetValueOrDefault(0) == 1;

                // Campo contenuto nel dettaglio
                CampoDinamicoBase campo = null;

                if (dettaglio.FkD2cId.HasValue)
                {
                    // campo dinamico
                    var dyn2Campo = listaCampiModello[dettaglio.FkD2cId.Value];
                    campo = new CampoDinamico(modello, dyn2Campo, this._dataAccessFactory, posOrizzontaleCorretta, posVerticaleCorretta);
                }
                else
                {
                    // campo testuale
                    var dyn2Testo = listaTestiModello[dettaglio.FkD2mdtId.Value];
                    campo = new CampoDinamicoTestuale(modello, dyn2Testo, this._dataAccessFactory);
                }

                riga[posOrizzontaleCorretta] = campo;
                campi.Add(campo);
            }


            // Dopo avere popolato tutte le righe cerca di organizzare i gruppi in cui è strutturato il modello.
            // Un gruppo è un insieme di una o più righe consecutive con flag riga multipla == true
            var indiceGruppo = 0;
            var rigaMultiplaTrovata = false;

            foreach (var riga in righe)
            {
                if (riga.NumeroColonne == 0)
                    continue;

                if (rigaMultiplaTrovata)
                {
                    if (riga.TipoRiga != TipoRigaEnum.Singola)
                    {
                        gruppi.AggiungiRigaAGruppo(indiceGruppo, riga);
                    }
                    else
                    {
                        indiceGruppo++;
                        rigaMultiplaTrovata = false;
                    }
                }
                else
                {
                    if (riga.TipoRiga != TipoRigaEnum.Singola)
                    {
                        rigaMultiplaTrovata = true;
                        gruppi.AggiungiRigaAGruppo(indiceGruppo, riga);
                    }
                }
            }

            return new ModelloDinamicoBase.StrutturaModello(righeMultiple, righe, campi, gruppi);
        }

    }
}
