using System;
using System.Runtime.Serialization;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione
{
    [Serializable]
    public class ComuneNonAttivoException : Exception
    {
        public ComuneNonAttivoException()
        {
        }

        public ComuneNonAttivoException(string message) : base(message)
        {
        }

        public ComuneNonAttivoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComuneNonAttivoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}