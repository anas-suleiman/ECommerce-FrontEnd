using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Shared
{
    public abstract class BaseSdkClient
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl;
        protected string BaseUrl
        {
            set
            {
                _baseUrl = value;
                if (_baseUrl.EndsWith("/"))
                {
                    _baseUrl = _baseUrl[0..^1];
                }
            }
        }
        public BaseSdkClient(string baseUrl, HttpClient httpClient)
        {
            BaseUrl = baseUrl;
            _httpClient = httpClient;
        }

        protected virtual async Task<T> SendJsonRequest<T>(HttpMethod method, string url, object payload)
        {
            try
            {
                var json = JsonConvert.SerializeObject(payload);
 

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(method, BuildUrl(url)) { Content = content };

                var response = await _httpClient.SendAsync(request);
 
                var result =  await ConvertJsonResult<T>(response);


                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        protected virtual async Task<T> ConvertJsonResult<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Request unsuccessful! Status Code 500. ResponseText: " + await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception($"Request unsuccessful! Status Code: {response.StatusCode}");
                }
            }

            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseText) ?? throw new Exception("Received invalid response from service");
        }

        private string BuildUrl(string url)
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new ArgumentException("BaseUrl is not set!");
            }
            return url.StartsWith("/") ? $"{_baseUrl}{url}" : $"{_baseUrl}/{url}";
        }
    }
}
