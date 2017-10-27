using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;


namespace BasicDDD.Infra.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public int Add(Log log)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = "insert into log(Inserted, UserId, Description, OperationType) values(@Inserted, @UserId, @Description, @OperationType)";

                return con.Execute(sql, log);
            }
        }
        
    }
}
