using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Send_Notification
{
    class Program
    {
        static string URI = "";
        static string accessToken = "";
        static void Main(string[] args)
        {
            Push(URI, accessToken, "Hello 10");
        }

        public static HttpStatusCode Push(string pushUri, string accessToken, string text)
        {
            var subscriptionUri = new Uri(pushUri);

            var request = (HttpWebRequest)WebRequest.Create(subscriptionUri);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Headers = new WebHeaderCollection();
            request.Headers.Add("X-WNS-Type", "wns/toast");
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            string data = "<?xml version='1.0' encoding='utf-8'?>" + GetToastMessage();

            byte[] notificationMessage = Encoding.Default.GetBytes(data);
            request.ContentLength = notificationMessage.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return response.StatusCode;
        }

        private static string GetToastMessage()
        {
            return @"<toast>
                        <visual lang=""en-US"">
                            <binding template=""ToastText01"">
                                <text id=""1"">New test arrived!</text>
                            </binding>  
                        </visual>
                    </toast>";
        }
    }
}
