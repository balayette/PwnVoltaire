using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Navigate("https://projet-voltaire.fr");
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
            string str = "";
            foreach (HtmlElement htmlElement in spans)
            {
                str += htmlElement.InnerText;
            }
            if (str == "")
            {
                this.richTextBox1.Text += "\nEmpty sentence";
                return null;
            }
            this.richTextBox1.Text += $"\n{str}";
            return str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            var str = this.GetSentence();
            if (str == null)
                return;
            var resp = HttpUtility.HtmlDecode(PwnVoltaire.GetReadableOutput(this._api.GetApiResp(str)));
            this.richTextBox1.Text += "\n" + resp + "\n";
        }
    }
}
