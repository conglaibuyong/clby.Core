using Microsoft.Extensions.Options;

namespace clby.Core.Logging
{
    public class ActionLoggingOptions : IOptions<ActionLoggingOptions>
    {

        public bool HasSession { get; set; } = false;

        public string UserInfoKey { get; set; } = "UserId";


        ActionLoggingOptions IOptions<ActionLoggingOptions>.Value
        {
            get
            {
                return this;
            }
        }
    }
}
