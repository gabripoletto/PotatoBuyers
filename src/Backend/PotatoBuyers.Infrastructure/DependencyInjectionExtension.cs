using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PotatoBuyers.Domain.Repositories.User;
using PotatoBuyers.Infrastructure.DataAccess;
using PotatoBuyers.Infrastructure.DataAccess.Repositories;
using PotatoBuyers.Infrastructure.DataAccess.Repositories.User;
using PotatoBuyers.Infrastructure.Extensions;

namespace PotatoBuyers.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext_MySqlServer(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.ConnectionString();  
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));

            services.AddDbContext<PotatoBuyersDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
} 
