using System.IO;
using System.Net;
using System.Text;

namespace PwnVoltaire
{
    public class Api
    {
        private string _apiString;

        public Api()
        {
            this._apiString = "http://syn-web01.synapse-fr.com/api/textchecker/correct_logged";
        }

        public string GetApiResp(string sentence)
        {
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
        }
    }
}
