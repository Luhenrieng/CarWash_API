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
    public class UserTokenRepository : IUserTokenRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public int Add(UserToken userToken)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into UserToken( 
                                                Inserted, 
                                                UserId,
                                                Token) 
                                                values(
                                                @Inserted, 
                                                @UserId,
                                                @Token)";

                return con.Execute(sql, userToken);
            }
        }
    }
}
