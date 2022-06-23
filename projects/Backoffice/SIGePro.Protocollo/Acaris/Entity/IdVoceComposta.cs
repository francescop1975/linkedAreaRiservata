using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class IdVoceComposta : IIDAcaris
    {
        private class Constants 
        {
            public static char Separatore = Convert.ToChar(".");
        }

        public string Voce { get; }

        public string IdAcaris { get; }

        public IdVoceComposta(IProtocolloSerializer serializer, ObjectWSClient client, RepositoryId repositoryId, PrincipalId principalId, string voce, int idTitolario)
        {
            var voci = voce.Split(Constants.Separatore).Select(x => Convert.ToInt32(x));
            int? padre = null;
            int? titolario = idTitolario;

            string lIdAcaris = null;

            foreach (var v in voci)
            {
                var idVoce = new IdVoce(serializer, client, repositoryId, principalId, v, titolario, padre);
                lIdAcaris = idVoce.IdAcaris;
                padre = idVoce.DbKey;
                titolario = null;
            }

            this.Voce = voce;
            this.IdAcaris = lIdAcaris;

        }

        public override string ToString()
        {
            return $"IdVoceComposta: [voce={this.Voce} idAcaris={this.IdAcaris}]";
        }
    }
}
