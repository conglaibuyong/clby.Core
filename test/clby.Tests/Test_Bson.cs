using clby.Core.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace clby.Tests
{
    public class Test_Bson
    {
        [Fact]
        public void Test_TryGet()
        {
            var bd = new BsonDocument();
            var v1 = new BsonDocument();
            v1.Add("k11", "v11");
            bd.Add("k1", v1);

            var r = bd.TryGet("k1.k11");

            Assert.Equal(r.AsString,"v11");
        }

    }
}
