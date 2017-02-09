using clby.Core.Json.Converters;
using Newtonsoft.Json;

namespace clby.Core.Json
{
    public class JsonConverters
    {
        public static readonly JsonConverter[] All = new JsonConverter[] {
            new DateTimeConverter(),
            new ObjectIdConverter(),
            new BsonValueConverter(),
            new DictionaryStringBsonValue()
        };
    }
}
