﻿using System.Collections.Specialized;

namespace Init.Sigepro.FrontEnd.QsParameters.Fvg
{
    public class QsFvgIdScheda : BaseQuerystringParameter<int>
    {
        public const string QuerystringParameterName = "scheda";

        public QsFvgIdScheda(int value) :
            base(value.ToString())
        {
        }

        public QsFvgIdScheda(NameValueCollection qs) :
            base(qs)
        {
        }

        protected override bool Validate(string value)
        {
            // Inserire la logica di validazione. Es:
            // return value.Between(37, 37);

            return base.Validate(value);
        }

        // Restituisce il nome del parametro in querystring
        public override string ParameterName
        {
            get
            {
                return QuerystringParameterName;
            }
        }
    }

}