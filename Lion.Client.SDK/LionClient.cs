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

        /// <summary>
        /// variate the bool type feature flag
        /// </summary>
        /// <param name="key">feature flag key</param>
        /// <param name="user">the business user which will sent to the lion service</param>
        /// <param name="defaultValue">the default return value when there are any kind of exception occurred</param>
        /// <returns></returns>
        public bool BoolVariation(string key, LionUser user, bool defaultValue = false)
        {
            try
            {
                var feedbackUser = new LionUser(user.Key);
                //send user request event and save user
                if (!string.IsNullOrEmpty(user.Key))
                {
                    feedbackUser = SendFlagRequestEvent(key, user);
                }

                var requestUrl = string.Format("{0}/Flags/{1}", DefaultAPIUri, key);
                var response = _httpClient.GetAsync(requestUrl);

                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    return defaultValue;
                }

                var result = response.Result.Content.ReadAsStringAsync().Result;
                var flag = JsonConvert.DeserializeObject<Models.FeatureFlagTargeting>(result);

                var feature = new Feature(flag);
                return feature.Evaluate(feedbackUser, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        private LionUser SendFlagRequestEvent(string key, LionUser user)
        {
            var userAPI = string.Format("{0}/User", DefaultAPIUri);
            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(userAPI, httpContent);
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                return new LionUser(user.Key)
                {
                    Name = user.Name,
                    Email = user.Email,
                    Custom = user.Custom,
                };
            }
            var result = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<LionUser>(result);
        }
    }
}