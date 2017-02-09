using System;

namespace clby.Core.Misc
{
    public sealed class ActivityContextScope : IDisposable
    {
        private ActivityContext current = ActivityContext.Current;
        private ActivityContext newContext;

        public ActivityContextScope(string activityName,
            ContextScopeOption contextScopeOption = ContextScopeOption.Required)
        {
            switch (contextScopeOption)
            {
                case ContextScopeOption.Required:
                    {
                        if (null == current)
                        {
                            ActivityContext.Current = newContext = new ActivityContext(activityName);
                        }
                        break;
                    }
                case ContextScopeOption.RequiresNew:
                    {
                        ActivityContext.Current = newContext = new ActivityContext(activityName);
                        break;
                    }
                case ContextScopeOption.Suppress:
                    {
                        ActivityContext.Current = null;
                        break;
                    }
            }
        }

        public void Dispose()
        {
            ActivityContext.Current = current;
            if (null != current)
            {
                newContext.Dispose();
            }
        }
    }

    public enum ContextScopeOption
    {
        Required,
        RequiresNew,
        Suppress
    }
}
