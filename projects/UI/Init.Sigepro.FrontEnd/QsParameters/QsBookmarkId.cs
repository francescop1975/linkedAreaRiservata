﻿using System.Collections.Specialized;

namespace Init.Sigepro.FrontEnd.QsParameters
{
    public class QsBookmarkId : BaseQuerystringParameter<string>
    {
        public const string QuerystringParameterName = "bookmark";

        public QsBookmarkId(string value) :
            base(value.ToString())
        {
        }

        public QsBookmarkId(NameValueCollection qs) :
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