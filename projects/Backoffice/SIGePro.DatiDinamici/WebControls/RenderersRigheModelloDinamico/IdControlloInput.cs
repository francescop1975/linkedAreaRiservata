﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.WebControls.RenderersRigheModelloDinamico
{
	internal interface IdControlloInput
	{
		int IndiceMolteplicitaValore { get; }
		int IndiceRiga { get; }
		string AsString();
	}
}
