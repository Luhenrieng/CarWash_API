using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Repositories
{
    public interface IOrderedItemRepository
    {
        int Add(OrderedItem orderedItem);
        IEnumerable<OrderItemReport> ListOrderItemByUser(int userId, int UserRoleId);
        IEnumerable<OrderItemReport> ListAllOrderItem();
    }
}
