using System;
using RestEasy.BasicRest.Dto;
using RestEasy.Core.Markers;

namespace RestEasy.BasicRest.Domain
{
    public class UserDomain : IDomain<UserDto>
    {
        public Guid Id { get; private set; }


        public UserDomain()
        {
            Id = Guid.NewGuid();
        }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        
        
        public void Map(UserDto dto, bool firstCreation = false)
        {
            if (!firstCreation) Id = dto.Id;
            
            FirstName = dto.FirstName;
            SecondName = dto.SecondName;
        }

        public UserDto Map()
        {
            return new UserDto {Id = Id,FirstName = FirstName, SecondName = SecondName};
        }
    }
}