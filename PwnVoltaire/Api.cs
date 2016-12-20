using System.IO;
using System.Net;
using System.Text;

namespace PwnVoltaire
{
    public class Api
    {
        private string _apiString;
        private StreamReader _rdr;

        public Api()
        {
            this._apiString = "http://syn-web01.synapse-fr.com/api/textchecker/correct_logged";
        }

        public string GetApiResp(string sentence)
        {
            WebRequest _wr;
            _wr = WebRequest.Create(this._apiString);
            _wr.Method = "POST";
            _wr.ContentType = "text/xml; charset=utf-8";
            var payload =
                    $"<RequestData><details>{sentence}</details><userlogin>undefined</userlogin></RequestData>";
            var dataStream = _wr.GetRequestStream();
            var data = Encoding.UTF8.GetBytes(payload);

            dataStream.Write(data, 0, data.Length);
            dataStream.Close();
            var resp = _wr.GetResponse().GetResponseStream();
            this._rdr = new StreamReader(resp);
            var r = this._rdr.ReadToEnd();
            resp.Close();
            return r;
        }
    }
}
