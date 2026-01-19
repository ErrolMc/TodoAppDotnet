using System.Net.Http;

namespace TodoAppFrontend.Http
{
    public class PostResponse<T> where T : class
    {
        public HttpResponseMessage HttpResponse { get; set; }
        public T Data { get; set; }

        public bool IsSuccessful => HttpResponse.IsSuccessStatusCode;
    }
}