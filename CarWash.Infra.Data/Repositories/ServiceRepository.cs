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
    public class ServiceRepository : IServiceRepository
    {

        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public List<Service> ListAllServices()
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = "select Id, Name, DefaultPrice, Active From Service";

                return con.Query<Service>(sql).ToList();
            }
        }

        public int AddServiceToWasher(ServicesXUser serviceXUser)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into Services_X_User( 
                            UserId,
                            ServiceId,
                            SpecificPrice) 
                            values(
                            @UserId, 
                            @ServiceId,
                            @SpecificPrice)";

                return con.Execute(sql, serviceXUser);
            }
        }
    }
}
