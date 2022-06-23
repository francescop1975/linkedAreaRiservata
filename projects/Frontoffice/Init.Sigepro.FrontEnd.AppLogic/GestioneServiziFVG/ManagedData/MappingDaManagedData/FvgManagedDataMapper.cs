using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.DebugConfiguration;
using Init.Sigepro.FrontEnd.AppLogic.Utils.SerializationExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData.MappingDaManagedData
{
    [XmlRoot(ElementName = "managed-data-mappings")]
    public partial class FvgManagedDataMapper
    {

        public class Mapping
        {
            string _dataType = "string";
            string _dataFormat = "";

            [XmlElement(ElementName = "campo")]
            public string Campo { get; set; }

            [XmlElement(ElementName = "xpath")]
            public string XPath { get; set; }

            [XmlElement(ElementName = "data-type")]
            public string DataType
            {
                get { return String.IsNullOrEmpty(this._dataType) ? "string" : this._dataType; }
                set { this._dataType = value; }
            }

            [XmlElement(ElementName = "data-format")]
            public string DataFormat
            {
                get { return String.IsNullOrEmpty(this._dataFormat) ? "" : this._dataFormat; }
                set { this._dataFormat = value; }
            }

            public IEnumerable<ManagedDataValue> ApplyTo(XmlDocument document, Dictionary<string, int> registroCampi)
            {
                if (!registroCampi.ContainsKey(this.Campo))
                {
                    return Enumerable.Empty<ManagedDataValue>();
                }

                var idCampo = registroCampi[this.Campo];
                var navigator = document.CreateNavigator();
                var expression = navigator.Compile(this.XPath);

                var iterator = navigator.Select(expression);

                var rVal = new List<ManagedDataValue>();
                var indice = 0;

                while (iterator.MoveNext())
                {
                    var valore = ReadValue(iterator.Current);

                    if (!String.IsNullOrEmpty(valore.Valore) && this.DataType == "DateTime")
                    {
                        if (DateTime.TryParseExact(valore.Valore, this.DataFormat, null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                        {
                            valore = new ItemValue(dt.ToString("yyyyMMdd"), dt.ToString("dd/MM/yyyy"));
                        }
                    }

                    rVal.Add(new ManagedDataValue
                    {
                        Id = idCampo,
                        NomeCampo = this.Campo,
                        Valore = valore.Valore,
                        ValoreDecodificato = valore.ValoreDecodificato,
                        IndiceMolteplicita = indice
                    });

                    ++indice;
                }

                return rVal;
            }

            protected virtual ItemValue ReadValue(XPathNavigator navigator)
            {
                return new ItemValue(navigator.Value, navigator.Value);
            }


        }

        [XmlElement(ElementName = "map")]
        public List<Mapping> Map { get; set; }

        public FvgManagedDataMapper()
        {
            this.Map = new List<Mapping>();
        }

        public static FvgManagedDataMapper LoadFrom(string fileName, IFVGDebugConfiguration debugConfiguration)
        {
            var fileNames = new List<string>
            {
                fileName
            };

            if (debugConfiguration.IsDebugEnabled)
            {
                fileNames.Add(debugConfiguration.ManagedDataMappingsDevFile);
            }

            var path = fileNames.Select(x => CheckPath(x)).Where(x => !String.IsNullOrEmpty(x)).FirstOrDefault();

            if (String.IsNullOrEmpty(path))
            {
                return new FvgManagedDataMapper();
            }

            var fileContent = File.ReadAllText(path);

            return fileContent.DeserializeXML<FvgManagedDataMapper>();
        }

        private static string CheckPath(string fileName)
        {
            if (fileName.StartsWith("~"))
            {
                fileName = HttpContext.Current.Server.MapPath(fileName);
            }

            return File.Exists(fileName) ? fileName : null;
        }
    }
}
