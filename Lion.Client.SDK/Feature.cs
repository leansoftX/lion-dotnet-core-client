using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Lion.Client.SDK
{
    public class Feature
    {
        public Models.FeatureFlagTargeting _flag;
        public Feature(Models.FeatureFlagTargeting flag)
        {
            _flag = flag;
        }
        public bool Evaluate(LionUser user)
        {
            return MatchesUserTarget(user);
        }

        private bool MatchesUserTarget(LionUser user)
        {
            var feature_on_targeting_users = _flag.FeatureOnTargetingUsers;
            var feature_off_targeting_users = _flag.FeatureOffTargetingUsers;
            if (feature_on_targeting_users.Any(i => i.Key.Equals(user.Key)))
            {
                return true;
            }

            if (feature_off_targeting_users.Any(i => i.Key.Equals(user.Key)))
            {
                return false;
            }

            return _flag.IsTargeting;


        }
    }
}
