

using System;

namespace BasicDDD.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Inserted { get; set; }
        public bool Active { get; set; }


        public bool SpecialClient(Client client)
        {
            return client.Active && DateTime.Now.Year - client.Inserted.Year >= 5;
        }
    }
}
