using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwnVoltaire
{
    public class Error
    {
        private string _message;
        private ErrorTypes _errorType;
        private int _proba;
        private string _substitution;
        private int _start;
        private int _end;

        private string _errorString = "type=\"";
        private string _subString = "substitution=\"";
        private string _startString = "start=\"";
        private string _endString = "end=\"";
        private string _probaString = "proba=\"";

        public int Start => this._start;

        public int End => this._end;

        public Error(string message, string error)
        {
            this._parseMessage(message);
            this._parseError(error);
        }

        public string Description(string baseSentece)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this._message);
            sb.AppendLine($" Remplacer par : {this._substitution} \nNiveau de confiance : {this._proba}");
            return sb.ToString();
        }

        private void _parseMessage(string msg)
        {
            this._message = msg.Replace("<message>", "")
                .Replace("</message>", "")
                .Replace("[b]", "")
                .Replace("[/b]", "");
        }

        private void _parseError(string err)
        {
            var t = this._getNextChars(err.Substring(err.IndexOf(this._errorString) + this._errorString.Length));
            switch (t)
            {
                case "spell":
                    this._errorType = ErrorTypes.Spelling;
                    break;
                case "grammar":
                    this._errorType = ErrorTypes.Grammar;
                    break;
                default:
                    this._errorType = ErrorTypes.Unknown;
                    break;
            }
            this._substitution = this._getNextChars(err.Substring(err.IndexOf(this._subString) + this._subString.Length));
            this._start = this._getNextInt(err.Substring(err.IndexOf(this._startString) + this._startString.Length));
            this._end = this._getNextInt(err.Substring(err.IndexOf(this._endString) + this._endString.Length));
            this._proba = this._getNextInt(err.Substring(err.IndexOf(this._probaString) + this._probaString.Length));
        }

        private string _getNextChars(string str)
        {
            var ret = "";
            foreach (var c in str)
            {
                if (Char.IsLetter(c))
                    ret += c;
                else
                    break;
            }
            return ret;
        }

        private int _getNextInt(string str)
        {
            string ret = "";
            foreach (var c in str)
            {
                if (Char.IsDigit(c))
                    ret += c;
                else
                    break;
            }
            return int.Parse(ret);
        }
    }
}
