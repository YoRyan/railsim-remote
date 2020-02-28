using System;

namespace RailSimRemote.Models
{
    public struct LocoName
    {
        public string Provider;
        public string Product;
        public string Engine;
        public LocoName(string str)
        {
            string[] split = str.Split(new string[] { ".:." }, 3, StringSplitOptions.None);
            if (split.Length < 3)
            {
                Provider = Product = Engine = "";
            }
            else
            {
                Provider = split[0];
                Product = split[1];
                Engine = split[2];
            }
        }
    }
}