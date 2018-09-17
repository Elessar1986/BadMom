using AutoMapper;
using BadMom.Models.User;
using BadMom.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class UserData
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public byte[] Photo { get; set; }

        public bool Confirmed { get; set; }

        public bool? Gender { get; set; }

        public int? Status { get; set; }

        public virtual List<Consumption> Consumption { get; set; }

       // public virtual List<Events> Events { get; set; }

        public virtual List<Income> Income { get; set; }

        public virtual List<Source> Source { get; set; }

        public virtual List<PersonalMessage> PersonalMessage { get; set; }

        public virtual List<PersonalMessage> PersonalMessage1 { get; set; }

        public virtual List<long> FavoriteAdverts { get; set; }

    }
}