using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK
{
    public static class LionMatchOperator
    {
        public static bool Match(string logicOperator, string firstValue, string secondValue)
        {
            var result = true;
            switch (logicOperator)
            {
                case "greater_than":
                    result = GreaterThan(firstValue, secondValue);
                    break;
                case "less_than":
                    result = LessThan(firstValue, secondValue);
                    break;
                case "equal":
                    result = Equals(firstValue, secondValue);
                    break;
                case "contain":
                    result = Contains(firstValue, secondValue);
                    break;
                case "starts_with":
                    result = StartWith(firstValue, secondValue);
                    break;
                case "ends_with":
                    result = Endwith(firstValue, secondValue);
                    break;
                default:
                    break;
            }
            return result;
        }

        public static bool GreaterThan(string firstValue, string secondValue)
        {
            try
            {
                return double.Parse(firstValue) > double.Parse(secondValue);
            }
            catch
            {
                return false;
            }
        }

        public static bool LessThan(string firstValue, string secondValue)
        {
            try
            {
                return double.Parse(firstValue) < double.Parse(secondValue);
            }
            catch
            {
                return false;
            }
        }

        public static bool Equals(string firstValue, string secondValue)
        {
            return firstValue.Equals(secondValue);
        }

        public static bool Contains(string firstValue, string secondValue)
        {
            return firstValue.Contains(secondValue);
        }

        public static bool StartWith(string firstValue, string secondValue)
        {
            return firstValue.StartsWith(secondValue);
        }

        public static bool Endwith(string firstValue, string secondValue)
        {
            return firstValue.EndsWith(secondValue);
        }
    }
}
