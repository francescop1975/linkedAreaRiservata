using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.Database;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.DebugConfiguration;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData.MappingDaManagedData;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData
{
    public class FvgManagedDataRepository : IFvgManagedDataRepository
    {
        private readonly IFVGWebServiceProxy _webServiceProxy;
        private readonly IFVGDebugConfiguration _debugConfiguration;

        public FvgManagedDataRepository(IFVGWebServiceProxy webServiceProxy, IFVGDebugConfiguration debugConfiguration)
        {
            _webServiceProxy = webServiceProxy;
            _debugConfiguration = debugConfiguration;
        }

        public IEnumerable<FvgDatabase.ValoreCampoDinamico> GetValoriDatiDinamici(long codiceIstanza, IEnumerable<string> listaCampiDelModulo)
        {
            var reader = new FvgDatiDinamiciManagedDataReader(listaCampiDelModulo, this._webServiceProxy);
            return reader.GetValoriDatiDinamici(codiceIstanza);
        }

        public IEnumerable<FvgManagedDataMapper.ManagedDataValue> ReadAllValues(long codiceIstanza, IEnumerable<IDyn2Campo> listaCampiDinamici)
        {
            var mapper = CreateDataMapper();
            var dataReader = new FvgManagedDataReader(mapper, this._webServiceProxy, listaCampiDinamici);

            return dataReader.ReadAllValues(codiceIstanza).ToList();
        }

        public void SalvaFilePdf(long codiceIstanza, string idModulo, BinaryFile binaryFile)
        {
            this._webServiceProxy.SalvaFilePdf(codiceIstanza, idModulo, binaryFile.FileContent);
        }

        private FvgManagedDataMapper CreateDataMapper()
        {
            var configurationFile = "~/moduli-fvg/compilazione/managed-data-mappings.xml";
            return FvgManagedDataMapper.LoadFrom(configurationFile, this._debugConfiguration);
        }
    }
}
