using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace PwnVoltaire
{
    public static class PwnVoltaire
    {
        private static string startStr = "<error id=\"";
        private static string endStr = "</error>";

        public static List<Error> GetReadableOutput(string s)
        {
            
            var xml = HttpUtility.HtmlDecode(s);
            
            var ret = new List<Error>();
            //var errors = Regex.Matches(xml, "<error nb=\" \">(.*?)</errors>")
            
            /*
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            foreach(XmlElement sentence in doc.GetElementsByTagName("error"))
            {
                Console.WriteLine("Error : ");
                Console.WriteLine(sentence.GetAttribute("type"));
            }
            */
            

            /*
            // fuck XML
            if (!xml.Contains("errors"))
                return ret;
                */
            Console.WriteLine("Contains errors");
            var messages = Regex.Matches(xml, "<message>(.*?)</message>");
            var errors = Regex.Matches(xml, "<error (.*?)>");
            for (int i = 0; i < messages.Count; i++)
            {
                Console.WriteLine(messages[i].Value);
                Console.WriteLine(errors[i].Value);
                ret.Add(new Error(messages[i].Value, errors[i].Value));
            }
            return ret;
        }
    }
}
