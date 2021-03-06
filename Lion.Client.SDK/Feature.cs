﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lion.Client.SDK.Models;

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
            if (feature_on_targeting_users.Any(i => i.Key.Equals(user.Key)))
            {
                return true;
            }
            var feature_off_targeting_users = _flag.FeatureOffTargetingUsers;
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
                    if (condition.UserAttribute.DataType.Equals("group"))
                    {
                        var segment = (from item in _flag.FeatureFlagSegments where item.SegmentGuid.ToString().Equals(condition.ExpectValue) select item).FirstOrDefault();
                        if (segment == null)
                        {
                            isMatchRule = false;
                            break;
                        }
                        else
                        {
                            var userBelongSegment = CheckUserBelongSegment(user, segment);
                            if ((!userBelongSegment && condition.UserAttribute.Name.Equals("用户在组中")) || (userBelongSegment && condition.UserAttribute.Name.Equals("用户不在组中")))
                            {
                                isMatchRule = false;
                                break;
                            }
                        }
                    }
                    else
                    {
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
                            isMatchRule = LionMatchOperator.Match(condition.Operation, attributeValue, condition.ExpectValue);
                            #endregion
                        }
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

        private bool CheckUserBelongSegment(LionUser user, SegmentTargetingObj segment)
        {
            var includeUserKeys = (from item in segment.IncludeUsers select item.Key).ToList();
            if (includeUserKeys.Contains(user.Key))
            {
                return true;
            }
            var excludeUserKeys = (from item in segment.ExcludeUsers select item.Key).ToList();
            if (excludeUserKeys.Contains(user.Key))
            {
                return false;
            }
            foreach (var rule in segment.Rules)
            {
                if (MatchRule(user, rule))
                {
                    return true;
                }
            }
            return false;
        }

        private bool MatchRule(LionUser user, RuleObj rule)
        {
            foreach (var condition in rule.Conditions)
            {
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
                    return false;
                }
                else
                {
                    var attributeValue = user.Custom[attributeName].ToString();
                    if (!LionMatchOperator.Match(condition.Operation, attributeValue, condition.ExpectValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        
    }
}
