using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities
{
    public class Ordered
    {

        public enum EnumOrderedStatus
        {
            Iniciado = 1,
            Cancelado = 2,
            Negado = 3,
            Aceito = 4,
            Finalizado = 5
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int WasherId { get; set; }
        public DateTime Created { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
    }
}
