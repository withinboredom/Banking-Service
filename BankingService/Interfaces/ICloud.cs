using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace Interfaces
{
    public interface ICloud
    {
        IEnumerable<T> GetObject<T>(CloudTable table, string partition) where T : class, ITableEntity, new();
        T GetObject<T>(CloudTable table, string partition, string row) where T : class, ITableEntity;
        T GetObject<T>(CloudTable table, Guid partition, Guid row) where T : class, ITableEntity;
        CloudTable GetTable(string table, string connectionString);
        T SetObject<T>(CloudTable table, T obj) where T : class, ITableEntity;
        string ToKey(string id);
        string ToKey(Guid id);
    }
}