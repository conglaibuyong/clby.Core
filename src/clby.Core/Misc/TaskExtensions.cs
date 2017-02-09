using System.Threading.Tasks;

namespace clby.Core.Misc
{
    public static class TaskExtensions
    {
        public static void IgnoreExceptions(this Task task)
        {
            task.ContinueWith(t => { var ignored = t.Exception; },
                TaskContinuationOptions.OnlyOnFaulted |
                TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
