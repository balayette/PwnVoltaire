using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

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
            // fuck XML
            if (!xml.Contains("errors"))
                return ret;

            var messages = Regex.Matches(xml, "<message>(.*?)</message>");
            var errors = Regex.Matches(xml, "<error (.*?)>");
            for (int i = 0; i < messages.Count; i++)
            {
                ret.Add(new Error(messages[i].Value, errors[i].Value));
            }
            return ret;
        }
    }
}
