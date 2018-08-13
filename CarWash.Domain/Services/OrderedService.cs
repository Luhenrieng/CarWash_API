using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Domain.Services
{
    public class OrderedService : IOrderedService
    {
        private readonly IOrderedRepository _orderedRepository;
        private readonly IOrderedItemRepository _orderedItemRepository;

        public OrderedService(IOrderedRepository orderedRepository, IOrderedItemRepository orderedItemRepository)
        {
            this._orderedRepository = orderedRepository;
            this._orderedItemRepository = orderedItemRepository;
        }

        public int Add(Ordered ordered)
        {
            return _orderedRepository.Add(ordered);
        }

        public IEnumerable<OrderReport> ListAllOrderReport()
        {
            var listOrderReport = this._orderedRepository.ListAllOrderReport();

            var listOrderItemReport = this._orderedItemRepository.ListAllOrderItem();

            foreach (var order in listOrderReport)
            {
                var itensToOrder = listOrderItemReport.Where(i => i.OrderId == order.OrderId).ToList();

                if (itensToOrder != null && itensToOrder.Count() > 0)
                {
                    order.Itens = itensToOrder;
                }
            }

            return listOrderReport.OrderBy(o => o.OrderId);
        }

        public IEnumerable<OrderReport> ListOrderByUser(int userId, int userRoleId)
        {
            var listOrderReport = this._orderedRepository.ListOrderByUser(userId, userRoleId);

            var listOrderItemReport = this._orderedItemRepository.ListOrderItemByUser(userId, userRoleId);

            foreach(var order in listOrderReport)
            {
                var itensToOrder = listOrderItemReport.Where(i => i.OrderId == order.OrderId).ToList();

                if(itensToOrder != null && itensToOrder.Count() > 0)
                {
                    order.Itens = itensToOrder;
                }
            }

            return listOrderReport.OrderBy(o => o.OrderId);
        }

        public bool CreateOrder(CreateOrder order)
        {
            Ordered ordered;
            OrderedItem item;

            if(order != null)
            {
                ordered = new Ordered();
                ordered.UserId = order.UserId;
                ordered.WasherId = order.WasherId;
                ordered.Created = DateTime.Now;
                ordered.TotalPrice = order.TotalPrice;
                ordered.Status = (int)Ordered.EnumOrderedStatus.Iniciado;
                ordered.Id = _orderedRepository.Add(ordered);

                if(ordered.Id > 0 && order.ListItens != null)
                {
                    foreach(var i in order.ListItens)
                    {
                        item = new OrderedItem();
                        item.OrderedId = ordered.Id;
                        item.ServiceId = i.ServiceId;
                        item.Price = i.Price;
                        item.Id = _orderedItemRepository.Add(item);
                    }
                }
            }

            return true;
        }
    }
}
