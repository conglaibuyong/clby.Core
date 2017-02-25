using clby.Core.Json;
using clby.Core.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace clby.Core.Mvc
{
    public static class ControllerExtensions
    {
        public static string GetValue(this Controller controller, string key)
        {
            var values = controller.Request.Query[key];
            if (values.Any())
            {
                return values[0];
            }
            if (controller.Request.HasFormContentType)
            {
                values = controller.Request.Form[key];
                if (values.Any())
                {
                    return values[0];
                }
            }
            values = controller.Request.Cookies[key];
            if (values.Any())
            {
                return values[0];
            }
            return null;
        }

        public static string GetStringParameter(this Controller controller, string key, string defaultValue = "")
        {
            return controller.GetValue(key) ?? defaultValue;
        }

        public static bool GetBooleanParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            return "true".Equals(value.ToLower());
        }
        public static bool TryGetBooleanParameter(this Controller controller, string key, bool defaultValue = false)
        {
            var value = controller.GetValue(key) ?? (defaultValue ? "true" : "false");
            return "true".Equals(value.ToLower());
        }

        public static int GetIntParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                return intValue;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static int TryGetIntParameter(this Controller controller, string key, int defaultValue = 0)
        {
            var value = controller.GetValue(key);
            int intValue = defaultValue;
            int.TryParse(value, out intValue);
            return intValue;
        }

        public static double GetDoubleParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            double doubleValue;
            if (double.TryParse(value, out doubleValue))
            {
                return doubleValue;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static double TryGetDoubleParameter(this Controller controller, string key, double defaultValue = 0)
        {
            var value = controller.GetValue(key);
            double doubleValue = defaultValue;
            double.TryParse(value, out doubleValue);
            return doubleValue;
        }

        public static long GetLongParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            long longValue;
            if (long.TryParse(value, out longValue))
            {
                return longValue;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static long TryGetLongParameter(this Controller controller, string key, long defaultValue = 0)
        {
            var value = controller.GetValue(key);
            long longValue = defaultValue;
            long.TryParse(value, out longValue);
            return longValue;
        }

        public static decimal GetDecimalParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            decimal decimalValue;
            if (decimal.TryParse(value, out decimalValue))
            {
                return decimalValue;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static decimal TryGetDecimalParameter(this Controller controller, string key, decimal defaultValue = 0)
        {
            var value = controller.GetValue(key);
            decimal decimalValue = defaultValue;
            decimal.TryParse(value, out decimalValue);
            return decimalValue;
        }

        public static DateTime GetDateTimeParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            DateTime dateTimeValue;
            if (DateTime.TryParse(value, out dateTimeValue))
            {
                return dateTimeValue;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static DateTime TryGetDateTimeParameter(this Controller controller, string key, DateTime? defaultValue = null)
        {
            var value = controller.GetValue(key);
            DateTime dateTimeValue = defaultValue == null || !defaultValue.HasValue ? DateTime.MinValue : defaultValue.Value;
            DateTime.TryParse(value, out dateTimeValue);
            return dateTimeValue;
        }


        /// <summary>
        /// Json扩展，替代Json
        /// </summary>
        public static IActionResult JsonEx(this Controller controller, bool result, string msg, object json)
        {
            var value = new
            {
                result = result,
                msg = msg,
                json = json
            };
            ContentResult cr = new ContentResult();
            cr.Content = Newtonsoft.Json.JsonConvert.SerializeObject(value, JsonConverters.All);
            cr.ContentType = "application/json";
            return cr;

        }

        /// <summary>
        /// Json扩展，替代Json
        /// </summary>
        public static IActionResult JsonEx(this Controller controller, object json)
        {
            return new ContentResult
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(json, JsonConverters.All),
                ContentType = "application/json"
            };
        }


        #region Bson

        public static ObjectId GetObjectIdParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            var _id = ObjectId.Empty;
            if (ObjectId.TryParse(value, out _id))
            {
                return _id;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static ObjectId TryGetObjectIdParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            var _id = ObjectId.Empty;
            ObjectId.TryParse(value, out _id);
            return _id;
        }

        public static BsonDocument GetBsonDocumentParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            var bson = new BsonDocument();
            if (BsonDocument.TryParse(value, out bson))
            {
                return bson;
            }
            else
            {
                throw new ArgumentException("Value cannot be empty.", key);
            }
        }
        public static BsonDocument TryGetBsonDocumentParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);
            var bson = new BsonDocument();
            BsonDocument.TryParse(value, out bson);
            return bson;
        }

        public static Dictionary<string, BsonValue> GetDictionaryStringBsonValueParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);

            return BsonSerializer.Deserialize<Dictionary<string, BsonValue>>(value);
        }
        public static Dictionary<string, BsonValue> TryGetDictionaryStringBsonValueParameter(this Controller controller, string key)
        {
            var value = controller.GetValue(key);
            Ensure.IsNotNullOrEmpty(value, key);

            var d = new Dictionary<string, BsonValue>();
            try
            {
                d = BsonSerializer.Deserialize<Dictionary<string, BsonValue>>(value);
            }
            catch { }

            return d;
        }



        #endregion


        #region Cookies

        public static void SetCookie(this Controller controller, string key, string value, CookieOptions options = null)
        {
            options = options ?? new CookieOptions();
            options.HttpOnly = false;

            controller.Response.Cookies.Delete(key);
            controller.Response.Cookies.Append(key, value, options);
        }

        #endregion

    }
}
