﻿using System;
using System.Collections.Specialized;

namespace Init.Sigepro.FrontEnd.QsParameters
{
    public class QsTimestamp : BaseQuerystringParameter<string>
    {
        public const string QuerystringParameterName = "_ts";

        public QsTimestamp() :
            base(DateTime.Now.Millisecond.ToString())
        {

        }

        public QsTimestamp(string value) :
            base(value.ToString())
        {
        }

        public QsTimestamp(NameValueCollection qs) :
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