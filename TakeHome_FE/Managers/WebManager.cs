using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TakeHome_FE.Managers
{
    public static class WebManager
    {
        public static string GetRawWebpage(string url)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
