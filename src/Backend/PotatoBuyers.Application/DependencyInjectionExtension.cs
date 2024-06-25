using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PotatoBuyers.Application.Services.AutoMapper;
using PotatoBuyers.Application.Services.Cryptography;
using PotatoBuyers.Application.UseCases.User.Register;

namespace PotatoBuyers.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCase(services);
            AddPasswordEncripter(services, configuration);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
