using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models
{
    public class ServiceDescriptionViewModel
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SpecificPrice { get; set; }
    }
}