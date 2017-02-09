using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace clby.Core.MongoDB
{
    public sealed class MongoHelper
    {
        private const string DefaultProvider = "default";
        private static Dictionary<string, IMongoDatabase> Mongos = GetInstances();
        private static Dictionary<string, IMongoDatabase> GetInstances()
        {
            Dictionary<string, IMongoDatabase> ms = new Dictionary<string, IMongoDatabase>();
            //foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            //{
            //    if (conn.ProviderName.Contains("mongodb"))
            //    {
            //        var key = conn.Name;
            //        var arr = conn.ProviderName.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //        if (arr.Length == 2) key = arr[1];
            //        var Client = new MongoClient(conn.ConnectionString);
            //        ms.Add(key, Client.GetDatabase(conn.Name));
            //    }
            //}
            return ms;
        }
        public static IMongoDatabase GetInstance(string dbName = null)
        {
            if (string.IsNullOrEmpty(dbName)) dbName = DefaultProvider;
            return Mongos[dbName];
        }
        public static IMongoCollection<T> GetCollection<T>(string collectionName = null, string dbName = null)
        {
            var cName = string.IsNullOrEmpty(collectionName) ? ReflectionHelper.GetScrubbedGenericName(typeof(T)) : collectionName;
            if (string.IsNullOrEmpty(dbName)) dbName = DefaultProvider;
            return Mongos[dbName].GetCollection<T>(cName);
        }

        public static BsonDocument RunCommand(Command<BsonDocument> Command, string dbName = null)
        {
            return GetInstance().RunCommand<BsonDocument>(Command);
        }
        public static BsonDocument Eval(string js, string[] args, bool nolock = false, string dbName = null)
        {
            BsonDocument bd = new BsonDocument();
            bd.Add("eval", js);
            if (args != null && args.Length > 0) bd.Add("args", BsonArray.Create(args));
            bd.Add("nolock", nolock);
            return GetInstance().RunCommand<BsonDocument>(new BsonDocumentCommand<BsonDocument>(bd));
            //{ "retval" : {}, "ok" : 1.0 }
        }
    }

    public static class MongoHelperEx
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
