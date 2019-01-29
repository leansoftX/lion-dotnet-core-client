using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SDKKey", _sdkKey);
        }

        public bool BoolVariation(string key)
        {
            var flagStatusAPI = string.Format("{0}/FlagStatuses/{1}", DefaultAPIUri, key);
            var result = _httpClient.GetStringAsync(flagStatusAPI).Result;
            return bool.Parse(result);
        }

        public bool BoolVariation(string key, LionUser user, bool defaultValue = false)
        {
            
            var requestUrl = string.Format("{0}/Flags/{1}", DefaultAPIUri, key);
            var response =  _httpClient.GetAsync(requestUrl);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                //send user request event and save user
                sendFlagRequestEvent(key, user);
                return defaultValue;

            }

            var result = response.Result.Content.ReadAsStringAsync().Result;
            var flag= JsonConvert.DeserializeObject<Models.FeatureFlagTargeting>(result);

            var feature = new Feature(flag);
            return feature.Evaluate(user);

        }


        private void sendFlagRequestEvent(string key, LionUser user)
        {
            var userAPI = string.Format("{0}/User", DefaultAPIUri);
            var httpContent=new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync(userAPI, httpContent);
        }


    }
}