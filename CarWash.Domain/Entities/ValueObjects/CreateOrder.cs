using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities.ValueObjects
{
    public class CreateOrder
    {
        public string token { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WasherId { get; set; }
        public DateTime Created { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public List<OrderItem> ListItens { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderedId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
    }
}
