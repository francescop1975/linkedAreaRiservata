﻿using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneMenu;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.GestioneMenu
{
    [TestClass]
    public class MenuUrlBuilderTests
    {

        IAliasSoftwareResolver aliasSoftwareResolver;
        IAuthenticationDataResolver authResolver;
        IUrlEncoder urlEncoder = new HttpUtilityUrlEncoder();

        [TestInitialize]
        public void Initialize()
        {
            aliasSoftwareResolver = new StaticAliasSoftwareResolver("XXX", "YY");
            authResolver = new StaticAuthenticationDataResolver("123-456-789", "XXX");
        }

        [TestMethod]
        public void Se_CompletaUrl_true_aggiunge_alias_e_software_all_url()
        {

            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "~/mio-url.ext",
                        CompletaUrl = true
                    }
                }
            });

            Assert.AreEqual("~/mio-url.ext?idcomune=XXX&software=YY", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Se_CompletaUrl_false_non_aggiunge_alias_e_software_all_url()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "~/mio-url.ext",
                        CompletaUrl = false
                    }
                }
            });

            Assert.AreEqual("~/mio-url.ext", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Se_CompletaUrl_true_sostituisce_segnaposto_alias_e_software()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "~/mio-url.ext?idcomune={idcomune}&software={software}",
                        CompletaUrl = true
                    }
                }
            });

            Assert.AreEqual("~/mio-url.ext?idcomune=XXX&software=YY", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Se_CompletaUrl_true_sostituisce_segnaposto_token()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "~/mio-url.ext?idcomune=ABC&software=DE&token={token}",
                        CompletaUrl = true
                    }
                }
            });

            Assert.AreEqual("~/mio-url.ext?idcomune=ABC&software=DE&token=123-456-789", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Se_url_contiene_graticcio_non_viene_modificato()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "#",
                        CompletaUrl = true
                    }
                }
            });

            Assert.AreEqual("#", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Se_url_contiene_escape_html_per_e_commerciale_sostituisce_con_e()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "https://aida2015.comune.livorno.it/areariservata/Login.aspx?idComune=E625&amp;software=SS&amp;token={token}",
                        CompletaUrl = true
                    }
                }
            });

            Assert.AreEqual("https://aida2015.comune.livorno.it/areariservata/Login.aspx?idComune=E625&software=SS&token=123-456-789", result.Sezioni[0].Url);
        }

        [TestMethod]
        public void Le_sostituzioni_vengono_effettuate_anche_se_completaUrl_false()
        {
            var builder = new MenuUrlBuilder(aliasSoftwareResolver, authResolver, urlEncoder);

            var result = builder.ParseMenu(new MainMenu
            {
                Sezioni = new List<SezioneMenu>
                {
                    new SezioneMenu
                    {
                        Url = "~/miourl.ext?token={token}&alias={idComune}&software={software}",
                        CompletaUrl = false
                    }
                }
            });

            Assert.AreEqual("~/miourl.ext?token=123-456-789&alias=XXX&software=YY", result.Sezioni[0].Url);
        }
    }
}
