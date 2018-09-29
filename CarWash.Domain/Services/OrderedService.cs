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

        public string ValidateOrder(CreateOrder order)
        {
            if (order == null) return "Objeto de entrada no formato incorreto ou não informado.";
            if (order.ScheduledDateTime == null) return "Data de agendamento não pode ser nula.";
            if (order.ScheduledDateTime < DateTime.Now) return "Data e hora de agendamento não pode ser menor que a data e hora atual.";
            if (order.UserId == 0) return "Id do cliente não pode ser nulo.";
            if (order.WasherId == 0) return "Id do lavador não pode ser nulo.";
            if (order.TotalPrice == 0) return "Valor do pedido não pode ser nulo.";
            if (order.ListItens == null || order.ListItens.Count == 0) return "O pedido deve conter no mínimo 1 item.";

            int hour = order.ScheduledDateTime.Hour;
            int minute = order.ScheduledDateTime.Minute;

            if (hour < 8 || hour > 19) return "Horário de agendamento deve ser entre 08:00 e 19:59h.";

            return "";
        }

        public int CreateOrder(CreateOrder order)
        {
            Ordered ordered;
            OrderedItem item;
            int orderedIdResult = 0;

            if(order != null)
            {
                ordered = new Ordered();
                ordered.UserId = order.UserId;
                ordered.WasherId = order.WasherId;
                ordered.Created = DateTime.Now;
                ordered.ScheduledDateTime = order.ScheduledDateTime;
                ordered.TotalPrice = order.TotalPrice;
                ordered.Status = (int)Ordered.EnumOrderedStatus.Iniciado;
                ordered.Id = _orderedRepository.Add(ordered);

                if(ordered.Id > 0 && order.ListItens != null)
                {
                    orderedIdResult = ordered.Id;

                    foreach (var i in order.ListItens)
                    {
                        item = new OrderedItem();
                        item.OrderedId = ordered.Id;
                        item.ServiceId = i.ServiceId;
                        item.Price = i.Price;
                        item.Id = _orderedItemRepository.Add(item);
                    }
                }
            }

            return orderedIdResult;
        }

        public string UpdateOrderStatus(UpdateOrderStatus updateOrderStatus)
        {
            return this._orderedRepository.UpdateOrderStatus(updateOrderStatus);
        }
    }
}
