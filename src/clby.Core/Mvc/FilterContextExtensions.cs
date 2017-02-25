using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace clby.Core.Mvc
{
    public static class FilterContextExtensions
    {
        public static Dictionary<string, string> GetFormCollections(this FilterContext context)
        {
            var r = context.HttpContext.Request;
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
        public static Dictionary<string, string> GetQueryCollections(this FilterContext context)
        {
            var r = context.HttpContext.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Query)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }
        public static Dictionary<string, string> GetRequestHeaders(this FilterContext context)
        {
            var r = context.HttpContext.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Headers)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }
        public static Dictionary<string, string> GetResponseHeaders(this FilterContext context)
        {
            var r = context.HttpContext.Response;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Headers)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }

        public static Dictionary<string, string> GetCookies(this FilterContext context)
        {
            var r = context.HttpContext.Request;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            foreach (var item in r.Cookies)
            {
                Dict.Add(item.Key, item.Value);
            }
            return Dict;
        }

        public static Dictionary<string, BsonBinaryData> GetSession(this FilterContext context)
        {
            var s = context.HttpContext.Session;
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
        public static Dictionary<string, string> GetUserInfo(this FilterContext context, string key = "UserId")
        {
            var s = context.HttpContext.Session;
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            Dict.Add(key, s.GetString(key));
            return Dict;
        }

    }
}
