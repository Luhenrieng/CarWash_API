using BasicDDD.Domain.Entities;
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
                var sql = @"insert into OrderedItem(OrderedId, ServiceIdId, Price) 
                                            values(@OrderedId, @ServiceIdId, @Price)";

                return con.Execute(sql, orderedItem);
            }
        }
    }
}
