/**
 * modulo principale
 */

//inizializzo le variabili che verranno usate per gestire gli elementi DOM
var html_selectedFoglioTitolo, html_selectedParticelleHtmlList, html_createdParticelleHtmlList,
	html_fogli_DropDownList, html_mappali_DropDownList, html_mappali_DropDownList_empty,
	html_selectionBlockDiv, navigation, layersProgetto,
	html_selectedParticelleHtmlListTitle, html_createdParticelleHtmlListTitle,
	html_buttonShowModal, html_modalBox, modalParticellaValue,
	html_buttonReset, html_buttonConfirm, html_buttonConfirmParticellaNuova,
	map = undefined;

/**
 * Funzione di inizializzazione applicazione
 */
function run(paramParticelleInput, paramCallBack) {

	//imposto i parametri passati
	setArrayParticelleInput(paramParticelleInput);
	setCallBack(paramCallBack);

	/**
	 * oggetto contenente:
	 * - coordinata mappa punto cliccato
	 * - numero foglio selezionato
	 * - array particelle selezionate
	 */
	navigation = {
		intersectionPoint: undefined,
		seLectedFoglio: undefined,
		selectedParticelle: [],
		createdParticelle: []
	};

	/**
	 * CARICO GLI ELEMENTI FORM
	 */

	//blocco container di particelle selezionate
	html_selectionBlockDiv = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.selection_block_div);

	//titolo HTML "selezionato il foglio numero 5"
	html_selectedFoglioTitolo = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.selected_foglio_div);

	//lista HTML delle particelle selezionate
	html_selectedParticelleHtmlList = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.selected_particelle_div);
	//lista HTML delle particelle create
	html_createdParticelleHtmlList = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.created_particelle_div);
	//titolo di lista HTML delle particelle selezionate
	html_selectedParticelleHtmlListTitle = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.selected_particelle_title);
	//titolo di lista HTML delle particelle create
	html_createdParticelleHtmlListTitle = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.created_particelle_title);

	//elemento <select> con tutti i fogli selezionabili
	html_fogli_DropDownList = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.fogli_input_select);

	//elemento <select> con tutti i mappali (relativi al foglio selezionato) selezionabili
	html_mappali_DropDownList = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.mappali_input_select);

	//elemento <select> vuoto da mostrare in fase di aggiornamento del contenuto della <select> particelle
	html_mappali_DropDownList_empty = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.mappali_input_select_alt_txt);

	//inizializzo bottone 'CONFERMA SELEZIONE'
	html_buttonConfirm = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.button_confirm_selection);

	//inizializzo bottone 'APRI MODAL PER CREAZIONE PARTKEY'
	html_buttonShowModal = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.button_open_modal_partkey);

	//box modale
	html_modalBox = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.modal_box);

	//campo input in finestra modale
	modalParticellaValue = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.modal_particella_input);

	//inizializzo bottone 'CONFERMA INSERIMENTO PARTICELLA MANUALMENTE'
	html_buttonConfirmParticellaNuova = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.modal_particella_button_confirm);

	//inizializzo bottone reset
	html_buttonReset = wrappers.object.getObjectFromDOMbySelector(config_ol.html_page_elments.button_reset_selection);

	//inizializzo oggetto contenente tutti i layers usati nel programma
	layersProgetto = createMapLayers();


	//INIZIALIZZAZIONE MAPPA
	//@todo verificare se serve overlay per eventuali popup informativi
	//mapParams['overlays'] = overlays;

	var mapParams = {
		//commentare questo control custom se crea problemi il bottone massimizza la mappa
		controls: ol.control.defaults().extend([
			new ol.control.FullScreen({
				source: 'fullscreen'
			})
		]),

		layers: new ol.layer.Group({
			title: 'Livelli di Base',
			layers: [
				layersProgetto.tms,
				layersProgetto.layer_carto_base,
				layersProgetto.layer_perimetro_comunale
			]
		}),
		target: config_ol.html_page_elments.map_div,
		view: new ol.View({
			projection: config_ol.projection,
			center: config_ol.territory_center_point,
			zoom: config_ol.initial_zoom,
			minZoom: config_ol.min_zoom,
			maxZoom: config_ol.max_zoom,
			extent: config_ol.tms_extent
		})
	};

	map = new ol.Map(mapParams);

	//aggiungo i layer del catasto e il layer vettoriale
	map.addLayer(layersProgetto.layer_catasto_fogli);
	map.addLayer(layersProgetto.layer_catasto_particelle);
	map.addLayer(layersProgetto.layer_vett_foglio);
	map.addLayer(layersProgetto.layer_vett_particelle);

	//attivo gestione evento click su mappa
	map.on('click', onMapClick);


	//INIZIALIZZO TENDINE DI RICERCA FOGLI CATASTALI
	//IL METODO DI ATTERRAGGIO SI OCCUPA DI INIZIALIZZARE 
	//LA MAPPA CON EVENTUALI DATI DA PRE-CARICARE
	catastoUtils.requestAllFogli(initializeFormAndMap);
}