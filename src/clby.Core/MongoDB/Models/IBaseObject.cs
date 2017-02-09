using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clby.Core.MongoDB.Models
{
    public class BaseObject : BaseObject<ObjectId>
    {
        public BaseObject()
            : base()
        {
            _id = ObjectId.GenerateNewId();
            _Transactions = new List<ObjectId>();
        }

    }
    public class BaseObject<TId> : IBaseObject
    {
        public BaseObject()
        {
            _CreateTime = DateTime.Now;
            _LastModyTime = DateTime.Now;
            _Version = BaseObjectEx.NewVertion;
        }
        public TId _id { get; set; }
        public bool _IsDelete { get; set; }
        public DateTime _CreateTime { get; set; }
        public DateTime _LastModyTime { get; set; }
        public string _Version { get; set; }
        public List<TId> _Transactions { get; set; }

    }
    public interface IBaseObject
    {
        bool _IsDelete { get; set; }
        DateTime _CreateTime { get; set; }
        DateTime _LastModyTime { get; set; }
        string _Version { get; set; }
    }

    public class Reference<T> : BaseReference<T, ObjectId, string> where T : IBaseObject, new()
    {
        public Reference() { }
        public Reference(ObjectId _id, string data = null, string dbName = null)
            : base(_id, data, dbName) { }
        public Reference(ObjectId _id, Type type, string data = null, string dbName = null)
            : base(_id, type, data, dbName) { }
    }
    public class Reference<T, TId> : BaseReference<T, TId, string> where T : IBaseObject, new()
    {
        public Reference() { }
        public Reference(TId _id, string data = null, string dbName = null)
            : base(_id, data, dbName) { }
        public Reference(TId _id, Type type, string data = null, string dbName = null)
            : base(_id, type, data, dbName) { }
    }
    public class BaseReference<T, TId, K> where T : IBaseObject, new()
    {
        public BaseReference() { }
        public BaseReference(TId _id, K data, string dbName)
        {
            Ref = new DBRef<TId>(ReflectionHelper.GetScrubbedGenericName(typeof(T)), _id, dbName);
            Data = data;
        }
        public BaseReference(TId _id, Type type, K data, string dbName)
        {
            Ref = new DBRef<TId>(ReflectionHelper.GetScrubbedGenericName(type), _id, dbName);
            Data = data;
        }
        public DBRef<TId> Ref { get; set; }
        public K Data { get; set; }

        [BsonIgnore]
        public T RealValue { get; set; }
    }
    public class Reference : BaseReference<string>
    {
        public Reference() { }
        public Reference(string collectionName, ObjectId _id, string data = null, string dbName = null)
            : base(collectionName, _id, data, dbName) { }
    }
    public class BaseReference<K>
    {
        public BaseReference() { }
        public BaseReference(string collectionName, ObjectId _id, K data, string dbName)
        {
            Ref = new DBRef<ObjectId>(collectionName, _id, dbName);
            Data = data;
        }
        public DBRef<ObjectId> Ref { get; set; }
        public K Data { get; set; }
        [BsonIgnore]
        public BsonDocument RealValue { get; set; }
    }
    public class DBRef<TId>
    {
        public DBRef() { }
        public DBRef(string collectionName, TId id, string dbName)
        {
            this.CollectionName = collectionName;
            this.Id = id;
            this.DBName = dbName;
        }
        public string CollectionName { get; set; }
        public string DBName { get; set; }
        public TId Id { get; set; }
    }

    public static class BaseObjectEx
    {
        public static string NewVertion
        {
            get
            {
                return string.Format("{0}:{1}", Guid.NewGuid().ToString(), DateTime.Now.ToString("yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            }
        }
    }
    public static class ReferenceEx
    {
        public static BsonDocument Fetch<K>(this BaseReference<K> Value)
        {
            Value.RealValue = MongoHelper
                .GetCollection<BsonDocument>(Value.Ref.CollectionName, Value.Ref.DBName)
                .Find<BsonDocument>(Builders<BsonDocument>.Filter.Eq("_id", Value.Ref.Id), null)
                .SingleAsync()
                .Result;
            return Value.RealValue;
        }
        public static Task<BsonDocument> FetchAsync<K>(this BaseReference<K> Value)
        {
            return MongoHelper
                .GetCollection<BsonDocument>(Value.Ref.CollectionName, Value.Ref.DBName)
                .Find<BsonDocument>(Builders<BsonDocument>.Filter.Eq("_id", Value.Ref.Id), null)
                .SingleAsync()
                .ContinueWith(t => Value.RealValue = t.Result);
        }

        public static T Fetch<T, TId, K>(this BaseReference<T, TId, K> Value) where T : class, IBaseObject, new()
        {
            Value.RealValue = MongoHelper
                .GetCollection<T>(Value.Ref.CollectionName, Value.Ref.DBName)
                .Find<T>(Builders<T>.Filter.Eq("_id", Value.Ref.Id), null)
                .SingleAsync()
                .Result;
            return Value.RealValue;
        }
        public static Task<T> FetchAsync<T, TId, K>(this BaseReference<T, TId, K> Value) where T : class, IBaseObject, new()
        {
            return MongoHelper
                .GetCollection<T>(Value.Ref.CollectionName, Value.Ref.DBName)
                .Find<T>(Builders<T>.Filter.Eq("_id", Value.Ref.Id), null)
                .SingleAsync()
                .ContinueWith(t => Value.RealValue = t.Result);
        }

        //public static List<T> Fetch<T, TId, K>(this IEnumerable<BaseReference<T, TId, K>> values) where T : class,IBaseObject, new()
        //{
        //    if (!values.Any()) throw new ArgumentNullException("values");
        //    var Values = values.ToList();
        //    var items = MongoHelper
        //        .GetCollection<T>(Values[0].Ref.CollectionName, Values[0].Ref.DBName)
        //        .Find<T>(Builders<T>.Filter.In("_id", Values.Select(t => t.Ref.Id)))
        //        .ToListAsync()
        //        .Result;
        //    foreach (var item in items)
        //    {
        //        //HACK: TODO: 如果数据比较大，性能会比较慢
        //        var id = (TId)(item.GetType().GetProperty("_id").GetValue(item));
        //        Values.FindAll(t => t.Ref.Id.Equals(id)).ForEach(t => t.RealValue = item);
        //    }
        //    return Values.Select(t => t.RealValue).ToList();
        //}
        //public static Task<List<T>> FetchAsync<T, TId, K>(this IEnumerable<BaseReference<T, TId, K>> values) where T : class,IBaseObject, new()
        //{
        //    if (!values.Any()) throw new ArgumentNullException("values");
        //    var Values = values.ToList();
        //    return MongoHelper
        //        .GetCollection<T>(Values[0].Ref.CollectionName, Values[0].Ref.DBName)
        //        .Find<T>(Builders<T>.Filter.In("_id", Values.Select(t => t.Ref.Id)))
        //        .ToListAsync().ContinueWith(ts =>
        //        {
        //            var items = ts.;
        //            foreach (var item in items)
        //            {
        //                //HACK: TODO: 如果数据比较大，性能会比较慢
        //                var id = (TId)(item.GetType().GetProperty("_id").GetValue(item));
        //                Values.FindAll(t => t.Ref.Id.Equals(id)).ForEach(t => t.RealValue = item);
        //            }
        //            return Values.Select(t => t.RealValue).ToList();
        //        });
        //}

        public static T Fetch<T, TId, K>(this BaseReference<T, TId, K> Value, IDictionary<TId, T> mapSet) where T : IBaseObject, new()
        {
            T reslut = default(T);
            mapSet.TryGetValue(Value.Ref.Id, out reslut);
            Value.RealValue = reslut;
            return Value.RealValue;
        }
        public static List<T> Fetch<T, TId, K>(this IEnumerable<BaseReference<T, TId, K>> values, IDictionary<TId, T> mapSet) where T : IBaseObject, new()
        {
            var Values = values.ToList();
            Values.ForEach(t => t.RealValue = mapSet[t.Ref.Id]);
            return Values.Select(t => t.RealValue).ToList();
        }

    }

}
