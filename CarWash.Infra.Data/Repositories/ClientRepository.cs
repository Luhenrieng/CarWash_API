using BasicDDD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;

namespace BasicDDD.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {

        public List<Client> GetAll()
        {
            List<Client> ListClients = new List<Client>();

            Client cli1 = new Client();
            cli1.Id = 1;
            cli1.FirstName = "Carlos";
            cli1.LastName = "Drumond";
            cli1.Email = "carlos@gmail.com";
            cli1.Inserted = DateTime.Now;
            cli1.Active = true;

            Client cli2 = new Client();
            cli2.Id = 2;
            cli2.FirstName = "Joao";
            cli2.LastName = "Rodrigues";
            cli2.Email = "joao@gmail.com";
            cli2.Inserted = DateTime.Now;
            cli2.Active = true;

            Client cli3 = new Client();
            cli3.Id = 3;
            cli3.FirstName = "Thiago";
            cli3.LastName = "Carvalho";
            cli3.Email = "ti@gmail.com";
            cli3.Inserted = DateTime.Now;
            cli3.Active = true;

            ListClients.Add(cli1);
            ListClients.Add(cli2);
            ListClients.Add(cli3);

            return ListClients;
        }
    }
}
