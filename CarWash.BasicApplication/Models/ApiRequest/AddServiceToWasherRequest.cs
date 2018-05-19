using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models.ApiRequest
{
    public class AddServiceToWasherRequest
    {
        public string Token { get; set; }
        public int ServiceId { get; set; }
        public decimal SpecificPrice { get; set; }
    }
}