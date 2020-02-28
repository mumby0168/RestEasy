using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEasy.BasicRest.Data;
using RestEasy.BasicRest.Domain;
using RestEasy.BasicRest.Dto;
using RestEasy.Core.Persistence;

namespace RestEasy.BasicRest.Repositorys
{
    public class UserRepository : IRepository<UserDomain, UserDto>
    {
        public Task AddAsync(UserDomain domain)
        {
            UserStorage.Users.Add(domain);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(UserDomain domain)
        {
            throw new NotImplementedException();
        }

        public Task<UserDomain> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDomain>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(UserDomain domain)
        {
            throw new NotImplementedException();
        }
    }
}