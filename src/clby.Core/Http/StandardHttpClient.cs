using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace clby.Core.Http
{
    public class StandardHttpClient
    {
        private HttpClient _client;
        private ILogger<StandardHttpClient> _logger;
        public HttpClient Inst => _client;

        public StandardHttpClient(ILogger<StandardHttpClient> logger)
        {
            _client = new HttpClient();
            _logger = logger;
        }

        public Task<HttpResponseMessage> GetAsync(string uri)
            => _client.GetAsync(uri);
        public Task<HttpResponseMessage> DeleteAsync(string uri) 
            => _client.DeleteAsync(uri);
        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content) 
            => _client.PostAsync(uri, content);

    }
}
