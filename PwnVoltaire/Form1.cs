using System;
using System.Web;
using System.Windows.Forms;

namespace PwnVoltaire
{
    public partial class Form1 : Form
    {
        private Api _api;

        private void InitStuff()
        {
            this._api = new Api();
            this.webBrowser1.ScriptErrorsSuppressed = false;
            this.webBrowser1.Navigate("https://projet-voltaire.fr");
        }

        private string _getJsPayload(int start, int end)
        {
            // top quality js
            return
                "function underlineStuff(){for(var b=document.getElementsByClassName(\"pointAndClickSpan\"),c=" + start + ",d=" + end + ",e=0,f=0;f<b.length;f++){for(var g=0;g<b[f].innerHTML.length;g++)c<=e&&d>=e&&(b[f].style.color=\"red\",b[f].style.textDecoration=\"underline\"),e++}}";
        }

        public Form1()
        {
            InitializeComponent();
            InitStuff();
        }

        private string GetSentence()
        {
            var elements = this.webBrowser1.Document.GetElementsByTagName("div");
            HtmlElement el = null;
            foreach (HtmlElement htmlElement in elements)
            {
                if (htmlElement.GetAttribute("className") == "sentence")
                {
                    el = htmlElement;
                    break;
                }
            }
            if (el == null)
            {
                this.richTextBox1.Text += "\nDidn't find a sentence";
                return null;
            }
            var spans = el.GetElementsByTagName("span");
            var str = "";
            foreach (HtmlElement htmlElement in spans)
            {
                str += htmlElement.InnerText;
            }

            if (str != "")
                return str;

            this.richTextBox1.Text += "\nEmpty sentence";
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            var str = this.GetSentence();
            if (str == null)
                return;
            var errors = PwnVoltaire.GetReadableOutput(this._api.GetApiResp(str));
            if (errors.Count == 0)
            {
                this.richTextBox1.Text = "Pas d'erreurs, je crois...";
                return;
            }
            foreach (var error in errors)
            {
                this.richTextBox1.Text += HttpUtility.HtmlDecode(error.Description(str));
                var js = this._getJsPayload(error.Start, error.End);
                HtmlDocument doc = this.webBrowser1.Document;
                HtmlElement head = doc.GetElementsByTagName("head")[0];
                HtmlElement s = doc.CreateElement("script");
                s.SetAttribute("text", js);
                head.AppendChild(s);
                this.webBrowser1.Document.InvokeScript("underlineStuff");
            }
        }
    }
}
