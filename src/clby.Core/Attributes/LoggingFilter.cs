﻿using clby.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using clby.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace clby.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoggingFilter : Attribute, IActionFilter
    {
        //static ILog logger = LogManager.GetLogger("", "Logs");

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var log = new ActionLog();

            log.ControllerName = context.RouteData.Values["controller"] as string;
            log.ActionName = context.RouteData.Values["action"] as string;

            log.Cookies = context.GetCookies();
            log.Session = context.GetSession();
            log.UserInfo = context.GetUserInfo();
            log.SessionId = context.HttpContext.Session?.Id;
            log.FormCollections = context.GetFormCollections();
            log.QueryCollections = context.GetQueryCollections();
            log.RequestHeaders = context.GetRequestHeaders();
            log.ResponseHeaders = context.GetResponseHeaders();

            context.HttpContext.Response.Headers.Add("LogId", log._id.ToString());

            context.HttpContext.Items["___ActionLog___"] = log;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var log = context.HttpContext.Items["___ActionLog___"] as ActionLog;
            if (log == null) return;

            log.ActionEndTime = DateTime.Now;
            log.ActionTime = (log.ActionEndTime - log.ActionStartTime).TotalSeconds;
            if (context.Exception != null)
            {
                log.Exception = context.Exception.ToString();
            }
            else
            {
                var r = context.Result;
                if (r.GetType() == typeof(ContentResult))
                {
                    log.Result.Add("Content", (r as ContentResult)?.Content);
                }
            }

            //logger.Info(log);
        }

    }
}
