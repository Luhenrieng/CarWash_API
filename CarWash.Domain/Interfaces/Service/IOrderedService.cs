using BasicDDD.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Service
{
    public interface IOrderedService
    {
        int Add(Entities.Ordered ordered);
        int CreateOrder(Entities.ValueObjects.CreateOrder order);
        string ValidateOrder(Entities.ValueObjects.CreateOrder order);
        IEnumerable<OrderReport> ListAllOrderReport();
        IEnumerable<OrderReport> ListOrderByUser(int userId, int UserRoleId);
        string UpdateOrderStatus(UpdateOrderStatus updateOrderStatus);
    }
}
