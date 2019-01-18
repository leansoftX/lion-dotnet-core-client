using System;
using System.Net.Http;
using System.Threading.Tasks;

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
        }

        public bool BoolVariation(string key)
        {
            var flagStatusAPI = string.Format("{0}/FlagStatuses/{1}/{2}", DefaultAPIUri, key, _sdkKey);
            var result = _httpClient.GetStringAsync(flagStatusAPI).Result;
            return bool.Parse(result);
        }


    }
}