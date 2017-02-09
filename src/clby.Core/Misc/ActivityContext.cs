using System;
using System.Collections.Generic;

namespace clby.Core.Misc
{
    public sealed class ActivityContext : IDisposable
    {
        public string ActivityName { get; private set; }
        public DateTime StartTime { get; private set; }
        public IDictionary<string, object> Properties { get; private set; }

        [ThreadStatic]
        private static ActivityContext Context;

        public ActivityContext(string activityName)
        {
            this.ActivityName = activityName;
            this.StartTime = DateTime.Now;
            this.Properties = new Dictionary<string, object>();
        }

        public static ActivityContext Current
        {
            get
            {
                return Context;
            }
            internal set
            {
                Context = value;
            }
        }

        public void Dispose()
        {
            foreach (var property in this.Properties.Values)
            {
                var disposable = property as IDisposable;
                if (null != disposable)
                {
                    disposable.Dispose();
                }
            }
        }

    }
}
