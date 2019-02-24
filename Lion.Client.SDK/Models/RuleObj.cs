using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class RuleObj
    {
        public Guid RuleGuid { get; set; }

        public string RuleName { get; set; }

        public Guid VariationGuid { get; set; }

        public List<ConditionObj> Conditions { get; set; }
    }
}
