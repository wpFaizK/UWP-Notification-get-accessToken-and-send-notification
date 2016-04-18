using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Notification_AuthenticateWNS
{
    class Program
    {
        private static string sid = "";
        private static string secret = "";

        static void Main(string[] args)
        {
            getAccessToken();
        }
      
        public static void getAccessToken()
        {
            var urlEncodedSid = HttpUtility.UrlEncode(String.Format("{0}", sid));
            var urlEncodedSecret = HttpUtility.UrlEncode(secret);

            var body =
              String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", urlEncodedSid, urlEncodedSecret);

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string response = client.UploadString("https://login.live.com/accesstoken.srf", body);
           
        }
    }


}
