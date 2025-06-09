using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Models.DTO;
using PetStore.DL.Cache;
using PetStore.DL.Interfaces;
using PetStore.DL.Kafka.KafkaCache;
using PetStore.DL.Kafka;
using PetStore.DL.Repositories;
using PetStore.Models.Configurations.CachePopulator;

namespace PetStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Регистрация на репозиторита
            services.AddSingleton<IPetRepository, PetRepository>();
            services.AddSingleton<IOwnerRepository, OwnerRepository>();

            // Добавяне на кеш конфигурации
            services.AddCache<PetCacheConfiguration, PetRepository, Pet, string>(config);
            services.AddCache<OwnerCacheConfiguration, OwnerRepository, Owner, string>(config);

            // Kafka hosted services
            services.AddHostedService<KafkaCache<string, Pet>>();
            services.AddHostedService<KafkaCache<string, Owner>>();

            return services;
        }

        public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(
            this IServiceCollection services,
            IConfiguration config)
            where TCacheConfiguration : CacheConfiguration
            where TCacheRepository : class, ICacheRepository<TKey, TData>
            where TData : ICacheItem<TKey>
            where TKey : notnull
        {
            var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

            if (!configSection.Exists())
            {
                throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing in appsettings!");
            }

            services.Configure<TCacheConfiguration>(configSection);

            services.AddSingleton<ICacheRepository<TKey, TData>, TCacheRepository>();
            services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData, TCacheConfiguration>>();
            services.AddHostedService<MongoCachePopulator<TData, TCacheConfiguration, TKey>>();

            return services;
        }
    }
}

