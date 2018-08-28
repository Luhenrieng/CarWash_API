using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities.ValueObjects
{
    public class UpdateOrderStatus
    {
        public string Token { get; set; }
        public int WasherId { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}
