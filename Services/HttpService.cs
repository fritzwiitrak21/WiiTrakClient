using System.Text.Json;
using System.Text;
using WiiTrakClient.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using WiiTrakClient.DTOs;


namespace WiiTrakClient.Services
{
    public class HttpService: IHttpService
    {
        private readonly HttpClient httpClient;
        private readonly string token;

        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public HttpService(IHttpClientFactory clientFactory)
        {
            this.httpClient = clientFactory.CreateClient("WebAPI");
        }

        public string BaseUrl => httpClient?.BaseAddress?.AbsoluteUri;

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            //HttpClientHeaders(token);
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, responseHTTP);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> PostForm(string url, MultipartFormDataContent content)
        {
            var response = await httpClient.PostAsync(url, content);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, false, response);
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Patch(string url, string data)
        {
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options);
        }

        private  void HttpClientHeaders(string token = "")
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        //public async Task<string> AuthenticateAsync(LoginDto login)
        //{
        //    LoginDto logindto = new LoginDto
        //    {
        //        Username= login.Username,
        //        Password = login.Password,
        //    };

        //    CancellationTokenSource cancellationToken = new CancellationTokenSource();

        //    const string postLogin = "Authenticate/Login";

        //    return await Postabc<string, LoginDto>(postLogin, logindto).ConfigureAwait(false);
        //}

        //public async Task<HttpResponseWrapper<TResponse>> Postabc<T, TResponse>(string url, T data)
        //{
        //    var dataJson = JsonSerializer.Serialize(data);
        //    var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(url, stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
        //        return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
        //    }
        //    else
        //    {
        //        return new HttpResponseWrapper<TResponse>(default, false, response);
        //    }
        //}

        //private async Task<TResult> SendPostEntityAsync<TResult, TRequest>(string controller, TRequest data, bool isDeviceConfig, TResult tResult)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(AppConstants.Host))
        //        {
        //            var content = new StringContent(JsonConvert.SerializeObject(data));
        //            content.Headers.ContentType = new MediaTypeHeaderValue(JSON);
        //            var response = await Client.PostAsync(new Uri(AppConstants.Host + controller), content).ConfigureAwait(false);

        //            await HandleResponseAsync(response).ConfigureAwait(false);
        //            string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            TResult result = await Task.Run(() =>
        //                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings)).ConfigureAwait(false);

        //            return result;
        //        }
        //    }
        //    catch (ArithmeticException)
        //    {
        //        return tResult;
        //    }
        //    return default;
        //}'
    }
}
