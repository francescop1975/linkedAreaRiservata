using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalLib2.Data
{
    public static class DatabaseExtensions
    {
        public static string QueryParameter(this DataBase dataBase, string parameterName)
        {
            return dataBase.Specifics.QueryParameterName(parameterName);
        }

        public static ICommandParameterFactory Add(this ICommandParameterFactory mp, string parameterName, object value)
        {
            mp.AddParameter(parameterName, value);

            return mp;
        }
    }
}
