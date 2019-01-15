using System;

namespace LeansoftX.Lion.Client
{
    public class LionClient
    {
        public bool BoolVariation(string key)
        {
            if (key == "true")
            {
                return true;
            }
            return false;
        }
    }
}
