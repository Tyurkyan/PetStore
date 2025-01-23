using PetStore.Models.DTO;

namespace PetStore.BL.Interfaces
{
    public interface IBlOwnerPetService
    {
        List<Pet> GetPetByOwnerId(string ownerId);
    }
}
