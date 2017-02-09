using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace clby.Core.MongoDB.Extensions
{
    public class P<T>
    {
        public ProjectionDefinition<T> Projection { get; private set; }

        #region 构造
        public P(ProjectionDefinition<T> projection)
        {
            this.Projection = projection;
        }
        public P(BsonDocument document)
        {
            this.Projection = new BsonDocumentProjectionDefinition<T>(document);
        }
        public P(string json)
        {
            this.Projection = new JsonProjectionDefinition<T>(json);
        }
        public P(object obj)
        {
            this.Projection = new ObjectProjectionDefinition<T>(obj);
        }
        #endregion

        public static P<T> As<TProjection>()
        {
            throw new NotImplementedException();
            //return new P<T>(Builders<T>.Projection.As<TProjection>());
        }

        public static P<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Expression<Func<TProjection, bool>> filter)
        {
            return new P<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter));
        }
        public static P<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Q<TProjection> filter)
        {
            return new P<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
        }
        public static P<T> ElemMatch<TProjection>(string field, Q<TProjection> filter)
        {
            return new P<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
        }

        public static P<T> Exclude(string field)
        {
            return new P<T>(Builders<T>.Projection.Exclude(field));
        }
        public static P<T> Exclude(Expression<Func<T, object>> field)
        {
            return new P<T>(Builders<T>.Projection.Exclude(field));
        }

        public static P<T> Expression<TProjection>(Expression<Func<T, TProjection>> expression)
        {
            throw new NotImplementedException();
            //return new P<T>(Builders<T>.Projection.Expression<TProjection>(expression));
        }

        public static P<T> Include(string field)
        {
            return new P<T>(Builders<T>.Projection.Include(field));
        }
        public static P<T> Include(Expression<Func<T, object>> field)
        {
            return new P<T>(Builders<T>.Projection.Include(field));
        }

        public static P<T> MetaTextScore(string field)
        {
            return new P<T>(Builders<T>.Projection.MetaTextScore(field));
        }

        public static P<T> Slice(string field, int skip, int? limit = null)
        {
            return new P<T>(Builders<T>.Projection.Slice(field, skip, limit));
        }
        public static P<T> Slice(Expression<Func<T, object>> field, int skip, int? limit = null)
        {
            return new P<T>(Builders<T>.Projection.Slice(field, skip, limit));
        }


        #region &
        public static P<T> operator &(P<T> lhs, P<T> rhs)
        {
            Ensure.IsNotNull<P<T>>(lhs, "lhs");
            Ensure.IsNotNull<P<T>>(rhs, "rhs");
            return new P<T>(Builders<T>.Projection.Combine(lhs.Projection, rhs.Projection));
        }
        #endregion

        public static implicit operator P<T>(ProjectionBuilder<T> projection)
        {
            return new P<T>(projection.Projection);
        }

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }

    }

    public static class Projection<T>
    {
        public static ProjectionBuilder<T> As<TProjection>()
        {
            throw new NotImplementedException();
            //return new ProjectionBuilder<T>(Builders<T>.Projection.As<TProjection>());
        }

        public static ProjectionBuilder<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Expression<Func<TProjection, bool>> filter)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter));
        }
        public static ProjectionBuilder<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Q<TProjection> filter)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
        }
        public static ProjectionBuilder<T> ElemMatch<TProjection>(string field, Q<TProjection> filter)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
        }

        public static ProjectionBuilder<T> Exclude(string field)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Exclude(field));
        }
        public static ProjectionBuilder<T> Exclude(Expression<Func<T, object>> field)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Exclude(field));
        }

        public static ProjectionBuilder<T> Expression<TProjection>(Expression<Func<T, TProjection>> expression)
        {
            throw new NotImplementedException();
            //return new ProjectionBuilder<T>(Builders<T>.Projection.Expression<TProjection>(expression));
        }

        public static ProjectionBuilder<T> Include(string field)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Include(field));
        }
        public static ProjectionBuilder<T> Include(Expression<Func<T, object>> field)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Include(field));
        }

        public static ProjectionBuilder<T> MetaTextScore(string field)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.MetaTextScore(field));
        }

        public static ProjectionBuilder<T> Slice(string field, int skip, int? limit = null)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Slice(field, skip, limit));
        }
        public static ProjectionBuilder<T> Slice(Expression<Func<T, object>> field, int skip, int? limit = null)
        {
            return new ProjectionBuilder<T>(Builders<T>.Projection.Slice(field, skip, limit));
        }
    }
    public class ProjectionBuilder<T> : IMongoProjection
    {
        public ProjectionDefinition<T> Projection { get; private set; }

        #region 构造
        public ProjectionBuilder(ProjectionDefinition<T> projection)
        {
            this.Projection = projection;
        }
        public ProjectionBuilder(BsonDocument document)
        {
            this.Projection = new BsonDocumentProjectionDefinition<T>(document);
        }
        public ProjectionBuilder(string json)
        {
            this.Projection = new JsonProjectionDefinition<T>(json);
        }
        public ProjectionBuilder(object obj)
        {
            this.Projection = new ObjectProjectionDefinition<T>(obj);
        }
        #endregion

        public ProjectionBuilder<T> As<TProjection>()
        {
            throw new NotImplementedException();
        }

        public ProjectionBuilder<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Expression<Func<TProjection, bool>> filter)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.ElemMatch<TProjection>(field, filter));
            return this;
        }
        public ProjectionBuilder<T> ElemMatch<TProjection>(Expression<Func<T, IEnumerable<TProjection>>> field, Q<TProjection> filter)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
            return this;
        }
        public ProjectionBuilder<T> ElemMatch<TProjection>(string field, Q<TProjection> filter)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.ElemMatch<TProjection>(field, filter.Filter));
            return this;
        }

        public ProjectionBuilder<T> Exclude(string field)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Exclude(field));
            return this;
        }
        public ProjectionBuilder<T> Exclude(Expression<Func<T, object>> field)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Exclude(field));
            return this;
        }

        public ProjectionBuilder<T> Expression<TProjection>(Expression<Func<T, TProjection>> expression)
        {
            throw new NotImplementedException();
        }

        public ProjectionBuilder<T> Include(string field)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Include(field));
            return this;
        }
        public ProjectionBuilder<T> Include(Expression<Func<T, object>> field)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Include(field));
            return this;
        }

        public ProjectionBuilder<T> MetaTextScore(string field)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.MetaTextScore(field));
            return this;
        }

        public ProjectionBuilder<T> Slice(string field, int skip, int? limit = null)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Slice(field, skip, limit));
            return this;
        }
        public ProjectionBuilder<T> Slice(Expression<Func<T, object>> field, int skip, int? limit = null)
        {
            Projection = Builders<T>.Projection.Combine(Projection, Builders<T>.Projection.Slice(field, skip, limit));
            return this;
        }

        #region &
        public static ProjectionBuilder<T> operator &(ProjectionBuilder<T> lhs, ProjectionBuilder<T> rhs)
        {
            Ensure.IsNotNull<ProjectionBuilder<T>>(lhs, "lhs");
            Ensure.IsNotNull<ProjectionBuilder<T>>(rhs, "rhs");
            return new ProjectionBuilder<T>(Builders<T>.Projection.Combine(lhs.Projection, rhs.Projection));
        }
        #endregion

        public string ToJson()
        {
            return ToBsonDocument().ToJson();
        }
        public BsonDocument ToBsonDocument()
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return this.Projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        }
    }
    public interface IMongoProjection
    { }
}
