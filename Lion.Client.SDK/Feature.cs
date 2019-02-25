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
        public bool Evaluate(LionUser user, bool defaultValue)
        {
            return MatchesUserTarget(user, defaultValue);
        }

        private bool MatchesUserTarget(LionUser user, bool defaultValue)
        {
            #region Targeting Off
            if (!_flag.IsTargeting)
            {
                var targetingOffVairation = (from item in _flag.FeatureFlagVariations where item.FeatureFlagVariationGuid.Equals(_flag.TargetingOffVariation) select item).FirstOrDefault();
                if (targetingOffVairation != null)
                {
                    return Boolean.Parse(targetingOffVairation.Value);
                }
                else
                {
                    return defaultValue;
                }
            }
            #endregion

            #region Targeting User
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
            #endregion

            #region Targeting Rule
            foreach (var rule in _flag.Rules)
            {
                var isMatchRule = true;
                foreach (var condition in rule.Conditions)
                {
                    if (!isMatchRule)
                    {
                        break;
                    }
                    var hasAttribute = false;
                    var attributeName = "";
                    foreach (var key in user.Custom.Keys)
                    {
                        if (key.Equals(condition.UserAttribute.Name))
                        {
                            hasAttribute = true;
                            attributeName = key;
                            break;
                        }
                    }
                    if (!hasAttribute)
                    {
                        isMatchRule = false;
                        break;
                    }
                    else
                    {
                        #region Get User Attibute Value
                        var attributeValue = user.Custom[attributeName].ToString();
                        isMatchRule = MatchCondition(condition.Operation, attributeValue, condition.ExpectValue);
                        #endregion
                    }
                }
                //All conditions are passed
                if (isMatchRule)
                {
                    var matchedVariation = (from item in _flag.FeatureFlagVariations where item.FeatureFlagVariationGuid.Equals(rule.VariationGuid) select item).FirstOrDefault();
                    if (matchedVariation != null)
                    {
                        return Boolean.Parse(matchedVariation.Value);
                    }
                }
            }
            #endregion

            #region Default
            var defaultVariation = (from item in _flag.FeatureFlagVariations where item.FeatureFlagVariationGuid.Equals(_flag.DefaultVariation) select item).FirstOrDefault();
            if (defaultVariation != null)
            {
                return Boolean.Parse(defaultVariation.Value);
            }
            else
            {
                return defaultValue;
            }
            #endregion
        }

        private bool MatchCondition(string logicOperator, string firstValue, string secondValue)
        {
            var result = true;
            switch (logicOperator)
            {
                case "greater_than":
                    try
                    {
                        var convertedFirstValue = double.Parse(firstValue);
                        var convertedSecondValue = double.Parse(secondValue);
                        if (convertedFirstValue <= convertedSecondValue)
                        {
                            result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                    break;
                case "less_than":
                    try
                    {
                        var convertedFirstValue = double.Parse(firstValue);
                        var convertedSecondValue = double.Parse(secondValue);
                        if (convertedFirstValue >= convertedSecondValue)
                        {
                            result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                    break;
                case "equal":
                    if (!firstValue.Equals(secondValue))
                    {
                        result = false;
                    }
                    break;
                case "contain":
                    if (!firstValue.Contains(secondValue))
                    {
                        result = false;
                    }
                    break;
                case "starts_with":
                    if (!firstValue.StartsWith(secondValue))
                    {
                        result = false;
                    }
                    break;
                case "ends_with":
                    if (!firstValue.EndsWith(secondValue))
                    {
                        result = false;
                        break;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
