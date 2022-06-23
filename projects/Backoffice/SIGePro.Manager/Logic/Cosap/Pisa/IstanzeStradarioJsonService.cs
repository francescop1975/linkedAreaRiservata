using Init.SIGePro.Data;
using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.Manager.Logic.Cosap.Pisa.Json;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class IstanzeStradarioJsonService : JsonClient
    {

        public IstanzeStradarioJsonService(Authorization auth, String url) : base(auth, url)
        {

        }

        public IEnumerable<IstanzeStradario> GetStradari(int codiceIstanza)
        {
            var url = $"{ParametriConfigurazione.Get.RestIstanzeStradarioJsonUrl}/{codiceIstanza}";

            var result = this.downloadSerializedJsonData<List<IstanzeStradarioRestBean>>();

            return result.ConvertAll(new Converter<IstanzeStradarioRestBean, IstanzeStradario>(IstanzeStradarioRestBeanToIstanzeStradario));
        }

        public IstanzeStradario InserisciStradario(IstanzeStradario stradario)
        {
            var url = $"{ParametriConfigurazione.Get.RestIstanzeStradarioJsonUrl}/{stradario.CODICEISTANZA}";
            var request = IstanzeStradarioToIstanzeStradarioRestBean(stradario);

            var stradarioBean =  this.downloadSerializedJsonDataPOSTAuthenticated<IstanzeStradarioRestBean>(request );

            return IstanzeStradarioRestBeanToIstanzeStradario(stradarioBean);

        }

        private static IstanzeStradarioRestBean IstanzeStradarioToIstanzeStradarioRestBean(IstanzeStradario stradario) 
        {
            
            var result = new IstanzeStradarioRestBean 
            { 
                AccessoDescrizione = null,
                AccessoNumero = null,
                AccessoTipo = null,
                Cap = stradario.CAP,
                Circoscrizione = stradario.CIRCOSCRIZIONE,
                Civico = stradario.CIVICO,
                CodiceCivico = stradario.CODICECIVICO,
                CodiceIstanza = Convert.ToInt32(stradario.CODICEISTANZA),
                CodiceStradario = Convert.ToInt32(stradario.CODICESTRADARIO),
                Colore = stradario.COLORE,
                Esponente = stradario.ESPONENTE,
                EsponenteInterno = stradario.ESPONENTEINTERNO,
                Fabbricato = stradario.FABBRICATO,
                Frazione = stradario.FRAZIONE,
                IdComune = stradario.IDCOMUNE,
                IdPuntoSit = stradario.IdPuntoSit,
                Interno = stradario.INTERNO,
                Km = stradario.Km,
                Latitudine = stradario.Latitudine,
                Longitudine = stradario.Longitudine,
                Note = stradario.NOTE,
                Piano = stradario.Piano,
                Scala = stradario.SCALA,
                Uuid = stradario.Uuid
            };

            if (!String.IsNullOrEmpty(stradario.ID))
                result.Id = Convert.ToInt32( stradario.ID );

            if (!String.IsNullOrEmpty(stradario.PRIMARIO))
                result.Primario = stradario.PRIMARIO == "1";

            if (stradario.TipolocalizzazioneId.HasValue)
                result.TipiLocalizzazioniId = stradario.TipolocalizzazioneId.Value;

            return result;
        }

        private static IstanzeStradario IstanzeStradarioRestBeanToIstanzeStradario(IstanzeStradarioRestBean bean)
        {
            return new IstanzeStradario 
            { 
                CAP = bean.Cap,
                CIRCOSCRIZIONE = bean.Circoscrizione,
                CIVICO = bean.Civico,
                CODICECIVICO = bean.CodiceCivico,
                CODICEISTANZA = bean.CodiceIstanza.ToString(),
                CODICESTRADARIO = bean.CodiceStradario.ToString(),
                COLORE = bean.Colore,
                ESPONENTE = bean.Esponente,
                ESPONENTEINTERNO = bean.EsponenteInterno,
                FABBRICATO = bean.Fabbricato,
                FRAZIONE = bean.Frazione,
                ID = bean.Id.ToString(),
                IDCOMUNE = bean.IdComune,
                IdPuntoSit = bean.IdPuntoSit,
                INTERNO = bean.Interno,
                Km = bean.Km,
                Latitudine = bean.Latitudine,
                Longitudine = bean.Longitudine,
                NOTE = bean.Note,
                Piano = bean.Piano,
                PRIMARIO = bean.Primario == true ? "1" : "0",
                SCALA = bean.Scala,
                TipolocalizzazioneId = bean.TipiLocalizzazioniId,
                Uuid = bean.Uuid
            };
        }
    }
}
