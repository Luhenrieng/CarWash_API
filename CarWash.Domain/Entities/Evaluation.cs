using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }
        public int OrderedId { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
    }
}
