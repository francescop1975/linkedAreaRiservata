
			using System;
			using System.IO;
			using Init.SIGePro.Data;
			using PersonalLib2.Data;

			namespace Init.SIGePro.Verticalizzazioni
			{
				/**************************************************************************************************************************************
				*
				* Classe generata automaticamente dalla verticalizzazione PROTOCOLLO_DOCER il 26/08/2014 17.28.01
				* NON MODIFICARE DIRETTAMENTE!!!
				*
				***************************************************************************************************************************************/
			
			
				/// <summary>
				/// Abilita il sistema di protocollazione STANDARD DOCER creato dalla Regione Emilia Romagna per le integrazioni con i vari sistemi di protocollo presenti nella regione. E' uno standard, alla stregua di DocArea, ed è principalmente un gestore documentale in grado anche di trattare l'argomento della protocollazione, che poi è quello che interessa questa verticalizzazione, che deve essere abilitata per integrarsi con i sistemi che utilizzano lo Standard DocEr
				/// </summary>
				public partial class VerticalizzazioneProtocolloDocer : Verticalizzazione
				{
					private const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_DOCER";
                    
                    public VerticalizzazioneProtocolloDocer()
                    {

                    }

					public VerticalizzazioneProtocolloDocer(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, NOME_VERTICALIZZAZIONE , software, codiceComune){}
					
					
					/// <summary>
					/// Costante che indica come deve essere chiamato il documento principale che viene passato al sistema documentale, in vbg il documento principale è il primo della lista. I documenti possono assumere i seguenti valori: PRINCIPALE, ALLEGATO, ANNOTAZIONE, ANNESSO, da specifiche al momento va inserito il valore PRINCIPALE. Da considerare inoltre, sempre al momento, è possibile protocollare solamente un documento alla volta.
					/// </summary>
					public string AllegatoPrincipale
					{
						get{ return GetString("ALLEGATO_PRINCIPALE");}
						set{ SetString("ALLEGATO_PRINCIPALE" , value); }
					}
					
					/// <summary>
					/// Parametro aggiuntivo all'autenticazione riferita al web service, per loggarsi al web service di login è necessario che sia presente, oltre che username e password, anche anche il parametro Applicazione, in sostanza sarebbe l'identificativo dell'applicazione dalla quale l'utente sta effettuando l'autenticazione
					/// </summary>
					public string Applicazione
					{
						get{ return GetString("APPLICAZIONE");}
						set{ SetString("APPLICAZIONE" , value); }
					}
					
					/// <summary>
					/// Indicare in questo parametro il Codice dell'amministrazione, questo dato andrà a valorizzare gli elementi CODICEAMMINISTRAZIONE della segnatura che viene inviata al metodo protocollazione del web service. Se non valorizzato questo dato verrà preso dal parametro CODICEENTE come succedeva in passato.
					/// </summary>
					public string Codiceamministrazione
					{
						get{ return GetString("CODICEAMMINISTRAZIONE");}
						set{ SetString("CODICEAMMINISTRAZIONE" , value); }
					}
					
					/// <summary>
					/// Codice dell'Area Organizzativa Omogenea, questo parametro viene passato al web service della gestione documentale, in particolare questo dato è una proprietà del documento che viene inviato al sistema documentale
					/// </summary>
					public string CodiceAoo
					{
						get{ return GetString("CODICE_AOO");}
						set{ SetString("CODICE_AOO" , value); }
					}
					
					/// <summary>
					/// Parametro aggiuntivo all'autenticazione riferita al web service, per loggarsi al web service di login è necessario che sia presente, oltre che username e password, anche anche il parametro Codice Ente
					/// </summary>
					public string CodiceEnte
					{
						get{ return GetString("CODICE_ENTE");}
						set{ SetString("CODICE_ENTE" , value); }
					}

                    /// <summary>
                    /// (Facoltativo) Se valorizzato a 1 consente, in fase di protocollazione istanza o movimento da backoffice, di inviare il file segnatura.xml che sarebbe il file che viene inviato al web service per eseguire la protocollazione; se valorizzato a 0 o non valorizzato non esegue nessuna funzionalità.
                    /// </summary>
                    public string InviaSegnatura
                    {
                        get { return GetString("INVIA_SEGNATURA"); }
                        set { SetString("INVIA_SEGNATURA", value); }
                    }

					/// <summary>
					/// Password riferita all'autenticazione al web service
					/// </summary>
					public string Password
					{
						get{ return GetString("PASSWORD");}
						set{ SetString("PASSWORD" , value); }
					}
					
					/// <summary>
					/// Unità organizzativa, è riferita all'unità di smistamento in fase di protocollazione, questo dato non è obbligatorio.
					/// </summary>
					public string Uo
					{
						get{ return GetString("UO");}
						set{ SetString("UO" , value); }
					}
					
					/// <summary>
					/// Il sistema standard docer ha un web service apposito creato per gestire solo la fascicolazione, dentro questo parametro va indicato, appunto, l'end point del servizio che gestisce la fascicolazione
					/// </summary>
					public string UrlFascicolazione
					{
						get{ return GetString("URL_FASCICOLAZIONE");}
						set{ SetString("URL_FASCICOLAZIONE" , value); }
					}
					
					/// <summary>
					/// Il sistema standard docer ha un web service apposito creato per gestire solo la gestione documentale, dentro questo parametro va indicato, appunto, l'end point del servizio della gestione documentale
					/// </summary>
					public string UrlGestioneDocs
					{
						get{ return GetString("URL_GESTIONE_DOCS");}
						set{ SetString("URL_GESTIONE_DOCS" , value); }
					}
					
					/// <summary>
					/// Per effettuare operazioni con un protocollo standard docer è necessario prima di tutto autenticarsi al web service, il sistema standard docer ha un web service apposito creato per gestire solo l'autenticazione, dentro questo parametro va indicato, appunto, l'end point del servizio che gestisce il Login
					/// </summary>
					public string UrlLogin
					{
						get{ return GetString("URL_LOGIN");}
						set{ SetString("URL_LOGIN" , value); }
					}
					
					/// <summary>
					/// Il sistema standard docer ha un web service apposito creato per gestire solo il sistema di protocollazione, dentro questo parametro va indicato, appunto, l'end point del servizio che gestisce la protocollazione
					/// </summary>
					public string UrlProtocollazione
					{
						get{ return GetString("URL_PROTOCOLLAZIONE");}
						set{ SetString("URL_PROTOCOLLAZIONE" , value); }
					}

                    /// <summary>
                    /// Il sistema standard docer ha un web service apposito creato per gestire solo il sistema di registrazione particolare, dentro questo parametro va indicato, appunto, l'end point del servizio che gestisce la registrazione particolare
                    /// </summary>
                    public string UrlRegParticolare
                    {
                        get { return GetString("URL_REG_PARTICOLARE"); }
                        set { SetString("URL_REG_PARTICOLARE", value); }
                    }

					/// <summary>
					/// Username riferito all'autenticazione al web service
					/// </summary>
					public string Username
					{
						get{ return GetString("USERNAME");}
						set{ SetString("USERNAME" , value); }
					}

                    /// <summary>
                    /// Il sistema standard docer ha un web service apposito creato per gestire solo l'invio delle PEC gestito nelle protocollazioni in partenza, dentro questo parametro va indicato, appunto, l'end point del servizio.
                    /// </summary>
                    public string UrlPec
                    {
                        get { return GetString("URL_PEC"); }
                        set { SetString("URL_PEC", value); }
                    }

                    /// <summary>
                    /// Parametro che indica come gestire l'invio della PEC per le protocollazioni in PARTENZA, se non valorizzato o valorizzato a 0 non avverrà nessun invio della PEC e non si presenterà nessun pulsante che possa inviarla durante la protocollazione in partenza, se valorizzato a 1 l'invio della PEC sarà automatico ogni volta che verrà richiesto un protocollo con flusso in PARTENZA, se valorizzato a 2 l'invio della PEC sarà manuale, ossia l'invio sarà subordinato solamente al clic del pulsante INVIA MAIL che viene visualizzato subito dopo che vengono restituiti i dati di protocollo.
                    /// </summary>
                    public string TipoInvioPec
                    {
                        get { return GetString("TIPO_INVIO_PEC"); }
                        set { SetString("TIPO_INVIO_PEC", value); }
                    }

                    /// <summary>
                    /// Indica con quale type id devono essere gestite le anagrafiche del web service. Le anagrafiche DocEr hanno un campo denominato type id, che sarebbe una sorta di categoria, questo dato deve essere specificato sia in fase di ricerca che in fase di creazione o aggiornamento anagrafica. Alcuni type id sono fissi, come AOO, ENTE, FASCICOLO E TITOLARIO, esiste poi l'ANAGRAFICA CUSTOM dove indicare il tipo di anagrafica da gestire, ad esempio può essere inserito il valore RICHIEDENTI, AZIENDE....Questo valore deve riportare un tipo di anagrafica custom gestita solo da VBG, ad esempio ANAGRAFICA_VBG, e deve essere prima inserita manualmente dal cliente nel sistema docer e poi riportata in questo valore, in pratica quando si vanno a fare ricerche o inserimenti di anagrafiche, questo type_id deve essere presente nel sistema docer.
                    /// </summary>
                    public string TypeIdAnagCustom
                    {
                        get { return GetString("TYPE_ID_ANAG_CUSTOM"); }
                        set { SetString("TYPE_ID_ANAG_CUSTOM", value); }
                    }
                    /// <summary>
                    /// Indicare in questo parametro il nome del campo codice obbligatorio ai fini della gestione delle Anagrafiche Custom su docer, questo campo è obbligatorio su docer se si vuole gestire le anagrafiche custom e deve essere creato lato server, ossia dal cliente.
                    /// </summary>
                    public string AnagCustomCodice
                    {
                        get { return GetString("ANAG_CUSTOM_CODICE"); }
                        set { SetString("ANAG_CUSTOM_CODICE", value); }
                    }

                    /// <summary>
                    /// Indicare in questo parametro il nome del campo descrizione obbligatorio ai fini della gestione delle Anagrafiche Custom su docer, questo campo è obbligatorio su docer se si vuole gestire le anagrafiche custom e deve essere creato lato server, ossia dal cliente.
                    /// </summary>
                    public string AnagCustomDescrizione
                    {
                        get { return GetString("ANAG_CUSTOM_DESCRIZIONE"); }
                        set { SetString("ANAG_CUSTOM_DESCRIZIONE", value); }
                    }

                    /// <summary>
                    /// Questo parametro serve in fase di lettura protocollo nel caso in cui non sia specificato l'ID (ad esempio quando viene indicato manualmente numero e data protocollo su istanze / movimenti), in particolare indica il numero di caratteri (indicati nel parametro PAD_NUMPROTO_CHAR) da cui fare il padding a sinistra, ad esempio se viene indicato il numero 1234 ed il valore di questo parametro è indicato a 7 e quello del parametro PAD_NUMPROTO_CHAR a 0, il numero sarà trasformato, in fase di lettura, a 0001234.
                    /// </summary>
                    public string UsaLdapAuth
                    {
                        get { return GetString("USA_LDAP_AUTH"); }
                        set { SetString("USA_LDAP_AUTH", value); }
                    }

                    /// <summary>
                    /// 
                    /// </summary>
                    public string PadNumProtoLength
                    {
                        get { return GetString("PAD_NUMPROTO_LENGTH"); }
                        set { SetString("PAD_NUMPROTO_LENGTH", value); }
                    }

                    /// <summary>
                    /// A stretto legame con il parametro PAD_NUMPROTO_LENGTH serve per indicare quale carattere utilizzare durante il padding dei caratteri del numero protocollo, di default è impostato a 0.
                    /// </summary>
                    public string PadNumProtoChar
                    {
                        get { return GetString("PAD_NUMPROTO_CHAR"); }
                        set { SetString("PAD_NUMPROTO_CHAR", value); }
                    }

                    /// <summary>
                    /// Se impostato a 1 il sistema di protocollazione non riceverà i metadati non obbligatori. Questa modifica si rende necessaria in quanto la logica della valorizzazione dei metadati al tipo documento è completamente gestita da codice, e può essere utile nel caso in cui ci sia un disallineamento con DocEr per quanto riguarda appunto i metadati.
                    /// </summary>
                    public string DisabilitaMetadati
                    {
                        get { return GetString("DISABILITA_METADATI"); }
                        set { SetString("DISABILITA_METADATI", value); }
                    }

                    /// <summary>
                    /// Questo parametro viene utilizzato durante la creazione dell''xml datipec da inviare al web service PEC di DocEr, e sta ad indicare se deve essere compilata la sezione Documenti (valore uguale a 1) o meno (valore diverso da 1 o non presente). Questo parametro si è reso necessario in quanto sono presenti due xsd differenti in DocEr uno dove è presente e obbligatorio compilare la sezione Documenti e uno no. In teoria il verticale (VBG) dovrebbe utilizzare l''xsd dove non è presente il nodo Documenti (lo dovrebbe utilizzare solamente il provider di protocollazione) ma abbiamo visto che questa parte non è ben chiara in quanto in alcune installazioni l''xsd di docer è uno in altre è un altro. Quindi, in attesa di chiarire la situzione si è previsto entrambe le soluzioni.
                    /// </summary>
                    public string UsaDocumentiPec
                    {
                        get { return GetString("USA_DOCUMENTI_PEC"); }
                        set { SetString("USA_DOCUMENTI_PEC", value); }
                    }

                    /// <summary>
                    /// Questo parametro sta ad indicare se si intende ignorare il warning della ricerca / inserimento / aggiornamento delle anagrafiche custom (valore uguale a 1) o meno (valore diverso da 1 o parametro non presente). Questo parametro si rende necessario in quanto la comunicazione con le anagrafiche custom è di bassissima importanza e spesso è causa di errori da parte di solr / docer, il sistema, in casi di errori, restituisce semplicemente un warning che viene poi visualizzato dall'utente generando preoccupazione anche se in realtà è di scarsissima importanza. Il warning anche se non visualizzato (valore uguale a 1) sarà comunque inserito nei log.
                    /// </summary>
                    public string IgnoraWarnAnag
                    {
                        get { return GetString("IGNORA_WARN_ANAG"); }
                        set { SetString("IGNORA_WARN_ANAG", value); }
                    }

                    /// <summary>
                    /// Indicare in questo parametro il valore, obbligatoriamente numerico, del numero di retry alla chiamata a protocollaById che il sistema deve fare in caso di errore di protocollazione del web service. Di default non saranno impostate retry, quindi è come l''equivalente del valore 0.
                    /// </summary>
                    public string TentativiRetry
                    {
                        get { return GetString("N_TENTATIVI_RETRY"); }
                        set { SetString("N_TENTATIVI_RETRY", value); }
                    }

                    /// <summary>
                    /// Questo parametro si rende necessario in quanto la logica standard relativa alle impostazioni delle ACL successivamente alla creazione di un fascicolo impone che venga fatta la chiamata updateACLFascicolo del ws di Fascicolazione, mentre inizialmente l'impostazione delle ACL veniva fatto con il metodo setACLFascicolo del ws di gestioneDocumentale. Visto e considerato che molti fornitori di protocollo funzionano correttamente con la vecchia versione, quindi quella che invoca il metodo setACLFascicolo, si è deciso di distinguere l'utilizzo di uno piuttosto che di un altro metodo tramite questo parametro che, se impostato a 1 utilizzerà il nuovo sistema, quindi quello standard che usa il metodo updateACLFascicolo del ws di fascicolazione, altrimenti, con qualsiasi altro valore, sarà utilizzata la vecchia logica funzionante con i protocolli di Maggioli e ADS, ossia utilizzando il metodo setACLFascicolo del ws di gestione documentale.
                    /// </summary>
                    public string CompatibilitaFasc
                    {
                        get { return GetString("COMPATIBILITA_FASC"); }
                        set { SetString("COMPATIBILITA_FASC", value); }
                    }

                }
			}
			