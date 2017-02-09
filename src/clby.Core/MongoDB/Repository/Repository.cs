using clby.Core.Misc;
using clby.Core.MongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clby.Core.MongoDB.Extensions;
using MongoDB.Bson;

namespace clby.Core.MongoDB.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : IBaseObject, new()
    {
        public IMongoCollection<T> Collection { get; private set; }
        public Repository()
        {
            Collection = MongoHelper.GetCollection<T>();
        }
        public Repository(string dbName = null)
        {
            Collection = MongoHelper.GetCollection<T>(null, dbName);
        }

        #region base
        #region Async
        public Task InsertOneAsync(T entity)
        {
            return Collection.InsertOneAsync(entity);
        }
        public Task InsertManyAsync(params T[] entitys)
        {
            return Collection.InsertManyAsync(entitys);
        }
        public Task ReplaceOneAsync(Q<T> query, T entity, bool isUpsert = false)
        {
            entity._LastModyTime = DateTime.Now;
            return Collection.ReplaceOneAsync(query.Filter, entity, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public Task DeleteOneAsync(Q<T> query)
        {
            return Collection.DeleteOneAsync(query.Filter);
        }
        public Task DeleteManyAsync(Q<T> query)
        {
            return Collection.DeleteManyAsync(query.Filter);
        }
        public Task UpdateOneAsync(Q<T> query, M<T> update, bool isUpsert = false)
        {
            var _update = update & M<T>.Set(t => t._LastModyTime, DateTime.Now);
            return Collection.UpdateOneAsync(query.Filter, _update.Update, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public Task UpdateManyAsync(Q<T> query, M<T> update, bool isUpsert = false)
        {
            var _update = update & M<T>.Set(t => t._LastModyTime, DateTime.Now);
            return Collection.UpdateManyAsync(query.Filter, _update.Update, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public Task<T> FindOneAndUpdateAsync(Q<T> query, M<T> update, bool retBefore = false, bool isUpsert = false)
        {
            FindOneAndUpdateOptions<T, T> options = new FindOneAndUpdateOptions<T, T>();
            options.IsUpsert = isUpsert;
            options.ReturnDocument = retBefore ? ReturnDocument.Before : ReturnDocument.After;
            var _update = update & M<T>.Set(t => t._LastModyTime, DateTime.Now);
            return Collection.FindOneAndUpdateAsync(query.Filter, _update.Update, options);
        }
        public Task<T> FindOneAndReplaceAsync(Q<T> query, T entity, bool retBefore = false, bool isUpsert = false)
        {
            FindOneAndReplaceOptions<T, T> options = new FindOneAndReplaceOptions<T, T>();
            options.IsUpsert = isUpsert;
            options.ReturnDocument = retBefore ? ReturnDocument.Before : ReturnDocument.After;
            entity._LastModyTime = DateTime.Now;
            return Collection.FindOneAndReplaceAsync(query.Filter, entity, options);
        }
        public Task<T> FindOneAndDeleteAsync(Q<T> query)
        {
            return Collection.FindOneAndDeleteAsync(query.Filter);
        }
        public Task<List<T>> FindAsync(Q<T> query, S<T> sort = null, P<T> projection = null)
        {
            var ret = Collection.Find(query.Filter);
            if (sort != null) ret = ret.Sort(sort.Sort);
            if (projection != null) ret = ret.Project<T>(projection.Projection);
            return ret.ToListAsync();
        }
        public Task<List<T>> FindAsync(Q<T> query, int skip, int limit, S<T> sort = null, P<T> projection = null)
        {
            Ensure.IsGreaterThanOrEqualToZero(skip, "skip");
            Ensure.IsGreaterThanOrEqualToZero(limit, "limit");
            var ret = Collection.Find(query.Filter);
            if (sort != null) ret = ret.Sort(sort.Sort);
            if (projection != null) ret = ret.Project<T>(projection.Projection);
            ret = ret.Skip(skip).Limit(limit);
            return ret.ToListAsync();
        }
        public Task<T> FindOneAsync(Q<T> query, P<T> projection = null)
        {
            var ret = Collection.Find<T>(query.Filter).Limit(1);
            if (projection != null)
                ret = ret.Project<T>(projection.Projection);
            return ret.FirstOrDefaultAsync();
        }
        public Task<T> FindOneByIdAsync(ObjectId _id, P<T> projection = null)
        {
            var ret = Collection.Find<T>(Builders<T>.Filter.Eq("_id", _id));
            if (projection != null)
                ret = ret.Project<T>(projection.Projection);
            return ret.SingleOrDefaultAsync();
        }
        public Task<long> CountAsync(Q<T> query)
        {
            return Collection.CountAsync(query.Filter);
        }
        public Task BulkWriteAsync(IEnumerable<W<T>> requests, bool isOrdered = false)
        {
            List<WriteModel<T>> Requests = new List<WriteModel<T>>();
            #region
            requests.ToList().ForEach(t =>
            {
                WriteModel<T> wm = null;
                var ttype = t.GetType();
                if (ttype == typeof(InsertOneW<T>))
                {
                    var tmp = (t as InsertOneW<T>);
                    wm = new InsertOneModel<T>(tmp.Document);
                }
                else if (ttype == typeof(ReplaceOneW<T>))
                {
                    var tmp = (t as ReplaceOneW<T>);
                    tmp.Document._LastModyTime = DateTime.Now;
                    wm = new ReplaceOneModel<T>(tmp.Query.Filter, tmp.Document);
                }
                else if (ttype == typeof(DeleteOneW<T>))
                {
                    var tmp = (t as DeleteOneW<T>);
                    wm = new DeleteOneModel<T>(tmp.Query.Filter);
                }
                else if (ttype == typeof(DeleteManyW<T>))
                {
                    var tmp = (t as DeleteManyW<T>);
                    wm = new DeleteManyModel<T>(tmp.Query.Filter);
                }
                else if (ttype == typeof(UpdateOneW<T>))
                {
                    var tmp = (t as UpdateOneW<T>);
                    var _update = tmp.Update & M<T>.Set(k => k._LastModyTime, DateTime.Now);
                    wm = new UpdateOneModel<T>(tmp.Query.Filter, _update.Update);
                }
                else if (ttype == typeof(UpdateManyW<T>))
                {
                    var tmp = (t as UpdateManyW<T>);
                    var _update = tmp.Update & M<T>.Set(k => k._LastModyTime, DateTime.Now);
                    wm = new UpdateManyModel<T>(tmp.Query.Filter, _update.Update);
                }
                Requests.Add(wm);
            });
            #endregion
            return Collection.BulkWriteAsync(Requests, new BulkWriteOptions() { IsOrdered = isOrdered });
        }
        public Task<List<K>> AggregateAsync<K>(List<BsonDocument> pipeline, bool allowDiskUse = false)
        {
            return Collection
                .AggregateAsync<K>(pipeline, new AggregateOptions() { AllowDiskUse = allowDiskUse })
                .Result
                .ToListAsync();
        }
        #endregion

        #region Sync
        public void InsertOne(T entity)
        {
            Collection.InsertOne(entity);
        }
        public void InsertMany(params T[] entitys)
        {
            Collection.InsertMany(entitys);
        }
        public R ReplaceOne(Q<T> query, T entity, bool isUpsert = false)
        {
            entity._LastModyTime = DateTime.Now;
            entity._Version = BaseObjectEx.NewVertion;
            return Collection.ReplaceOne(query.Filter, entity, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public R DeleteOne(Q<T> query)
        {
            return Collection.DeleteOne(query.Filter);
        }
        public R DeleteMany(Q<T> query)
        {
            return Collection.DeleteMany(query.Filter);
        }
        public R UpdateOne(Q<T> query, M<T> update, bool isUpsert = false)
        {
            var _update = update
                & M<T>.Set(t => t._LastModyTime, DateTime.Now)
                & M<T>.Set(t => t._Version, BaseObjectEx.NewVertion);
            return Collection.UpdateOne(query.Filter, _update.Update, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public R UpdateMany(Q<T> query, M<T> update, bool isUpsert = false)
        {
            var _update = update
                & M<T>.Set(t => t._LastModyTime, DateTime.Now)
                & M<T>.Set(t => t._Version, BaseObjectEx.NewVertion);
            return Collection.UpdateMany(query.Filter, _update.Update, new UpdateOptions() { IsUpsert = isUpsert });
        }
        public T FindOneAndUpdate(Q<T> query, M<T> update, bool retBefore = false, bool isUpsert = false)
        {
            FindOneAndUpdateOptions<T, T> options = new FindOneAndUpdateOptions<T, T>();
            options.IsUpsert = isUpsert;
            options.ReturnDocument = retBefore ? ReturnDocument.Before : ReturnDocument.After;
            var _update = update
                & M<T>.Set(t => t._LastModyTime, DateTime.Now)
                & M<T>.Set(t => t._Version, BaseObjectEx.NewVertion);
            return Collection.FindOneAndUpdate(query.Filter, _update.Update, options);
        }
        public T FindOneAndReplace(Q<T> query, T entity, bool retBefore = false, bool isUpsert = false)
        {
            FindOneAndReplaceOptions<T, T> options = new FindOneAndReplaceOptions<T, T>();
            options.IsUpsert = isUpsert;
            options.ReturnDocument = retBefore ? ReturnDocument.Before : ReturnDocument.After;
            entity._LastModyTime = DateTime.Now;
            entity._Version = BaseObjectEx.NewVertion;
            return Collection.FindOneAndReplace(query.Filter, entity, options);
        }
        public T FindOneAndDelete(Q<T> query)
        {
            return Collection.FindOneAndDelete(query.Filter);
        }
        public List<T> Find(Q<T> query, S<T> sort = null, P<T> projection = null)
        {
            var ret = Collection.Find(query.Filter);
            if (sort != null) ret = ret.Sort(sort.Sort);
            if (projection != null) ret = ret.Project<T>(projection.Projection);
            return ret.ToList();
        }
        public List<T> Find(Q<T> query, int skip, int limit, S<T> sort = null, P<T> projection = null)
        {
            Ensure.IsGreaterThanOrEqualToZero(skip, "skip");
            Ensure.IsGreaterThanOrEqualToZero(limit, "limit");
            var ret = Collection.Find(query.Filter);
            if (sort != null) ret = ret.Sort(sort.Sort);
            if (projection != null) ret = ret.Project<T>(projection.Projection);
            ret = ret.Skip(skip).Limit(limit);
            return ret.ToList();
        }
        public T FindOne(Q<T> query, P<T> projection = null)
        {
            var ret = Collection.Find<T>(query.Filter).Limit(1);
            if (projection != null)
                ret = ret.Project<T>(projection.Projection);
            return ret.FirstOrDefault();
        }
        public T FindOneById(ObjectId _id, P<T> projection = null)
        {
            var ret = Collection.Find<T>(Builders<T>.Filter.Eq("_id", _id));
            if (projection != null)
                ret = ret.Project<T>(projection.Projection);
            return ret.SingleOrDefault();
        }
        public long Count(Q<T> query)
        {
            return Collection.Count(query.Filter);
        }
        public R BulkWrite(IEnumerable<W<T>> requests, bool isOrdered = false)
        {
            List<WriteModel<T>> Requests = new List<WriteModel<T>>();
            #region
            requests.ToList().ForEach(t =>
            {
                WriteModel<T> wm = null;
                var ttype = t.GetType();
                if (ttype == typeof(InsertOneW<T>))
                {
                    var tmp = (t as InsertOneW<T>);
                    wm = new InsertOneModel<T>(tmp.Document);
                }
                else if (ttype == typeof(ReplaceOneW<T>))
                {
                    var tmp = (t as ReplaceOneW<T>);
                    tmp.Document._LastModyTime = DateTime.Now;
                    tmp.Document._Version = BaseObjectEx.NewVertion;
                    wm = new ReplaceOneModel<T>(tmp.Query.Filter, tmp.Document);
                }
                else if (ttype == typeof(DeleteOneW<T>))
                {
                    var tmp = (t as DeleteOneW<T>);
                    wm = new DeleteOneModel<T>(tmp.Query.Filter);
                }
                else if (ttype == typeof(DeleteManyW<T>))
                {
                    var tmp = (t as DeleteManyW<T>);
                    wm = new DeleteManyModel<T>(tmp.Query.Filter);
                }
                else if (ttype == typeof(UpdateOneW<T>))
                {
                    var tmp = (t as UpdateOneW<T>);
                    var _update = tmp.Update
                        & M<T>.Set(k => k._LastModyTime, DateTime.Now)
                        & M<T>.Set(k => k._Version, BaseObjectEx.NewVertion);
                    wm = new UpdateOneModel<T>(tmp.Query.Filter, _update.Update);
                }
                else if (ttype == typeof(UpdateManyW<T>))
                {
                    var tmp = (t as UpdateManyW<T>);
                    var _update = tmp.Update
                        & M<T>.Set(k => k._LastModyTime, DateTime.Now)
                        & M<T>.Set(k => k._Version, BaseObjectEx.NewVertion);
                    wm = new UpdateManyModel<T>(tmp.Query.Filter, _update.Update);
                }
                Requests.Add(wm);
            });
            #endregion
            return Collection.BulkWrite(Requests, new BulkWriteOptions() { IsOrdered = isOrdered });
        }
        public List<K> Aggregate<K>(List<BsonDocument> pipeline, bool allowDiskUse = false)
        {
            return Collection
                .Aggregate<K>(pipeline, new AggregateOptions() { AllowDiskUse = allowDiskUse }).ToList();
        }
        #endregion
        #endregion

        public bool Remove(Q<T> query)
        {
            R ret = this.UpdateOne(query, M<T>.Set(t => t._IsDelete, true));
            return !ret.IsAcknowledged || ret.DeletedCount > 0;
        }
        public bool RemoveAll(Q<T> query)
        {
            R ret = this.UpdateMany(query, M<T>.Set(t => t._IsDelete, true));
            return !ret.IsAcknowledged || ret.DeletedCount > 0;
        }

    }
}
