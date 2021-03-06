﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Entities.ValueObjects
{
    public class Washer
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cep { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Active { get; set; }
        public int EvaluationAmount { get; set; }
        public int? ScoreSum { get; set; }
        public decimal? ScoreAverage { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
