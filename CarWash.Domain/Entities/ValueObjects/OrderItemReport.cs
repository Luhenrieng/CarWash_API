using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities.ValueObjects
{
    public class OrderItemReport
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
    }
}
