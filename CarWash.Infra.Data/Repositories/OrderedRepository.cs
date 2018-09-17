using BasicDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using System.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Infra.Data.Repositories
{
    public class OrderedRepository : IOrderedRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public string UpdateOrderStatus(UpdateOrderStatus updateOrderStatus)
        {
            var listOrders = this.ListOrderByWasher(updateOrderStatus.WasherId);

            var order = listOrders.FirstOrDefault(c => c.OrderId == updateOrderStatus.OrderId);

            if (order == null) return "Pedido não existe.";

            if(order.Status != 1) return "Este pedido não pode assumir esse status.";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = String.Format(@"Update Ordered 
                            Set Status = {0}
                            Where Id = {1}", updateOrderStatus.Status, updateOrderStatus.OrderId);

                    con.Execute(sql);

                return "";
            }
        }

        public int Add(Ordered ordered)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into Ordered(UserId, WasherId, Created, TotalPrice, Status, ScheduledDateTime) 
                                            values(@UserId, @WasherId, @Created, @TotalPrice, @Status, @ScheduledDateTime);
                                            Select @@Identity;";

                return con.Query<int>(sql, ordered).Single();
            }
        }

        public IEnumerable<OrderReport> ListAllOrderReport()
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"Select
                            O.Id OrderId,
                            O.Created,
                            O.ScheduledDateTime,
                            O.TotalPrice,
                            O.Status,
                            U1.Id UserId,
                            U1.Name UserName,
                            U2.Id WasherId,
                            U2.Name WasherName 
                            from Ordered O
                            Inner Join User U1 on U1.Id = O.UserId
                            Inner Join User U2 on U2.Id = O.WasherId";

                return con.Query<OrderReport>(sql);
            }
        }

        public IEnumerable<OrderReport> ListOrderByUser(int userId, int UserRoleId)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"Select
                            O.Id OrderId,
                            O.Created,
                            O.ScheduledDateTime,
                            O.TotalPrice,
                            O.Status,
                            U1.Id UserId,
                            U1.Name UserName,
                            U2.Id WasherId,
                            U2.Name WasherName 
                            from Ordered O
                            Inner Join User U1 on U1.Id = O.UserId
                            Inner Join User U2 on U2.Id = O.WasherId";

                var where = UserRoleId == 1 ? String.Format(" where UserId = {0}", userId) : String.Format(" where WasherId = {0}", userId);

                var query = sql + where;

                return con.Query<OrderReport>(query);
            }
        }

        public IEnumerable<OrderReport> ListOrderByWasher(int washerId)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"Select
                            O.Id OrderId,
                            O.Created,
                            O.ScheduledDateTime,
                            O.TotalPrice,
                            O.Status,
                            U1.Id UserId,
                            U1.Name UserName,
                            U2.Id WasherId,
                            U2.Name WasherName 
                            from Ordered O
                            Inner Join User U1 on U1.Id = O.UserId
                            Inner Join User U2 on U2.Id = O.WasherId";

                var where = String.Format(" where WasherId = {0}", washerId);

                var query = sql + where;

                return con.Query<OrderReport>(query);
            }
        }
    }
}
