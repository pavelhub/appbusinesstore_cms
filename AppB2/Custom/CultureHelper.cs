using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppB2.Custom
{
    public class CultureHelper
    {
        private static readonly Dictionary<string,string> _cultures = new Dictionary<string,string>() {
            {"English","en"},
            {"Русский","ru"}         
        };

        public static string GetValidCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture();

            if (_cultures.Values.Contains(name))
                return name;
     
            /*foreach (var c in _cultures.Keys)
                if (c.StartsWith(name.Substring(0, 2)))
                    return c;*/
          
            return GetDefaultCulture();
        }

        public static Dictionary<string, string> GetAll()
        {
            return _cultures;
        }

        //ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4
        //en-US
        public static string GetHeaderLang(string data)
        {
            if (!string.IsNullOrEmpty(data) && data.Length >= 2)
            {
                //string lang_part = data.Split(';').First();
                string lang = data.Substring(0, 2);//lang_part.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Last();
                return GetValidCulture(lang);
            }
            else return GetDefaultCulture();
        }

        public static string GetDefaultCulture()
        {
            return _cultures.Values.First();
        }
    }
}