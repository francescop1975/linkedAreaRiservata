using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData.MappingDaManagedData;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData
{
    public class FvgManagedDataReader
    {
        private readonly FvgManagedDataMapper _mapper;
        private readonly IFVGWebServiceProxy _serviceProxy;
        private readonly Dictionary<string, int> _nomeCampoToId = new Dictionary<string, int>();

        public FvgManagedDataReader(FvgManagedDataMapper mapper, IFVGWebServiceProxy serviceProxy, IEnumerable<IDyn2Campo> listaCampiDinamici)
        {
            this._mapper = mapper;
            this._serviceProxy = serviceProxy;

            listaCampiDinamici.ToList().ForEach(x => System.Diagnostics.Debug.WriteLine(x.Nomecampo));

            this._nomeCampoToId = listaCampiDinamici.Select(x => new
            {
                NomeCampo = x.Nomecampo,
                Id = x.Id.Value
            })
            .GroupBy(x => x.Id)
            .Select(y => y.First())
            .ToDictionary(x => x.NomeCampo, x => x.Id);
        }

        public IEnumerable<FvgManagedDataMapper.ManagedDataValue> ReadAllValues(long codiceIstanza)
        {
            var managedData = this._serviceProxy.GetManagedDataDaCodiceIstanza(codiceIstanza);

            return this._mapper.Map.SelectMany(x => x.ApplyTo(managedData, this._nomeCampoToId));
        }
    }
}
