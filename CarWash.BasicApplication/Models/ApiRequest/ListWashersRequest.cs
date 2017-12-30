using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models.ApiRequest
{
    public class ListWashersRequest
    {
        public string Token { get; set; }
        public int MaxRadius { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}