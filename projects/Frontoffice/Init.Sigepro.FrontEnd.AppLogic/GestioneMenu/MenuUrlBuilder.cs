using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using System.Collections.Generic;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneMenu
{
    public partial class MenuUrlBuilder
    {
        private readonly IAliasSoftwareResolver _aliasSoftwareResolver;
        private readonly IAuthenticationDataResolver _authenticationDataResolver;
        private readonly IUrlEncoder _urlEncoder;

        public MenuUrlBuilder(IAliasSoftwareResolver aliasSoftwareResolver, IAuthenticationDataResolver authenticationDataResolver, IUrlEncoder urlEncoder)
        {
            _aliasSoftwareResolver = aliasSoftwareResolver;
            _authenticationDataResolver = authenticationDataResolver;
            _urlEncoder = urlEncoder;
        }

        public MainMenu ParseMenu(MainMenu menu)
        {
            ParseSezioni(menu.Sezioni);
            ParseSottosezioni(menu.MenuUtente);
            ParseSezioni(menu.MenuDestra);

            return menu;
        }

        private void ParseSezioni(IEnumerable<SezioneMenu> sezioni)
        {
            foreach (var sezione in sezioni)
            {
                sezione.Url = CompletaUrl(sezione);
                ParseSottosezioni(sezione.Items);
            }
        }

        private void ParseSottosezioni(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
            {
                item.Url = CompletaUrl(item);
            }
        }

        private string CompletaUrl(IMenuItemConUrl item)
        {
            var tokenized = new TokenizedUrl(item.Url);

            if (item.CompletaUrl)
            {
                tokenized.AggiungiAlias(_aliasSoftwareResolver.AliasComune);
                tokenized.AggiungiSoftware(_aliasSoftwareResolver.Software);
            }

            tokenized.SostituisciSoftware(_aliasSoftwareResolver.Software);
            tokenized.SostituisciAlias(_aliasSoftwareResolver.AliasComune);
            tokenized.SostituisciToken(_authenticationDataResolver.DatiAutenticazione.Token);

            return tokenized.Rebuild(this._urlEncoder);
        }

    }
}
