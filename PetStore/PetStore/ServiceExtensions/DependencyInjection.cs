using PetStore.Models.Configurations;

namespace PetStore.ServiceExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
     
            services.Configure<MongoDbConfiguration>(config.GetSection(nameof(MongoDbConfiguration)));

            return services;
        }
    }
}
