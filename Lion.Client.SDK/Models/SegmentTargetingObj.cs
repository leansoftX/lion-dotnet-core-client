using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class SegmentTargetingObj
    {
        public int ID { get; set; }

        public Guid SegmentGuid { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        #region Targeting

        public Guid EnvironmentGuid { get; set; }

        public IList<User> IncludeUsers { get; set; }

        public IList<User> ExcludeUsers { get; set; }

        public IList<RuleObj> Rules { get; set; }

        public IList<string> DeleteRules { get; set; }
        #endregion
    }
}
