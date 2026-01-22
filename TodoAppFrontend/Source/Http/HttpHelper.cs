using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TodoAppFrontend.Source;

namespace TodoAppFrontend.Http
{
    public static class HttpHelper
    {
        public static async Task<ApiResponse<U>> PostAsync<T, U>(this HttpClient httpClient, string url, T request) 
            where T : class 
            where U : class
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                return await ProcessResponseAsync<U>(response);
            }
            catch (Exception)
            {
                // http request failed
                return ApiResponse<U>.Failure(ResultType.HttpFailure, "Http request failed");
            }
        }

        public static async Task<ApiResponse<T>> GetAsync<T>(this HttpClient httpClient, string url) 
            where T : class
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                return await ProcessResponseAsync<T>(response);
            }
            catch (Exception)
            {
                // http request failed
                return ApiResponse<T>.Failure(ResultType.HttpFailure, "Http request failed");
            }
        }

        public static async Task<Result> DeleteAsync(this HttpClient httpClient, string url)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    return Result.Success();
                return Result.Failure(response.ReasonPhrase);
            }
            catch (Exception)
            {
                // http request failed
                return Result.Failure(ResultType.HttpFailure, "Http request failed");
            }
        }

        public static async Task<ApiResponse<U>> PutAsync<T, U>(this HttpClient httpClient, string url, T request) 
            where T : class 
            where U : class
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync(url, content);

                return await ProcessResponseAsync<U>(response);
            }
            catch (Exception)
            {
                // http request failed
                return ApiResponse<U>.Failure(ResultType.HttpFailure, "Http request failed");
            }
        }

        private static async Task<ApiResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage responseMessage) 
            where T : class
        {
            if (!responseMessage.IsSuccessStatusCode)
                return ApiResponse<T>.Failure(ResultType.UnknownFailure, responseMessage.ReasonPhrase, responseMessage);

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            T data = null;

            try
            {
                data = JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception) 
            {
                // deserialization failed
            }

            return ApiResponse<T>.Success(data, responseMessage);
        }
    }
}