using System;
using System.IO;
using System.Net;
using System.Text;

namespace LeansoftX.Lion.Client
{
    public class LionClient
    {
        private string _sdkKey { get; set; }

        private const string LionAPIURL = "http://lion-test.devcloudx.com/";

        public LionClient(string sdkKey)
        {
            _sdkKey = sdkKey;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public bool BoolVariation(string key)
        {
            var url = LionAPIURL + "/api/FlagStatuses/" + key + "/" + _sdkKey;
            var result = Get(url);
            return Boolean.Parse(result);
        }


        #region Api
        private string Get(string sURL)
        {
            using (HttpWebResponse webResponse = GetResponse(sURL))
            using (Stream stream = webResponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private string Post(string postUrl, string paramData)
        {
            return PostWebRequest(postUrl, paramData, new UTF8Encoding());
        }


        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            byte[] byteArray = dataEncode.GetBytes(paramData);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";

            webReq.ContentLength = byteArray.Length;
            using (Stream newStream = webReq.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        private HttpWebResponse GetResponse(string sUrl)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(sUrl);
            return (HttpWebResponse)webRequest.GetResponse();
        }
        #endregion

    }
}