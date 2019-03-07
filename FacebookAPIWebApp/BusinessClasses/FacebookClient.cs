using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace FacebookAPIWebApp.BusinessClasses
{

    public interface IFacebookClient
    {
        T Get<T>(string accessToken, string endpoint, string args = null);
        string Post(string accessToken, string endpoint, object data, string args = null);
    }

    public class FacebookClient : IFacebookClient
    {
        private string _baseAddress = "";

        public FacebookClient()
        {
            _baseAddress = "https://graph.facebook.com/v3.2/";
        }

        public T Get<T>(string accessToken, string endpoint, string args = null)
        {
            string uriResource = string.Format("{0}?{1}&access_token={2}", endpoint, args, accessToken);

            string finalUri = _baseAddress + uriResource;

            WebRequest request = WebRequest.Create(finalUri);
            request.Timeout = 30000;            
            request.ContentType = "application/json";            
            request.Method = "GET";             
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string respostaEnvio = reader.ReadToEnd();

            //return respostaEnvio;

            return JsonConvert.DeserializeObject<T>(respostaEnvio);
        }

        public string Post(string accessToken, string endpoint, object data, string args = null)
        {
            string uriResource = string.Format("{0}?{1}&access_token={2}", endpoint, args, accessToken);

            string finalUri = _baseAddress + uriResource;

            var objSerializado = JsonConvert.SerializeObject(data);

            WebRequest request = WebRequest.Create(finalUri);
            request.Timeout = 30000;
            byte[] byteArray = Encoding.UTF8.GetBytes(objSerializado);
            request.ContentType = "application/json";
            Stream dataStream;
            request.Method = "POST";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string respostaEnvio = reader.ReadToEnd();            

            return respostaEnvio;
        }
    }
}