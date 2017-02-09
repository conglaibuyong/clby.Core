using clby.Core.Misc;
using clby.Core.MongoDB.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace clby.Core.MongoDB.Extensions
{
    public class Transactions : BaseObject
    {
        public Transactions()
        {
            Status = TransactionStatus.Initial;
            Items = new List<TransactionItem>();
            Results = new List<BsonDocument>();
        }

        public TransactionStatus Status { get; set; }
        public List<TransactionItem> Items { get; set; }
        public List<BsonDocument> Results { get; set; }
        public int ReTry { get; set; }
    }
    public class TransactionItem
    {
        public string CollectionName { get; set; }
        public string Query { get; set; }
        public string Update { get; set; }
    }
    public enum TransactionStatus
    {
        Initial,
        Pending,
        Applied,
        Done,
        Close
    }

    public static class TransactionsEx
    {
        public static void AddItem(this Transactions t, string CollectionName, string Query, string Update)
        {
            Ensure.IsNotNull(t, "Transactions.t");
            Ensure.IsNotNullOrEmpty(CollectionName, "CollectionName");
            Ensure.IsNotNullOrEmpty(Query, "Query");
            Ensure.IsNotNullOrEmpty(Update, "Update");

            t.Items.Add(new TransactionItem() { CollectionName = CollectionName, Query = Query, Update = Update });
        }
        public static void AddItem(this Transactions t, TransactionItem item)
        {
            Ensure.IsNotNull(t, "Transactions.t");
            Ensure.IsNotNull(item, "TransactionItem.item");
            Ensure.IsNotNull(item.CollectionName, "TransactionItem.item.CollectionName");
            Ensure.IsNotNull(item.Query, "TransactionItem.item.Query");
            Ensure.IsNotNull(item.Update, "TransactionItem.item.Update");

            t.Items.Add(item);
        }

        public static void Commit(this Transactions t)
        {
            Ensure.IsNotNull(t, "Transactions.t");
            Ensure.IsGreaterThanOrEqualTo(t.Items.Count, 2, "Transactions.Items");
            Ensure.IsEqualTo(MongoHelper.GetCollection<Transactions>().Count(Q<Transactions>.Eq(e => e._id, t._id).Filter), 0, "Transactions.This");

            MongoHelper.GetCollection<Transactions>().InsertOne(t);

            //var result = Policy.Handle<Exception>()
            //.Retry(3, (exception, retryCount) => { LoggerHelper.Error(new { type = "Transactions.Commit", Retry = retryCount, Transactions = t }, exception); })
            //.Execute(() => { return MongoHelper.Eval(string.Format("DoTransaction('{0}');", t._id.ToString()), null); });

            var result = MongoHelper.Eval(string.Format("DoTransaction('{0}');", t._id.ToString()), null);

            if (!result.Contains("retval") || !result["retval"].ToBsonDocument().Contains("result") || !result["retval"]["result"].AsBoolean)
            {
                throw new Exception(result["retval"]["msg"].AsString);
            }
        }
        public static void ReTryCommit(this Transactions t)
        {
            Ensure.IsNotNull(t, "Transactions.t");
            Ensure.IsGreaterThanOrEqualTo(t.Items.Count, 2, "Transactions.Items");

            MongoHelper.GetCollection<Transactions>()
                .UpdateOne(Q<Transactions>.Eq(k => k._id, t._id).Filter, M<Transactions>.Inc(k => k.ReTry, 1).Update);

            //var result = Policy.Handle<Exception>()
            //.Retry(3, (exception, retryCount) => { LoggerHelper.Error(new { type = "Transactions.ReTry.Commit", Retry = retryCount, Transactions = t }, exception); })
            //.Execute(() => { return MongoHelper.Eval(string.Format("ReTryTransaction('{0}');", t._id.ToString()), null); });

            var result = MongoHelper.Eval(string.Format("ReTryTransaction('{0}');", t._id.ToString()), null);

            if (!result.Contains("retval") || !result["retval"].ToBsonDocument().Contains("result") || !result["retval"]["result"].AsBoolean)
            {
                throw new Exception(result["retval"]["msg"].AsString);
            }
        }
    }
}
