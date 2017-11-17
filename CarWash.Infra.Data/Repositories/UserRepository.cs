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
    public class UserRepository : IUserRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public int Add(User user)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"insert into User( 
                                                RoleId, 
                                                Name,
                                                Email,
                                                Document,
                                                Inserted,
                                                BirthDate,
                                                Cep,
                                                Address,
                                                AddressNumber,
                                                Complement,
                                                District,
                                                City,
                                                Country,
                                                PhoneNumber,
                                                GeoLocation) 

                                                values(
                                                @RoleId, 
                                                @Name,
                                                @Email,
                                                @Document,
                                                @Inserted,
                                                @BirthDate,
                                                @Cep,
                                                @Address,
                                                @AddressNumber,
                                                @Complement,
                                                @District,
                                                @City,
                                                @Country,
                                                @PhoneNumber,
                                                @GeoLocation)";

                return con.Execute(sql, user);
            }
        }
    }
}
