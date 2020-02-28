using System;
using RestEasy.BasicRest.Dto;
using RestEasy.Core.Markers;

namespace RestEasy.BasicRest.Domain
{
    public class UserDomain : IDomain<UserDto>
    {
        public Guid Id { get; }


        public UserDomain()
        {
            Id = Guid.NewGuid();
        }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        
        
        public void Map(UserDto dto)
        {
            FirstName = dto.FirstName;
            SecondName = dto.SecondName;
        }

        public UserDto Map()
        {
            return new UserDto {FirstName = FirstName, SecondName = SecondName};
        }
    }
}