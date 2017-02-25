using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace clby.Core.Models
{
    internal class ActionLog
    {
        public ActionLog()
        {
            _id = ObjectId.GenerateNewId();
            ActionStartTime = DateTime.Now;
            ActionEndTime = DateTime.Now;
            Result = new BsonDocument();
        }

        public ObjectId _id { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionStartTime { get; set; }
        public DateTime ActionEndTime { get; set; }
        public double ActionTime { get; set; }

        public Dictionary<string, string> Cookies { get; set; }

        public Dictionary<string, BsonBinaryData> Session { get; set; }
        public Dictionary<string, string> UserInfo { get; set; }
        public string SessionId { get; set; }

        public Dictionary<string, string> FormCollections { get; set; }
        public Dictionary<string, string> QueryCollections { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; }
        public Dictionary<string, string> ResponseHeaders { get; set; }

        public BsonDocument Result { get; set; }
        public string Exception { get; set; }
    }
}
