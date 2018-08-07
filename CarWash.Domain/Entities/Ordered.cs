using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities
{
    public class Ordered
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WasherId { get; set; }
        public DateTime Created { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
    }
}
