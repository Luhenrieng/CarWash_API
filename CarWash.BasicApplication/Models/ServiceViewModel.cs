using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DefaultPrice { get; set; }
        public bool Active { get; set; }
    }
}