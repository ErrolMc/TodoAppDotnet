using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoAppFrontend.Http
{
    public static class HttpHelper
    {
        public static async Task<PostResponse<U>> PostAsync<T, U>(this HttpClient httpClient, string url, T request) 
            where T : class 
            where U : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            var postResponse = new PostResponse<U>
            {
                HttpResponse = response,
            };

            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                postResponse.Data = JsonConvert.DeserializeObject<U>(responseContent);
            }
            catch (Exception) { }

            return postResponse;
        }
    }
}