using PetStore.Models.DTO;
using Mapster;
using PetStore.Models.Response;

namespace PetStore.MapsterConfig
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<Pet, PetResponse>.NewConfig();
            TypeAdapterConfig<Owner, OwnerResponse>.NewConfig();

        }
    }
}
