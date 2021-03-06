//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// Codice sorgente generato automaticamente da xsd, versione=1.1.4322.2032.
// 
using System;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.Logic.Visura
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gruppoinit.it/b1/ConcessioniEAutorizzazioni/Visura")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://gruppoinit.it/b1/ConcessioniEAutorizzazioni/Visura", IsNullable = false)]
	public class RichiestaListaPratiche
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string CodEnte;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string CodSportello;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string IdPratica;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string NumeroPratica;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string NumeroProtocollo;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string NumeroAutorizzazione;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
		public string AnnoIstanza;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
		public string MeseIstanza;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
		public string CodiceIntervento;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Oggetto;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
		public System.DateTime DataProtocollo;

		/// <remarks/>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool DataProtocolloSpecified;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CodFiscale", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public RichiestaListaPraticheCodFiscale[] CodFiscale;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PartitaIva", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public RichiestaListaPratichePartitaIva[] PartitaIva;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
		public string CodViario;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Indirizzo;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string CodCivico;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Civico;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string TipoCatasto;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Foglio;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Particella;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Subalterno;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Stato;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public int? LimiteRecords;


        public bool IsRicercaMappali => !String.IsNullOrEmpty(this.TipoCatasto) ||
                                                    !String.IsNullOrEmpty(this.Foglio) ||
                                                    !String.IsNullOrEmpty(this.Particella) ||
                                                    !String.IsNullOrEmpty(this.Subalterno);

    }

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gruppoinit.it/b1/ConcessioniEAutorizzazioni/Visura")]
	public class RichiestaListaPraticheCodFiscale
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(true)]
		public bool cercaComeRichiedente = true;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaComeAzienda = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaNeiSoggettiCollegati = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaComeTecnico = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaAncheComePartitaIva = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://gruppoinit.it/b1/ConcessioniEAutorizzazioni/Visura")]
	public class RichiestaListaPratichePartitaIva
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(true)]
		public bool cercaComeRichiedente = true;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaComeAzienda = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaNeiSoggettiCollegati = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaComeTecnico = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool cercaAncheComeCodiceFiscale = false;

		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value;
	}

}
