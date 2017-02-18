using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace clby.Tests
{
    public class Test_Mongo
    {
        [Fact]
        public void t_DateTimeOffSet()
        {
            //var Client = new MongoClient("mongodb://127.0.0.1:27017/");
            //var MongoDatabase = Client.GetDatabase("tests");

            //MongoDatabase.GetCollection<TC>("TC").InsertOne(new TC()
            //{
            //    dt = new DateTimeOffset(DateTime.Now)
            //});


        }


        public class TC
        {
            public ObjectId _id { get; set; }
            public DateTimeOffset dt { get; set; }

            public DateTime dt1 { get; set; }

            public TimeSpan dt2 { get; set; }
        }

    }
}
