using clby.Core.Misc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace clby.Core.Mvc
{
    [DebuggerStepThrough]
    public static class HttpContextExtensions
    {
        public static Dictionary<string, string> GetCollections(this HttpContext context)
        {
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            Dict.AddRange(context.GetQueryCollections());
            if (context.Request.HasFormContentType)
            {
                Dict.AddRange(context.GetFormCollections());
            }
            return Dict;
        }

        public static Dictionary<string, string> GetFormCollections(this HttpContext context)
        {
            var r = context.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            if (r.HasFormContentType)
            {
                foreach (var item in r.Form)
                {
                    Dict.Add(item.Key, item.Value);
                }
            }
            return Dict;
        }
        public static Dictionary<string, string> GetQueryCollections(this HttpContext context)
        {
            var r = context.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Query)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }
        public static Dictionary<string, string> GetRequestHeaders(this HttpContext context)
        {
            var r = context.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Headers)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }
        public static Dictionary<string, string> GetResponseHeaders(this HttpContext context)
        {
            var r = context.Response;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Headers)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }

        public static Dictionary<string, string> GetCookies(this HttpContext context)
        {
            var r = context.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Cookies)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }

        public static Dictionary<string, BsonBinaryData> GetSession(this HttpContext context)
        {
            var s = context.Session;
            Dictionary<string, BsonBinaryData> Dict = new Dictionary<string, BsonBinaryData>();
            foreach (var key in s.Keys)
            {
                byte[] b;
                if (s.TryGetValue(key, out b))
                {
                    Dict.Add(key, new BsonBinaryData(b));
                }
            }
            return Dict;
        }
        public static Dictionary<string, string> GetUserInfo(this HttpContext context, string key = "UserId")
        {
            var s = context.Session;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            Dict.Add(key, s.GetString(key));
            return Dict;
        }

    }
}
