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

        public int Add(Ordered ordered)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into Ordered(UserId, WasherId, Created, TotalPrice, Status) 
                                            values(@UserId, @WasherId, @Created, @TotalPrice, @Status);
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
    }
}
