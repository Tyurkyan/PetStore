using PetStore.Models.DTO;

namespace PetStore.BL.Interfaces
{
    public interface IBlOwnerPetService
    {
        Task<List<Pet>> GetPetByOwnerIdAsync(string ownerId);
    }
}
