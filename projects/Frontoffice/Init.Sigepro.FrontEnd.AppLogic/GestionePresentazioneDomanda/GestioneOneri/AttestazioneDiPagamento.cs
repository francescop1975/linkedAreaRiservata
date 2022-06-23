﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.StcService;
using log4net;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri
{
	public class AttestazioneDiPagamento
	{
        ILog _log = LogManager.GetLogger(typeof(AttestazioneDiPagamento));

		public static AttestazioneDiPagamento NonPresente = new AttestazioneDiPagamento(false, String.Empty, (int?)null, false);

		bool	_presente		= false;
		string	_nomeFile		= String.Empty;
		int?	_codiceOggetto	= (int?)null;
		bool	_firmatoDigitalmente = false;

        public bool Presente { 
            get {
                _log.DebugFormat("AttestazioneDiPagamento.Presente - valore: {0}, ts={1}", this._presente, this._ts);
                return this._presente; 
            } 
        }
		public string	NomeFile		{ get { return this._nomeFile; } }
		public int?		CodiceOggetto	{ get { return this._codiceOggetto; } }
		public bool FirmatoDigitalmente { get { return this._firmatoDigitalmente; } }

        long _ts = DateTime.Now.Ticks;

		public AttestazioneDiPagamento(bool presente, string nomeFile, int? codiceOggetto, bool firmatoDigitalmente)
		{
			this._presente				= presente;
			this._nomeFile				= nomeFile;
			this._codiceOggetto			= codiceOggetto;
			this._firmatoDigitalmente	= firmatoDigitalmente;

            this._log.DebugFormat("Creata nuova attestazione di pagamento, presente={0}, nome={1}, codiceOggetto={2}, firmatoDigitalmente={3}, _ts={4}", this._presente, this._nomeFile, this._codiceOggetto, this.FirmatoDigitalmente, this._ts);
		}

		public DocumentiType ToDocumentiType()
		{
			return new DocumentiType
			{
				id = String.Empty,
				documento = "Copia della ricevuta attestante l'avvenuto pagamento degli oneri ",
				tipoDocumento = "Altro",
				allegati = new AllegatiType
				{
					id = this.CodiceOggetto.ToString(),
					allegato = this.NomeFile
				}
			};
		}
	}
}
