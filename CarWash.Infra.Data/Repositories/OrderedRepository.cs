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
    }
}
