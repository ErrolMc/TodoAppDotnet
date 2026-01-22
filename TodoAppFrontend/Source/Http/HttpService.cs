using System;
using System.Net.Http;

namespace TodoAppFrontend.Source
{
    public class HttpService
    {
        protected static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:53667/")
        };
    }
}