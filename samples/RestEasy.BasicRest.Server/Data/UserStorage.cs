using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using RestEasy.BasicRest.Domain;
using RestEasy.BasicRest.Dto;

namespace RestEasy.BasicRest.Data
{
    public static class UserStorage
    {
        public static List<UserDomain> Users { get; set; } = new List<UserDomain>();

        public static void Seed()
        {
            Users.Add(new UserDomain()
            {
                FirstName = "Joe",
                SecondName = "Bloggs"
            });
            Users.Add(new UserDomain()
            {
                FirstName = "Jess",
                SecondName = "Bloggs"
            });
    }
    }
}