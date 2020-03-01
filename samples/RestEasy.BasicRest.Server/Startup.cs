    using System;
using System.Collections.Generic;
    using System.IO;
    using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEasy.BasicRest.Domain;
using RestEasy.BasicRest.Dto;
using RestEasy.BasicRest.Repositorys;

namespace RestEasy.BasicRest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRouting();
            //Needed to provide the framework with the data to use and repository to perform the data access
            services.AddRestEasy()
                .AddRestEasyApi<UserDomain, UserDto, UserRepository>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            //Ensure this is before exception middleware in order to provide proper responses.
            app.UseRestEasy();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                // Add this here to map an API of the format
                // NOTE: replace <replace> with own values
                // GET => http://<hostname>:<port>/api/users/<id> => Single UserDto [OK]
                // GET => http://<hostname>:<port>/api/users/ => All User Dto [OK]       
                // POST http://<hostname>:<port>/api/users/ BODY => JSON of UserDto => [OK] Creates User   
                // PUT http://<hostname>:<port>/api/users/ BODY => JSON of UserDto => [OK] Updates User
                // DELETE http://<hostname>:<port>/api/users/ BODY => JSON of UserDto => [OK] Remove User
                endpoints.MapRestEasyApi<UserDomain, UserDto>("api/users");
            });
        }
    }
}