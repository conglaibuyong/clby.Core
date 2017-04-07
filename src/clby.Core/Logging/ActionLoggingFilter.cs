using Microsoft.AspNetCore.Mvc.Filters;
using System;
using clby.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.Extensions.Options;

namespace clby.Core.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ActionLoggingFilter : Attribute, IActionFilter
    {
        static ILog logger =
            LogManager.GetLogger(Log4Helper.LoggerRepository.Name, "ActionLogs");

        private ActionLoggingOptions Options = null;

        public ActionLoggingFilter(IOptions<ActionLoggingOptions> options)
        {
            Options = options?.Value ?? default(ActionLoggingOptions);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var log = new ActionLog();

            log.ControllerName = context.RouteData.Values["controller"] as string;
            log.ActionName = context.RouteData.Values["action"] as string;

            log.Cookies = context.GetCookies();

            if (Options.HasSession)
            {
                log.Session = context.GetSession();
                log.SessionId = context.HttpContext.Session?.Id;

                log.UserInfo = context.GetUserInfo(Options.UserInfoKey);
            }

            log.FormCollections = context.GetFormCollections();
            log.QueryCollections = context.GetQueryCollections();
            log.RequestHeaders = context.GetRequestHeaders();
            log.ResponseHeaders = context.GetResponseHeaders();

            context.HttpContext.Response.Headers.Add("LogId", log._id.ToString());

            context.HttpContext.Items["___ActionLoggingFilter___"] = log;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var log = context.HttpContext.Items["___ActionLoggingFilter___"] as ActionLog;
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

            logger.Info(log);
        }

    }
}
