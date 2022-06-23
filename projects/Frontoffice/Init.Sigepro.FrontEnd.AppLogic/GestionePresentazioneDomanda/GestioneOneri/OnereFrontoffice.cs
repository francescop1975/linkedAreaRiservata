﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.StcService;
using Init.Sigepro.FrontEnd.AppLogic.ObjectSpace.PresentazioneIstanza;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri
{
	public class OnereFrontoffice
	{
		public class CausaleOnere
		{
			public int Codice { get; private set; }
			public string Descrizione { get; private set; }

			public CausaleOnere(int codice, string descrizione)
			{
				this.Codice = codice;
				this.Descrizione = descrizione;
			}

			public override string ToString()
			{
				return this.Descrizione;
			}
		}

		public class EndoOInterventoOrigineOnere
		{
			public int Codice { get; private set; }
			public string Descrizione { get; private set; }

			public EndoOInterventoOrigineOnere(int codice, string descrizione)
			{
				this.Codice = codice;
				this.Descrizione = descrizione;
			}

			public override string ToString()
			{
				return this.Descrizione;
			}
		}

		public enum ProvenienzaOnereEnum
		{
			Endoprocedimento,
			Intervento
		}

		public CausaleOnere Causale { get; private set; }
		public EndoOInterventoOrigineOnere EndoOInterventoOrigine { get; private set; }
		public ProvenienzaOnereEnum Provenienza { get; private set; }
		//public string TipoOnere { get; private set; }
		public decimal Importo { get; private set; }
		public decimal ImportoPagato { get; private set; }
		public string Note { get; private set; }
		public EstremiPagamento EstremiPagamento { get; private set; }
		public ModalitaPagamentoOnereEnum ModalitaPagamento { get; private set; }
		public StatoPagamentoOnereEnum StatoPagamento { get; private set; }
		[Obsolete("Utilizzare UniqueId")]
		public string IdOperazionePagamento { get; private set; }

		public string UniqueId { get; private set; }
		public string IdPosizioneNodoPagamenti { get; private set; }
		public string IUV { get; private set; }

		public string StatoPagamentoNativo { get; private set; }
		public DateTime DataUltimaVerifica { get; private set; }


		public static OnereFrontoffice FromOneriRow(PresentazioneIstanzaDbV2.OneriDomandaRow row)
		{
			var causale = new CausaleOnere(row.CodiceCausale, row.Causale);
			var endoOIntervento = new OnereFrontoffice.EndoOInterventoOrigineOnere(row.CodiceInterventoOEndoOrigine, row.InterventoOEndoOrigine);
			var provenienza = row.TipoOnere == "E" ? ProvenienzaOnereEnum.Endoprocedimento : ProvenienzaOnereEnum.Intervento;
			var importo = (decimal)row.Importo;
			var importoPagato = (decimal)row.ImportoPagato;
			var note = row.Note;
			var modalitaPagamento = ModalitaPagamentoOnereEnum.NonDovuto;

			if (!row.IsModalitaPagamentoNull() && !String.IsNullOrEmpty(row.ModalitaPagamento))
			{
				modalitaPagamento = (ModalitaPagamentoOnereEnum)Convert.ToInt32(row.ModalitaPagamento);
			}

			var onere = new OnereFrontoffice(provenienza, causale, endoOIntervento, importo, importoPagato, note, modalitaPagamento);

			if (!String.IsNullOrEmpty(row.StatoPagamentoOnline))
			{
				onere.StatoPagamento = (StatoPagamentoOnereEnum)Convert.ToInt32(row.StatoPagamentoOnline);
				onere.IdOperazionePagamento = row.IdPagamentoOnline;
				onere.UniqueId = row.UniqueId;
				onere.IdPosizioneNodoPagamenti = row.IdPosizioneNodoPagamenti;
				onere.IUV = row.IUV;
				onere.StatoPagamentoNativo = row.StatoPagamentoNativo;
				if (!String.IsNullOrEmpty(row.DataUltimaVerifica))
				{
					onere.DataUltimaVerifica = DateTime.ParseExact(row.DataUltimaVerifica, OneriWriteInterface.Constants.DateTimeFormat, null);
				}
			}

			if (modalitaPagamento == ModalitaPagamentoOnereEnum.GiaPagato || (modalitaPagamento == ModalitaPagamentoOnereEnum.Online && onere.StatoPagamento == StatoPagamentoOnereEnum.PagamentoRiuscito))
			{
				var formatoData = (row.DataPagmento ?? "").Length == 10 ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm:ss";


				onere.EstremiPagamento = new EstremiPagamento(
					new TipoPagamento(row.CodiceTipoPagamento, row.DescrizioneTipoPagamento),
					String.IsNullOrEmpty(row.DataPagmento) ? (DateTime?)null : DateTime.ParseExact(row.DataPagmento, formatoData, null),
					row.NumeroPagamento,
					importoPagato
				);
			}



			return onere;
		}

		protected OnereFrontoffice(ProvenienzaOnereEnum provenienza, CausaleOnere causale, EndoOInterventoOrigineOnere endoOInterventoOrigine, decimal importo, decimal importoPagato, string note, ModalitaPagamentoOnereEnum modalitaPagamento)
		{
			this.Provenienza = provenienza;
			this.Causale = causale;
			this.EndoOInterventoOrigine = endoOInterventoOrigine;
			this.Importo = importo;
			this.Note = note;
			this.ImportoPagato = importoPagato;
			this.ModalitaPagamento = modalitaPagamento;
		}

		public OneriType ToOneriType()
		{
			return new OneriType
			{
				causale = new CausaleOnereType
				{
					id = this.Causale.Codice.ToString(),
					causale = this.Causale.Descrizione
				},
				codiceProcedimento = this.Provenienza == OnereFrontoffice.ProvenienzaOnereEnum.Endoprocedimento ? this.EndoOInterventoOrigine.Codice.ToString() : String.Empty,
				annotazioni = this.Note,
				nonDovuto = this.ModalitaPagamento == ModalitaPagamentoOnereEnum.NonDovuto,
				nonDovutoSpecified = true,
				scadenze = new OneriScadenzeType[]{
					new OneriScadenzeType{
						dataScadenza = DateTime.Now,
						dataScadenzaSpecified = true,
						importoRata = Math.Round( Double.Parse( this.ImportoPagato.ToString("N2")), 2),
						numeroRata = "1",
						pagamenti = this.EstremiPagamento == null ?
										null :
										new OneriPagamentiType[]{
											new OneriPagamentiType
											{
												data = this.EstremiPagamento.Data.GetValueOrDefault(DateTime.MinValue),
												importo = Math.Round( Double.Parse( this.ImportoPagato.ToString("N2")), 2),
												modalita = this.EstremiPagamento.TipoPagamento.Descrizione,
												rifDocumento = this.EstremiPagamento.Riferimento
											}
										}
					}
				},
				importo = Math.Round(Double.Parse(this.ImportoPagato.ToString("N2")), 2)
			};
		}
	}
}
