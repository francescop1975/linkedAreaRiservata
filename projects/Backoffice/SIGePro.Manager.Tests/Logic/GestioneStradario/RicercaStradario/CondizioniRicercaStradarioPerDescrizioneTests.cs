using Init.SIGePro.Manager.Logic.GestioneStradario.RicercaStradario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario.RicercaStradario
{
    [TestClass]
    public class CondizioniRicercaStradarioPerDescrizioneTests
    {
        [TestMethod]
        public void Se_comuneLocalizzazione_vuoto_usa_codicecomune()
        {
            var codiceComune = "E256";
            var cond = new CondizioniRicercaStradarioPerDescrizione(codiceComune, "", TipoFiltroWhere.And, Enumerable.Empty<string>(), TipoLikeEnum.LikeCompleta);

            Assert.AreEqual(codiceComune, cond.ComuneLocalizzazione);
        }
    }
}
