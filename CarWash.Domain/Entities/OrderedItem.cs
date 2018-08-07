using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities
{
    public class OrderedItem
    {
        public int Id { get; set; }
        public int OrderedId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
    }
}
