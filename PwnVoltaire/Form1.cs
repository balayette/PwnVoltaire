using System;
using System.Web;
using System.Windows.Forms;
using WebKit;
using WebKit.DOM;

namespace PwnVoltaire
{
    public partial class Form1 : Form
    {
        private Api _api;

        private void InitStuff()
        {
            this._api = new Api();
            this.webKitBrowser1.Navigate("https://www.projet-voltaire.fr/");
        }

        private string _getJsPayload(int start, int end)
        {
            // top quality js
            return
                "for(var b=document.getElementsByClassName(\"pointAndClickSpan\"),c=" + start + ",d=" + end + ",e=0,f=0;f<b.length;f++){console.log(b[f].innerHTML);for(var g=0;g<b[f].innerHTML.length;g++)c<=e&&d>=e&&(b[f].style.color=\"red\",b[f].style.textDecoration=\"underline\"),e++}";
        }

        public Form1()
        {
            InitializeComponent();
            InitStuff();
        }

        private string GetSentence()
        {
            try
            {
                var elements = this.webKitBrowser1.Document.GetElementsByTagName("div");
                Element el = null;
                foreach (Node node in elements)
                {
                    var elem = node as Element;
                    if (elem == null)
                        continue;
                    if (!elem.HasAttribute("class"))
                        continue;
                    if (elem.GetAttribute("class") == "sentence")
                    {
                        el = elem;
                        break;
                    }
                }

                if (el == null || el.ChildNodes.Length < 1)
                {
                    this.richTextBox1.Text += "\nDidn't find a sentence";
                    return null;
                }

                var str = "";
                foreach (Node child in el.ChildNodes)
                {
                    var elem = child as Element;
                    if (elem == null)
                        continue;
                    if (elem.TagName == "span" || elem.TagName == "SPAN")
                    {
                        Console.WriteLine("Found a part of the sentence...");
                        Console.WriteLine(elem.TextContent);
                        str += elem.TextContent;
                    }
                }
                if (str != "")
                {
                    return str;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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
                foreach(var error in errors)
                {
                    this.richTextBox1.Text += HttpUtility.HtmlDecode(error.Description(str));
                    var js = this._getJsPayload(error.Start, error.End);
                    this.webKitBrowser1.StringByEvaluatingJavaScriptFromString(js);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(e.ToString());
            }
        }
            

        private void webKitBrowser1_Load(object sender, EventArgs e)
        {

        }
    }
}
