using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    public class CodiceFiscalePersonaAventeTitoloCondition: QueryConditionBase
    {
        private readonly FiltroPersonaAventeTitoloDiVisura _personaAventeTitolo;

        public CodiceFiscalePersonaAventeTitoloCondition(DataBase database, FiltroPersonaAventeTitoloDiVisura personaAventeTitolo)
            :base(database, "FiltriRichiedente")
        {
            if (String.IsNullOrEmpty(personaAventeTitolo?.CodiceFiscale))
            {
                return;
            }

            var codiceFiscale = personaAventeTitolo.CodiceFiscale;
            //var cercaNeiSoggettiCollegati = personaAventeTitolo.CercaNeiSoggettiCollegati;

            var sb = new StringBuilder();
            sb.Append("(");
            sb.Append($"( ANAGRAFE.CODICEFISCALE = {QueryParameterName("cf1")} OR ANAGRAFE.partitaiva = {QueryParameterName("cf2")} )");
            sb.Append($" OR (TECNICO.codicefiscale = {QueryParameterName("cf3")} OR TECNICO.partitaiva = {QueryParameterName("cf4")})");
            sb.Append($" OR (AZIENDA.codicefiscale = {QueryParameterName("cf5")} OR AZIENDA.partitaiva = {QueryParameterName("cf6")})");

            AddParameter("cf1", codiceFiscale);
            AddParameter("cf2", codiceFiscale);
            AddParameter("cf3", codiceFiscale);
            AddParameter("cf4", codiceFiscale);
            AddParameter("cf5", codiceFiscale);
            AddParameter("cf6", codiceFiscale);

            //if (cercaNeiSoggettiCollegati)
            //{
                sb.Append($@"OR EXISTS(
			        SELECT 
				        istanzerichiedenti.codiceistanza 
			        FROM 
				        istanzerichiedenti
					        INNER JOIN anagrafe soggetticollegati ON
						        soggetticollegati.idcomune = istanzerichiedenti.idcomune AND
						        soggetticollegati.codiceanagrafe = istanzerichiedenti.codicerichiedente 
                            inner join tipisoggetto tsvisura on
                                tsvisura.idcomune = istanzerichiedenti.idcomune and 
                                tsvisura.codicetiposoggetto = istanzerichiedenti.codicetiposoggetto

			        WHERE 
				        istanzerichiedenti.idcomune = istanze.idcomune AND
				        istanzerichiedenti.codiceistanza = istanze.codiceistanza AND 
				        ( soggetticollegati.codicefiscale = {QueryParameterName("cf7")} OR soggetticollegati.partitaiva = {QueryParameterName("cf8")} ) and
                        {database.Specifics.NvlFunction("tsvisura.flag_livelli_visura_pratica", 0)} > 0
		        )");

                AddParameter("cf7", codiceFiscale);
                AddParameter("cf8", codiceFiscale);
            // }

            sb.Append(")");

            this.Query = sb.ToString();
            _personaAventeTitolo = personaAventeTitolo;
        }
    }
}
