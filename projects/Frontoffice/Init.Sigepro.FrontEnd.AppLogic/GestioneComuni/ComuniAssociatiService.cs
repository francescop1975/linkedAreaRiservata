using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
    public class ComuniAssociatiService : IComuniAssociatiService
    {
        private readonly IComuniRepository _comuniRepository;
        private readonly ISoftwareResolver _softwareResolver;

        public ComuniAssociatiService(IComuniRepository comuniRepository, ISoftwareResolver softwareResolver)
        {
            _comuniRepository = comuniRepository ?? throw new ArgumentNullException(nameof(comuniRepository));
            _softwareResolver = softwareResolver ?? throw new ArgumentNullException(nameof(softwareResolver));
        }

        public IEnumerable<DatiComuneCompatto> GetComuniAssociati(string[] codiciComuneDaEscludere = null)
        {
            var comuniAssociati = this._comuniRepository.GetComuniAssociati(this._softwareResolver.Software);

            if (codiciComuneDaEscludere == null || codiciComuneDaEscludere.Length == 0)
            {
                return comuniAssociati;
            }

            return comuniAssociati.Where(x => !codiciComuneDaEscludere.Contains(x.CodiceComune));
        }
    }
}
