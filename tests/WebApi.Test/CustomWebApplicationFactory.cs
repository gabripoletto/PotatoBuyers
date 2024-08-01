using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PotatoBuyers.Infrastructure.DataAccess;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
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
                });
        }
    }
}
