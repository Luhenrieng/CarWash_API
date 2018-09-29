using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace CarWash.Util.Moip
{
    public class Moip
    {
        public void FindCustomer(string id)
        {
            string authInfo = "NINZLPAVPTKLLE74EOXKYGWSO718KLG0" + ":" + "42RFCFLTVS97FX5ITQRVHYEUQAVCPUKXW6GZA0KW";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            var request = (HttpWebRequest)WebRequest.Create("https://sandbox.moip.com.br/v2/customers/" + id);
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }
    }
}
