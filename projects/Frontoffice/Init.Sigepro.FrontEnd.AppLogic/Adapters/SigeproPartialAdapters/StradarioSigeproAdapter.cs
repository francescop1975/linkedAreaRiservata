using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters
{
    public class StradarioSigeproAdapter : IIstanzaSigeproPartialAdapter
    {
        private readonly IStradarioRepository _stradarioRepository;

        public StradarioSigeproAdapter(IStradarioRepository stradarioRepository)
        {
            _stradarioRepository = stradarioRepository;
        }

        public void Adatta(IDomandaOnlineReadInterface src, Istanze istanza, IstanzaSigeproAdapterFlags flags)
        {
            var aliasComune = src.AltriDati.AliasComune;

            var stradarioList = new List<IstanzeStradario>();

            foreach (var indirizzo in src.Localizzazioni.Indirizzi)
            {
                var codStradario = indirizzo.CodiceStradario;

                var rigaStradario = _stradarioRepository.GetByCodiceStradario(aliasComune, codStradario);

                if (rigaStradario == null)
                {
                    continue;
                }

                var str = new IstanzeStradario
                {
                    CODICESTRADARIO = codStradario.ToString(),
                    CIVICO = indirizzo.Civico,
                    ESPONENTE = indirizzo.Esponente,
                    COLORE = indirizzo.Colore,
                    SCALA = indirizzo.Scala,
                    INTERNO = indirizzo.Interno,
                    ESPONENTEINTERNO = indirizzo.EsponenteInterno,
                    Piano = indirizzo.Piano,
                    NOTE = indirizzo.Note,
                    Uuid = indirizzo.Uuid,
                    Longitudine = indirizzo.Longitudine,
                    Latitudine = indirizzo.Latitudine,
                    Km = indirizzo.Km,

                    Stradario = new Stradario
                    {
                        PREFISSO = rigaStradario.PREFISSO,
                        CODICESTRADARIO = codStradario.ToString(),
                        DESCRIZIONE = indirizzo.Indirizzo,//rigaStradario.DESCRIZIONE,
                        LOCFRAZ = rigaStradario.LOCFRAZ,
                        CAP = rigaStradario.CAP,
                        CODVIARIO = rigaStradario.CODVIARIO
                    }

                };

                if (rigaStradario.ComuneLocalizzazione != null)
                {
                    str.ComuneLocalizzazione = new Comuni
                    {
                        COMUNE = rigaStradario.ComuneLocalizzazione.COMUNE,
                        PROVINCIA = rigaStradario.ComuneLocalizzazione.PROVINCIA,
                        CODICECOMUNE = rigaStradario.ComuneLocalizzazione.CODICECOMUNE,
                        SIGLAPROVINCIA = rigaStradario.ComuneLocalizzazione.SIGLAPROVINCIA
                    };
                }


                stradarioList.Add(str);
            }

            istanza.Stradario = stradarioList.ToArray();
        }
    }
}
