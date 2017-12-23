using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models
{
    public class UserTokenViewModel
    {
        public int Id { get; set; }
        public DateTime Inserted { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}