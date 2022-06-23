using System;
using System.Runtime.Serialization;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    [Serializable]
    internal class InvalidParameterNameException : Exception
    {
        public InvalidParameterNameException()
        {
        }

        public InvalidParameterNameException(string message) : base(message)
        {
        }

        public InvalidParameterNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidParameterNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}