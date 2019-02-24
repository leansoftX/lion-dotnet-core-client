using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class ConditionObj
    {
        public AttributeObj UserAttribute { get; set; }

        public string Operation { get; set; }

        public string ExpectValue { get; set; }
    }
}
