using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace clby.Core.Http
{
    public class PolicyHttpClient
    {
        private HttpClient _client;
        private PolicyWrap _policyWrapper;
        private ILogger<PolicyHttpClient> _logger;
        public HttpClient Inst => _client;

        public PolicyHttpClient(Policy[] policies, ILogger<PolicyHttpClient> logger)
        {
            _client = new HttpClient();
            _logger = logger;
            _policyWrapper = Policy.WrapAsync(policies);
        }

        private Task<T> Invoker<T>(Func<Task<T>> action)
            => _policyWrapper.ExecuteAsync(() => action());


        public Task<HttpResponseMessage> GetAsync(string uri)
            => Invoker(() => _client.GetAsync(uri));
        public Task<HttpResponseMessage> DeleteAsync(string uri)
            => Invoker(() => _client.DeleteAsync(uri));
        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
            => Invoker(() => _client.PostAsync(uri, content));

    }
}
