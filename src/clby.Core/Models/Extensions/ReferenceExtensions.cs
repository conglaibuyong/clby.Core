using clby.Core.MongoDB;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace clby.Core.Models.Extensions
{
    public static class ReferenceExtensions
    {
        public static T Fetch<T, TId, K>(this BaseReference<T, TId, K> Value, IMongoDbOperator mdb) where T : class, IBaseObject, new()
        {
            Value.RealValue = mdb
                .GetCollection<T>(Value.Ref.CollectionName)
                .Find<T>(Builders<T>.Filter.Eq("_id", Value.Ref.Id), null)
                .FirstOrDefault();
            return Value.RealValue;
        }
        public static Task<T> FetchAsync<T, TId, K>(this BaseReference<T, TId, K> Value, IMongoDbOperator mdb) where T : class, IBaseObject, new()
        {
            return mdb
                .GetCollection<T>(Value.Ref.CollectionName)
                .Find<T>(Builders<T>.Filter.Eq("_id", Value.Ref.Id), null)
                .FirstOrDefaultAsync();
        }
    }
}
