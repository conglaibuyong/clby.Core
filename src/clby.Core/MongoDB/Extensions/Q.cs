using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace clby.Core.MongoDB.Extensions
{
    public class Q<T>
    {
        public FilterDefinition<T> Filter { get; private set; }
        #region 构造
        public Q()
        {
            this.Filter = Builders<T>.Filter.Empty;
        }
        public Q(FilterDefinition<T> filter)
        {
            this.Filter = filter;
        }
        public Q(string json)
        {
            this.Filter = new JsonFilterDefinition<T>(json);
        }
        public Q(BsonDocument document)
        {
            this.Filter = new BsonDocumentFilterDefinition<T>(document);
        }
        public Q(Expression<Func<T, bool>> expression)
        {
            this.Filter = new ExpressionFilterDefinition<T>(expression);
        }
        public Q(object obj)
        {
            this.Filter = new ObjectFilterDefinition<T>(obj);
        }
        #endregion

        public static Q<T> All<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.All(field, values));
        }
        public static Q<T> All<TItem>(string field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.All(field, values));
        }

        public static Q<T> And(IEnumerable<Q<T>> filters)
        {
            return new Q<T>(Builders<T>.Filter.And(filters.Select(t => t.Filter)));
        }
        public static Q<T> And(params Q<T>[] filters)
        {
            return new Q<T>(Builders<T>.Filter.And(filters.Select(t => t.Filter)));
        }

        public static Q<T> AnyEq<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyEq(field, value));
        }
        public static Q<T> AnyEq<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyEq(field, value));
        }

        public static Q<T> AnyGt<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyGt(field, value));
        }
        public static Q<T> AnyGt<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyGt(field, value));
        }

        public static Q<T> AnyGte<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyGte(field, value));
        }
        public static Q<T> AnyGte<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyGte(field, value));
        }

        public static Q<T> AnyIn<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.AnyIn(field, values));
        }
        public static Q<T> AnyIn<TItem>(string field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.AnyIn(field, values));
        }

        public static Q<T> AnyLt<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyLt(field, value));
        }
        public static Q<T> AnyLt<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyLt(field, value));
        }

        public static Q<T> AnyLte<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyLte(field, value));
        }
        public static Q<T> AnyLte<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyLte(field, value));
        }

        public static Q<T> AnyNe<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyNe(field, value));
        }
        public static Q<T> AnyNe<TItem>(string field, TItem value)
        {
            return new Q<T>(Builders<T>.Filter.AnyNe(field, value));
        }

        public static Q<T> AnyNin<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.AnyNin(field, values));
        }
        public static Q<T> AnyNin<TItem>(string field, IEnumerable<TItem> values)
        {
            return new Q<T>(Builders<T>.Filter.AnyNin(field, values));
        }

        public static Q<T> ElemMatch<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Expression<Func<TItem, bool>> filter)
        {
            return new Q<T>(Builders<T>.Filter.ElemMatch(field, filter));
        }
        public static Q<T> ElemMatch<TItem>(Expression<Func<T, IEnumerable<TItem>>> field, Q<TItem> query)
        {
            return new Q<T>(Builders<T>.Filter.ElemMatch(field, query.Filter));
        }
        public static Q<T> ElemMatch<TItem>(string field, Q<TItem> query)
        {
            return new Q<T>(Builders<T>.Filter.ElemMatch(field, query.Filter));
        }

        public static Q<T> Empty()
        {
            return new Q<T>();
        }

        public static Q<T> Eq<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Eq(field, value));
        }
        public static Q<T> Eq<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Eq(field, value));
        }

        public static Q<T> Exists(Expression<Func<T, object>> field, bool exists = true)
        {
            return new Q<T>(Builders<T>.Filter.Exists(field, exists));
        }
        public static Q<T> Exists(string field, bool exists = true)
        {
            return new Q<T>(Builders<T>.Filter.Exists(field, exists));
        }

        public static Q<T> Gt<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Gt(field, value));
        }
        public static Q<T> Gt<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Gt(field, value));
        }

        public static Q<T> Gte<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Gte(field, value));
        }
        public static Q<T> Gte<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Gte(field, value));
        }

        public static Q<T> In<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values)
        {
            return new Q<T>(Builders<T>.Filter.In(field, values));
        }
        public static Q<T> In<TField>(string field, IEnumerable<TField> values)
        {
            return new Q<T>(Builders<T>.Filter.In(field, values));
        }

        public static Q<T> Lt<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Lt(field, value));
        }
        public static Q<T> Lt<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Lt(field, value));
        }

        public static Q<T> Lte<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Lte(field, value));
        }
        public static Q<T> Lte<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Lte(field, value));
        }

        public static Q<T> Mod(Expression<Func<T, object>> field, long modulus, long remainder)
        {
            return new Q<T>(Builders<T>.Filter.Mod(field, modulus, remainder));
        }
        public static Q<T> Mod(string field, long modulus, long remainder)
        {
            return new Q<T>(Builders<T>.Filter.Mod(field, modulus, remainder));
        }

        public static Q<T> Ne<TField>(Expression<Func<T, TField>> field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Ne(field, value));
        }
        public static Q<T> Ne<TField>(string field, TField value)
        {
            return new Q<T>(Builders<T>.Filter.Ne(field, value));
        }

        public static Q<T> Nin<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values)
        {
            return new Q<T>(Builders<T>.Filter.Nin(field, values));
        }
        public static Q<T> Nin<TField>(string field, IEnumerable<TField> values)
        {
            return new Q<T>(Builders<T>.Filter.Nin(field, values));
        }

        public static Q<T> Not(Q<T> query)
        {
            return new Q<T>(Builders<T>.Filter.Not(query.Filter));
        }

        public static Q<T> OfType<TDerived>() where TDerived : T
        {
            return new Q<T>(Builders<T>.Filter.OfType<TDerived>());
        }
        public static Q<T> OfType<TDerived>(Expression<Func<TDerived, bool>> derivedFieldFilter) where TDerived : T
        {
            return new Q<T>(Builders<T>.Filter.OfType<TDerived>(derivedFieldFilter));
        }
        public static Q<T> OfType<TDerived>(Q<TDerived> derivedFieldFilter) where TDerived : T
        {
            return new Q<T>(Builders<T>.Filter.OfType<TDerived>(derivedFieldFilter.Filter));
        }
        public static Q<T> OfType<TField, TDerived>(Expression<Func<T, TField>> field, Expression<Func<TDerived, bool>> derivedFieldFilter) where TDerived : TField
        {
            return new Q<T>(Builders<T>.Filter.OfType<TField, TDerived>(field, derivedFieldFilter));
        }
        public static Q<T> OfType<TField, TDerived>(string field, Q<TDerived> derivedFieldFilter) where TDerived : TField
        {
            return new Q<T>(Builders<T>.Filter.OfType<TField, TDerived>(field, derivedFieldFilter.Filter));
        }
        public static Q<T> OfType<TField, TDerived>(Expression<Func<T, TField>> field) where TDerived : TField
        {
            return new Q<T>(Builders<T>.Filter.OfType<TField, TDerived>(field));
        }
        public static Q<T> OfType<TField, TDerived>(string field) where TDerived : TField
        {
            return new Q<T>(Builders<T>.Filter.OfType<TField, TDerived>(field));
        }

        public static Q<T> Or(IEnumerable<Q<T>> filters)
        {
            return new Q<T>(Builders<T>.Filter.Or(filters.Select(t => t.Filter)));
        }
        public static Q<T> Or(params Q<T>[] filters)
        {
            return new Q<T>(Builders<T>.Filter.Or(filters.Select(t => t.Filter)));
        }

        public static Q<T> Regex(Expression<Func<T, object>> field, Regex regex)
        {
            return new Q<T>(Builders<T>.Filter.Regex(field, new BsonRegularExpression(regex)));
        }
        public static Q<T> Regex<TField>(string field, Regex regex)
        {
            return new Q<T>(Builders<T>.Filter.Regex(field, new BsonRegularExpression(regex)));
        }

        public static Q<T> Size(Expression<Func<T, object>> field, int size)
        {
            return new Q<T>(Builders<T>.Filter.Size(field, size));
        }
        public static Q<T> Size(string field, int size)
        {
            return new Q<T>(Builders<T>.Filter.Size(field, size));
        }

        public static Q<T> SizeGt(Expression<Func<T, object>> field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeGt(field, size));
        }
        public static Q<T> SizeGt(string field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeGt(field, size));
        }

        public static Q<T> SizeGte(Expression<Func<T, object>> field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeGte(field, size));
        }
        public static Q<T> SizeGte(string field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeGte(field, size));
        }

        public static Q<T> SizeLt(Expression<Func<T, object>> field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeLt(field, size));
        }
        public static Q<T> SizeLt(string field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeLt(field, size));
        }

        public static Q<T> SizeLte(Expression<Func<T, object>> field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeLte(field, size));
        }
        public static Q<T> SizeLte(string field, int size)
        {
            return new Q<T>(Builders<T>.Filter.SizeLte(field, size));
        }

        public static Q<T> Text(string search, string language = null)
        {
            return new Q<T>(Builders<T>.Filter.Text(search, language));
        }

        public static Q<T> Type(Expression<Func<T, object>> field, BsonType type)
        {
            return new Q<T>(Builders<T>.Filter.Type(field, type));
        }
        public static Q<T> Type(string field, BsonType Type)
        {
            return new Q<T>(Builders<T>.Filter.Type(field, Type));
        }

        public static Q<T> Where(Expression<Func<T, bool>> expression)
        {
            return new Q<T>(Builders<T>.Filter.Where(expression));
        }

        #region & |
        public static Q<T> operator &(Q<T> lhs, Q<T> rhs)
        {
            Ensure.IsNotNull<Q<T>>(lhs, "lhs");
            Ensure.IsNotNull<Q<T>>(rhs, "rhs");
            return new Q<T>(lhs.Filter & rhs.Filter);
        }
        public static Q<T> operator |(Q<T> lhs, Q<T> rhs)
        {
            Ensure.IsNotNull<Q<T>>(lhs, "lhs");
            Ensure.IsNotNull<Q<T>>(rhs, "rhs");
            return new Q<T>(lhs.Filter | rhs.Filter);
        }
        #endregion

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Filter.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }

    }
}
