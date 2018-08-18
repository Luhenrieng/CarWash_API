using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models
{
    public class OrderItemReportViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
    }
}