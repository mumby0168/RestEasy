using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.BasicRest.Client.Data
{
    public class UserService
    {
        private readonly HttpClient _client;
        private readonly Settings _settings;

        public UserService(IHttpClientFactory factory, Settings settings)
        {
            _client = factory.CreateClient();
            _settings = settings;
        }
        
        public async Task AddUser(string firstName, string secondName)
        {
            var user = new User()
            {
                FirstName = firstName,
                SecondName = secondName,
            };

            string json = JsonSerializer.Serialize(user);
            
            await _client.PostAsync($"{_settings.BaseUrl}", new StringContent(json));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var result = await _client.GetAsync(_settings.BaseUrl);;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<User>>(json);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<User>();
        }

        public async Task RemoveUserAsync(Guid id)
        {
            await _client.DeleteAsync(_settings.BaseUrl + id);
        }
    }
}