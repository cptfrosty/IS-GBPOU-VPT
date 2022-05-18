using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    class SmsRu
    {
        public class SmsRuSendResponse
        {
            public int Status { get; private set; }
            public string[] Lines { get; private set; }

            public SmsRuSendResponse()
            {
                Status = Int32.MinValue;
                Lines = null;
            }

            public SmsRuSendResponse(int status)
            {
                Status = status;
                Lines = null;
            }

            public SmsRuSendResponse(int status, string[] ids)
                : this(status)
            {
                Lines = ids;
            }
        }

        protected string _service_url = "http://sms.ru/sms/";
        protected string _apiKey = null;
        protected string _senderName = null;

        public SmsRu(string apiKey)
        {
            _apiKey = apiKey;
        }

        public SmsRu(string apiKey, string senderName) : this(apiKey)
        {
            _senderName = senderName;
        }

        public SmsRuSendResponse Send(string sendTo, string text)
        {
            return Send(sendTo, text, _senderName, 0, false, false);
        }
        public SmsRuSendResponse Send(string sendTo, string text, long time)
        {
            return Send(sendTo, text, _senderName, time, false, false);
        }
        public SmsRuSendResponse Send(string sendTo, string text, bool isTest)
        {
            return Send(sendTo, text, _senderName, 0, false, isTest);
        }
        public SmsRuSendResponse Send(string sendTo, string text, bool isTranslit, bool isTest)
        {
            return Send(sendTo, text, _senderName, 0, isTranslit, isTest);
        }
        public SmsRuSendResponse Send(string sendTo, string text, long time, bool isTranslit, bool isTest)
        {
            return Send(sendTo, text, _senderName, time, isTranslit, isTest);
        }
        public SmsRuSendResponse Send(string sendTo, string text, string sendFrom, long time, bool isTranslit, bool isTest)
        {
            var result = new SmsRuSendResponse();
            using (var wc = new System.Net.WebClient())
            {
                string suffix = String.Empty;
                if (!String.IsNullOrEmpty(sendFrom))
                {
                    suffix += "&from=" + sendFrom;
                }
                if (time > 0)
                {
                    suffix += "&time=" + time.ToString();
                }
                if (isTest)
                {
                    suffix += "&test=1";
                }
                if (isTranslit)
                {
                    suffix += "&translit=1";
                }
                string[] response = wc.DownloadString(String.Format(_service_url + "send?api_id={0}&to={1}&text={2}{3}&partner_id={4}", _apiKey, sendTo, text, suffix, 103184)).Replace("\r", String.Empty).Split('\n');
                int status = 0;
                if (Int32.TryParse(response[0], out status))
                {
                    var lines = new string[response.Length - 1];
                    Array.Copy(response, 1, lines, 0, lines.Length);
                    result = new SmsRuSendResponse(status, lines);
                }
            }
            return result;
        }      
    }


}
