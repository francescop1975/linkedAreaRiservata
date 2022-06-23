using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione
{
    public abstract class AProtocollazioneResolver : IProtocollazioneResolver
    {
        public ParametriRegoleInfo Parametri { get; }

        public AProtocollazioneResolver(ParametriRegoleInfo parametri)
        {

            this.Parametri = parametri;
        }
        public RepositoryId RepositoryId => this.Parametri.RepositoryID;
        public PrincipalId PrincipalId => this.Parametri.PrincipalID;
        public IdAoo IdAoo => this.Parametri.IdAoo;
        public IdStruttura IdStruttura => this.Parametri.IdStruttura;
        public IdNodo IdNodo => this.Parametri.IdNodo;
        public string OfficialBookPortUrl => this.Parametri.OfficialBookPortUrl;
        public string ObjectPortUrl => this.Parametri.ObjectPortUrl;
        public abstract enumTipoRegistrazioneDaCreare TipologiaCreazione { get; }
        public abstract RegistrazioneRequest InfoRichiestaCreazione { get; }
    }
}
