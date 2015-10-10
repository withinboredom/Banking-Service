using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.WindowsAzure.Storage.Table;

namespace Utility.Test
{
    class TestableCloud : ICloud
    {
        private Dictionary<string, Dictionary<string, object>> db = new Dictionary<string, Dictionary<string, object>>(); 

        public IEnumerable<T> GetObject<T>(CloudTable table, string partition) where T : class, ITableEntity, new()
        {
            throw new NotImplementedException();
        }

        public T GetObject<T>(CloudTable table, string partition, string row) where T : class, ITableEntity
        {
            throw new NotImplementedException();
        }

        public T GetObject<T>(CloudTable table, Guid partition, Guid row) where T : class, ITableEntity
        {
            throw new NotImplementedException();
        }

        public CloudTable GetTable(string table, string connectionString)
        {
            throw new NotImplementedException();
        }

        public T SetObject<T>(CloudTable table, T obj) where T : class, ITableEntity
        {
            throw new NotImplementedException();
        }

        public string ToKey(string id)
        {
            throw new NotImplementedException();
        }

        public string ToKey(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
