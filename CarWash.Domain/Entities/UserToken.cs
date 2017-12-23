using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities
{
    public class UserToken
    {
        public int Id { get; set; }
        public DateTime Inserted { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
