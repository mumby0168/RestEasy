# RestEasy
A simple, quick and effective way to make rest api's in asp.net core

This is a new project please see the Final Words seciton to see what is in the pipeline.

## Inspiration
The main inspiration for this was the hassle for creating controllers and defining routes. In addition controllers can very often violate the first principle of SOLID, Single Responsibility this in the case of a API can make testing difficult and a pain and often sends people in the wrong direction.
REST is so common now and people want a quick way to make an API this is where RestEasy comes in it allows with a few classes and extensions to a empty ASP.NET CORE application to have a working REST API.

## Patterns and Practices Considered
RestEasy aims to bring best practice and simplicity into a API. It makes use of the repository pattern to decouple the framework from any single persitance choice. It also provides a simple API to map a domain object to a data transfer object (DTO) this is aiming to help with a Domain Driven Design implementation.
It also follows the simple idea of extensions to the ASP.NET CORE framework.

## Getting Started
This section will show the basic example that currently gets a API up and running.

This assumes that the inital install of the library has been completed (A NUGET INSTALL SECTION TO COME!)

### Step 1: Define a domain object
The first thing to do is define a domain model the interface to use for this is defined in the library and takes a generic an example of this is shown below:

The key points to note are:
1. The generic parameter to ```IDomain<TDto>``` is the representation of the data that the API will return known as a data transfer object.
2. It forces a ```Guid``` to uniquely identify the domain object it is best practice to keep this ```private``` as it should not be changed.
3. All fields should be ```private``` and changed by the map function. 
3. The other properties are optional and can contain any other values in this case first and second name.
4. The 2 map methods are responsible for conversion to and from a data transfer object.
```c#
public class UserDomain : IDomain<UserDto>
{
    // Forced property and should be private
    public Guid Id { get; private set; }

    public string FirstName { get; private set; }
    
    public string SecondName { get; private set; }

    public UserDomain()
    {
        Id = Guid.NewGuid();
    }
        
    // Maps the user dto to a domain object
    public void Map(UserDto dto, bool firstCreation = false)
    {
        // Used in the case when a update is being performed
        if (!firstCreation) Id = dto.Id;
        
        FirstName = dto.FirstName;
        SecondName = dto.SecondName;
    }
    // Maps a domain object to dto
    public UserDto Map()
    {
        return new UserDto {Id = Id,FirstName = FirstName, SecondName = SecondName};
    }
}
```

### Step 2: Define a data transfer object (DTO)
This is the data representation that will be sent to the consumer of the API and simply implement the interface ```IDto``` this should be a simple POCO object.
```c#
public class UserDto : IDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
}
``` 
### Step 3: Create a repository
This allows data to be stored in any way desired this could use a EF Core database, Mongo DB or simply just a in memory data store.

The example below shows a simple in memory implementation which implements the interface ```IRepository<TDomain, TDto>```

The key points to note are:

1. The generic paramters use both the dto and the domain object created in steps 1 & 2.
2. The way to use another provider would be injecting say a ```DbContext``` or ```IMongoCollection<T>``` into this class.

```c#
public class UserRepository : IRepository<UserDomain, UserDto>
{    
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
```
### Step 4: Add it to a ASP.NET CORE Web Application\

This can be intergrated with more complex ASP.NET Core web application or just simply with the Empty template as used in the samples. An example of configuring the empty templates ```Startup.cs``` file is shown below:

#### Key things to note are:

#### Services Registriation

1. The services needed are added in the call to ```AddRestEasy()``` extension method.
2. The services needed for the custom API in this case user is done through the ```AddRestEasyApi<TDomain, TDto, TRepository>()``` extension method.

#### App Configuration

1. The first call to ```UseRestEasy()``` adds the middleware to handle exceptions that occur returning status code errors.
2. The second call to ```MapRestEasyApi<TDomain, TDto>("<endpoint>")``` inside the endpoint mapping creates the endpoints noted in the comments taking the base path as a parameters 

```c#
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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {                
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
```

## Final words

Any questions please feel free to raise an issue and I will endeavour to answer them.

Any additions or ideas please raise and issue or better a pull request and we can start to evovle the library together.

The next few things that I am looking to add are: 

1. Good logging for debugging
2. Security through user roles
3. Ability to define extra more specialised endpoints on top of the standard endpoints.
4. Allow for a paging endpoint to be added.
5. CQRS integration
6. Service bus plugins to handle messages
7. Ability to do some sort of filtering using maybe OAuth or Graph QL

Any help or feedback will be much apprecaited.