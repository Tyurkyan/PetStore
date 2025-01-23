using PetStore.DL.Interfaces;
using PetStore.DL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace PetStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddDataDependencies(this IServiceCollection services)
        {

            services.AddSingleton<IPetRepository, PetRepository>();
            services.AddSingleton<IOwnerRepository, OwnerRepository>();

            return services;
        }
    }
}
