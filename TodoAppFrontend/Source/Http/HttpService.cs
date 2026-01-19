using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TodoAppFrontend.Source;
using TodoAppShared;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace TodoAppFrontend.Source
{
    public class HttpService
    {
        protected static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:53667/")
        };

        public async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, content);
        }

        public static async Task<T> DeserialiseContent<T>(HttpContent content) where T : class
        {
            string responseString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}