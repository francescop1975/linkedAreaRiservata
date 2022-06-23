using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Contesti
{
	public enum ContestoModelloEnum
	{
		Istanza,
		Anagrafe,
		Attivita,
        Mercati,
        Posteggi
    }

	public static class ContestoTranslator
	{
		public static string ContestoEnumToContestoBase(ContestoModelloEnum contesto)
		{
			if (contesto == ContestoModelloEnum.Attivita)
				return "AT";

			if (contesto == ContestoModelloEnum.Anagrafe )
				return "AN";

            if (contesto == ContestoModelloEnum.Mercati)
                return "ME";

            if (contesto == ContestoModelloEnum.Posteggi)
                return "PO";

            return "IS";
		}

		public static ContestoModelloEnum ContestoBaseToContestoEnum(string idContestoBase)
		{
			if (idContestoBase == "AT")
				return ContestoModelloEnum.Attivita;

			if (idContestoBase == "AN")
				return ContestoModelloEnum.Anagrafe;

			if (idContestoBase == "IS")
				return ContestoModelloEnum.Istanza;

            if (idContestoBase == "ME")
                return ContestoModelloEnum.Mercati;

            if (idContestoBase == "PO")
                return ContestoModelloEnum.Posteggi;

            throw new ArgumentException("il contesto base " + idContestoBase + " non è valido");

			//public const string CONTESTO_ISTANZE = "IS";
			//public const string CONTESTO_ANAGRAFE = "AN";
			//public const string CONTESTO_ATTIVITA = "AT";
		}
	}
}
