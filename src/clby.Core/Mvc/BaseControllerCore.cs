using clby.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace clby.Core.Mvc
{
    public abstract class BaseControllerCore : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Prepare();
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Prepare();
            return base.OnActionExecutionAsync(context, next);
        }

        public virtual void Prepare()
        { }
    }
    
}
