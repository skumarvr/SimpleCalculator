using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest.App
{
    class CalculatorTestApiClient : ICalculatorTestClient
    {
        class ApiError
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }
        }

        readonly string _baseUrl = "";
        readonly string _addEndpoint = "api/Calculator/Add?start={0}&amount={1}";
        readonly string _subtractEndpoint = "api/Calculator/Subtract?start={0}&amount={1}";
        readonly string _multiplyEndpoint = "api/Calculator/Multiply?start={0}&by={1}";
        readonly string _divideEndpoint = "api/Calculator/Divide?start={0}&by={1}";

        HttpClient client = new HttpClient();

        public CalculatorTestApiClient(string baseUrl)
        {
            _baseUrl = baseUrl + (baseUrl.EndsWith("/") ? "" : "/");
        }

        public async Task AddAsync(int start, int amount)
        {
            Console.WriteLine($"API : Add request : {start}, {amount}");
            var url = _baseUrl + string.Format(_addEndpoint, start, amount);
            await InvokeApiUrl(url);
        }

        public async Task SubtractAsync(int start, int amount)
        {
            Console.WriteLine($"API : Subtract request : {start}, {amount}");
            var url = _baseUrl + string.Format(_subtractEndpoint, start, amount);
            await InvokeApiUrl(url);
        }

        public async Task MultiplyAsync(int start, int by)
        {
            Console.WriteLine($"API : Multiply request : {start}, {by}");
            var url = _baseUrl + string.Format(_multiplyEndpoint, start, by); ;
            await InvokeApiUrl(url);
        }

        public async Task DivideAsync(int start, int by)
        {
            Console.WriteLine($"API : Divide request : {start}, {by}");
            var url = _baseUrl + string.Format(_divideEndpoint, start, by);
            await InvokeApiUrl(url);
        }

        private async Task InvokeApiUrl(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API : Result : {result}");
                }
                else
                {
                    var apiError = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiError>(result);
                    Console.WriteLine($"API : Error : {apiError.Message}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"API : Exception : {ex.Message}");
            }
        }
    }
}
