// -----------------------------------------------------------------------
// <copyright file="WebApiComunicator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PasteBin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using System.Collections.Specialized;


    public static class WebApiComunicator
    {
        private delegate void SendPostDelegate(string title, string massage, EVisibility vis, ExpireData expDate, string format);
        private static SendPostDelegate spd ;
        static public bool isError;
        static public string errorMessage;
        static public string lastNoteURl;

        public static void SendPostAsyn(string title, string massage, EVisibility vis, ExpireData expDate, string format, MainWindow win )
        {
            spd = new SendPostDelegate(SendPost);
            spd.BeginInvoke(title, massage, vis, expDate, format, win.UpdateMe, null);
        
        }
        
        public static void SendPost(string title,string massage ,EVisibility vis, ExpireData expDate,string format )
        {

             using (WebClient client = new WebClient())
            {

                byte[] response = client.UploadValues("http://pastebin.com/api/api_post.php", new NameValueCollection()
                {
                    { "api_option", "paste" },
                    { "api_user_key", Properties.Settings.Default.UserKey },
                    { "api_paste_privat",  ((int)vis).ToString() },
                    { "api_paste_name",title },
                    { "api_paste_expire_date", expDate.GetDescription() },
                    { "api_paste_format", format },
                    { "api_dev_key", Properties.Settings.Default.DevKey },
                    { "api_paste_code", massage },
                });

                if( System.Text.Encoding.Default.GetString(response).Contains("pastebin.com"))
                {
                    lastNoteURl= System.Text.Encoding.Default.GetString(response);
                    isError=false;
                }
                else
                {
                    errorMessage = System.Text.Encoding.Default.GetString(response);
                    isError=true;
                }
               
            }
        
        }

       

    }
}
