using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.Database;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.GenerazionePdfModulo;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData;
using Init.Sigepro.FrontEnd.Infrastructure.FileEncoding;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.Manager.DTO.Endoprocedimenti;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG
{
    public class ServiziFVGService
    {
        private static class Constants
        {
            public const string SegnapostoNomeModello = "$nomeModello";
            public const string SegnapostoDataUltimaModifica = "$dataUltimaModifica";
        }


        public class SchedaDinamicaEndoprocedimento
        {
            public int Id { get; set; }
            public string Descrizione { get; set; }
            public bool Compilata { get; set; }
            public int Ordine { get; set; }
            public bool Pubblica { get; set; }
        }

        public class AllegatoEndoprocedimento
        {
            public int Id { get; internal set; }
            public int CodiceOggetto { get; internal set; }
        }

        public class EndoprocedimentoDaCompilare
        {
            public readonly int Id;
            public readonly string Descrizione;
            public readonly DateTime? DataUltimaModifica;
            internal IEnumerable<SchedaDinamicaEndoprocedimento> ListaSchede;
            public IEnumerable<SchedaDinamicaEndoprocedimento> ListaSchedePubblicate  => this.ListaSchede.Where( x => x.Pubblica);
            public bool TutteLeSchedeSonoCompilate => !ListaSchedePubblicate.Where(x => !x.Compilata).Any();
            public IEnumerable<AllegatoEndoprocedimento> Allegati;

            public EndoprocedimentoDaCompilare(int id, string descrizione, DateTime? dataUltimaModifica)
            {
                this.Id = id;
                this.Descrizione = descrizione;
                this.DataUltimaModifica = dataUltimaModifica;

                this.ListaSchede = Enumerable.Empty<SchedaDinamicaEndoprocedimento>();
                this.Allegati = Enumerable.Empty<AllegatoEndoprocedimento>();
            }
            
            public int? GetIdSchedaSuccessiva(int idSchedaRiferimento)
            {
                var numeroSchede = this.ListaSchedePubblicate.Count();

                for (int i = 0; i < numeroSchede; i++)
                {
                    var scheda = this.ListaSchedePubblicate.ElementAt(i);

                    if (scheda.Id == idSchedaRiferimento)
                    {
                        // La scheda compilata è l'ultima
                        if (i < numeroSchede - 1)
                        {
                            return this.ListaSchedePubblicate.ElementAt(i + 1).Id;
                        }
                    }
                }

                return null;
            }
        }



        private readonly IAliasResolver _aliasResolver;
        private readonly ITokenApplicazioneService _tokenApplicazioneService;
        private readonly IDatiDinamiciRepository _datiDinamiciRepository;
        private readonly IFvgDatabaseFactory _databaseFactory;
        private readonly IFvgManagedDataRepository _managedDataRepository;
        private readonly IHtmlToPdfFileConverter _fileConverter;
        private readonly IEndoprocedimentiService _endoService;
        private readonly SostituzioneSegnapostoRiepilogoService _sostituzioneSegnapostoRiepilogoService;
        private readonly IOggettiService _oggettiService;


        public ServiziFVGService(IAliasResolver aliasResolver, ITokenApplicazioneService tokenApplicazioneService, IDatiDinamiciRepository datiDinamiciRepository, IFvgDatabaseFactory databaseFactory, IFvgManagedDataRepository managedDataRepository,IEndoprocedimentiService endoService, SostituzioneSegnapostoRiepilogoService sostituzioneSegnapostoRiepilogoService, IHtmlToPdfFileConverter fileConverter, IOggettiService oggettiService)
        {
            this._aliasResolver = aliasResolver;
            this._tokenApplicazioneService = tokenApplicazioneService;
            this._datiDinamiciRepository = datiDinamiciRepository;
            this._databaseFactory = databaseFactory;
            _managedDataRepository = managedDataRepository;
            //this._webServiceProxy = webServiceProxyFactory.CreateService();
            this._endoService = endoService;
            this._sostituzioneSegnapostoRiepilogoService = sostituzioneSegnapostoRiepilogoService;
            this._fileConverter = fileConverter;
            this._oggettiService = oggettiService;

         
        }

        public void SalvaScheda(ModelloDinamicoBase modello)
        {
            modello.Salva();
            modello.SalvaCampiNonVisibili();
        }

        public ModelloDinamicoIstanza GetModelloDinamico(long codiceIstanza, string idModulo, int idModello, int indiceScheda)
        {
            var database = this._databaseFactory.Create(codiceIstanza, idModulo);
            var modello = this._datiDinamiciRepository.GetCacheModelloDinamico(idModello);
            var factory = new FvgDyn2DataAccessFactory(modello, database, _tokenApplicazioneService);
            var loader = new ModelloDinamicoLoader(factory, _aliasResolver.AliasComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Frontoffice);
            var scheda = new ModelloDinamicoIstanza(loader, idModello, indiceScheda, false);

            return scheda;
        }

        public EndoprocedimentoDaCompilare GetDatiModulo(long codiceIstanza, string idModulo)
        {
            var database = this._databaseFactory.Create(codiceIstanza, idModulo);

            var datiModulo =  GetDatiModulo(database, idModulo);

            // Salvo sempre i dati di tutti i campi recuperabili dal managed data tramite espressioni xpath
            var campiDinamici = this.CampiDinamiciDaIdSchede(datiModulo.ListaSchede.Select(x => x.Id));
            var listaDati = this._managedDataRepository.ReadAllValues(codiceIstanza, campiDinamici);

            database.SalvaListaDati(listaDati);

            return datiModulo;
        }

        private IEnumerable<IDyn2Campo> CampiDinamiciDaIdSchede(IEnumerable<int> idSchede)
        {
            return idSchede.SelectMany(x => this._datiDinamiciRepository.GetCacheModelloDinamico(x).ListaCampiDinamici.Values);
        }

        private EndoprocedimentoDaCompilare GetDatiModulo(FvgDatabase database, string idModulo)
        {
            var endoprocedimento = this._endoService.GetByIdEndoMappato(idModulo);

            if (endoprocedimento == null)
            {
                throw new ArgumentException($"Il modulo {idModulo} non è stato configurato");
            }

            var rVal = new EndoprocedimentoDaCompilare(endoprocedimento.Id, endoprocedimento.Descrizione, endoprocedimento.DataUltimaModifica);

            database.SincronizzaSchede(endoprocedimento.Schede);

            if (!database.ContieneValori)
            {
                var listaCampiDelModulo = CampiDinamiciDaIdSchede(endoprocedimento.Schede.Select( x => x.Id))
                                            .Select(x => x.Nomecampo); ;

                database.InizializzaValoriDaManagedData(listaCampiDelModulo, this._managedDataRepository);
            }

            rVal.ListaSchede = endoprocedimento.Schede.Select(x => new SchedaDinamicaEndoprocedimento
            {
                Id = x.Id,
                Descrizione = x.Descrizione,
                Ordine = x.Ordine.GetValueOrDefault(9999),
                Compilata = database.IsSchedaCompilata(x.Id),
                Pubblica = x.Pubblica
            }).OrderBy(x => x.Ordine);

            rVal.Allegati = endoprocedimento.Allegati == null ? 
                                Enumerable.Empty< AllegatoEndoprocedimento>() : 
                                endoprocedimento.Allegati
                                                .Select(x => new AllegatoEndoprocedimento
                                                {
                                                    Id = x.Codice,
                                                    CodiceOggetto = x.CodiceOggetto.Value
                                                });

            return rVal;
        }

        public BinaryFile GeneraPdfModulo(long codiceIstanza, string idModulo)
        {
            var database = this._databaseFactory.Create(codiceIstanza, idModulo);

            return GeneraPdfModulo(database, idModulo);
        }

        private BinaryFile GeneraPdfModulo(FvgDatabase database, string idModulo)
        {
            var endoInCompilazione = this.GetDatiModulo(database, idModulo);

            if (endoInCompilazione == null)
            {
                throw new ArgumentException($"Il modulo {idModulo} non è configurato");
            }

            var reader = new FvgDatiDinamiciRiepilogoReader(this._aliasResolver, this._datiDinamiciRepository, database, endoInCompilazione.ListaSchede);
            var template = @"<!doctype html>
<html>
<head>
    <meta http-equiv='Content-Type' content='text/html;charset=utf-8' />
	<title>Document</title>
</head>
<body>
    Nome modello: $nomeModello<br />
    Data ultima modifica: $dataUltimaModifica<br />
	<schedeDinamiche />
    <noteCompilazione />
</body>
</html>";

            if (endoInCompilazione.Allegati.Any())
            {
                var oggetto = this._oggettiService.GetById(endoInCompilazione.Allegati.First().CodiceOggetto);
                template = UnknownEncodingToString.Convert(oggetto.FileContent);
            }

            // Da rimuovere in produzione
            // template = File.ReadAllText(@"c:\temp\B1_-_Commercio_in_sede_fissa_web.html");
            template = template.Replace(Constants.SegnapostoDataUltimaModifica, endoInCompilazione.DataUltimaModifica.HasValue ? endoInCompilazione.DataUltimaModifica.Value.ToString("dd/MM/yyyy") : "");
            template = template.Replace(Constants.SegnapostoNomeModello, endoInCompilazione.Descrizione);

            var html = this._sostituzioneSegnapostoRiepilogoService.ProcessaRiepilogo(reader, template);

            return this._fileConverter.Converti($"riepilogo-{idModulo}.pdf", html, new RenderingFlags
            {
                ConvertToPdfa = false
            });
        }

        public void AllegaPdfADomanda(long codiceIstanza, string idModulo)
        {
            var binaryFile = GeneraPdfModulo(codiceIstanza, idModulo);

            this._managedDataRepository.SalvaFilePdf(codiceIstanza, idModulo, binaryFile);
        }

        public BinaryFile GeneraAnteprimaModulo(string idModulo)
        {
            return GeneraPdfModulo(new FvgDatabase(), idModulo);
        }
    }
}
