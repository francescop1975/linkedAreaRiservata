using Init.SIGePro.Manager.Logic.Visura;
using Init.SIGePro.Manager.Logic.Visura.QueryRicerca;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGePro.Manager.Tests.Fakes.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Visura.QueryRicerca
{
    [TestClass]
    public class DataIstanzaConditionTests
    {
        [TestMethod]
        public void DataIstanzaCondition_imposta_correttamente_il_numero_e_il_nome_dei_parametri()
        {
            var filtro = new FiltroPeriodoPresentazione
            {
                Anno = 2019,
                Mese = 1
            };
            var cls = new DataIstanzaCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            cls.VerifyParameters();
        }
    }

    [TestClass]
    public class FiltroPeriodoPresentazioneTests
    {
        [TestMethod]
        public void FiltroPeriodoPresentazione_se_solo_anno_impostato_restituisce_da_1_gennaio_a_31_dicembre()
        {
            var filtro = new FiltroPeriodoPresentazione
            {
                Anno = 2020
            };

            var range = filtro.ToDateRange();

            Assert.AreEqual(2020, range.Min.Year);
            Assert.AreEqual(1, range.Min.Month);
            Assert.AreEqual(1, range.Min.Day);

            Assert.AreEqual(2020, range.Max.Year);
            Assert.AreEqual(12, range.Max.Month);
            Assert.AreEqual(31, range.Max.Day);
        }

        [TestMethod]
        public void FiltroPeriodoPresentazione_se_mese_impostato_dal_primo_giorno_del_mese_all_ultimo()
        {
            var filtro = new FiltroPeriodoPresentazione
            {
                Anno = 2020,
                Mese = 1
            };

            var range = filtro.ToDateRange();

            Assert.AreEqual(2020, range.Min.Year);
            Assert.AreEqual(1, range.Min.Month);
            Assert.AreEqual(1, range.Min.Day);

            Assert.AreEqual(2020, range.Max.Year);
            Assert.AreEqual(1, range.Max.Month);
            Assert.AreEqual(31, range.Max.Day);
        }

        [TestMethod]
        public void FiltroPeriodoPresentazione_se_anno_non_importato_usa_anno_corrente()
        {
            var filtro = new FiltroPeriodoPresentazione
            {
                Mese = 1
            };

            var range = filtro.ToDateRange();

            Assert.AreEqual(DateTime.Now.Year, range.Min.Year);
            Assert.AreEqual(DateTime.Now.Year, range.Max.Year);
        }
    }
}
