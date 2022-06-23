using System;

namespace Init.SIGePro.DatiDinamici.Scripts
{

    /// <summary>
    /// Rappresenta un errore generato durante la compilazione di uno script
    /// </summary>
    public class EvaluationException : Exception
    {
        public string Code { get; protected set; }

        public EvaluationException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }

}
