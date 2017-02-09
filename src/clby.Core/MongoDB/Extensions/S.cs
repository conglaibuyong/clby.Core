using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace clby.Core.MongoDB.Extensions
{
    public class S<T>
    {
        public SortDefinition<T> Sort { get; private set; }

        #region 构造
        public S(SortDefinition<T> sort)
        {
            this.Sort = sort;
        }
        public S(BsonDocument document)
        {
            this.Sort = new BsonDocumentSortDefinition<T>(document);
        }
        public S(string json)
        {
            this.Sort = new JsonSortDefinition<T>(json);
        }
        public S(object obj)
        {
            this.Sort = new ObjectSortDefinition<T>(obj);
        }
        #endregion

        public static S<T> Ascending(string field)
        {
            return new S<T>(Builders<T>.Sort.Ascending(field));
        }
        public static S<T> Ascending(Expression<Func<T, object>> field)
        {
            return new S<T>(Builders<T>.Sort.Ascending(field));
        }
        public static S<T> Descending(string field)
        {
            return new S<T>(Builders<T>.Sort.Descending(field));
        }
        public static S<T> Descending(Expression<Func<T, object>> field)
        {
            return new S<T>(Builders<T>.Sort.Descending(field));
        }
        public static S<T> MetaTextScore(string field)
        {
            return new S<T>(Builders<T>.Sort.MetaTextScore(field));
        }

        #region &
        public static S<T> operator &(S<T> lhs, S<T> rhs)
        {
            Ensure.IsNotNull<S<T>>(lhs, "lhs");
            Ensure.IsNotNull<S<T>>(rhs, "rhs");
            return new S<T>(Builders<T>.Sort.Combine(lhs.Sort, rhs.Sort));
        }
        #endregion

        public static implicit operator S<T>(SortBuilder<T> sort)
        {
            return new S<T>(sort.Sort);
        }

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Sort.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }

    }

    public static class Sort<T>
    {
        public static SortBuilder<T> Ascending(string field)
        {
            return new SortBuilder<T>(Builders<T>.Sort.Ascending(field));
        }
        public static SortBuilder<T> Ascending(Expression<Func<T, object>> field)
        {
            return new SortBuilder<T>(Builders<T>.Sort.Ascending(field));
        }
        public static SortBuilder<T> Descending(string field)
        {
            return new SortBuilder<T>(Builders<T>.Sort.Descending(field));
        }
        public static SortBuilder<T> Descending(Expression<Func<T, object>> field)
        {
            return new SortBuilder<T>(Builders<T>.Sort.Descending(field));
        }
        public static SortBuilder<T> MetaTextScore(string field)
        {
            return new SortBuilder<T>(Builders<T>.Sort.MetaTextScore(field));
        }
    }
    public class SortBuilder<T> : IMongoSort
    {
        public SortDefinition<T> Sort { get; private set; }

        #region 构造
        public SortBuilder(SortDefinition<T> sort)
        {
            this.Sort = sort;
        }
        public SortBuilder(BsonDocument document)
        {
            this.Sort = new BsonDocumentSortDefinition<T>(document);
        }
        public SortBuilder(string json)
        {
            this.Sort = new JsonSortDefinition<T>(json);
        }
        public SortBuilder(object obj)
        {
            this.Sort = new ObjectSortDefinition<T>(obj);
        }
        #endregion

        public SortBuilder<T> Ascending(string field)
        {
            Sort = Builders<T>.Sort.Combine(Sort, Builders<T>.Sort.Ascending(field));
            return this;
        }
        public SortBuilder<T> Ascending(Expression<Func<T, object>> field)
        {
            Sort = Builders<T>.Sort.Combine(Sort, Builders<T>.Sort.Ascending(field));
            return this;
        }
        public SortBuilder<T> Descending(string field)
        {
            Sort = Builders<T>.Sort.Combine(Sort, Builders<T>.Sort.Descending(field));
            return this;
        }
        public SortBuilder<T> Descending(Expression<Func<T, object>> field)
        {
            Sort = Builders<T>.Sort.Combine(Sort, Builders<T>.Sort.Descending(field));
            return this;
        }
        public SortBuilder<T> MetaTextScore(string field)
        {
            Sort = Builders<T>.Sort.Combine(Sort, Builders<T>.Sort.MetaTextScore(field));
            return this;
        }

        #region &
        public static SortBuilder<T> operator &(SortBuilder<T> lhs, SortBuilder<T> rhs)
        {
            Ensure.IsNotNull<SortBuilder<T>>(lhs, "lhs");
            Ensure.IsNotNull<SortBuilder<T>>(rhs, "rhs");
            return new SortBuilder<T>(Builders<T>.Sort.Combine(lhs.Sort, rhs.Sort));
        }
        #endregion

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Sort.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }
    }
    public interface IMongoSort
    { }
}
