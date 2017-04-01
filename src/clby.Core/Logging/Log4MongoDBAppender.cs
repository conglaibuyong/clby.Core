using clby.Core.MongoDB;
using log4net.Appender;
using log4net.Core;
using MongoDB.Bson;
using System;
using System.Linq;

namespace clby.Core.Logging
{
    public class Log4MongoDBAppender : AppenderSkeleton
    {
        private IMongoDbOperator mdb = null;

        public string ConnectionString { get; set; }
        public string DbName { get; set; }

        public override void ActivateOptions()
        {
            mdb = new MongoDbOperator(ConnectionString, DbName);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            /*
             可能存在性能问题
             */

            var cName = loggingEvent.LoggerName + "_" + DateTime.Now.ToLocalTime().ToString("yyyyMM");
            var collection = mdb.GetCollection<BsonDocument>(cName);
            var log = loggingEvent.MessageObject.GetType() == typeof(string)
                ? new BsonDocument(new BsonElement("Content", loggingEvent.MessageObject.ToString()))
                : loggingEvent.MessageObject.ToBsonDocument();
            if (loggingEvent.ExceptionObject != null)
            {
                log.Add(new BsonElement("___Exception___", (loggingEvent.ExceptionObject as Exception).ToString()));
            }
            log.Add(new BsonElement("___ThreadName___", loggingEvent.ThreadName));
            log.Add(new BsonElement("___Domain___", loggingEvent.Domain));
            log.Add(new BsonElement("___TimeStamp___", loggingEvent.TimeStamp));
            log.Add(new BsonElement("___UserName___", loggingEvent.UserName));
            collection.InsertOneAsync(log);
        }

        //protected override void Append(LoggingEvent[] loggingEvents)
        //{
        //    var cName = loggingEvents.First().LoggerName + "_" + DateTime.Now.ToLocalTime().ToString("yyyyMM");
        //    var collection = mdb.GetCollection<BsonDocument>(cName);
        //    collection.InsertManyAsync(loggingEvents.Select(loggingEvent =>
        //    {
        //        var log = loggingEvent.MessageObject.GetType() == typeof(string)
        //        ? new BsonDocument(new BsonElement("Content", loggingEvent.MessageObject.ToString()))
        //        : loggingEvent.MessageObject.ToBsonDocument();
        //        if (loggingEvent.ExceptionObject != null)
        //        {
        //            log.Add(new BsonElement("___Exception___", (loggingEvent.ExceptionObject as Exception).ToString()));
        //        }
        //        log.Add(new BsonElement("___ThreadName___", loggingEvent.ThreadName));
        //        log.Add(new BsonElement("___Domain___", loggingEvent.Domain));
        //        log.Add(new BsonElement("___TimeStamp___", loggingEvent.TimeStamp));
        //        log.Add(new BsonElement("___UserName___", loggingEvent.UserName));
        //        return log;
        //    }));
        //}

    }
}
