using clby.Core.Misc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Linq;

namespace clby.Core.MongoDB
{
    [DebuggerStepThrough]
    public static class BsonExtensions
    {
        public static BsonValue TryGet(this BsonValue bv, string pKey)
        {
            Ensure.IsNotNullOrEmpty(pKey, "TryGet.pKey");
            BsonValue tmp = bv;
            if (tmp == null
                || tmp == BsonNull.Value
                || string.IsNullOrEmpty(pKey)
                || !tmp.IsBsonDocument)
            {
                return tmp;
            }
            var keys = pKey.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var k in keys)
            {
                if (tmp.IsBsonDocument)
                {
                    BsonDocument bd = tmp.AsBsonDocument;
                    if (bd.Contains(k))
                    {
                        tmp = bd[k];
                    }
                    else
                    {
                        return BsonNull.Value;
                    }
                }
                else
                {
                    return BsonNull.Value;
                }
            }
            return tmp;
        }
        public static BsonValue Get(this BsonValue bv, string pKey)
        {
            Ensure.IsNotNullOrEmpty(pKey, "Get.pKey");
            BsonValue result = bv;
            var keys = pKey.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var k in keys)
            {
                result = result.AsBsonDocument[k];
            }
            return result;
        }
        public static bool Has(this BsonValue bv, string pKey)
        {
            Ensure.IsNotNullOrEmpty(pKey, "Has.pKey");

            BsonValue tmp = bv;

            if (tmp == null
                || tmp == BsonNull.Value
                || string.IsNullOrEmpty(pKey)
                || !tmp.IsBsonDocument)
            {
                return false;
            }

            var keys = pKey.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var k in keys)
            {
                if (tmp.IsBsonDocument)
                {
                    BsonDocument bd = tmp.AsBsonDocument;
                    if (bd.Contains(k))
                    {
                        tmp = bd[k];
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Contains(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "Has.pKey");

            if (bv == null
                || bv == BsonNull.Value
                || string.IsNullOrEmpty(key)
                || !bv.IsBsonDocument)
            {
                return false;
            }

            return bv.AsBsonDocument.Contains(key);
        }

        public static ObjectId TryAsObjectId(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsObjectId.key");
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsObjectId)
                {
                    return tmp.AsObjectId;
                }
                else if (tmp.IsString)
                {
                    ObjectId id;
                    if (ObjectId.TryParse(tmp.AsString, out id))
                    {
                        return id;
                    }
                }
            }
            return ObjectId.Empty;
        }
        public static ObjectId TryAsObjectId(this BsonValue bv)
        {
            if (bv.IsObjectId)
            {
                return bv.AsObjectId;
            }
            else if (bv.IsString)
            {
                ObjectId id;
                if (ObjectId.TryParse(bv.AsString, out id))
                {
                    return id;
                }
            }
            return ObjectId.Empty;
        }
        public static ObjectId AsObjectId(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsObjectId.key");
            return bv.AsBsonDocument[key].AsObjectId;
        }

        public static string TryAsString(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsString.key");
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsString)
                {
                    return tmp.AsString;
                }
            }
            return null;
        }
        public static string TryAsString(this BsonValue bv)
        {
            if (bv.IsString)
            {
                return bv.AsString;
            }
            return null;
        }
        public static string AsString(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsString.key");
            return bv.AsBsonDocument[key].AsString;
        }

        public static DateTime TryAsLocalTime(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsLocalTime.key");
            DateTime ret = DateTime.MinValue;
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsValidDateTime)
                {
                    return tmp.ToUniversalTime().ToLocalTime();
                }
                else if (tmp.IsString)
                {
                    DateTime.TryParse(bv.AsString, out ret);
                }
            }
            return ret;
        }
        public static DateTime TryAsLocalTime(this BsonValue bv)
        {
            DateTime ret = DateTime.MinValue;
            if (bv.IsValidDateTime)
            {
                return bv.ToUniversalTime().ToLocalTime();
            }
            else if (bv.IsString)
            {
                DateTime.TryParse(bv.AsString, out ret);
            }
            return ret;
        }
        public static DateTime AsLocalTime(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsUniversalTime.key");
            return bv.AsBsonDocument[key].ToUniversalTime().ToLocalTime();
        }

        public static BsonArray TryAsBsonArray(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsBsonArray.key");
            if (bv.IsBsonDocument)
            {
                var tmp = bv.AsBsonDocument;
                if (tmp.Contains(key) && tmp[key].IsBsonArray)
                {
                    return tmp[key].AsBsonArray;
                }
            }
            return new BsonArray();
        }
        public static BsonArray TryAsBsonArray(this BsonValue bv)
        {
            if (bv.IsBsonArray)
            {
                return bv.AsBsonArray;
            }
            return new BsonArray();
        }
        public static BsonArray AsBsonArray(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsBsonArray.key");
            return bv.AsBsonDocument[key].AsBsonArray;
        }

        public static BsonDocument TryAsBsonDocument(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsBsonDocument.key");
            if (bv.IsBsonDocument)
            {
                var tmp = bv.AsBsonDocument;
                if (tmp.Contains(key) && tmp[key].IsBsonDocument)
                {
                    return tmp[key].AsBsonDocument;
                }
            }
            return new BsonDocument();
        }
        public static BsonDocument TryAsBsonDocument(this BsonValue bv)
        {
            if (bv.IsBsonDocument)
            {
                return bv.AsBsonDocument;
            }
            return new BsonDocument();
        }
        public static BsonDocument AsBsonDocument(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsBsonDocument.key");
            return bv.AsBsonDocument[key].AsBsonDocument;
        }

        public static bool TryAsBoolean(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsBoolean.key");
            bool ret = false;
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsBoolean)
                {
                    return tmp.AsBoolean;
                }
                else if (tmp.IsString)
                {
                    bool.TryParse(bv.AsString, out ret);
                }
            }
            return ret;
        }
        public static bool TryAsBoolean(this BsonValue bv)
        {
            bool ret = false;
            if (bv.IsBoolean)
            {
                return bv.AsBoolean;
            }
            else if (bv.IsString)
            {
                bool.TryParse(bv.AsString, out ret);
            }
            return ret;
        }
        public static bool AsBoolean(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsBoolean.key");
            return bv.AsBsonDocument[key].AsBoolean;
        }

        public static double TryAsDouble(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsDouble.key");
            double ret = 0;
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsDouble)
                {
                    return tmp.AsDouble;
                }
                else if (tmp.IsInt32)
                {
                    return tmp.AsInt32;
                }
                else if (tmp.IsString)
                {
                    double.TryParse(bv.AsString, out ret);
                }
            }
            return ret;
        }
        public static double TryAsDouble(this BsonValue bv)
        {
            double ret = 0;
            if (bv.IsDouble)
            {
                return bv.AsDouble;
            }
            else if (bv.IsInt32)
            {
                return bv.AsInt32;
            }
            else if (bv.IsString)
            {
                double.TryParse(bv.AsString, out ret);
            }
            return ret;
        }
        public static double AsDouble(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsDouble.key");
            var tmp = bv[key];
            if (tmp.IsDouble)
            {
                return tmp.AsDouble;
            }
            else if (tmp.IsInt32)
            {
                return tmp.AsInt32;
            }
            else if (tmp.IsString)
            {
                var v = tmp.AsString;
                return Convert.ToDouble(string.IsNullOrEmpty(v) ? "0" : v);
            }
            return 0;
        }

        public static int TryAsInt32(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "TryAsInt32.key");
            int ret = 0;
            if (bv.IsBsonDocument && bv.Contains(key))
            {
                var tmp = bv[key];
                if (tmp.IsInt32)
                {
                    return tmp.AsInt32;
                }
                else if (tmp.IsString)
                {
                    int.TryParse(bv.AsString, out ret);
                }
            }
            return ret;
        }
        public static int TryAsInt32(this BsonValue bv)
        {
            int ret = 0;
            if (bv.IsInt32)
            {
                return bv.AsInt32;
            }
            else if (bv.IsString)
            {
                int.TryParse(bv.AsString, out ret);
            }
            return ret;
        }
        public static int AsInt32(this BsonValue bv, string key)
        {
            Ensure.IsNotNullOrEmpty(key, "AsInt32.key");
            var tmp = bv[key];
            if (tmp.IsInt32)
            {
                return tmp.AsInt32;
            }
            else if (tmp.IsString)
            {
                return Convert.ToInt32(tmp.AsString);
            }
            return 0;
        }

        //扩展




        public static string ToJson<T>(this FilterDefinition<T> filter)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this UpdateDefinition<T> update)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return update.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this SortDefinition<T> sort)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return sort.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this ProjectionDefinition<T> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return projection.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
        public static string ToJson<T>(this FieldDefinition<T> field)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return field.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToJson();
        }
    }
}
