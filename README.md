# UWP-Notification-get-accessToken-and-send-notification

I tried to find out how Windows 10 notificaton works, but could not find a lot of samples

Here is what my sample does

1. Read https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-windows-push-notification-services--wns--overview?f=255&MSPPError=-2147217396
2. requests a push notification channel from the Universal Windows Platform - this can be found easy
3. Authenticating your cloud service - this is when you need access token .. the sample has the request to get access token 
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
4. Send puch notification 
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
