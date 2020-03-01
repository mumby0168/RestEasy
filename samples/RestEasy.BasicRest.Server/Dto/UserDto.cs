using System;
using RestEasy.Core.Markers;

namespace RestEasy.BasicRest.Dto
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
    }
}