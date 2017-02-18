using clby.Core.Misc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace clby.Core.MongoDB
{
    public class MongoDbHelper : IMongoDbHelper
    {
        private MongoClient client = null;
        private IMongoDatabase db = null;

        public MongoDbHelper(IOptions<MongoDBOptions> options)
        {
            MongoDBOptions value = options.Value;
            Ensure.IsNotNull(value.Settings, "Settings");
            Ensure.IsNotNullOrEmpty(value.DbName, "DbName");

            client = new MongoClient(value.Settings);
            db = client.GetDatabase(value.DbName);
        }

        public IMongoDatabase GetInstance()
        {
            return db;
        }
        public IMongoDatabase GetInstance(string dbName)
        {
            Ensure.IsNotNullOrEmpty(dbName, "dbName");
            return client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return db.GetCollection<T>(ReflectionHelper.GetScrubbedGenericName(typeof(T)));
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            Ensure.IsNotNullOrEmpty(collectionName, "collectionName");
            return db.GetCollection<T>(collectionName);
        }

        public Task<BsonDocument> RunCommand(Command<BsonDocument> Command)
        {
            return db.RunCommandAsync<BsonDocument>(Command);
        }
        public Task<BsonDocument> Eval(BsonJavaScript js, BsonArray args, bool nolock = false)
        {
            BsonDocument bd = new BsonDocument();
            bd.Add("eval", js);
            if (args != null && args.Count > 0)
            {
                bd.Add("args", args);
            }
            bd.Add("nolock", nolock);
            return db.RunCommandAsync<BsonDocument>(new BsonDocumentCommand<BsonDocument>(bd));
        }



    }
}
