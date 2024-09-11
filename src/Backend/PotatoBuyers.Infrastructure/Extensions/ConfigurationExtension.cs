using Microsoft.Extensions.Configuration;

namespace PotatoBuyers.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            //Ajustar após atualizar .net
            //return configuration.GetValue<bool>("inMemoryTest");

            return false;
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection");
        }
    }
}
