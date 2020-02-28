using RestEasy.Core.Markers;

namespace RestEasy.BasicRest.Dto
{
    public class UserDto : IDto
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
    }
}