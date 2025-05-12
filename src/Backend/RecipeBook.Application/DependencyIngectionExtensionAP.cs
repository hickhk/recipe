using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.AutoMapper;
using RecipeBook.Application.Services.Criptografia;
using RecipeBook.Application.UseCases.User.Interfaces;
using RecipeBook.Application.UseCases.User.Register;

namespace RecipeBook.Application
{
    public static class DependencyIngectionExtensionAP
    {
        public static void AddAplicationDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapperDependencyInjection(services);
            AddUserDependencyInjection(services);
            AddEncrypterDependencyInjection(services, configuration);
        }

        public static void AddAutoMapperDependencyInjection(this IServiceCollection services)
#pragma warning disable format
        {
            var autoMapper = new AutoMapper.MapperConfiguration(cfg => 
                cfg.AddProfile(new AutoMapping())
                ).CreateMapper();

            services.AddSingleton(autoMapper);
        }

        private static void AddUserDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IRegisterUser, RegisterUser>();
        }

        private static void AddEncrypterDependencyInjection(IServiceCollection services, IConfiguration confituration)
        {
            string additionalKey = confituration.GetValue<string>("Settings:Pssword:AdditionalKey");
            services.AddScoped(option => new Encrypt(additionalKey));
        }
    }
}
