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
    public class EvaluationRepository : IEvaluationRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public int Add(Evaluation evaluation)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into Evaluation(OrderedId, UserIdFrom, UserIdTo, Date, Score) 
                                            values(@OrderedId, @UserIdFrom, @UserIdTo, @Date, @Score);
                                            Select @@Identity;";

                return con.Query<int>(sql, evaluation).Single();
            }
        }
    }
}
