using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.Database;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData.MappingDaManagedData;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData
{
    public interface IFvgManagedDataRepository
    {
        /// <summary>
        /// Cerca di recuperare dal managed data eventuali valori compilati nella fase iniziale di presentazione della domanda utilizzando il file di mappature 
        /// per recuperare le espressioni xpath
        /// </summary>
        /// <param name="codiceIstanza"></param>
        /// <param name="listaCampiDinamic"></param>
        /// <returns></returns>
        IEnumerable<FvgManagedDataMapper.ManagedDataValue> ReadAllValues(long codiceIstanza, IEnumerable<IDyn2Campo> listaCampiDinamic);

        IEnumerable<FvgDatabase.ValoreCampoDinamico> GetValoriDatiDinamici(long codiceIstanza, IEnumerable<string> listaCampiDelModulo);

        void SalvaFilePdf(long codiceIstanza, string idModulo, BinaryFile binaryFile);
    }
}
