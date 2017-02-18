using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace clby.Core.MongoDB
{
    public static class BsonExtensions
    {



        public static string ToJson<T>(this FilterDefinition<T> filter)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this UpdateDefinition<T> update)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return update.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this SortDefinition<T> sort)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return sort.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this ProjectionDefinition<T> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return projection.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this FieldDefinition<T> field)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return field.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
    }
}
