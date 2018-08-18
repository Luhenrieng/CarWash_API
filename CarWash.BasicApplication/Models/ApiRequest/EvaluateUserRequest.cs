using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models.ApiRequest
{
    public class EvaluateUserRequest
    {
        public string Token { get; set; }
        public int OrderedId { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public int Score { get; set; }
    }
}