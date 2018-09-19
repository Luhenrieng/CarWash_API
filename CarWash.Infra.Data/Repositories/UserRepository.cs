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
                                                Password,
                                                Document,
                                                Inserted,
                                                BirthDate,
                                                Cep,
                                                Address,
                                                AddressNumber,
                                                Complement,
                                                District,
                                                City,
                                                State,
                                                PhoneNumber,
                                                Latitude,
                                                Longitude,
                                                Active) 

                                                values(
                                                @RoleId, 
                                                @Name,
                                                @Email,
                                                @Password,
                                                @Document,
                                                @Inserted,
                                                @BirthDate,
                                                @Cep,
                                                @Address,
                                                @AddressNumber,
                                                @Complement,
                                                @District,
                                                @City,
                                                @State,
                                                @PhoneNumber,
                                                @Latitude,
                                                @Longitude,
                                                @Active)";

                return con.Execute(sql, user);
            }
        }


        public List<ServiceDescription> ListService(int userId)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = "select SU.UserId, SU.ServiceId, S.Name, S.DefaultPrice, SU.SpecificPrice from Services_X_User SU inner join Service S on S.Id = SU.ServiceId where SU.UserId = " + userId;

                return con.Query<ServiceDescription>(sql).ToList();
            }
        }


        public List<User> List()
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"select Id, 
                                    RoleId, 
                                    Name,
                                    Email,
                                    Password,
                                    Document,
                                    Inserted,
                                    BirthDate,
                                    Cep,
                                    Address,
                                    AddressNumber,
                                    Complement,
                                    District,
                                    City,
                                    State,
                                    PhoneNumber,
                                    Latitude,
                                    Longitude,
                                    Active
                                    from User";

                return con.Query<User>(sql).ToList();
            }
        }

        public List<Washer> ListWasher()
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"select U.Id, 
                            U.RoleId, 
                            U.Name,
                            U.Email,
                            U.Document,
                            U.Inserted,
                            U.BirthDate,
                            U.Cep,
                            U.Address,
                            U.AddressNumber,
                            U.Complement,
                            U.District,
                            U.City,
                            U.State,
                            U.PhoneNumber,
                            U.Latitude,
                            U.Longitude,
                            U.Active,
                            (Select Count(*) from Evaluation Where UserIdTo = U.Id) as EvaluationAmount,
                            (Select Sum(Evaluation.Score) from Evaluation Where UserIdTo = U.Id) as ScoreSum,
                             (Case
		                            When Min(SU.SpecificPrice) Is not null and Min(SU.SpecificPrice) <> 0 Then Min(SU.SpecificPrice)
		                            When Min(S.DefaultPrice) Is not null and Min(S.DefaultPrice) <> 0 Then Min(S.DefaultPrice)
		                            Else Null
	                            End) As MinPrice
                            from User U
                            left join Services_X_User SU on SU.UserId = U.Id
                            left join Service S on S.Id = SU.ServiceId
                            Where 
                            (U.Active = 1)
                            AND (U.RoleId = 2 or U.RoleId = 3)
                            Group By 
                            U.Id, 
                            U.RoleId, 
                            U.Name,
                            U.Email,
                            U.Document,
                            U.Inserted,
                            U.BirthDate,
                            U.Cep,
                            U.Address,
                            U.AddressNumber,
                            U.Complement,
                            U.District,
                            U.City,
                            U.State,
                            U.PhoneNumber,
                            U.Latitude,
                            U.Longitude,
                            U.Active";

                var listWasher = con.Query<Washer>(sql).ToList();

                foreach(var washer in listWasher)
                {
                    if(washer.ScoreSum.HasValue)
                        washer.ScoreAverage = ((decimal)washer.ScoreSum / (decimal)washer.EvaluationAmount);
                }

                return listWasher;
            }
        }

        public User GetByLogin(string email, string password)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"select Id, 
                                    RoleId, 
                                    Name,
                                    Email,
                                    Password,
                                    Document,
                                    Inserted,
                                    BirthDate,
                                    Cep,
                                    Address,
                                    AddressNumber,
                                    Complement,
                                    District,
                                    City,
                                    State,
                                    PhoneNumber,
                                    Latitude,
                                    Longitude,
                                    Active
                                    from User
                                    where Email = '" + email + "' and Password = '" + password + "'";

                return con.Query<User>(sql).ToList().FirstOrDefault();
            }
        }

        public User GetByEmail(string email)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"select Id, 
                                    RoleId, 
                                    Name,
                                    Email,
                                    Password,
                                    Document,
                                    Inserted,
                                    BirthDate,
                                    Cep,
                                    Address,
                                    AddressNumber,
                                    Complement,
                                    District,
                                    City,
                                    State,
                                    PhoneNumber,
                                    Latitude,
                                    Longitude,
                                    Active
                                    from User
                                    where Email = '" + email + "'";

                return con.Query<User>(sql).ToList().FirstOrDefault();
            }
        }

        public User GetByToken(string token)
        {
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                var sql = @"select U.Id, 
	                            RoleId, 
	                            Name,
	                            Email,
	                            Password,
	                            Document,
	                            U.Inserted,
	                            BirthDate,
	                            Cep,
	                            Address,
	                            AddressNumber,
	                            Complement,
	                            District,
	                            City,
	                            State,
	                            PhoneNumber,
	                            Latitude,
	                            Longitude,
	                            Active
	                            from User U
                                inner join UserToken UT on UT.UserId = U.Id
                                where UT.Token = '" + token + "'";

                return con.Query<User>(sql).ToList().FirstOrDefault();
            }
        }
    }
}
