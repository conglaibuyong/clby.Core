using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace clby.Core.MongoDB.Extensions
{
    public class M<T>
    {
        public UpdateDefinition<T> Update { get; private set; }
        #region 构造
        public M(UpdateDefinition<T> update)
        {
            this.Update = update;
        }
        public M(BsonDocument document)
        {
            this.Update = new BsonDocumentUpdateDefinition<T>(document);
        }
        public M(string json)
        {
            this.Update = new JsonUpdateDefinition<T>(json);
        }
        public M(object obj)
        {
            this.Update = new ObjectUpdateDefinition<T>(obj);
        }
        #endregion

        public static M<T> AddToSet<TItem>(string field, TItem value)
        {
            return new M<T>(Builders<T>.Update.AddToSet(field, value));
        }
        public static M<T> AddToSet<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new M<T>(Builders<T>.Update.AddToSet(field, value));
        }
        public static M<T> AddToSetEach<TItem>(string field, IEnumerable<TItem> values)
        {
            return new M<T>(Builders<T>.Update.AddToSetEach(field, values));
        }
        public static M<T> AddToSetEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new M<T>(Builders<T>.Update.AddToSetEach(field, values));
        }

        public static M<T> BitwiseAnd<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseAnd(field, value));
        }
        public static M<T> BitwiseAnd<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseAnd(field, value));
        }
        public static M<T> BitwiseOr<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseOr(field, value));
        }
        public static M<T> BitwiseOr<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseOr(field, value));
        }
        public static M<T> BitwiseXor<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseXor(field, value));
        }
        public static M<T> BitwiseXor<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.BitwiseXor(field, value));
        }

        public static M<T> CurrentDate(string field)
        {
            return new M<T>(Builders<T>.Update.CurrentDate(field));
        }
        public static M<T> CurrentDate(Expression<Func<T, object>> field)
        {
            return new M<T>(Builders<T>.Update.CurrentDate(field));
        }
        public static M<T> CurrentTimestamp(string field)
        {
            return new M<T>(Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
        }
        public static M<T> CurrentTimestamp(Expression<Func<T, object>> field)
        {
            return new M<T>(Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
        }

        public static M<T> Inc<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.Inc<TField>(field, value));
        }
        public static M<T> Inc<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.Inc(field, value));
        }
        public static M<T> Max<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.Max<TField>(field, value));
        }
        public static M<T> Max<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.Max(field, value));
        }
        public static M<T> Min<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.Min<TField>(field, value));
        }
        public static M<T> Min<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.Min(field, value));
        }
        public static M<T> Mul<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.Mul<TField>(field, value));
        }
        public static M<T> Mul<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.Mul(field, value));
        }

        public static M<T> PopFirst(string field)
        {
            return new M<T>(Builders<T>.Update.PopFirst(field));
        }
        public static M<T> PopFirst(Expression<Func<T, object>> field)
        {
            return new M<T>(Builders<T>.Update.PopFirst(field));
        }
        public static M<T> PopLast(string field)
        {
            return new M<T>(Builders<T>.Update.PopLast(field));
        }
        public static M<T> PopLast(Expression<Func<T, object>> field)
        {
            return new M<T>(Builders<T>.Update.PopLast(field));
        }

        public static M<T> Pull<TItem>(string field, TItem value)
        {
            return new M<T>(Builders<T>.Update.Pull(field, value));
        }
        public static M<T> Pull<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new M<T>(Builders<T>.Update.Pull(field, value));
        }
        public static M<T> PullAll<TItem>(string field, IEnumerable<TItem> values)
        {
            return new M<T>(Builders<T>.Update.PullAll(field, values));
        }
        public static M<T> PullAll<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new M<T>(Builders<T>.Update.PullAll(field, values));
        }
        public static M<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Expression<Func<TItem, bool>> filter)
        {
            return new M<T>(Builders<T>.Update.PullFilter(field, filter));
        }
        public static M<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Q<TItem> query)
        {
            return new M<T>(Builders<T>.Update.PullFilter(field, query.Filter));
        }
        public static M<T> PullFilter<TItem>(string field, Q<TItem> query)
        {
            return new M<T>(Builders<T>.Update.PullFilter(field, query.Filter));
        }

        public static M<T> Push<TItem>(string field, TItem value)
        {
            return new M<T>(Builders<T>.Update.Push(field, value));
        }
        public static M<T> Push<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new M<T>(Builders<T>.Update.Push(field, value));
        }
        public static M<T> PushEach<TItem>(string field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            return new M<T>(Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
        }
        public static M<T> PushEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            return new M<T>(Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
        }

        public static M<T> Rename(string field, string newName)
        {
            return new M<T>(Builders<T>.Update.Rename(field, newName));
        }
        public static M<T> Rename(Expression<Func<T, object>> field, string newName)
        {
            return new M<T>(Builders<T>.Update.Rename(field, newName));
        }

        public static M<T> Set<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.Set<TField>(field, value));
        }
        public static M<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.Set(field, value));
        }
        public static M<T> SetOnInsert<TField>(string field, TField value)
        {
            return new M<T>(Builders<T>.Update.SetOnInsert<TField>(field, value));
        }
        public static M<T> SetOnInsert<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new M<T>(Builders<T>.Update.SetOnInsert(field, value));
        }
        public static M<T> Unset(string field)
        {
            return new M<T>(Builders<T>.Update.Unset(field));
        }
        public static M<T> Unset(Expression<Func<T, object>> field)
        {
            return new M<T>(Builders<T>.Update.Unset(field));
        }


        #region &
        public static M<T> operator &(M<T> lhs, M<T> rhs)
        {
            Ensure.IsNotNull<M<T>>(lhs, "lhs");
            Ensure.IsNotNull<M<T>>(rhs, "rhs");
            return new M<T>(Builders<T>.Update.Combine(lhs.Update, rhs.Update));
        }
        #endregion

        public static implicit operator M<T>(UpdateBuilder<T> update)
        {
            return new M<T>(update.Update);
        }

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Update.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }

    }

    public static class Update<T>
    {
        public static UpdateBuilder<T> AddToSet<TItem>(string field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.AddToSet(field, value));
        }
        public static UpdateBuilder<T> AddToSet<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.AddToSet(field, value));
        }
        public static UpdateBuilder<T> AddToSetEach<TItem>(string field, IEnumerable<TItem> values)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.AddToSetEach(field, values));
        }
        public static UpdateBuilder<T> AddToSetEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.AddToSetEach(field, values));
        }

        public static UpdateBuilder<T> BitwiseAnd<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseAnd(field, value));
        }
        public static UpdateBuilder<T> BitwiseAnd<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseAnd(field, value));
        }
        public static UpdateBuilder<T> BitwiseOr<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseOr(field, value));
        }
        public static UpdateBuilder<T> BitwiseOr<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseOr(field, value));
        }
        public static UpdateBuilder<T> BitwiseXor<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseXor(field, value));
        }
        public static UpdateBuilder<T> BitwiseXor<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.BitwiseXor(field, value));
        }

        public static UpdateBuilder<T> CurrentDate(string field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.CurrentDate(field));
        }
        public static UpdateBuilder<T> CurrentDate(Expression<Func<T, object>> field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.CurrentDate(field));
        }
        public static UpdateBuilder<T> CurrentTimestamp(string field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
        }
        public static UpdateBuilder<T> CurrentTimestamp(Expression<Func<T, object>> field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
        }

        public static UpdateBuilder<T> Inc<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Inc<TField>(field, value));
        }
        public static UpdateBuilder<T> Inc<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Inc(field, value));
        }
        public static UpdateBuilder<T> Max<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Max<TField>(field, value));
        }
        public static UpdateBuilder<T> Max<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Max(field, value));
        }
        public static UpdateBuilder<T> Min<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Min<TField>(field, value));
        }
        public static UpdateBuilder<T> Min<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Min(field, value));
        }
        public static UpdateBuilder<T> Mul<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Mul<TField>(field, value));
        }
        public static UpdateBuilder<T> Mul<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Mul(field, value));
        }

        public static UpdateBuilder<T> PopFirst(string field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PopFirst(field));
        }
        public static UpdateBuilder<T> PopFirst(Expression<Func<T, object>> field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PopFirst(field));
        }
        public static UpdateBuilder<T> PopLast(string field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PopLast(field));
        }
        public static UpdateBuilder<T> PopLast(Expression<Func<T, object>> field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PopLast(field));
        }

        public static UpdateBuilder<T> Pull<TItem>(string field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Pull(field, value));
        }
        public static UpdateBuilder<T> Pull<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Pull(field, value));
        }
        public static UpdateBuilder<T> PullAll<TItem>(string field, IEnumerable<TItem> values)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PullAll(field, values));
        }
        public static UpdateBuilder<T> PullAll<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PullAll(field, values));
        }
        public static UpdateBuilder<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Expression<Func<TItem, bool>> filter)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PullFilter(field, filter));
        }
        public static UpdateBuilder<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Q<TItem> query)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PullFilter(field, query.Filter));
        }
        public static UpdateBuilder<T> PullFilter<TItem>(string field, Q<TItem> query)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PullFilter(field, query.Filter));
        }

        public static UpdateBuilder<T> Push<TItem>(string field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Push(field, value));
        }
        public static UpdateBuilder<T> Push<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Push(field, value));
        }
        public static UpdateBuilder<T> PushEach<TItem>(string field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
        }
        public static UpdateBuilder<T> PushEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
        }

        public static UpdateBuilder<T> Rename(string field, string newName)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Rename(field, newName));
        }
        public static UpdateBuilder<T> Rename(Expression<Func<T, object>> field, string newName)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Rename(field, newName));
        }

        public static UpdateBuilder<T> Set<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Set<TField>(field, value));
        }
        public static UpdateBuilder<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Set(field, value));
        }
        public static UpdateBuilder<T> SetOnInsert<TField>(string field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.SetOnInsert<TField>(field, value));
        }
        public static UpdateBuilder<T> SetOnInsert<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.SetOnInsert(field, value));
        }
        public static UpdateBuilder<T> Unset(string field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Unset(field));
        }
        public static UpdateBuilder<T> Unset(Expression<Func<T, object>> field)
        {
            return new UpdateBuilder<T>(Builders<T>.Update.Unset(field));
        }

    }
    public class UpdateBuilder<T> : IMongoUpdate
    {
        public UpdateDefinition<T> Update { get; private set; }
        #region 构造
        public UpdateBuilder(UpdateDefinition<T> update)
        {
            this.Update = update;
        }
        public UpdateBuilder(BsonDocument document)
        {
            this.Update = new BsonDocumentUpdateDefinition<T>(document);
        }
        public UpdateBuilder(string json)
        {
            this.Update = new JsonUpdateDefinition<T>(json);
        }
        public UpdateBuilder(object obj)
        {
            this.Update = new ObjectUpdateDefinition<T>(obj);
        }
        #endregion

        public UpdateBuilder<T> AddToSet<TItem>(string field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.AddToSet(field, value));
            return this;
        }
        public UpdateBuilder<T> AddToSet<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.AddToSet(field, value));
            return this;
        }
        public UpdateBuilder<T> AddToSetEach<TItem>(string field, IEnumerable<TItem> values)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.AddToSetEach(field, values));
            return this;
        }
        public UpdateBuilder<T> AddToSetEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.AddToSetEach(field, values));
            return this;
        }

        public UpdateBuilder<T> BitwiseAnd<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseAnd(field, value));
            return this;
        }
        public UpdateBuilder<T> BitwiseAnd<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseAnd(field, value));
            return this;
        }
        public UpdateBuilder<T> BitwiseOr<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseOr(field, value));
            return this;
        }
        public UpdateBuilder<T> BitwiseOr<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseOr(field, value));
            return this;
        }
        public UpdateBuilder<T> BitwiseXor<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseXor(field, value));
            return this;
        }
        public UpdateBuilder<T> BitwiseXor<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.BitwiseXor(field, value));
            return this;
        }

        public UpdateBuilder<T> CurrentDate(string field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.CurrentDate(field));
            return this;
        }
        public UpdateBuilder<T> CurrentDate(Expression<Func<T, object>> field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.CurrentDate(field));
            return this;
        }
        public UpdateBuilder<T> CurrentTimestamp(string field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
            return this;
        }
        public UpdateBuilder<T> CurrentTimestamp(Expression<Func<T, object>> field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.CurrentDate(field, UpdateDefinitionCurrentDateType.Timestamp));
            return this;
        }

        public UpdateBuilder<T> Inc<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Inc(field, value));
            return this;
        }
        public UpdateBuilder<T> Inc<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Inc(field, value));
            return this;
        }
        public UpdateBuilder<T> Max<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Max(field, value));
            return this;
        }
        public UpdateBuilder<T> Max<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Max(field, value));
            return this;
        }
        public UpdateBuilder<T> Min<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Min(field, value));
            return this;
        }
        public UpdateBuilder<T> Min<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Min(field, value));
            return this;
        }
        public UpdateBuilder<T> Mul<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Mul(field, value));
            return this;
        }
        public UpdateBuilder<T> Mul<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Mul(field, value));
            return this;
        }

        public UpdateBuilder<T> PopFirst(string field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PopFirst(field));
            return this;
        }
        public UpdateBuilder<T> PopFirst(Expression<Func<T, object>> field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PopFirst(field));
            return this;
        }
        public UpdateBuilder<T> PopLast(string field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PopLast(field));
            return this;
        }
        public UpdateBuilder<T> PopLast(Expression<Func<T, object>> field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PopLast(field));
            return this;
        }

        public UpdateBuilder<T> Pull<TItem>(string field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Pull(field, value));
            return this;
        }
        public UpdateBuilder<T> Pull<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Pull(field, value));
            return this;
        }
        public UpdateBuilder<T> PullAll<TItem>(string field, IEnumerable<TItem> values)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PullAll(field, values));
            return this;
        }
        public UpdateBuilder<T> PullAll<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PullAll(field, values));
            return this;
        }
        public UpdateBuilder<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Expression<Func<TItem, bool>> filter)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PullFilter(field, filter));
            return this;
        }
        public UpdateBuilder<T> PullFilter<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Q<TItem> query)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PullFilter(field, query.Filter));
            return this;
        }
        public UpdateBuilder<T> PullFilter<TItem>(string field, Q<TItem> query)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PullFilter(field, query.Filter));
            return this;
        }

        public UpdateBuilder<T> Push<TItem>(string field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Push(field, value));
            return this;
        }
        public UpdateBuilder<T> Push<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Push(field, value));
            return this;
        }
        public UpdateBuilder<T> PushEach<TItem>(string field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
            return this;
        }
        public UpdateBuilder<T> PushEach<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values, int? slice = null, int? position = null, S<TItem> sort = null)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.PushEach(field, values, slice, position, sort != null ? sort.Sort : null));
            return this;
        }

        public UpdateBuilder<T> Rename(string field, string newName)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Rename(field, newName));
            return this;
        }
        public UpdateBuilder<T> Rename(Expression<Func<T, object>> field, string newName)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Rename(field, newName));
            return this;
        }

        public UpdateBuilder<T> Set<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Set<TField>(field, value));
            return this;
        }
        public UpdateBuilder<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Set(field, value));
            return this;
        }
        public UpdateBuilder<T> SetOnInsert<TField>(string field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.SetOnInsert<TField>(field, value));
            return this;
        }
        public UpdateBuilder<T> SetOnInsert<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.SetOnInsert(field, value));
            return this;
        }
        public UpdateBuilder<T> Unset(string field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Unset(field));
            return this;
        }
        public UpdateBuilder<T> Unset(Expression<Func<T, object>> field)
        {
            Update = Builders<T>.Update.Combine(Update, Builders<T>.Update.Unset(field));
            return this;
        }

        #region &
        public static UpdateBuilder<T> operator &(UpdateBuilder<T> lhs, UpdateBuilder<T> rhs)
        {
            Ensure.IsNotNull<UpdateBuilder<T>>(lhs, "lhs");
            Ensure.IsNotNull<UpdateBuilder<T>>(rhs, "rhs");
            return new UpdateBuilder<T>(Builders<T>.Update.Combine(lhs.Update, rhs.Update));
        }
        #endregion
        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Update.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }
    }
    public interface IMongoUpdate
    { }
}
