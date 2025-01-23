using Microsoft.Extensions.DependencyInjection;
using PetStore.BL.Interfaces;
using PetStore.BL.Services;

namespace PetStore.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddBusinessDependencies(this IServiceCollection services)
        {

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();


            return services;
        }
    }
}
