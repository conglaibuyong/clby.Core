using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace clby.Core.Models
{
    public class BaseObject : BaseObject<ObjectId>
    {
        public BaseObject()
            : base()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
    public class BaseObject<TId> : IBaseObject
    {
        public TId _id { get; set; }
        public bool _IsDelete { get; set; }
        public DateTime _CreateTime { get; set; } = DateTime.Now;
        public DateTime _LastModyTime { get; set; } = DateTime.Now;
    }
    public interface IBaseObject
    {
        bool _IsDelete { get; set; }
        DateTime _CreateTime { get; set; }
        DateTime _LastModyTime { get; set; }
    }

    public class Reference<T> : BaseReference<T, ObjectId, string> where T : IBaseObject, new()
    {
        public Reference(ObjectId _id, string data = null)
            : base(_id, data) { }
        public Reference(ObjectId _id, Type type, string data = null)
            : base(_id, type, data) { }
    }
    public class BaseReference<T, TId, K> where T : IBaseObject, new()
    {
        public BaseReference(TId _id, K data)
        {
            Ref = new DBRef<TId>(ReflectionHelper.GetScrubbedGenericName(typeof(T)), _id);
            Data = data;
        }
        public BaseReference(TId _id, Type type, K data)
        {
            Ref = new DBRef<TId>(ReflectionHelper.GetScrubbedGenericName(type), _id);
            Data = data;
        }
        public DBRef<TId> Ref { get; set; }
        public K Data { get; set; }
        [BsonIgnore]
        public T RealValue { get; set; }
    }
    public class DBRef<TId>
    {
        public DBRef(string collectionName, TId id)
        {
            this.CollectionName = collectionName;
            this.Id = id;
        }
        public string CollectionName { get; set; }
        public TId Id { get; set; }
    }

}
