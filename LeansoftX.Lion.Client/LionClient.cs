using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeansoftX.Lion.Client
{
    public class LionClient
    {
        private string _sdkKey { get; set; }

        private const string LionAPIURL = "http://lion-test.devcloudx.com/api/";

        public LionClient(string sdkKey)
        {
            _sdkKey = sdkKey;
        }

        public bool BoolVariation(string key)
        {
            return bool.Parse(new HttpClient().GetStringAsync(string.Format("{0}FlagStatuses/{1}/{2}", LionAPIURL, key, _sdkKey)).Result);
        }
    }
}