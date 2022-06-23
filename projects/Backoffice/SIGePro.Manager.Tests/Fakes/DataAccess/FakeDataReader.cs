using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Fakes.DataAccess
{

    public class FakeDatabaseDataSource: List<Dictionary<string, object>>
    {
        private int _currentIndex = -1;

        public Dictionary<string, object> CurrentElement => this[this._currentIndex];

        internal bool AtEndOfRecordset()
        {
            return this._currentIndex >= this.Count;
        }

        internal bool MoveToNextRecord()
        {
            this._currentIndex++;

            return this._currentIndex < this.Count;
        }

    }

    public class FakeDataReader : IDataReader
    {
        public FakeDatabaseDataSource CurrentDataSource = new FakeDatabaseDataSource();

        public object this[int i] => this.CurrentDataSource.CurrentElement[this.CurrentDataSource.CurrentElement.Keys.ElementAt(i)];

        public object this[string name] => this.CurrentDataSource.CurrentElement[name];

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public int FieldCount => this.CurrentDataSource.CurrentElement.Keys.Count;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            return (bool)this[i];
        }

        public byte GetByte(int i)
        {
            return (byte)this[i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
           return this[i].GetType().ToString();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)this[i];
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)this[i];
        }

        public double GetDouble(int i)
        {
            return (double)this[i];
        }

        public Type GetFieldType(int i)
        {
            return this[i].GetType();
        }

        public float GetFloat(int i)
        {
            return (float)this[i];
        }

        public Guid GetGuid(int i)
        {
            return (Guid)this[i];
        }

        public short GetInt16(int i)
        {
            return (Int16)this[i];
        }

        public int GetInt32(int i)
        {
            return (Int32)this[i];
        }

        public long GetInt64(int i)
        {
            return (Int64)this[i];
        }

        public string GetName(int i)
        {
            return this.CurrentDataSource.CurrentElement.Keys.ElementAt(i);
        }

        public int GetOrdinal(string name)
        {
            for (int i = 0; i < this.CurrentDataSource.CurrentElement.Keys.Count; i++)
            {
                if (this.CurrentDataSource.CurrentElement.Keys.ElementAt(i) == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            return (string)this[i];
        }

        public object GetValue(int i)
        {
            return this[i];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return this[i] == null || this[i] == DBNull.Value;
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            if (this.CurrentDataSource.AtEndOfRecordset())
            {
                return false;
            }

            return this.CurrentDataSource.MoveToNextRecord();
           
        }
    }

}
