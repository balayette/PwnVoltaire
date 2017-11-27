using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft;


namespace PwnVoltaire
{
    public class Api
    {
        private string _apiString;

        public Api()
        {
            //this._apiString = "http://syn-web01.synapse-fr.com/api/textchecker/correct_logged";
            // this._apiString = "http://orthographe.reverso.net/RISpellerWS/RestSpeller.svc/v1/CheckSpellingAsXml/language=fra?outputFormat=json&doReplacements=true&interfLang=fr&dictionary=both&spellOrigin=interactive&includeSpellCheckUnits=true&includeExtraInfo=true&isStandaloneSpeller=true";
            this._apiString = "http://orthographe.reverso.net/RISpellerWS/RestSpeller.svc/v1/CheckSpellingAsXml/language=fra?outputFormat=json&doReplacements=true&interfLang=fra&dictionary=both&spellOrigin=interactive&includeSpellCheckUnits=true&includeExtraInfo=true&isStandaloneSpeller=true";
        }

        public string GetApiResp(string sentence)
        {
            /*
            WebRequest wr = WebRequest.Create(this._apiString);
            wr.Method = "POST";
            wr.ContentType = "text/xml; charset=utf-8";
            var payload =
                    $"<RequestData><details>{sentence}</details><userlogin>undefined</userlogin></RequestData>";
            var data = Encoding.UTF8.GetBytes(payload);

            var dataStream = wr.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();

            var resp = wr.GetResponse().GetResponseStream();
            var rdr = new StreamReader(resp);

            var r = rdr.ReadToEnd();
            resp.Close();

            return r;
            */

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(this._apiString);
            wr.Method = "POST";
            wr.Headers.Add("Origin", "http://www.reverso.net");
            wr.Referer =  "http://www.reverso.net/orthographe/correcteur-francais/";
            wr.Host = "orthographe.reverso.net";
            wr.UserAgent =  "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            wr.Headers.Add("Username", "OnlineSpellerWS");
            wr.Headers.Add("Created", "01/01/0001 00:00:00");
            wr.Accept = "*/*";
            wr.Headers.Add("Access-Control-Request-Method", "POST");
            wr.Headers.Add("Access-Control-Request-Headers", "accept, content-type, created, username, x-requested-with");
            wr.Headers.Add("Accept-Language", "fr-FR,fr;q=0.8,en-US;q=0.6,en;q=0.4");
            wr.Headers.Add("Accept-Encoding:gzip, deflate, sdch");
            foreach(var k in wr.Headers.AllKeys)
            {
                Console.WriteLine(k + " " + wr.Headers[k]);
            }
            var data = Encoding.UTF8.GetBytes(sentence);
            Console.WriteLine(sentence);
            var dataStream = wr.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();
            
            var resp = wr.GetResponse().GetResponseStream();
            var rdr = new StreamReader(resp);

            var r = rdr.ReadToEnd();
            resp.Close();

            dynamic jsono = Newtonsoft.Json.JsonConvert.DeserializeObject(r);

            Console.WriteLine("RAW API RESPONSE : \n\n" + r + "\n\n\n");
            Console.WriteLine("Corrections : " + jsono.Corrections);

            return jsono.Corrections;
        }
    }
}
