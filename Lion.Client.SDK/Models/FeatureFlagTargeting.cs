using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class FeatureFlagTargeting
    {
        public int ID { get; set; }

        public Guid FeatureFlagGuid { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        #region Targeting

        public Guid EnvironmentGuid { get; set; }

        public bool IsTargeting { get; set; }

        public Guid DefaultVariation { get; set; }

        public Guid TargetingOffVariation { get; set; }

        public IList<User> FeatureOnTargetingUsers { get; set; }

        public IList<User> FeatureOffTargetingUsers { get; set; }

        public IList<RuleObj> Rules { get; set; }

        public IList<FeatureFlagVariationObj> FeatureFlagVariations { get; set; }

        #endregion
    }

}
