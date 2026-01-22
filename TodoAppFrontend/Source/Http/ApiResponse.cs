using System.Net.Http;
using TodoAppFrontend.Source;

namespace TodoAppFrontend.Http
{
    public class ApiResponse<T> : Result<T> where T : class
    {
        public HttpResponseMessage HttpResponse { get; set; }

        protected ApiResponse(T data, HttpResponseMessage httpResponse)
            : base(data)
        {
            this.HttpResponse = httpResponse;
        }

        protected ApiResponse(ResultType resultType, string errorMessage, HttpResponseMessage httpResponse = null)
            : base(resultType, errorMessage)
        {
            this.HttpResponse = httpResponse;
        }

        public static ApiResponse<T> Success(T data, HttpResponseMessage httpResponse) => new ApiResponse<T>(data, httpResponse);
        public static ApiResponse<T> Failure(ResultType resultType, string errorMessage, HttpResponseMessage httpResponse = null) => new ApiResponse<T>(resultType, errorMessage, httpResponse);

        public new bool IsSuccessful => base.IsSuccessful && HttpResponse != null && HttpResponse.IsSuccessStatusCode;
    }
}