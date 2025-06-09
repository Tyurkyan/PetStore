using PetStore.Models.DTO;
using PetStore.Models.Response;

namespace PetStore.DL.Interfaces
{
    public interface IPetBioGateway
    {
        Task<PetBioResponse> GetPetBioInfo(string petId);
    }
}
