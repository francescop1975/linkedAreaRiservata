using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class LocalizzazioneService
    {
        private readonly int _codiceIstanza;
        private readonly Authorization _auth;
        private readonly string _idComune;
        private readonly AuthenticationInfo _authInfo;

        public LocalizzazioneService(AuthenticationInfo authInfo, int codiceIstanza )
        {
            if (authInfo is null)
            {
                throw new ArgumentNullException(nameof(authInfo));
            }

            this._authInfo = authInfo;
            var software = new IstanzeMgr(authInfo.CreateDatabase()).GetById(authInfo.IdComune, codiceIstanza).SOFTWARE;

           

            this._auth = new Authorization
            {
                token = authInfo.Token,
                software = software,
                alias = authInfo.Alias,

            };
            this._idComune = authInfo.IdComune;
            this._codiceIstanza = codiceIstanza;

        }

        public bool AreaEsistente(int codiceArea) 
        {
            return new IstanzeAreeMgr(this._authInfo.CreateDatabase()).AreaEsistente(this._idComune, this._codiceIstanza, codiceArea);
        }

        public int SalvaDatiLocalizzazione(DatiLocalizzazione datiLocalizzazione ) 
        {
            if (datiLocalizzazione is null)
            {
                throw new ArgumentNullException(nameof(datiLocalizzazione), "Il parametro non può essere null");
            }

            var url = $"{ ParametriConfigurazione.Get.RestIstanzeStradarioJsonUrl}/{this._codiceIstanza}";


            var request = new IstanzeStradarioRestBean
            {
                codice_istanza = this._codiceIstanza,
                codice_stradario = datiLocalizzazione.CodiceStradario,
                primario = true,
                km = this.kilometrica(datiLocalizzazione.Km, datiLocalizzazione.Metro),
                latitudine = datiLocalizzazione.CoordinataX,
                longitudine = datiLocalizzazione.CoordinataY
            };

            return new JsonClient(this._auth, url).InserisciDettaglioIstanzestradario(request).id.Value;
        }

        private string kilometrica(int km, int? metro) 
        {
            var retVal = $"{km}";
            if (metro.HasValue) {
                retVal += $",{metro.Value.ToString().PadLeft(3,Convert.ToChar("0"))}";
            }
            return retVal;
        }
    }
}
