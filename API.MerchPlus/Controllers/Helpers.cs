using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace API.MerchPlus.Controllers
{
    public class Helpers
    {
        public DateTime? ConvertToDateTime(string incomingIn_DDMMYYYY)
        {
            if (incomingIn_DDMMYYYY != string.Empty)
            {
                DateTime insDt = new DateTime(
                    Convert.ToInt32(incomingIn_DDMMYYYY.Substring(6, 4)),
                    Convert.ToInt32(incomingIn_DDMMYYYY.Substring(3, 2)),
                    Convert.ToInt32(incomingIn_DDMMYYYY.Substring(0, 2))
                    );
                return insDt;
            }
            else
                return null;
        }

        public string SendSMSwithXML(string PostAddress, string xmlData)
        {
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PostAddress);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 300000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                // This sample only checks whether we get an "OK" HTTP status code back.
                // If you must process the XML-based response, you need to read that from
                // the response stream.
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(responseStream))
                    {
                        res = rdr.ReadToEnd();
                    }
                    return res;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "-1";
            }

        }
    }
}