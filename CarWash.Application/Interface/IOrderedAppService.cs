using BasicDDD.Domain.Entities.ValueObjects;
using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Application.Interface
{
    public interface IOrderedAppService
    {
        int Add(Ordered ordered);
        int CreateOrder(CreateOrder order);
        string ValidadeOrder(CreateOrder order);
        IEnumerable<OrderReport> ListAllOrderReport();
        IEnumerable<OrderReport> ListOrderByUser(int userId, int UserRoleId);
        string UpdateOrderStatus(UpdateOrderStatus updateOrderStatus);
    }
}
