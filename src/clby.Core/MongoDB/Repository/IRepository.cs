using clby.Core.MongoDB.Extensions;
using clby.Core.MongoDB.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clby.Core.MongoDB.Repository
{
    public interface IRepository<T> where T : IBaseObject, new()
    {
        #region base
        #region Async
        Task InsertOneAsync(T entity);
        Task InsertManyAsync(params T[] entitys);
        Task ReplaceOneAsync(Q<T> query, T entity, bool isUpsert = false);
        Task DeleteOneAsync(Q<T> query);
        Task DeleteManyAsync(Q<T> query);
        Task UpdateOneAsync(Q<T> query, M<T> update, bool isUpsert = false);
        Task UpdateManyAsync(Q<T> query, M<T> update, bool isUpsert = false);
        Task<T> FindOneAndUpdateAsync(Q<T> query, M<T> update, bool retBefore = false, bool isUpsert = false);
        Task<T> FindOneAndReplaceAsync(Q<T> query, T entity, bool retBefore = false, bool isUpsert = false);
        Task<T> FindOneAndDeleteAsync(Q<T> query);
        Task<List<T>> FindAsync(Q<T> query, S<T> sort = null, P<T> projection = null);
        Task<List<T>> FindAsync(Q<T> query, int skip, int limit, S<T> sort = null, P<T> projection = null);
        Task<T> FindOneAsync(Q<T> query, P<T> projection = null);
        Task<T> FindOneByIdAsync(ObjectId _id, P<T> projection = null);
        Task<long> CountAsync(Q<T> query);
        Task BulkWriteAsync(IEnumerable<W<T>> requests, bool isOrder = false);
        Task<List<K>> AggregateAsync<K>(List<BsonDocument> pipeline, bool AllowDiskUse = false);
        #endregion

        #region Sync
        void InsertOne(T entity);
        void InsertMany(params T[] entitys);
        R ReplaceOne(Q<T> query, T entity, bool isUpsert = false);
        R DeleteOne(Q<T> query);
        R DeleteMany(Q<T> query);
        R UpdateOne(Q<T> query, M<T> update, bool isUpsert = false);
        R UpdateMany(Q<T> query, M<T> update, bool isUpsert = false);
        T FindOneAndUpdate(Q<T> query, M<T> update, bool retBefore = false, bool isUpsert = false);
        T FindOneAndReplace(Q<T> query, T entity, bool retBefore = false, bool isUpsert = false);
        T FindOneAndDelete(Q<T> query);
        List<T> Find(Q<T> query, S<T> sort = null, P<T> projection = null);
        List<T> Find(Q<T> query, int skip, int limit, S<T> sort = null, P<T> projection = null);
        T FindOne(Q<T> query, P<T> projection = null);
        T FindOneById(ObjectId _id, P<T> projection = null);
        long Count(Q<T> query);
        R BulkWrite(IEnumerable<W<T>> requests, bool isOrder = false);
        List<K> Aggregate<K>(List<BsonDocument> pipeline, bool AllowDiskUse = false);
        #endregion
        #endregion
        bool Remove(Q<T> query);
        bool RemoveAll(Q<T> query);
    }
}
