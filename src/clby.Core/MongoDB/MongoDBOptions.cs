using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace clby.Core.MongoDB
{
    public class MongoDBOptions : IOptions<MongoDBOptions>
    {

        public MongoClientSettings Settings { get; set; }

        public string ConnectionString
        {
            set
            {
                MongoClientSettings.FromUrl(MongoUrl.Create(value));
            }
        }

        public string DbName { get; set; }


        MongoDBOptions IOptions<MongoDBOptions>.Value
        {
            get
            {
                return this;
            }
        }
    }
}
