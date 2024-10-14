using CommomTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Infrastructure.DataAccess;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private UserBase _user = default!;
        private string _password = string.Empty;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PotatoBuyersDbContext>));
                    if(descriptor is not null)
                        services.Remove(descriptor);

                    //Atualizar .Net pro 8 e trocar services.AddEntityFrameworkMySql() por services.AddEntityFrameworkInMemoryDatabase()
                    var provider = services.AddEntityFrameworkMySql().BuildServiceProvider();

                    services.AddDbContext<PotatoBuyersDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<PotatoBuyersDbContext>();

                    dbContext.Database.EnsureDeleted();

                    StartDataBase(dbContext);
                });
        }

        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;

        private void StartDataBase(PotatoBuyersDbContext dbContext)
        {
            (_user, _password) = UserBaseBuilder.Build();

            dbContext.Users.Add(_user);

            dbContext.SaveChanges();
        }
    }
}
