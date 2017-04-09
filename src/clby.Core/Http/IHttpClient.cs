using System.Net.Http;
using System.Threading.Tasks;

namespace clby.Core.Http
{
    public interface IHttpClient
    {
        HttpClient Inst { get; }

        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> DeleteAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
    }
}
