using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class FeatureFlagVariationObj
    {
        public Guid FeatureFlagVariationGuid { get; set; }

        public string Value { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
