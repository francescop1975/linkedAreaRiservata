using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SIGePro.Manager.Tests.Fakes.DataAccess
{
    public class FakeCommandParameterFactory : ICommandParameterFactory
    {
        public readonly Dictionary<string, object> Parameters = new Dictionary<string, object>();

        public ICommandParameterFactory Add(string parameterName, object value)
        {
            this.AddParameter(parameterName, value);

            return this;
        }

        public void AddParameter(string name, object value)
        {
            this.Parameters.Add(name, value);
        }
    }

    public class FakeDatabase : DataBase
    {
        public class QueryEseguita
        {
            public readonly string Sql;
            public readonly Dictionary<string, object> Parametri;

            public QueryEseguita(string sql, Dictionary<string, object> parametri)
            {
                this.Sql = sql;
                this.Parametri = parametri;
            }
        }

        public QueryEseguita UltimaQueryEseguita => this.QueryEseguite.Last();

        public object ExecuteScalarResult { set; private get; } = null;
        public FakeDatabaseDataSource DataSource = new FakeDatabaseDataSource();
        public List<QueryEseguita> QueryEseguite { get; } = new List<QueryEseguita>();

        public FakeDatabase()
            : base(String.Empty, ProviderType.OracleClient)
        {
        }

        public FakeDatabase(ProviderType providerType)
            : base(String.Empty, providerType)
        {
        }

        public override void ExecuteNonQuery(string sql, Action<ICommandParameterFactory> callback)
        {
            var parFactory = new FakeCommandParameterFactory();

            callback(parFactory);

            this.QueryEseguite.Add(new QueryEseguita(sql, parFactory.Parameters));
        }

        public override T ExecuteScalar<T>(string sql, T defaultValue, Action<ICommandParameterFactory> callback)
        {
            var parFactory = new FakeCommandParameterFactory();

            callback(parFactory);

            this.QueryEseguite.Add(new QueryEseguita(sql, parFactory.Parameters));

            if (this.ExecuteScalarResult == null)
            {
                return defaultValue;
            }

            return (T)this.ExecuteScalarResult;
        }

        public override IEnumerable<T> ExecuteReader<T>(string sql, Action<ICommandParameterFactory> callback, Func<IDataReader, T> func)
        {
            var parFactory = new FakeCommandParameterFactory();

            callback(parFactory);

            this.QueryEseguite.Add(new QueryEseguita(sql, parFactory.Parameters));

            var dataReader = new FakeDataReader();

            dataReader.CurrentDataSource = this.DataSource;

            var rVal = new List<T>();

            while (dataReader.Read())
            {
                rVal.Add(func(dataReader));
            }

            return rVal;
        }

        #region Gestione delle transazioni
        private bool _isInTransaction = false;

        public override bool IsInTransaction => this._isInTransaction;

        public override void BeginTransaction()
        {
            this._isInTransaction = true;
        }

        public override void CommitTransaction()
        {
            this._isInTransaction = false;
        }

        public override void RollbackTransaction()
        {
            this._isInTransaction = false;
        }
        #endregion
    }
}
