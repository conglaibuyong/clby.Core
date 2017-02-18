using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace clby.Core.MongoDB
{
    public interface IMongoDbHelper
    {
        IMongoDatabase GetInstance();
        IMongoDatabase GetInstance(string dbName);

        IMongoCollection<T> GetCollection<T>();
        IMongoCollection<T> GetCollection<T>(string collectionName);

        Task<BsonDocument> RunCommand(Command<BsonDocument> Command);
        Task<BsonDocument> Eval(BsonJavaScript js, BsonArray args, bool nolock = false);
    }
}
