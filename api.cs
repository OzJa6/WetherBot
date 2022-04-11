using System;
using System.Net;
using System.Text;

namespace api // Note: actual namespace depends on the project name.
{
    public class GetHTML
    {
        static string GetCityName()
        {
            string CityName = "";

            return CityName;
        }

        static string GenerateURL()
        {
            string url = "";

            return url;
        }

        public static string GetCityHTML(string url)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                html = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            return html;
        }
    }
}