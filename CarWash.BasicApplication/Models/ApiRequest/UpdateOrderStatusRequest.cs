using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models.ApiRequest
{
    public class UpdateOrderStatusRequest
    {
        public string Token { get; set; }
        public int WasherId { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}