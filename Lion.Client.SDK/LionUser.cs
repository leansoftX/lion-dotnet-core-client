using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lion.Client.SDK
{
    public class LionUser
    {


        [JsonProperty(PropertyName = "key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "custom", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JToken> Custom { get; set; }

        public LionUser(string key)
        {
            Key = key;
            Custom = new Dictionary<string, JToken>();

        }

        public static LionUser WithKey(string key)
        {
            return new LionUser(key);
        }


    }
    public static class UserExtensions
    {
        public static LionUser AndName(this LionUser user, string name)
        {
            user.Name = name;
            return user;
        }

        public static LionUser AndEmail(this LionUser user, string email)
        {
            user.Email = email;
            return user;
        }

        public static LionUser AndCustomAttribute(this LionUser user, string attribute, string value)
        {
            if (attribute == string.Empty)
                throw new ArgumentException("Attribute Name can not be empty");

            user.Custom.Add(attribute, new JValue(value));

            return user;
        }

        public static LionUser AndCustomAttribute(this LionUser user, string attribute, bool value)
        {
            if (attribute == string.Empty)
                throw new ArgumentException("Attribute Name can not be empty");

            user.Custom.Add(attribute, new JValue(value));

            return user;
        }

        public static LionUser AndCustomAttribute(this LionUser user, string attribute, int value)
        {
            if (attribute == string.Empty)
                throw new ArgumentException("Attribute Name can not be empty");

            user.Custom.Add(attribute, new JValue(value));

            return user;
        }

        public static LionUser AndCustomAttribute(this LionUser user, string attribute, float value)
        {
            if (attribute == string.Empty)
                throw new ArgumentException("Attribute Name can not be empty");

            user.Custom.Add(attribute, new JValue(value));

            return user;
        }

        public static LionUser AndCustomAttribute(this LionUser user, string attribute, List<string> value)
        {
            if (attribute == string.Empty)
                throw new ArgumentException("Attribute Name can not be empty");

            user.Custom.Add(attribute, new JArray(value.ToArray()));

            return user;
        }

    }
}