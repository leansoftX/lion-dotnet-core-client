using System.Net.Http;
using System.Net.Http.Headers;

namespace Lion.Client.SDK
{
    public class LionClient
    {
        private string _sdkKey { get; set; }
        private readonly HttpClient _httpClient;


        private const string DefaultAPIUri = "http://lion-test.devcloudx.com/api";

        public LionClient(string sdkKey)
        {
            _sdkKey = sdkKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SDKKey", _sdkKey);
        }

        public bool BoolVariation(string key)
        {
            var flagStatusAPI = string.Format("{0}/FlagStatuses/{1}", DefaultAPIUri, key);
            var result = _httpClient.GetStringAsync(flagStatusAPI).Result;
            return bool.Parse(result);
        }
    }
}