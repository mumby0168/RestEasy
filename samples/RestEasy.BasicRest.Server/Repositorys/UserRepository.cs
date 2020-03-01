using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEasy.BasicRest.Data;
using RestEasy.BasicRest.Domain;
using RestEasy.BasicRest.Dto;
using RestEasy.Core.Persistence;

namespace RestEasy.BasicRest.Repositorys
{
    public class UserRepository : IRepository<UserDomain, UserDto>
    {

        public UserRepository()
        {
            if (!UserStorage.Users.Any()) UserStorage.Seed();
        }
        
        public Task AddAsync(UserDomain domain)
        {
            UserStorage.Users.Add(domain);
            return Task.CompletedTask;
        }

        public async Task UpdateAsync(UserDomain domain)
        {
            var user = await GetAsync(domain.Id);
            user.FirstName = domain.FirstName;
            user.SecondName = domain.SecondName;
        }

        public Task<UserDomain> GetAsync(Guid id)
        {
            var user = UserStorage.Users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<UserDomain>> GetAllAsync()
        {
            return Task.FromResult(UserStorage.Users.AsEnumerable());
        }

        public async Task RemoveAsync(UserDomain domain)
        {
            var user = await GetAsync(domain.Id);
            UserStorage.Users.Remove(user);
        }
    }
}