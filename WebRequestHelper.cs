using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace LewisHenson.CineworldCinemas
{
    internal static class WebRequestHelper
    {
        public static string GetContent(string uri)
        {
            string content = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                content = streamReader.ReadToEnd().Trim();
                            }
                        }
                    }

                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.ToString());
            }

            return content;
        }
    }
}