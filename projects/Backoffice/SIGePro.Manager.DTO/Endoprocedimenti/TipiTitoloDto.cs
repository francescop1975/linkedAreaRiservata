using System.Collections.Generic;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.DTO.Endoprocedimenti
{
    public class TipiTitoloDtoFlags
	{
		[XmlElement(Order=0)]
		public bool MostraData { get; set; }
		[XmlElement(Order = 1)]
		public bool MostraNumero { get; set; }
		[XmlElement(Order = 2)]
		public bool MostraRilasciatoDa { get; set; }
		[XmlElement(Order = 3)]
		public bool RichiedeAllegato { get; set; }
		[XmlElement(Order = 4)]
		public bool VerificaFirmaAllegato { get; set; }
		[XmlElement(Order = 5)]
		public bool AllegatoObbligatorio { get; set; }
	}

	public class TipiTitoloDto: BaseDto<int, string>
	{
		public TipiTitoloDtoFlags Flags { get; set; }

		public TipiTitoloDto()
		{
			this.Flags = new TipiTitoloDtoFlags();
		}
	}

	public class TipiTitoloPerCodiceInventario
	{
		public int CodiceInventario { get; set; }
		public List<TipiTitoloDto> TipiTitolo { get; set; } = new List<TipiTitoloDto>();
	}
}
