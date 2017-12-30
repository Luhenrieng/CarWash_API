using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace CarWash.Util
{
    public class GoogleGeoCoding
    {
        public string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state)
        {
            string responseFromServer = "";
            string key = "AIzaSyCscPXuYljvh6OS1-VM4UZe8lYaepNbgcU";

            try
            {
                string Url = String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}+{1},{2},{3},{4}&key={5}", number, address, neighborhood, city, state, key);
                WebRequest request = WebRequest.Create(Url);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();

                string status = ((HttpWebResponse)response).StatusDescription;
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();

                MemoryStream stream1 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseFromServer));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));

                stream1.Position = 0;
                RootObject objeto = (RootObject)ser.ReadObject(stream1);

                if (objeto != null && objeto.results != null && objeto.results[0].geometry != null)
                {
                    if (objeto.results[0].geometry.location != null)
                    {
                        Location loc = objeto.results[0].geometry.location;
                        responseFromServer = loc.lat.ToString() + "&" + loc.lng.ToString();
                    }
                }
            }
            catch (Exception ex) { }

            return responseFromServer;
        }
    }


    [DataContract]
    public class AddressComponent
    {
        [DataMember]
        public string long_name { get; set; }
        [DataMember]
        public string short_name { get; set; }
        [DataMember]
        public List<string> types { get; set; }
    }

    [DataContract]
    public class Northeast
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
    }

    [DataContract]
    public class Southwest
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
    }

    [DataContract]
    public class Bounds
    {
        [DataMember]
        public Northeast northeast { get; set; }
        [DataMember]
        public Southwest southwest { get; set; }
    }

    [DataContract]
    public class Location
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
    }

    [DataContract]
    public class Northeast2
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
    }

    [DataContract]
    public class Southwest2
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }
    }

    [DataContract]
    public class Viewport
    {
        [DataMember]
        public Northeast2 northeast { get; set; }
        [DataMember]
        public Southwest2 southwest { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember]
        public Bounds bounds { get; set; }
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public string location_type { get; set; }
        [DataMember]
        public Viewport viewport { get; set; }
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public List<AddressComponent> address_components { get; set; }
        [DataMember]
        public string formatted_address { get; set; }
        [DataMember]
        public Geometry geometry { get; set; }
        [DataMember]
        public string place_id { get; set; }
        [DataMember]
        public List<string> postcode_localities { get; set; }
        [DataMember]
        public List<string> types { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public List<Result> results { get; set; }
        [DataMember]
        public string status { get; set; }
    }
}
