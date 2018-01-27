using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities.ValueObjects
{
    public class ServiceDescription
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SpecificPrice { get; set; }
    }
}
