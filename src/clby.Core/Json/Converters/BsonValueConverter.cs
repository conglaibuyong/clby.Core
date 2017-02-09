using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace clby.Core.Json.Converters
{
    public class BsonValueConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(BsonDocument))
            {
                writer.WriteStartObject();
                var bsonDocument = BsonValue.Create(value).AsBsonDocument;
                foreach (var bd in bsonDocument)
                {
                    WriteJson(writer, bd, serializer);
                }
                writer.WriteEndObject();
            }
            else if (value.GetType() == typeof(BsonArray))
            {
                writer.WriteStartArray();
                var bsonArray = BsonValue.Create(value).AsBsonArray;
                foreach (var bItem in bsonArray)
                {
                    WriteJson(writer, bItem, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                var bsonValue = BsonValue.Create(value);
                if (bsonValue.IsBsonDateTime)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("$date");
                    writer.WriteValue((bsonValue.AsBsonDateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000);
                    writer.WriteEndObject();
                }
                else if (bsonValue.IsObjectId)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("$oid");
                    writer.WriteValue(bsonValue.AsObjectId.ToString());
                    writer.WriteEndObject();
                }
                else if (bsonValue.IsBoolean)
                {
                    writer.WriteValue(bsonValue.AsBoolean);
                }
                else if (bsonValue.IsInt32)
                {
                    writer.WriteValue(bsonValue.AsInt32);
                }
                else if (bsonValue.IsInt64)
                {
                    writer.WriteValue(bsonValue.AsInt64);
                }
                else if (bsonValue.IsDouble)
                {
                    writer.WriteValue(bsonValue.AsDouble);
                }
                else if (bsonValue.IsBsonNull || bsonValue.IsBsonUndefined)
                {
                    writer.WriteValue("");
                }
                else
                {
                    writer.WriteValue(bsonValue.ToString());
                }
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return BsonValue.Create(reader.Value);
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BsonValue)
                || objectType == typeof(BsonDocument)
                || objectType == typeof(BsonArray);
        }
    }
}
