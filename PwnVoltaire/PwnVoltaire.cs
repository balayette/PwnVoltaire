using System.Text.RegularExpressions;
using System.Web;

namespace PwnVoltaire
{
    public static class PwnVoltaire
    {
        private static string startStr = "<error id=\"";
        private static string endStr = "</error>";
        public static string GetReadableOutput(string s)
        {
            var xml = HttpUtility.HtmlDecode(s);
            // fuck XML
            if (!xml.Contains("errors"))
                return "Pas d'erreur. Je crois...";

            var messages = Regex.Matches(xml, "<message>(.*?)</message>");
            string ret = "";
            foreach (Match match in messages)
            {
                ret += match.Value.Replace("<message>", "").Replace("</message>", "").Replace("[b]", "").Replace("[/b]", "") + "\n";
            }
            return ret;
        }
    }
}
