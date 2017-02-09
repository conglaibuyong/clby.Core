using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace clby.Core.MongoDB.Extensions
{
    public abstract class R
    {
        public bool IsAcknowledged { get; protected set; }
        public long MatchedCount { get; protected set; }
        public long ModifiedCount { get; protected set; }
        public long DeletedCount { get; protected set; }
        public long InsertedCount { get; protected set; }
        public long RequestCount { get; protected set; }
        public List<BsonValue> UpsertedIds { get; protected set; }


        public static implicit operator R(BulkWriteResult Result)
        {
            return new BulkWriteR(Result);
        }
        public static implicit operator R(ReplaceOneResult Result)
        {
            return new ReplaceR(Result);
        }
        public static implicit operator R(DeleteResult Result)
        {
            return new DeleteR(Result);
        }
        public static implicit operator R(UpdateResult Result)
        {
            return new UpdateR(Result);
        }
    }
    public sealed class BulkWriteR : R
    {
        public BulkWriteR(BulkWriteResult Result)
        {
            IsAcknowledged = Result.IsAcknowledged;
            if (Result.IsAcknowledged)
            {
                MatchedCount = Result.MatchedCount;
                ModifiedCount = Result.ModifiedCount;
                UpsertedIds = Result.Upserts.Select(t => t.Id).ToList();
                DeletedCount = Result.DeletedCount;
                InsertedCount = Result.InsertedCount;
                RequestCount = Result.RequestCount;
            }
        }
    }
    public sealed class ReplaceR : R
    {
        public ReplaceR(ReplaceOneResult Result)
        {
            IsAcknowledged = Result.IsAcknowledged;
            if (Result.IsAcknowledged)
            {
                MatchedCount = Result.MatchedCount;
                ModifiedCount = Result.ModifiedCount;
                UpsertedIds = new List<BsonValue>() { Result.UpsertedId };
            }
        }
    }
    public sealed class DeleteR : R
    {
        public DeleteR(DeleteResult Result)
        {
            IsAcknowledged = Result.IsAcknowledged;
            if (Result.IsAcknowledged)
            {
                DeletedCount = Result.DeletedCount;
            }
        }
    }
    public sealed class UpdateR : R
    {
        public UpdateR(UpdateResult Result)
        {
            IsAcknowledged = Result.IsAcknowledged;
            if (Result.IsAcknowledged)
            {
                MatchedCount = Result.MatchedCount;
                ModifiedCount = Result.ModifiedCount;
                UpsertedIds = new List<BsonValue>() { Result.UpsertedId };
            }
        }
    }
}
