using System;

namespace RestEasy.BasicRest.Client.Data
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
    }
}