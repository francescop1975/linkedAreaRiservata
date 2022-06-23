// -----------------------------------------------------------------------
// <copyright file="ModelloDinamicoStub.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SIGePro.DatiDinamici.v1.Tests.ValidazioneValoriCampi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Init.SIGePro.DatiDinamici;
    using Init.SIGePro.DatiDinamici.Contesti;
    using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
    using Init.SIGePro.DatiDinamici.Interfaces;
    using Init.SIGePro.DatiDinamici.Interfaces.WebControls;

    public class LoaderStub : IModelloDinamicoLoader
	{
		ModelloDinamicoBase _modello;

		public void SetModelloFrontoffice(bool value)
		{
			this.IsModelloFrontoffice = value;
		}

		public ModelloDinamicoBase.FlagsModello GetFlags()
		{
			throw new NotImplementedException();
		}

		public ModelloDinamicoBase.ScriptsModello GetScripts()
		{
			throw new NotImplementedException();
		}

		public ModelloDinamicoBase.StrutturaModello GetStrutturaModello()
		{
			throw new NotImplementedException();
		}

		public void SetModello(ModelloDinamicoBase modello)
		{
			_modello = modello;
		}

		public string Idcomune
		{
			get { return "idComune"; }
		}

		public string Token
		{
			get { return "token"; }
		}

		public string NomeModello
		{
			get { return "nome modello"; }
		}


        public bool IsModelloFrontoffice { get; private set; }

        public IDyn2DataAccessFactory DataAccessFactory => new Dyn2DataAccessFactoryStub();
    }

    public class Dyn2DataAccessFactoryStub : IDyn2DataAccessFactory
    {
        private ContestoModelloDinamico _tipoContesto;

        public IDyn2CampiManager GetCampiManager()
        {
            throw new NotImplementedException();
        }

        public IClasseContestoLoader GetClassLoader()
        {
            throw new NotImplementedException();
        }

        public IDyn2DettagliModelloManager GetDettagliModelloManager()
        {
            throw new NotImplementedException();
        }

        public IDyn2QueryDatiDinamiciManager GetDyn2QueryDatiDinamiciManager()
        {
            throw new NotImplementedException();
        }

        public IDyn2ModelliManager GetModelliManager()
        {
            return null; // throw new NotImplementedException();
        }

        public IDyn2ProprietaCampiManager GetProprietaCampiManager()
        {
            throw new NotImplementedException();
        }

        public IQueryLocalizzazioni GetQueryLocalizzazioni()
        {

            throw new NotImplementedException();
        }

        public IDyn2DatiRepository GetRepository()
        {
            throw new NotImplementedException();
        }

        public IDyn2ScriptCampiManager GetScriptCampiManager()
        {
            throw new NotImplementedException();
        }

        public IDyn2ScriptModelloManager GetScriptModelliManager()
        {
            return null;  //throw new NotImplementedException();
        }

        public IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            throw new NotImplementedException();
        }

        public IDyn2TestoModelloManager GetTestoModelloManager()
        {
            throw new NotImplementedException();
        }

        public string GetToken()
        {
            throw new NotImplementedException();
        }
    }

    public class ModelloDinamicoStub : ModelloDinamicoBase
	{
        ContestoModelloDinamico _contesto = new ContestoModelloDinamico("token", ContestoModelloEnum.Istanza, null);

        public ModelloDinamicoStub()
			:base(0, 0, false, null, new LoaderStub())
		{

		}

		public void ImpostaContesto(ContestoModelloEnum tipoContesto)
		{
			this._contesto = new ContestoModelloDinamico("token", tipoContesto, null);
		}
		
		protected override ContestoModelloDinamico InizializzaContesto()
		{
            return this._contesto;
		}
    }
}
