using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication.Models
{
    public class CreateOrderViewModel
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WasherId { get; set; }
        public DateTime Created { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public List<OrderItemViewModel> ListItens { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int OrderedId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
    }
}