using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PotatoBuyers.Domain.Repositories.User;
using PotatoBuyers.Infrastructure.DataAccess;
using PotatoBuyers.Infrastructure.DataAccess.Repositories;
using PotatoBuyers.Infrastructure.DataAccess.Repositories.User;
using PotatoBuyers.Infrastructure.Extensions;
using System.Reflection;

namespace PotatoBuyers.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);

            if (configuration.IsUnitTestEnviroment())
                return; 

            AddDbContext_MySqlServer(services, configuration);
            AddFluentMigrator_MySql(services, configuration);
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

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("PotatoBuyers.Infrastructure")).For.All();
            });
        }
    }
} 
