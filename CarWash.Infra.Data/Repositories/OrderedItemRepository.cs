using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Entities.ValueObjects;
using BasicDDD.Domain.Interfaces.Repositories;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Infra.Data.Repositories
{
    public class OrderedItemRepository : IOrderedItemRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public int Add(OrderedItem orderedItem)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into OrderedItem(OrderedId, ServiceId, Price) 
                                            values(@OrderedId, @ServiceId, @Price);
                                            Select @@Identity;";

                return con.Query<int>(sql, orderedItem).Single();
            }
        }

        public IEnumerable<OrderItemReport> ListOrderItemByUser(int userId, int UserRoleId)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"Select 
                            I.Id,
                            I.OrderedId OrderId,
                            I.ServiceId,
                            S.Name ServiceName,
                            I.Price
                            From OrderedItem I
                            Inner Join Ordered O on O.Id = I.OrderedId
                            Inner Join Service S on S.Id = I.ServiceId";

                var where = UserRoleId == 1 ? String.Format(" where O.UserId = {0}", userId) : String.Format(" where O.WasherId = {0}", userId);

                var query = sql + where;

                return con.Query<OrderItemReport>(query);
            }
        }

        public IEnumerable<OrderItemReport> ListAllOrderItem()
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"Select 
                            I.Id,
                            I.OrderedId OrderId,
                            I.ServiceId,
                            S.Name ServiceName,
                            I.Price
                            From OrderedItem I
                            Inner Join Ordered O on O.Id = I.OrderedId
                            Inner Join Service S on S.Id = I.ServiceId";
                
                return con.Query<OrderItemReport>(sql);
            }
        }
    }
}
