using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Service;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Application
{
    public class OrderedAppService : IOrderedAppService
    {
        private readonly IOrderedService _orderedService;

        public OrderedAppService(IOrderedService orderedService)
        {
            this._orderedService = orderedService;
        }

        public int Add(Ordered ordered)
        {
            return this._orderedService.Add(ordered);
        }

        public int CreateOrder(Domain.Entities.ValueObjects.CreateOrder order)
        {
            return _orderedService.CreateOrder(order);
        }

        public IEnumerable<OrderReport> ListAllOrderReport()
        {
            return _orderedService.ListAllOrderReport();
        }

        public IEnumerable<OrderReport> ListOrderByUser(int userId, int UserRoleId)
        {
            return _orderedService.ListOrderByUser(userId, UserRoleId);
        }

        public string UpdateOrderStatus(UpdateOrderStatus updateOrderStatus)
        {
            return this._orderedService.UpdateOrderStatus(updateOrderStatus);
        }

        public string ValidadeOrder(CreateOrder order)
        {
            return this._orderedService.ValidateOrder(order);
        }
    }
}
